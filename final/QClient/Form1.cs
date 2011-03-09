using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QServer;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace QClient
{
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
            e.Row.Cells["name"].Value = String.Empty;
            e.Row.Cells["surname"].Value = String.Empty;
            e.Row.Cells["cours"].Value = 1;
            e.Row.Cells["group"].Value = 1;
        }

        private void delPerson_Click(object sender, EventArgs e)
        {
            if (personGreed.CurrentRow == null)
            {
                return;
            }
            Person p = new Person();
            DataGridViewRow row = personGreed.CurrentRow;
            p.id = Int32.Parse(row.Cells["id"].Value.ToString());
            p.name = row.Cells["name"].Value.ToString();
            p.surname = row.Cells["surname"].Value.ToString();
            p.group = Int32.Parse(row.Cells["Group"].Value.ToString());
            p.cours = Int32.Parse(row.Cells["cours"].Value.ToString());
            if (MessageBox.Show("Удалить?", "Удалить?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SocketClient socket = new SocketClient();
                if (socket.DeletePerson(p))
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

        private void UpdatePersons()
        {
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
    }
}
