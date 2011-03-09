using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using QServer;

namespace QClient
{
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
    public partial class Form1 : Form
    {
        private bool personChange = false;

        public Form1()
        {
            InitializeComponent();
            UpdatePersons();
        }

        private void personGreed_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
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
                    if (!Person.ValidatName(personGreed.Rows[e.RowIndex].Cells["name"].Value) ||
                        !Person.ValidatName(personGreed.Rows[e.RowIndex].Cells["surname"].Value) ||
                        !Person.ValidateCourse(personGreed.Rows[e.RowIndex].Cells["cours"].Value) ||
                        !Person.ValidatGroup(personGreed.Rows[e.RowIndex].Cells["Group"].Value))
                    {
                        e.Cancel = true;
                        personGreed.Rows[e.RowIndex].ErrorText = "Некоректные данные";
                        return;
                    }
                    Person p = new Person();
                    p.id = Int32.Parse(personGreed.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    p.name = personGreed.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    p.surname = personGreed.Rows[e.RowIndex].Cells["surname"].Value.ToString();
                    p.group = Int32.Parse(personGreed.Rows[e.RowIndex].Cells["Group"].Value.ToString());
                    p.cours = Int32.Parse(personGreed.Rows[e.RowIndex].Cells["cours"].Value.ToString());
                    if (!p.Validate())
                    {
                        e.Cancel = true;
                        personGreed.Rows[e.RowIndex].ErrorText = "Некоректные данные";
                        return;
                    }
                    SocketClient socket = new SocketClient();
                    if (socket.SavePerson(ref p))
                    {
                        personGreed.Rows[e.RowIndex].Cells["id"].Value = p.id.ToString();
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
                    Int32.TryParse(personGreed.Rows[e.RowIndex].Cells["id"].Value.ToString(), out id);
                    if (id == 0)
                    {
                        //new person - clear row
                        foreach (DataGridViewCell cell in personGreed.Rows[e.RowIndex].Cells)
                        {
                            cell.Value = string.Empty;
                        }
                    }
                    else
                    {
                        //update from DB
                        SocketClient socket = new SocketClient();
                        Person p;
                        if (socket.GetPerson(id, out p))
                        {
                            personGreed.Rows[e.RowIndex].Cells["id"].Value = p.id.ToString();
                            personGreed.Rows[e.RowIndex].Cells["name"].Value = p.name.ToString();
                            personGreed.Rows[e.RowIndex].Cells["surname"].Value = p.surname.ToString();
                            personGreed.Rows[e.RowIndex].Cells["cours"].Value = p.cours.ToString();
                            personGreed.Rows[e.RowIndex].Cells["group"].Value = p.group.ToString();
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    personGreed.Rows[e.RowIndex].ErrorText = string.Empty;
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
            e.Row.Cells["id"].Value = 0;
        }

        private void delPerson_Click(object sender, EventArgs e)
        {
            if (personGreed.CurrentRow == null)
            {
                return;
            }
            DataGridViewRow row = personGreed.CurrentRow;
            if (MessageBox.Show("Удалить?", "Удалить?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SocketClient socket = new SocketClient();
                if (socket.DeletePerson(Int32.Parse(row.Cells["id"].Value.ToString())))
                {
                    personGreed.Rows.Remove(row);
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

        }

        private void UpdatePersons()
        {
            //force update
            //dont save changes
            personChange = false;
            personGreed.Rows.Clear();
            SocketClient socket = new SocketClient();
            Person[] persons;
            socket.GetAllPerson(out persons);
            if (persons != null)
            {
                foreach (Person p in persons)
                {
                    personGreed.Rows.Add(p.ToArray());
                }
            }
            personChange = false;
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
    }

}
