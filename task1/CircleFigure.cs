using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Task1
{
    class CircleFigure: BaseFigure
    {
        private int m_x = 0;
        private int m_y = 0;
        private int m_r = 0;

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

        public int R
        {
            get { return m_r; }
            set
            {
                if (value >= 0 && value <= 1024)
                {
                    m_r = value;
                }
            }
        }

        public CircleFigure(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
        }

        public override void Draw(Graphics g)
        {
            if (g != null)
            {
                Pen pen = new Pen(Color.Black, 1);
                g.DrawEllipse(pen, m_x - m_r / 2, m_y - m_r / 2, m_r, m_r);
            }
        }
    }
}
