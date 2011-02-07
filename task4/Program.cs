using System;
using System.Collections.Generic;
using System.Linq;

namespace Task4
{
    class Program
    {
        static void printArray(int[] array)
        {
            Console.WriteLine("---Start---");
            foreach (int i in array)
            {
                Console.Write(i.ToString() + ", ");
            }
        }

        static bool compareArray(int[] left, int[] right)
        {
            if (left.Length != right.Length)
            {
                return false;
            }
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            Reader r = null;
            string dataFile = @"c:\1\1.txt";
            List<int> etalon = new List<int>();
            try
            {
                r = new Reader(dataFile);
                etalon.AddRange(r.Read(r.Count));
                r.Reset();
            }
            catch (Exception)
            { }
            int[] i = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            Writer w = new Writer(dataFile);
            w.StartTransaction();
            w.Write(i);
            w.Write(i);
            etalon.AddRange(i);
            etalon.AddRange(i);
            w.Commit();
            w.StartTransaction();
            w.Write(i);
            w.RollBack();
            w.StartTransaction();
            w.Write(i);
            etalon.AddRange(i);
            w.Commit();

            //пробуем читать разными способами
            int[] array1;
            if (r == null)
            {
                r = new Reader(dataFile);
            }
            array1 = r.Read(r.Count);
            printArray(array1);
            Console.WriteLine();
            Console.WriteLine(compareArray(etalon.ToArray(), array1) ? "Equal" : "Not Equal");
            r.Reset();
            List<int> array2 = new List<int>();
            for (int j = 0; j < r.Count; j++)
            {
                array2.AddRange(r.Read(1));
            }
            Console.WriteLine();
            Console.WriteLine(compareArray(etalon.ToArray(), array2.ToArray()) ? "Equal" : "Not Equal");
            r.Reset();
            List<int> array3 = new List<int>();
            while(true)
            {
                try
                {
                    array3.AddRange(r.Read(1));
                }
                catch (NotEnoughDataLenght)
                {
                    break;
                }
            }
            Console.WriteLine();
            Console.WriteLine(compareArray(etalon.ToArray(), array3.ToArray()) ? "Equal" : "Not Equal");
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
