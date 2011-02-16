using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace task5
{
    class ImagePreview
    {
        private string folder = null;
        private Size size;

        private class SafeImage
        {
            public Image image;
            public volatile bool isReady;
            public string name;
        }

        private SafeImage[] imageList = null;

        private void LoadImage(SafeImage image)
        {
            Bitmap b = new Bitmap(image.name);
            image.image = (Image) new Bitmap((Image)b, size);
            image.isReady = true;
        }

        public bool OpenImageList(out Object[] list)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folder = fbd.SelectedPath;
                folder = @"c:\1\";
                DirectoryInfo di = new DirectoryInfo(folder);
                FileInfo[] files = di.GetFiles("*.jpg");
                list = files.ToArray();
                return true;
            }
            list = null;
            return false;
        }
        public Image LoadImage(string current, string prev, string next)
        {
            SafeImage[] newImageList = new SafeImage[3];
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
            if (newImageList[1] == null)
            {
                newImageList[1] = new SafeImage();
                newImageList[1].name = current;
                newImageList[1].isReady = true;
                Bitmap b = new Bitmap(newImageList[1].name);
                Bitmap scaled = new Bitmap((Image)b, size);
                newImageList[1].image = scaled;
            }
            if (prev != null && newImageList[0] == null)
            {
                newImageList[0] = new SafeImage();
                newImageList[0].name = prev;
                newImageList[0].isReady = false;
                ThreadPool.QueueUserWorkItem(o => LoadImage(newImageList[0]));
            }
            if (next != null && newImageList[2] == null)
            {
                newImageList[2] = new SafeImage();
                newImageList[2].name = next;
                newImageList[2].isReady = false;
                ThreadPool.QueueUserWorkItem(o => LoadImage(newImageList[2]));
            }
            imageList = newImageList;
            return newImageList[1].image;
        }

        public Image Refresh()
        {
            //Bitmap b = new Bitmap();
            //Bitmap scaled = new Bitmap((Image)b, size);
            //return (Image)scaled;
            return null;
        }

        public void SetSize(Size s)
        {
            size = s;
        }
    }
}
