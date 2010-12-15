using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Task1
{
    class RectangleFigure: BaseFigure
    {
        private int m_x = 0;
        private int m_y = 0;
        private int m_width = 0;
        private int m_height = 0;

        public int X
        {
            get { return m_x; }
            set
            {
                if (value >= 0 && value <= 1024)
                {
                    m_x = value;
                }
            }
        }

        public int Y
        {
            get { return m_y; }
            set
            {
                if (value >= 0 && value <= 1024)
                {
                    m_y = value;
                }
            }
        }

        public int Width
        {
            get { return m_width; }
            set
            {
                if (value >= 0 && value <= 1024)
                {
                    m_width = value;
                }
            }
        }

        public int Height
        {
            get { return m_height; }
            set
            {
                if (value >= 0 && value <= 1024)
                {
                    m_height = value;
                }
            }
        }
        public RectangleFigure(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override void Draw(Graphics g)
        {
            if (g != null)
            {
                Pen pen = new Pen(Color.Black, 1);
                g.DrawRectangle(pen, m_x, m_y, m_width, m_height);
            }
        }
    }
}
