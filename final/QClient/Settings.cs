using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QClient
{
    public partial class Settings : Form
    {
        public string Server
        {
            get { return EServer.Text; }
            set { EServer.Text = value; }
        }

        public int Port
        {
            get { return Int32.Parse(EPort.Text); }
            set { EPort.Text = value.ToString(); }
        }
        public Settings()
        {
            InitializeComponent();
        }
    }
}
