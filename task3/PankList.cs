using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    class PankList<T>
    {
        private int m_count;
        private int m_capacity;
        private T[] m_list;

        public int Count
        {
            get{ return m_count; }
        }

        public int Capacity
        {
            get { return m_capacity; }
        }

        public PankList(): this(4)
        {
        }

        public PankList(int capacity)
        {
            m_count = 0;
            m_capacity = capacity;
            m_list = new T[m_capacity];
        }

        public void Add(T item)
        {
            ValidateCapacity(1);
            m_list[m_count++] = item;
        }

        private void ValidateCapacity(int count/* = 1*/)
        {
            if(m_capacity < m_count + count)
            {
                //
                do
                {
                    m_capacity *=2;
                }while (m_capacity < m_count + count);
                T[] new_list = new T[m_capacity];
                for(int i = 0; i < m_count; i++)
                {
                    new_list[i] = m_list[i];
                }
                m_list = new_list;
            }
        }

        public T this [int number]
        {
            get {return m_list[number];}
        }

        public void Add(IEnumerable<T> list)
        {
        }
    }
}
