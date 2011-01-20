using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    class Program
    {
        static private void DumpArray(PankList<int> list)
        {
            Console.WriteLine("Capacity {0} ", list.Capacity);
            Console.WriteLine("Count {0} ", list.Count);

            foreach (int item in list.ToArray())
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            int[] array = {1,2,3,4,5,6,7,8,9};
            int[] array1 = {-1, -2};
            int[] array2 = { 14, 15 };
            PankList<int> list = new PankList<int>();
            DumpArray(list);
            list.AddRange(array);
            DumpArray(list);
            list.Add(10);
            list.Add(11);
            list.Add(16);
            DumpArray(list);
            list.InsertAt(0, 0);
            DumpArray(list);
            list.InsertAt(list.Count - 1, 13);
            list.InsertAt(list.Count - 2, 12);
            DumpArray(list);
            list.InsertRangeAt(0, array1);
            DumpArray(list);
            list.InsertRangeAt(list.Count - 1, array2);
            DumpArray(list);
            list.AddRange(list.ToArray());
            DumpArray(list);
            list.RemoveAt(0);
            list.RemoveAt(list.Count - 1);
            list.RemoveAt(list.Count / 2);
            DumpArray(list);
            Console.ReadKey();
        }
    }
}
