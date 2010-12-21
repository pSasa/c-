using System;

/// <summary>
/// Delegate for comparing two elements
/// </summary>
/// <param name="left">left object</param>
/// <param name="right">right object</param>
/// <returns>0 if left and right are equal
///          1 if left more than right
///          -1 if left less than right</returns>
public delegate int Compare(Object left, Object right);

namespace Task2
{
    #region EventArgs
    /// <summary>
    /// Class of argument for PartiallyDone event
    /// </summary>
    sealed class PartialEventArgs : EventArgs
    {
        public int done = 0;
    }
    /// <summary>
    /// Class of argument for ElementsReplacing event
    /// </summary>
    sealed class ReplaceEventArgs : EventArgs
    {
        public Object left;
        public Object right;
    }
    #endregion

    /// <summary>
    /// Implementation of Shell sorting method
    /// </summary>
    public static class ShellSort
    {
        #region events
        /// <summary>
        /// Event which class sends when it done some part of sorting
        /// </summary>
        public static event EventHandler PartiallyDone;

        /// <summary>
        /// Event which class sends before replacing two elements in array
        /// </summary>
        public static event EventHandler ElementsReplacing;
        #endregion

        #region ShellSort
        /// <summary>
        /// Sort array by Shell alhoritm
        /// </summary>
        /// <param name="array">array of object for sorting</param>
        /// <param name="cmp">function for compare two elements</param>
        public static void Sort(Object[] array, Compare cmp)
        {
            int len = array.Length;
            int shift = 0;
            //count of steps in Shell alhoritm
            int steps = 4;
            //current step in Shell alhoritm
            int step = 1;

            //calculate initial shift
            shift = (int)Math.Pow(2, steps - 1);
            while (shift > 0)
            {
                for (int i = shift; i < len; i++)
                {
                    Object temp = array[i];
                    int j;
                    for (j = i - shift; (j >= 0) && cmp(array[j], temp) > 0; j -= shift)
                    {
                        //need to 'change' elements. Send event
                        if (ElementsReplacing != null)
                        {
                            ReplaceEventArgs evArgs = new ReplaceEventArgs();
                            evArgs.left = array[j];
                            evArgs.right = array[j + shift];
                            ElementsReplacing(null, evArgs);
                        }
                        array[j + shift] = array[j];
                    }
                    array[j + shift] = temp;
                }
                // Current step was finised. Send event.
                if (PartiallyDone != null)
                {
                    PartialEventArgs evArgs = new PartialEventArgs();
                    evArgs.done = 100 * step / steps;
                    PartiallyDone(null, evArgs);
                }
                step++;
                shift = shift >> 1;
            }
        }
        #endregion
    }
}
