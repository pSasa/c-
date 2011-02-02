using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader r = new Reader(@"c:\1\1.ttx");
            int c = r.Count;
            c = r.Count;
            c = r.Count;

            int[] res = r.Read(15);
            res = r.Read(1);
            res = r.Read(1);
            Console.ReadKey();
            return;

            Writer w = new Writer(@"c:\1\1.ttx");
            w.StartTr();
            int[] i = { 1, 2, 3, 4, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 4, 5, 6, 7, 8, 9, 0 };
            w.Write(i);
            w.Write(i);
            w.Commit();
        }
    }
}
