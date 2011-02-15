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
        private ImagePreview imagePreview;
        public Form1()
        {
            InitializeComponent();
            imagePreview = new ImagePreview();
            imagePreview.SetSize(Size);
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            image.Image = imagePreview.LoadImage(fileList.SelectedItem.ToString(), null, null);
        }

        private void mOpenFolder_Click(object sender, EventArgs e)
        {
            Object[] list;
            if (imagePreview.OpenImageList(out list))
            {
                fileList.Items.Clear();
                fileList.Items.AddRange(list);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            imagePreview.SetSize(Size);
            image.Image = imagePreview.Refresh();
        }
    }
}
