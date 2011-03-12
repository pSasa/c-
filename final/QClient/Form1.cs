using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using QServer;

namespace QClient
{
    public partial class Form1 : Form
    {
        private bool itemChange = false;
        private Person[] persons = null;
        private Subject[] subjects = null;
        private Mark[] marks = null;

        public Form1()
        {
            InitializeComponent();
            UpdateAll();
        }

        private void personGreed_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
            if (itemChange)
            {
                //row was changed
                DialogResult answer = MessageBox.Show("Сохранить?", "Сохранить?", MessageBoxButtons.YesNoCancel);
                if (answer == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    row.ErrorText = string.Empty;
                    return;
                }
                else if (answer == DialogResult.Yes)
                {
                    Item item = GetItemFromSelectedRow((DataGridView)sender);
                    if (item == null)
                    {
                        e.Cancel = true;
                        row.ErrorText = "Некоректные данные";
                        return;
                    }
                    if (!item.Validate())
                    {
                        e.Cancel = true;
                        row.ErrorText = "Некоректные данные";
                        return;
                    }
                    SocketClient socket = new SocketClient();
                    if (socket.SaveItem(ref item))
                    {
                        row.Cells[0].Value = item.id.ToString();
                        row.ErrorText = string.Empty;
                    }
                    else
                    {
                        row.ErrorText = "ошибка сервера";
                        e.Cancel = true;
                    }
                }
                else
                {
                    int id = 0;
                    Int32.TryParse(row.Cells[0].Value.ToString(), out id);
                    if (id == 0)
                    {
                        //new person - clear row
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell is DataGridViewComboBoxCell)
                            {
                                cell.Value = ((DataGridViewComboBoxCell)cell).Items[0];
                            }
                            else
                            {
                                cell.Value = string.Empty;
                            }
                        }
                        //это писец!!! я задолбался это решение гуглить!
                        BeginInvoke(new Action(delegate { ((DataGridView)sender).Rows.RemoveAt(e.RowIndex); }));
                    }
                    else
                    {
                        //update from caсhe
                        Item[] items;
                        if (sender == personGrid)
                        {
                            items = persons;
                        }
                        else if (sender == subjectGrid)
                        {
                            items = subjects;
                        }
                        else
                        {
                            throw new Exception("неправильная таблица при откате изменений");
                        }
                        foreach (Item item in items)
                        {
                            if (item.id == id)
                            {
                                PutItemToRow(ref row, item);
                                break;
                            }
                        }
                    }
                    row.ErrorText = string.Empty;
                }
            }
            itemChange = e.Cancel;
        }

        private void personGreed_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            itemChange = true;
        }

        private void personGreed_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = 0;
            itemChange = false;
        }

        private void delPerson_Click(object sender, EventArgs e)
        {
            DataGridView grid = GetGridFromDeleteButton((Button)sender);
            DataGridViewRow row = grid.CurrentRow;
            if (row == null)
            {
                return;
            }
            if (MessageBox.Show("Удалить?", "Удалить?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SocketClient socket = new SocketClient();
                Item item = GetItemFromSelectedRow(grid);
                if (socket.DeleteItem(item))
                {
                    grid.Rows.Remove(row);
                    itemChange = false;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateAll()
        {
            UpdateItems(personGrid);
            UpdateItems(subjectGrid);
            UpdateItems(markGrid);
        }

        private void UpdateItems(DataGridView grid)
        {
            //force update
            //dont save changes
            itemChange = false;
            grid.Rows.Clear();
            SocketClient socket = new SocketClient();
            Item item = GetHelperFromSelectedGrid(grid);
            Item[] items;
            if (item is Person)
            {
                items = persons;
            }
            else if (item is Subject)
            {
                items = subjects;
            }
            else if (item is Mark)
            {
                items = marks;
            }
            else
            {
                throw new Exception("Некоректный тип при обновлении таблицы");
            }
            socket.GetAllItems(ref items, item);
            if (items != null)
            {
                foreach (Item i in items)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(grid);
                    PutItemToRow(ref row, i);
                    grid.Rows.Add(row);
                }
            }
            if (item is Person)
            {
                persons = (Person[])items;
            }
            else if (item is Subject)
            {
                subjects = (Subject[])items;
            }
            else if (item is Mark)
            {
                marks = (Mark[])items;
            }
            itemChange = false;
        }

        private void PutItemToRow(ref DataGridViewRow row, Item item)
        {
            if(row == null)
            {
                row = new DataGridViewRow();
            }
            if (item is Subject || item is Person)
            {
                row.SetValues(item.ToArray());
            }
            else if (item is Mark)
            {
                Mark m = (Mark)item;
                string[] ar = item.ToArray();
                if (row.Cells.Count == 0)
                {
                    row.CreateCells(markGrid);
                }
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)row.Cells[1];
                foreach (Item o in persons)
                {
                    if (m.person == o.id)
                    {
                        row.Cells[1].Value = o.ToString();
                        break;
                    }
                }
                cell = (DataGridViewTextBoxCell)row.Cells[2];
                foreach (Item o in subjects)
                {
                    if (m.subject == o.id)
                    {
                        row.Cells[2].Value = o.ToString();
                        break;
                    }
                }
                row.Cells[0].Value = m;
                row.Cells[3].Value = m.mark.ToString();
            }
            else
            {
                throw new Exception("Неизвесный Item");
            }

        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            ClientSettings cs;
            ClientSettings.LoadSettings(out cs);
            if (cs != null)
            {
                settings.Server = cs.Server;
                settings.Port = cs.Port;
            }
            if (settings.ShowDialog() == DialogResult.OK)
            {
                //save settings
                if (ClientSettings.SaveSettings(new ClientSettings(settings.Server, settings.Port)))
                {
                    SocketClient.SetServer(settings.Server, settings.Port);
                    UpdateAll();
                }
            }
        }

        private DataGridView GetGridFromDeleteButton(Button btn)
        {
            if (btn == delPerson)
            {
                return personGrid;
            }
            else if (btn == delSubject)
            {
                return subjectGrid;
            }
            else if (btn == delMark)
            {
                return markGrid;
            }
            return null;
        }

        private Item GetItemFromSelectedRow(DataGridView grid)
        {
            DataGridViewRow row = grid.CurrentRow;
            if (row == null)
            {
                throw new Exception("Элемент не выбран в таблице");
            }
            if (grid == personGrid)
            {
                if (!Person.ValidatName(row.Cells["name"].Value) ||
                    !Person.ValidatName(row.Cells["surname"].Value) ||
                    !Person.ValidateCourse(row.Cells["cours"].Value) ||
                    !Person.ValidatGroup(row.Cells["Group"].Value))
                {
                    return null;
                }
                Person p = new Person();
                p.id = Int32.Parse(row.Cells[0].Value.ToString());
                p.name = row.Cells["name"].Value.ToString();
                p.surname = row.Cells["surname"].Value.ToString();
                p.group = Int32.Parse(row.Cells["Group"].Value.ToString());
                p.cours = Int32.Parse(row.Cells["cours"].Value.ToString());
                return p;
            }
            else if (grid == subjectGrid)
            {
                if (!Subject.ValidatName(row.Cells["SubjectName"].Value) ||
                    !Subject.ValidatName(row.Cells["SubjectTeacher"].Value) ||
                    !Subject.ValidateHour(row.Cells["SubjectHour"].Value))
                {
                    return null;
                }
                Subject s = new Subject();
                s.id = Int32.Parse(row.Cells["SubjectId"].Value.ToString());
                s.name = row.Cells["SubjectName"].Value.ToString();
                s.teacher = row.Cells["SubjectTeacher"].Value.ToString();
                s.hour = Int32.Parse(row.Cells["SubjectHour"].Value.ToString());
                return s;
            }
            else if (grid == markGrid)
            {
                Mark m = (Mark)row.Cells["MarkId"].Value;
                return m;
            }
            return null;
        }
        private Item GetHelperFromSelectedGrid(DataGridView grid)
        {
            if (grid == personGrid)
            {
                return new Person();
            }
            else if (grid == subjectGrid)
            {
                return new Subject();
            }
            else if (grid == markGrid)
            {
                return new Mark();
            }
            return null;
        }

        private void editMark_Click(object sender, EventArgs e)
        {
            EditMark(false);
        }

        private void EditMark(bool isAdd)
        {
            Mark mark = null;
            DataGridViewRow row = null;
            if (!isAdd)
            {
                mark = (Mark)markGrid.CurrentRow.Cells[0].Value;
                row = markGrid.CurrentRow;
            }
            MarkForm form = new MarkForm();
            form.subjects = subjects;
            form.persons = persons;
            form.mark = mark;
            form.FillData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                mark = form.mark;
                PutItemToRow(ref row, mark);
                if (isAdd)
                {
                    markGrid.Rows.Add(row);
                }
            }
        }

        private void addMark_Click(object sender, EventArgs e)
        {
            EditMark(true);
        }

        private void markGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EditMark(false);
        }

        private void updateMenu_Click(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void personGrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    #region настройки клиента
    public class ClientSettings
    {
        public string Server = String.Empty;
        public int Port = 0;

        public ClientSettings()
        {
        }

        public ClientSettings(string server, int port)
        {
            Server = server;
            Port = port;
        }

        public static void LoadSettings(out ClientSettings o)
        {
            o = null;
            FileStream stream = null;
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(ClientSettings));
                stream = new FileStream(Application.StartupPath + @"\QClient.config", FileMode.Open);
                o = (ClientSettings)xml.Deserialize(stream);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static bool SaveSettings(ClientSettings o)
        {
            bool res = true;
            StreamWriter writer = null;
            try
            {
                XmlSerializer xml = new XmlSerializer(o.GetType());
                writer = new StreamWriter(Application.StartupPath + @"\QClient.config");
                xml.Serialize(writer, o);
            }
            catch (Exception)
            {
                res = false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
            return res;
        }
    }
    #endregion //настройки клиента

}
