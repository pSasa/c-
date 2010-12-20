using System;

public delegate int Compare(Object left, Object right);

namespace Task2
{
    class PartiallEventArgs : EventArgs
    {
        public int done = 0;
    }

    class ReplaceEventArgs : EventArgs
    {
        public int left;
        public int right;
    }
    class ShellSort
    {
        public static event EventHandler onPartiallyDone;
        public static event EventHandler onElementReplacing;

        public static void Sort(Object[] array, Compare cmp)
        {
            int shift = 0;
            int steps = 4;
            int len = array.Length;
            int step = 1;

            shift = (int)Math.Pow(2, steps - 1);

            while (shift > 0)
            {
                for (int i = shift; i < len; i++)
                {
                    Object temp = array[i];
                    int j;
                    for (j = i - shift; (j >= 0) && cmp(array[j], temp) > 0; j -= shift)
                    {
                        if (onElementReplacing != null)
                        {
                            ReplaceEventArgs evArgs = new ReplaceEventArgs();
                            evArgs.left = j;
                            evArgs.left = j + shift;
                            onElementReplacing(null, evArgs);
                        }
                        array[j + shift] = array[j];
                    }
                    array[j + shift] = temp;
                }
                if (onPartiallyDone != null)
                {
                    PartiallEventArgs evArgs = new PartiallEventArgs();
                    evArgs.done = 100 * step / steps;
                    onPartiallyDone(null, evArgs);
                }
                step++;
                shift = shift >> 1;
            }
        }
    }
}
