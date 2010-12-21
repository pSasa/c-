using System;

namespace Task2
{
    class Program
    {
        //Count of replacing during sorting
        static int replacingCount = 0;

        /// <summary>
        /// Compare two elements of Hardware object
        /// </summary>
        /// <param name="left">left element</param>
        /// <param name="right">right element</param>
        /// <returns>0 if left and right are equal
        ///          1 if left more than right
        ///          -1 if left less than right</returns>
        static int HardwareCompare(Object left, Object right)
        {
            Hardware leftItem = (Hardware)left;
            Hardware rightItem = (Hardware)right;
            int res;

            //at first compare types of devices
            //at second compare verdors of devices
            //finally compare models of devices
            res = leftItem.type.CompareTo(rightItem.type);
            if (res != 0)
            {
                return res;
            }
            res = leftItem.vendor.CompareTo(rightItem.vendor);
            if(res != 0)
            {
                return res;
            }
            if(leftItem.model > rightItem.model)
            {
                return 1;
            }
            if(leftItem.model < rightItem.model)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Write to console sorting progress
        /// </summary>
        /// <param name="sender">object who send message</param>
        /// <param name="ev">additional description of event</param>
        static void PartiallyDone(Object sender, EventArgs ev)
        {
            Console.WriteLine("Done percents: {0}", ((PartialEventArgs)ev).done);
        }

        /// <summary>
        /// Count number of replacing during sorting
        /// </summary>
        /// <param name="sender">object who send message</param>
        /// <param name="ev">additional description of event</param>
        static void ElementReplacing(Object sender, EventArgs ev)
        {
            replacingCount++;
        }

        static void Main(string[] args)
        {
            Object[] array;
            int arraySize = 100;
            array = new Object[arraySize];
            Console.WriteLine("Original array");
            //fill array
            for (int i = 0; i < arraySize; i++)
            {
                array[i] = new Hardware();
                Console.WriteLine(array[i]);
            }
            //preparing to sorting
            Compare cmp = new Compare(HardwareCompare);
            ShellSort.PartiallyDone += PartiallyDone;
            ShellSort.ElementsReplacing += ElementReplacing;
            //sort it
            ShellSort.Sort(array, cmp);
            //Print results
            Console.WriteLine("\nSorted array");
            for (int i = 0; i < arraySize; i++)
            {
                Console.WriteLine(array[i]);
            }
            Console.WriteLine("{0} replacing were made", replacingCount);
        }
    }

    sealed class Hardware
    {
        /// <summary>
        /// Type of device
        /// </summary>
        public string type;
        /// <summary>
        /// Vendor of device
        /// </summary>
        public string vendor;
        /// <summary>
        /// Model of device
        /// </summary>
        public int model;
        /// <summary>
        /// Helper for generate random numbers. Put it here for generate more random numbers
        /// </summary>
        static Random r = new Random();

        /// <summary>
        /// List of types of devices
        /// </summary>
        static string[] types = new string[] { "Printer", "Monitor", "Mouse", "Keyboard", "HDD", "Video", "Modem"};
        /// <summary>
        /// list of vendores
        /// </summary>
        static string[] vendors = new string[] { "Samsung", "LG", "Genius", "ATI", "BBK", "Sony", "D-Link", "Zyxel"};

        /// <summary>
        /// Generate element of Hardware with random fields
        /// </summary>
        public Hardware()
        {
            type = types[r.Next(6)];
            vendor = vendors[r.Next(7)];
            model = r.Next(1, 999);
        }

        public override string ToString()
        {
            return "Type:"+ type +"\tVendor:" + vendor+ "\tModel:"+ model;
        }
    }
}
