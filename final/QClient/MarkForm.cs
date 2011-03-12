using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QServer;

namespace QClient
{
    public partial class MarkForm : Form
    {
        public Person[] persons = null;
        public Subject[] subjects = null;
        public Mark mark = null;

        public void FillData()
        {
            if (persons == null || subjects == null || persons.Length == 0 || subjects.Length == 0)
            {
                throw new Exception("Массивы учеников и предметов незаполнены");
            }
            CPerson.Items.AddRange(persons);
            CSubject.Items.AddRange(subjects);
            if (mark != null)
            {
                foreach (Person p in persons)
                {
                    if (p.id == mark.person)
                    {
                        CPerson.SelectedItem = p;
                        break;
                    }
                }
                foreach (Subject s in subjects)
                {
                    if (s.id == mark.subject)
                    {
                        CSubject.SelectedItem = s;
                        break;
                    }
                }
                CMark.Text = mark.mark.ToString();
            }
            else
            {
                CPerson.SelectedIndex = 0;
                CSubject.SelectedIndex = 0;
                CMark.SelectedIndex = 2;
            }
        }

        public MarkForm()
        {
            InitializeComponent();
        }

        private void BOk_Click(object sender, EventArgs e)
        {
            if (mark == null)
            {
                mark = new Mark();
                mark.id = 0;
            }
            mark.person = ((Person)CPerson.SelectedItem).id;
            mark.subject = ((Subject)CSubject.SelectedItem).id;
            mark.mark = Int32.Parse(CMark.Text);
        }
    }
}
