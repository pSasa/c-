using System;

namespace Task2
{
    class Program
    {
        static int IntCompare(Object left, Object right)
        {
            if ((int)left == (int)right)
            {
                return 0;
            }
            else if ((int)left > (int)right)
            {
                return 1;
            }
            return -1;
        }
        static void PartiallyDone(Object sender, EventArgs ev)
        {
            Console.WriteLine("Done percent:" + ((PartiallEventArgs)ev).done);
        }

        static void ElementReplacing(Object sender, EventArgs ev)
        {
            Console.WriteLine("Replace elements:" + ((ReplaceEventArgs)ev).left);
        }

        static void Main(string[] args)
        {
            Object[] array;
            array = new Object[100];
            Random r = new Random();
            int s1 = 0;
            int s2 = 0;
            
            Console.WriteLine("Before");
            for (int i = 0; i < 100; i++)
            {
                array[i] = r.Next(500);
                Console.Write((int)array[i] + ",");
                s1 += (int)array[i];
            }
            Compare cmp = new Compare(IntCompare);
            ShellSort.onPartiallyDone += PartiallyDone;
            ShellSort.onElementReplacing += ElementReplacing;
            ShellSort.Sort(array, cmp);
            Console.WriteLine("");
            Console.WriteLine("After");
            for (int i = 0; i < 100; i++)
            {
                Console.Write(array[i] + ",");
                s2 += (int)array[i];
            }
            Console.WriteLine("");
            Console.WriteLine(s1 + "=" + s2);
        }
    }
}
