using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace task5
{
    class ImagePreview
    {
        private string folder = null;
        private Size size;

        private struct SafeImage
        {
            Image image;
            volatile bool isReady;
        }

        private ImagePreview[] imageList = new ImagePreview[3];

        private void LoadImage(ImagePreview image, string path)
        {

        }

        public bool OpenImageList(out Object[] list)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folder = fbd.SelectedPath;
                folder = @"c:\1\";
                DirectoryInfo di = new DirectoryInfo(folder);
                FileInfo[] files = di.GetFiles("*.png");
                list = files.ToArray();
                return true;
            }
            list = null;
            return false;
        }
        public Image LoadImage(string current, string prev, string next)
        {
            Bitmap b = new Bitmap(Path.Combine(folder, current));
            Bitmap scaled = new Bitmap((Image)b, size);
            return (Image)scaled;
        }

        public Image Refresh()
        {
            Bitmap b = new Bitmap();
            Bitmap scaled = new Bitmap((Image)b, size);
            return (Image)scaled;
        }

        public void SetSize(Size s)
        {
            size = s;
        }
    }
}
