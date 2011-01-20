using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Task3
{
    sealed public class PankList<T> : IEnumerable
    {
        #region Переменные

        private const int DEFAULT_CAPACITY = 4;
        private int m_count;
        private int m_capacity;
        private T[] m_list;

        #endregion

        #region Свойства

        /// <summary>
        /// Получить текущий размер массива
        /// </summary>
        public int Count
        {
            get{ return m_count; }
        }

        /// <summary>
        /// Получить максимальный размер листа (до следущего перераспрелделение размера)
        /// </summary>
        public int Capacity
        {
            get { return m_capacity; }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор который создает массив с размером DEFAULT_CAPACITY=4
        /// </summary>
        public PankList()
            : this(DEFAULT_CAPACITY)
        {
        }

        /// <summary>
        /// Конструктор позволяющий задать начальный размер
        /// </summary>
        /// <param name="capacity">начальный размер массива</param>
        public PankList(int capacity)
        {
            m_count = 0;
            m_capacity = capacity;
            m_list = new T[m_capacity];
        }

        #endregion

        #region Добавление элементов

        /// <summary>
        /// Добавить элемент в конец массива
        /// </summary>
        /// <param name="item">Новый элемент</param>
        public void Add(T item)
        {
            ValidateCapacity(1);
            m_list[m_count++] = item;
        }

        /// <summary>
        /// Добавить список элементов в конец массива
        /// </summary>
        /// <param name="collection">Список элементов которые нужно добавить</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection.Count() == 0)
            {
                //хотят добавить пустой массив
                //хз, может нужен exception, непонятно
                return;
            }
            ValidateCapacity(collection.Count());
            foreach (T item in collection)
            {
                m_list[m_count++] = item;
            }
        }

        /// <summary>
        /// Вставить новый элемент в заданное место
        /// все элементы начиная с новой позиции смещются вправо
        /// </summary>
        /// <param name="index">Позиция нового элемента в массиве</param>
        /// <param name="item">Новый элемент</param>
        public void InsertAt(int index, T item)
        {
            ValidateIndex(index);
            ValidateCapacity(1);
            m_count++;
            for (int i = m_count - 1; i > index; i--)
            {
                m_list[i] = m_list[i - 1];
            }
            m_list[index] = item;
        }

        /// <summary>
        /// Вставить список элементов в заданное место
        /// все элементы начиная с новой позиции смещются вправо
        /// </summary>
        /// <param name="index">Позиция первого нового элемента в массиве</param>
        /// <param name="item">Список вставляемых элементов</param>
        public void InsertRangeAt(int index, IEnumerable<T> collection)
        {
            ValidateIndex(index);
            int insertCount = collection.Count();
            if (insertCount == 0)
            {
                //хотят добавить пустой массив
                //хз, может нужен exception, непонятно
                return;
            }
            ValidateCapacity(insertCount);
            m_count += insertCount;
            for (int i = m_count-1; i > index + insertCount - 1; i--)
            {
                m_list[i] = m_list[i - insertCount];
            }
            foreach (T item in collection)
            {
                m_list[index++] = item;
            }
        }

#endregion

        #region Копия массива

        /// <summary>
        /// Получить копию массива как статический массив
        /// </summary>
        /// <returns>Статический массив со всеми элементами массива</returns>
        public T[] ToArray()
        {
            T[] array = new T[m_count];
            for (int i = 0; i < m_count; i++)
            {
                array[i] = m_list[i];
            }
            return array;
        }
        #endregion

        #region Удаление элементов

        /// <summary>
        /// Удалить элемент из массива
        /// элементы правее удаляемого, смещаются влево
        /// </summary>
        /// <param name="index">Индекс удаляемого элемента</param>
        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            m_count--;
            for (int i = index; i < m_count; i++)
            {
                m_list[i] = m_list[i+1];
            }
        }

        #endregion

        #region Функции проверки

        /// <summary>
        /// Проверить что размера текущего статического массива
        /// хватит для добавления новых элементов.
        /// При необходимости рармер увеличивается
        /// </summary>
        /// <param name="count">Количество элементов которые нужно добавить</param>
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

        /// <summary>
        /// Проверка индекса на валидность
        /// </summary>
        /// <param name="index">Индекс который нужно проверить</param>
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= m_count)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Операторы
        /// <summary>
        /// Оператор []
        /// </summary>
        /// <param name="index">индекс в массиве</param>
        /// <returns>элемент стоящий по заданному индексу</returns>
        public T this [int index]
        {
            get
            {
                ValidateIndex(index);
                return m_list[index];
            }
        }

        /// <summary>
        /// Реализация интерфейса IEnumerable
        /// </summary>
        /// <returns>Инумиратор статического массива</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_list.GetEnumerator();
        }

        #endregion
    }
}
