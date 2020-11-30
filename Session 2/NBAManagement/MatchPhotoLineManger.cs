using NBAManagement.Filter;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NBAManagement
{
    public class MatchPhotoLineManger
    {
        private readonly IEnumerable<Image> imageViews;
        private readonly PagedEnumerable<byte[]> images;

        public MatchPhotoLineManger(IEnumerable<Image> imageViews, IEnumerable<byte[]> imageSources)
        {
            this.imageViews = imageViews;
            this.images = new PagedEnumerable<byte[]>(imageSources, imageViews.Count());
            UpdateViewImages();
        }

        public void NextPage()
        {
            images.NextPage();
            UpdateViewImages();
        }

        public void PriviusPage()
        {
            images.PriviusPage();
            UpdateViewImages();
        }

        private void UpdateViewImages()
        {
            var imagesArray = images.ToArray();
            int i = 0;
            foreach(var imageView in imageViews)
            {
                using(var memory = new MemoryStream(imagesArray[i]))
                {
                    BitmapImage image = new BitmapImage();
                    memory.Position = 0;
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = memory;
                    image.EndInit();
                    image.Freeze();
                    imageView.Source = image;
                    i++;
                }
            }
        }
    }
}
