using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace task5
{
    public partial class Form1 : Form
    {
        private string folder = null;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folder = fbd.SelectedPath;
                folder = @"c:\1\";
                DirectoryInfo di = new DirectoryInfo(folder);
                FileInfo[] files = di.GetFiles("*.png");
                fileList.Items.Clear();
                fileList.Items.AddRange(files.ToArray() );
            }
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            image.Image = Image.FromFile(Path.Combine(folder, fileList.SelectedItem.ToString()));

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
