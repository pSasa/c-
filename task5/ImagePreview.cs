using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace task5
{
    sealed class ImagePreview
    {
        private string folder = null;
        private Size size;

        /// <summary>
        /// класс для хранения информации о картинке
        /// </summary>
        sealed private class SafeImage
        {
            public Image image;
            public Mutex mutex;
            public volatile bool isReady;
            public string name;

            public SafeImage(string path)
            {
                name = path;
                isReady = false;
                mutex = new Mutex();
            }
        }

        /// <summary>
        /// 3 картинки
        /// </summary>
        private SafeImage[] imageList = null;

        /// <summary>
        /// Загрузка картинки в отдельном потоке
        /// </summary>
        /// <param name="image">что нужно грузить</param>
        private void LoadImage(SafeImage image)
        {
            image.mutex.WaitOne();
            try
            {
                Bitmap b = new Bitmap(image.name);
                image.image = (Image)new Bitmap((Image)b, size);
                Debug.WriteLine("Load " + image.name);
            }
            catch (Exception)
            {
                //нет файла или он кривой кто должен ловить исключение?
                //наверное тот кто использует наш класс
                throw;
            }
            finally
            {
                //освобиться надо в любом случае
                image.isReady = true;
                image.mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Изменение размера картинки в отдельном потоке
        /// </summary>
        /// <param name="image">что нужно изменять</param>
        private void ResizeImage(SafeImage image)
        {
            image.mutex.WaitOne();
            try
            {
                image.image = (Image)new Bitmap((Image)image.image, size);
                Debug.WriteLine("Resize " + image.name);
            }
            finally
            {
                image.isReady = true;
                image.mutex.ReleaseMutex();
            }
        }
        
        /// <summary>
        /// Получить список картинок - открыть директорию
        /// Фильтруются только jpg файлы
        /// не нашел как применить более человеческий фильтр
        /// </summary>
        /// <param name="list">Массив имен картинок</param>
        /// <returns>true если директорию открыли
        /// false если отказались</returns>
        public bool OpenImageList(out Object[] list)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folder = fbd.SelectedPath;
                DirectoryInfo di = new DirectoryInfo(folder);
                FileInfo[] files = di.GetFiles("*.jpg");
                list = files.ToArray();
                return true;
            }
            list = null;
            return false;
        }

        /// <summary>
        /// запрос на получение одраза картинки
        /// текущая картинка грузится и отдается
        /// соседние кэшируются
        /// </summary>
        /// <param name="current">что нужно подгрузить</param>
        /// <param name="prev">имя соседней с текущей картинка</param>
        /// <param name="next">имя соседней с текущей картинка</param>
        /// <returns></returns>
        public Image LoadImage(string current, string prev, string next)
        {
            //новый массив картинок
            //не стал перетасовывать местами элементы в траром так как очень это геморно
            SafeImage[] newImageList = new SafeImage[3];
            //строим абсолютные пути
            if (current != null)
            {
                current = Path.Combine(folder, current);
            }
            else
            {
                //throw;
            }
            if (prev != null)
            {
                prev = Path.Combine(folder, prev);
            }
            if (next != null)
            {
                next = Path.Combine(folder, next);
            }
            //смотрим что закэшированно
            //критерий сравнения - абсолютный путь
            //критерий не очень честный но для занной задачи пойдет
            if (imageList != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (current.CompareTo(imageList[i].name) == 0)
                    {
                        newImageList[1] = imageList[i];
                    }
                    else if (prev != null && prev.CompareTo(imageList[i].name) == 0)
                    {
                        newImageList[0] = imageList[i];
                    }
                    else if (next != null && next.CompareTo(imageList[i].name) == 0)
                    {
                        newImageList[2] = imageList[i];
                    }
                }
            }
            //загружаем выбранную картинку (если не кэширована)
            //и нитью запускаем загрузку предыдущей и следующей (если не кэшированы)
            SafeImage sim;
            if (newImageList[1] == null)
            {
                newImageList[1] = new SafeImage(current);
                newImageList[1].isReady = true;
                Bitmap b = new Bitmap(newImageList[1].name);
                Bitmap scaled = new Bitmap((Image)b, size);
                newImageList[1].image = scaled;
            }
            if (prev != null && newImageList[0] == null)
            {
                newImageList[0] = new SafeImage(prev);
                sim = newImageList[0];
                Debug.WriteLine("Pool Load " + sim.name);
                ThreadPool.QueueUserWorkItem(o => LoadImage(sim));
                
            }
            if (next != null && newImageList[2] == null)
            {
                newImageList[2] = new SafeImage(next);
                sim = newImageList[2];
                Debug.WriteLine("Pool Load " + sim.name);
                ThreadPool.QueueUserWorkItem(o => LoadImage(sim));
            }
            imageList = newImageList;
            Image im = null;
            //если текущая картинка кэширована но еща в процессе загрузки - ждем пока не загрузится
            //здесь не использую спинлок так как можно процу загрузить пока тудет с диска/сети читаться
            do
            {
                imageList[1].mutex.WaitOne();
                if (imageList[1].isReady)
                {
                    im = imageList[1].image;
                }
                imageList[1].mutex.ReleaseMutex();
            } while (!imageList[1].isReady);
            return im;
        }

        /// <summary>
        /// Запрашивают повторно текущюю картинку
        /// скорее всего поменялся размер
        /// ждут образ с новым размером
        /// </summary>
        /// <returns>Образ актуализированной катринки</returns>
        public Image Refresh()
        {
            if (imageList == null)
            {
                return null;
            }
            //ждем пока не закончится отработка картинки
            Image im = null;
            do
            {
                imageList[1].mutex.WaitOne();
                im = imageList[1].image;
                imageList[1].mutex.ReleaseMutex();
            } while (!imageList[1].isReady);
            return im;
        }

        /// <summary>
        /// Задается размер под который надо картинки в кэше подогнать
        /// </summary>
        /// <param name="s">новый размер холста</param>
        public void SetSize(Size s)
        {
            //если поменялся размер - нитями запускаем resize картинок
            if (!size.Equals(s))
            {
                size = s;
                //resize all images
                if (imageList != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (imageList[i] != null)
                        {
                            imageList[1].mutex.WaitOne();
                            imageList[i].isReady = false;
                            imageList[1].mutex.ReleaseMutex();
                            SafeImage im = imageList[i];
                            ThreadPool.QueueUserWorkItem(o => ResizeImage(im));
                        }
                    }
                }
            }
        }
    }
}
