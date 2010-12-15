using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            BaseFigure figure = new CircleFigure(30, 30, 30);
            figure.Draw(e.Graphics);
            figure = new RectangleFigure(30, 30, 30,30);
            figure.Draw(e.Graphics);
            figure = new MixFigure(70, 30, 40, 30);
            figure.Draw(e.Graphics);
            figure = new MixFigure(120, 30, 30, 40);
            figure.Draw(e.Graphics);
        }
    }
}
