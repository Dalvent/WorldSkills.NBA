using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NBAManagement
{
    class ImagesLineManager
    {
        private readonly ICollection<System.Windows.Controls.Image> imageViews;
        private readonly IList<BitmapImage> imageSources;
        private int firstImageIndex = 0;


        public ImagesLineManager(ICollection<System.Windows.Controls.Image> imageViews, IList<BitmapImage> imageSources)
        {
            this.imageSources = imageSources;
            this.imageViews = imageViews;
            UpdateViewImages();
        }

        public void NextPage()
        {
            firstImageIndex = Math.Min(imageSources.Count - 1, firstImageIndex + imageViews.Count);
            UpdateViewImages();
        }

        public void PriviusPage()
        {
            firstImageIndex = Math.Max(0, firstImageIndex - imageViews.Count);
            UpdateViewImages();
        }

        private void UpdateViewImages()
        {
            int i = 0;
            foreach(var imageView in imageViews)
            {
                imageView.Source = imageSources[GetImageIndex(firstImageIndex, i)];
                i++;
            }
        }

        private int GetImageIndex(int index, int offset)
        {
            int finalIndex = index + offset;
            while(finalIndex >= imageSources.Count)
            {
                finalIndex -= imageSources.Count;
            }
            return finalIndex;
        }
    }
}
