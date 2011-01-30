using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Task4
{
    static class RWTool
    {
        public static int GetFileEnd(string file, out string value)
        {
            StreamReader sr = File.OpenText(file);
            int lines = 0;
            value = "";
            while (sr.EndOfStream)
            {
                value = sr.ReadLine();
                lines++;
            }
            sr.Close();
            return lines;
        }

    }
    class Writer
    {
        private string file;
        private string folder;
        private Random r = new Random();
        private Transaction transaction;
        private const int MinLines = 10;
        private const int MaxLines = 20;

        #region Transaction class
        private class Transaction
        {
            private string dataFile;
            private int state = 0;
            private List<string> files;

            public Transaction(string file)
            {
                dataFile = file;
                files = new List<string>();
            }

            public void Start()
            {
                if (state != 0)
                {
                    //thow
                }
                state = 1;
            }

            public void RollBack()
            {
                string folder = Path.GetDirectoryName(dataFile);
                foreach (string file in files)
                {
                    File.Delete(Path.Combine(folder, file));
                }
                files.Clear();
                state = 0;
            }

            public void Commit()
            {
                StreamWriter sw = File.AppendText(dataFile);
                foreach (string file in files)
                {
                    sw.WriteLine(file);
                }
                files.Clear();
                sw.Close();
                state = 0;
            }

            public void AddFile(string file)
            {
                files.Add(file);
            }
        }
        #endregion


        public Writer(string path)
        {
            file = path;
            folder = Path.GetDirectoryName(file);
            //проверяем что файл есть если нет то создаем его,
            //во всех методах теперь мы уверены что файл есть
            //если его нет то значит что-то не так
            if (!File.Exists(file))
            {
                StreamWriter sw = File.CreateText(file);
                sw.Close();
            }
            transaction = new Transaction(file);
        }

        public void StartTr()
        {
            transaction.Start();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void RollBack()
        {
            transaction.RollBack();
        }

        public void Write(int[] items)
        {
            int count = items.Count();
            int i = 0;

            while (i < count)
            {
                int lines = r.Next(MinLines, MaxLines);
                lines = Math.Min(lines, count - i);
                string newFile = Path.GetRandomFileName();
                StreamWriter sw = File.CreateText(Path.Combine(folder, newFile));
                transaction.AddFile(newFile);
                for (int j = 0; j < lines; j++)
                {
                    sw.WriteLine(items[i + j]);
                }
                sw.Close();
                i += lines;
            }
        }
    }
}
