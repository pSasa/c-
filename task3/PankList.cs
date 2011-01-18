using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    class PankList
    {
        private int m_count;
        private int m_capacity;
        private Object[] m_list;

        public int Count
        {
            get{ return m_count; }
        }

        public int Capacity
        {
            get { return m_capacity; }
        }

        public PankList()
        {
            m_capacity = 4;
            m_count = 0;
        }

        public PankList(int capacity)
        {
            m_count = 0;
            m_capacity = capacity;
        }

        public void Add(Object item)
        {

        }

        public void Add(IEnumerable<T> list)
        {
        }
    }
}
