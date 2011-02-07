using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Task4
{
    sealed class Writer
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
        /// Копия needWrite для транзакции
        /// </summary>
        private int needWriteTr = 0;
        /// <summary>
        /// Выставляется в true если не получилось сделать корректный Rollback
        /// все последующие манипуляции с данными вызывают Exception
        /// </summary>
        private bool isOk = true;
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
            private enum State {None, Started};
            private string dataFile;
            private State state = State.None;
            private List<string> files;
            private string lastFile = "";
            private const string backPostfix = "_back";

            /// <summary>
            /// Вернуть последний файл куда писались данные
            /// </summary>
            /// <returns></returns>
            public string GetLastFile()
            {
                if (files.Count > 0)
                {
                    return files[files.Count - 1];
                }
                else
                {
                    return lastFile;
                }
            }

            /// <summary>
            /// Получить состояние транзакции
            /// </summary>
            public bool Started
            {
                get { return state == State.Started; }
            }

            public Transaction(string file)
            {
                dataFile = file;
                files = new List<string>();
            }

            public void Start()
            {
                if (state == State.Started)
                {
                    throw new TransactionAlreadyStarted();
                }
                state = State.Started;
                string[] lines = File.ReadAllLines(dataFile);
                if (lines.Length > 0)
                {
                    lastFile = lines[lines.Length - 1];
                    string folder = Path.GetDirectoryName(dataFile);
                    File.Copy(Path.Combine(folder, lastFile), Path.Combine(folder, lastFile + backPostfix), true);
                }
                else
                {
                    lastFile = "";
                }
            }

            public void RollBack()
            {
                if (state == State.None)
                {
                    throw new TransactionIsnotStarted();
                }
                //обеспечиваем атомарность на случай ThreadAbortException
                //но если что-то с опрацией над файлами произойдет - писец
                try { }
                finally
                {
                    string folder = Path.GetDirectoryName(dataFile);
                    if (lastFile != "")
                    {
                        File.Replace(Path.Combine(folder, lastFile + backPostfix), Path.Combine(folder, lastFile), Path.Combine(folder, lastFile + "_"));
                    }
                    for (int i = 0; i < files.Count; i++)
                    {
                        File.Delete(Path.Combine(folder, files[i]));
                    }
                    files.Clear();
                    state = State.None;
                }
            }

            public void Commit()
            {
                if (state == State.None)
                {
                    throw new TransactionIsnotStarted();
                }
                //обеспечиваем атомарность на случай ThreadAbortException
                //но если что-то с опрацией над файлами произойдет - писец
                try { }
                finally
                {
                    StreamWriter sw = null;
                    try
                    {
                        sw = File.AppendText(dataFile);
                        for (int i = 0; i < files.Count; i++)
                        {
                            sw.WriteLine(files[i]);
                        }
                        if (lastFile != "")
                        {
                            File.Delete(Path.Combine(Path.GetDirectoryName(dataFile), lastFile + backPostfix));
                        }
                    }
                    finally
                    {
                        files.Clear();
                        if (sw != null)
                        {
                            sw.Close();
                        }
                        state = State.None;
                    }
                }
            }
            
            /// <summary>
            /// Добавить файл список новых файлов
            /// </summary>
            /// <param name="file"></param>
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
            StreamWriter sw = null;
            try
            {
                file = path;
                folder = Path.GetDirectoryName(file);
                //проверяем что файл есть если нет то создаем его,
                //во всех методах теперь мы уверены что файл есть
                //если его нет то значит что-то не так
                if (!File.Exists(file))
                {
                    sw = File.CreateText(file);
                    sw.Close();
                    sw = null;
                }
                transaction = new Transaction(file);
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new InvalidDataFormat(e);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
        #endregion

        #region Обработка транзакций
        /// <summary>
        /// Начать транзакцию
        /// </summary>
        public void StartTransaction()
        {
            if (!isOk)
            {
                throw new DataCorrupted();
            }
            try
            {
                transaction.Start();
                needWriteTr = needWrite;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new InvalidDataFormat(e);
            }
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public void Commit()
        {
            if (!isOk)
            {
                throw new DataCorrupted();
            }
            try
            {
                transaction.Commit();
            }
            catch (ThreadAbortException)
            {
                //как мне кажется тут оно не нужно
                isOk = false;
                throw;
            }
            catch (Exception e)
            {
                isOk = false;
                throw new InvalidDataFormat(e);
            }
        }

        /// <summary>
        /// Откатить изменения
        /// </summary>
        public void RollBack()
        {
            if (!isOk)
            {
                throw new DataCorrupted();
            }
            try
            {
                transaction.RollBack();
                needWrite = needWriteTr;
            }
            catch (ThreadAbortException)
            {
                //как мне кажется тут оно не нужно
                isOk = false;
                throw;
            }
            catch (Exception e)
            {
                isOk = false;
                throw new InvalidDataFormat(e);
            }
        }
        #endregion

        /// <summary>
        /// Записать в структуру какоето количество символов
        /// </summary>
        /// <param name="items">Массив который надо записать</param>
        public void Write(int[] items)
        {
            if (!isOk)
            {
                throw new DataCorrupted();
            }
            //если пользователь не стартовал транзакцию сам
            //оборачиваем в транзакцию каждый Write
            bool autoCommit = false;
            if (!transaction.Started)
            {
                autoCommit = true;
                StartTransaction();
            }
            StreamWriter sw = null;
            try
            {
                int count = items.Count();
                int i = 0;
                while (i < count)
                {
                    int lines;
                    if (needWrite > 0)
                    {
                        //дописываем в старый файл
                        lines = needWrite;
                        needWrite = 0;
                        sw = File.AppendText(Path.Combine(folder, transaction.GetLastFile()));
                    }
                    else
                    {
                        //создаем новый файл
                        lines = r.Next(MinLines, MaxLines);
                        string newFile = Path.GetRandomFileName();
                        sw = File.CreateText(Path.Combine(folder, newFile));
                        transaction.AddFile(newFile);
                    }
                    if (lines > count - i)
                    {
                        //порция закончилась но файл не полный
                        //запоминаем сколько нужно дописать в последний файл
                        needWrite = lines - count + i;
                        lines = count - i;
                    }
                    for (int j = 0; j < lines; j++)
                    {
                        sw.WriteLine(items[i + j]);
                    }
                    sw.Close();
                    sw = null;
                    i += lines;
                }
            }
            catch (ThreadAbortException)
            {
                RollBack();
                throw;
            }
            catch (Exception e)
            {
                RollBack();
                throw new InvalidDataFormat(e);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            if (autoCommit)
            {
                Commit();
            }
        }
    }
}
