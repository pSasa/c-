using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Task1
{
    class MixFigure:BaseFigure
    {
        BaseFigure[] figures = null;
        private int m_x = 0;
        private int m_y = 0;
        private int m_height = 0;
        private int m_width = 0;

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
            set { m_y = value; }
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

        public MixFigure(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            figures = new BaseFigure[2];
            figures[0] = new CircleFigure(m_x + m_width / 2, m_y + m_height /2, Math.Min(m_width, m_height));
            figures[1] = new RectangleFigure(m_x, m_y, m_width, m_height);
        }

        public override void  Draw(Graphics g)
        {
            int lenght = figures.Length;
            int i;

            for(i = 0; i < lenght; i++)
            {
                if (figures[i] != null)
                {
                    figures[i].Draw(g);
                }
            }
        }
    }
}
