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
        #region переменные
        /// <summary>
        /// главный файл
        /// </summary>
        private string file;
        /// <summary>
        /// Каталог главного файла. Часто используется поэтому вынес
        /// </summary>
        private string folder;
        /// <summary>
        /// Сколько нужно дописать в последний файл
        /// </summary>
        private int needWrite = 0;
        /// <summary>
        /// чтобы размер файла нового вычислять
        /// </summary>

        private Random r = new Random();
        private const int MinLines = 10;
        private const int MaxLines = 20;

        /// <summary>
        /// Класс отвечающий за работу транзакций
        /// </summary>
        private Transaction transaction;
        #endregion

        #region Transaction class
        private class Transaction
        {
            private string dataFile;
            private int state = 0;
            private List<string> files;
            private string last_file;

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
                string[] lines = File.ReadAllLines(dataFile);
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

        #region Конструктор
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="path">Путь к главному файлу</param>
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
        #endregion

        #region Обработка транзакций
        /// <summary>
        /// Начать транзакцию
        /// </summary>
        public void StartTr()
        {
            transaction.Start();
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public void Commit()
        {
            transaction.Commit();
        }

        /// <summary>
        /// Откатить изменения
        /// </summary>
        public void RollBack()
        {
            transaction.RollBack();
        }
        #endregion

        /// <summary>
        /// Записать в структуру какоето количество символов
        /// </summary>
        /// <param name="items">Массив который надо записать</param>
        public void Write(int[] items)
        {
            int count = items.Count();
            int i = 0;

            while (i < count)
            {
                int lines = r.Next(MinLines, MaxLines);
                if (lines > count - i)
                {
                    needWrite = lines - count + i;
                    lines = count - i;
                }
                
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
