using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Task4
{
    class InvalidDataFormat : Exception
    {
        public InvalidDataFormat(Exception inner)
            : base(null, inner)
        {
        }
    }

    class DataNotFound : Exception
    {
        public DataNotFound()
            : base(null)
        {
        }
    }

    class NotEnoughDataLenght : Exception
    {
        public NotEnoughDataLenght()
            : base(null)
        {
        }
    }

    class Reader
    {
        private string dataFile;
        private int dataFileIndex = 0;
        private int fileIndex = 0;

        private int lastCount = 0;
        private int lastFileCount = 0;

        public int Count
        {
            get
            {
                bool allGood = false;
                int count = lastCount;
                string[] files = null;
                try
                {
                    files = File.ReadAllLines(dataFile);
                    string folder = Path.GetDirectoryName(dataFile);
                    for (int i = lastFileCount; i < files.Length; i++)
                    {
                        string[] lines = File.ReadAllLines(Path.Combine(folder, files[i]));
                        count += lines.Length;
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
                        lastFileCount = files.Length;
                    }
                }
                return count;
            }
        }

        public Reader(string file)
        {
            if (file == null || !File.Exists(file))
            {
                throw new DataNotFound();
            }
            dataFile = file;
        }

        public int[] Read(int count)
        {
            if (count < 0)
            {
                //throw new Nu;
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
                if (allGood)
                {
                    fileIndex = fileIndexCopy;
                    dataFileIndex = dataFileIndexCopy;
                }
            }
            return res;
        }
    }
}
