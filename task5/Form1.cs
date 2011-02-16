﻿using System;
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
            int index = fileList.SelectedIndex;
            int count = fileList.Items.Count;
            string prev = null;
            string next = null;
            if (count > 1)
            {
                if (index == 0)
                {
                    prev = fileList.Items[fileList.Items.Count - 1].ToString();
                }
                else
                {
                    prev = fileList.Items[index - 1].ToString();
                }
            }
            if (count > 2)
            {
                if (index == fileList.Items.Count - 1)
                {
                    next = fileList.Items[0].ToString();
                }
                else
                {
                    next = fileList.Items[index + 1].ToString();
                }
            }
            image.Image = imagePreview.LoadImage(fileList.SelectedItem.ToString(), prev, next);
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
