using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Task4
{
    /// <summary>
    /// Исключение сигнализируещее что-что не так со структурой
    /// </summary>
    class InvalidDataFormat : Exception
    {
        public InvalidDataFormat(Exception inner)
            : base(null, inner)
        {
        }
    }

    /// <summary>
    /// Не найдена структура
    /// </summary>
    class DataNotFound : Exception
    {
        public DataNotFound()
            : base(null)
        {
        }
    }

    /// <summary>
    /// Запросили на чтение данных больше чем есть в структуре
    /// </summary>
    class NotEnoughDataLenght : Exception
    {
        public NotEnoughDataLenght()
            : base(null)
        {
        }
    }

    class Reader
    {
        #region Переменные
        /// <summary>
        /// главный файл
        /// </summary>
        private string dataFile;

        /// <summary>
        /// Файл из которого надо читать
        /// </summary>
        private int dataFileIndex = 0;
        
        /// <summary>
        /// Позиция в файле с которй надо читать
        /// </summary>
        private int fileIndex = 0;

        /// <summary>
        /// Данные о предыдущем выполнении свойства Count
        /// </summary>
        private int lastCount = 0;
        private int lastFileCount = 0;
        #endregion

        #region свойсво Count
        /// <summary>
        /// Получить размер структуры
        /// </summary>
        public int Count
        {
            get
            {
                //Алгоритм такой:
                //при первом запуске считываем все файлы и размеры суммируем
                //запоминаем номер последнего файла и размер без учета последнего файла
                //при повторном запуске считаем только файлы новые и последний (его размер мог поменяться)
                bool allGood = false;
                int count = lastCount;
                int endCount = 0;
                string[] files = null;
                try
                {
                    files = File.ReadAllLines(dataFile);
                    string folder = Path.GetDirectoryName(dataFile);
                    for (int i = lastFileCount; i < files.Length - 1; i++)
                    {
                        string[] lines = File.ReadAllLines(Path.Combine(folder, files[i]));
                        count += lines.Length;
                    }
                    if (files.Length > 0)
                    {
                        endCount = File.ReadAllLines(Path.Combine(folder, files[files.Length - 1])).Length;
                    }
                    allGood = true;
                }
                catch(Exception e)
                {
                    throw new InvalidDataFormat(e);
                }
                finally
                {
                    if (allGood)
                    {
                        lastCount = count;
                        lastFileCount = Math.Max(files.Length - 1, 0);
                    }
                }
                return count + endCount;
            }
        }
        #endregion

        #region Конструктор 
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="file">Путь до главного файла</param>
        public Reader(string file)
        {
            if (file == null || !File.Exists(file))
            {
                throw new DataNotFound();
            }
            dataFile = file;
        }
        #endregion

#region Метод чтения
        /// <summary>
        /// Прочитать данные из структуры
        /// </summary>
        /// <param name="count">Сколько элементов нужно прочитать</param>
        /// <returns>Массив из прочитанных элементов</returns>
        public int[] Read(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            bool allGood = false;
            string[] files = null;
            int fileIndexCopy = fileIndex;
            int dataFileIndexCopy = dataFileIndex;
            int[] res = null;
            try
            {
                files = File.ReadAllLines(dataFile);
            }
            catch (Exception e)
            {
                new InvalidDataFormat(e);
            }
            if (files.Length <= dataFileIndex)
            {
                throw new NotEnoughDataLenght();
            }
            try
            {
                string currentFile = files[dataFileIndexCopy];
                string folder = Path.GetDirectoryName(dataFile);
                int readed = 0;
                res = new int[count];
                while (readed < count)
                {
                    string[] lines = File.ReadAllLines(Path.Combine(folder, currentFile));
                    int needCopy = Math.Min(lines.Length - fileIndexCopy, count - readed);
                    for (int i = 0; i < needCopy; i++)
                    {
                         res[readed + i] = Int32.Parse(lines[fileIndexCopy + i]);
                    }
                    //Array.Copy(lines, fileIndexCopy, res, readed, needCopy);
                    readed += needCopy;
                    if (readed == count)
                    {
                        if (needCopy + fileIndexCopy == lines.Length)
                        {
                            fileIndexCopy = 0;
                            dataFileIndexCopy++;
                            if (files.Length <= dataFileIndex)
                            {
                                throw new NotEnoughDataLenght();
                            }
                        }
                        else
                        {
                            fileIndexCopy += needCopy;
                        }
                    }
                    else
                    {
                        fileIndexCopy = 0;
                        dataFileIndexCopy++;
                        if (files.Length <= dataFileIndex)
                        {
                            throw new NotEnoughDataLenght();
                        }
                        currentFile = files[dataFileIndexCopy];
                    }
                }
                allGood = true;
            }
            catch (Exception e)
            {
                throw new InvalidDataFormat(e);
            }
            finally
            {
                //Сохраняем новые позиции если все хорошо прочиталось
                if (allGood)
                {
                    fileIndex = fileIndexCopy;
                    dataFileIndex = dataFileIndexCopy;
                }
            }
            return res;
        }
        #endregion
    }
}
