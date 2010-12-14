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
            set { m_x = value; }
        }
        public CircleFigure()
        {

        }
        public override void Draw()
        {
            Pen pen = new Pen(Color.Black, 3);


        }
    }
}
