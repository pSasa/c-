using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using QServer;

namespace QClient
{
    public partial class Form1 : Form
    {
        private bool personChange = false;

        public Form1()
        {
            InitializeComponent();
            UpdateAll();
        }

        private void personGreed_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = ((DataGridView)sender).Rows[e.RowIndex];
            if (personChange)
            {
                //row was changed
                DialogResult answer = MessageBox.Show("Сохранить?", "Сохранить?", MessageBoxButtons.YesNoCancel);
                if (answer == DialogResult.Cancel)
                {
                    e.Cancel = true;
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
                        personChange = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    int id;
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
                    }
                    else
                    {
                        //update from DB
                        SocketClient socket = new SocketClient();
                        Item item = GetHelperFromSelectedGrid((DataGridView)sender);
                        if (socket.GetItem(id, ref item))
                        {
                            PutItemToRow(ref row, item);
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    row.ErrorText = string.Empty;
                }
            }
            personChange = e.Cancel;
        }

        private void personGreed_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            personChange = true;
        }

        private void personGreed_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = 0;
            personChange = false;
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
                    personChange = false;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateAll()
        {
            UpdatePersons();
            UpdateSubjects();
            UpdateMarks();
        }

        private void UpdatePersons()
        {
            //force update
            //dont save changes
            personChange = false;
            personGrid.Rows.Clear();
            MarkSubject.Items.Clear();
            SocketClient socket = new SocketClient();
            Person[] persons = null;
            socket.GetAllPerson(out persons);
            if (persons != null)
            {
                foreach (Person p in persons)
                {
                    personGrid.Rows.Add(p.ToArray());
                }
                MarkPerson.Items.AddRange(persons);
            }
            personChange = false;
        }
        private void UpdateSubjects()
        {
            //force update
            //dont save changes
            personChange = false;
            subjectGrid.Rows.Clear();
            MarkSubject.Items.Clear();
            SocketClient socket = new SocketClient();
            Subject[] subjects = null;
            socket.GetAllSubject(out subjects);
            if (subjects != null)
            {
                foreach (Subject s in subjects)
                {
                    subjectGrid.Rows.Add(s.ToArray());
                }
                MarkSubject.Items.AddRange(subjects);
            }
            personChange = false;
        }

        private void UpdateMarks()
        {
            //force update
            //dont save changes
            personChange = false;
            markGrid.Rows.Clear();
            SocketClient socket = new SocketClient();
            Mark[] marks;
            socket.GetAllMark(out marks);
            if (marks != null)
            {
                foreach (Mark m in marks)
                {
                    DataGridViewRow row = null;
                    PutItemToRow(ref row, m);
                    markGrid.Rows.Add(row);
                }
            }
            personChange = false;
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
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells[1];
                foreach (Item o in cell.Items)
                {
                    if (m.person == o.id)
                    {
                        row.Cells[1].Value = o;
                        break;
                    }
                }
                cell = (DataGridViewComboBoxCell)row.Cells[2];
                foreach (Item o in cell.Items)
                {
                    if (m.subject == o.id)
                    {
                        row.Cells[2].Value = o;
                        break;
                    }
                }
                row.Cells[0].Value = m.id.ToString();
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
                    UpdatePersons();
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
                if (!Mark.ValidatName(row.Cells["MarkPerson"].Value) ||
                    !Mark.ValidatName(row.Cells["MarkSubject"].Value) ||
                    !Mark.ValidateMark(row.Cells["MarkMark"].Value))
                {
                    return null;
                }
                Mark m = new Mark();
                m.id = Int32.Parse(row.Cells["MarkId"].Value.ToString());
                m.person = ((Person)row.Cells["MarkPerson"].Value).id;
                m.subject = ((Subject)row.Cells["MarkSubject"].Value).id;
                m.mark = Int32.Parse(row.Cells["MarkMark"].Value.ToString());
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

        private void markGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Context == (DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize))
            {
                e.ThrowException = false;
            }
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
