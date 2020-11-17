using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NBAManagement
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private ImagesLineManager imagesLineManager;
        public StartPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            imagesLineManager = new ImagesLineManager(
                new List<Image>() { image1, image2, image3 },
                FindLastMatchImages("MatchPhotos")
            );
        }

        private IList<BitmapImage> FindLastMatchImages(string folderName)
        {
            var foundImages = new List<BitmapImage>();
            var imagePathes = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}/{folderName}", "*.jpg");
            foreach(string imagePath in imagePathes)
            {
                foundImages.Add(new BitmapImage(new Uri(imagePath)));
            }
            return foundImages;
        }

        private void NextPhotosButton_Click(object sender, RoutedEventArgs e)
        {
            imagesLineManager.NextPage();
        }

        private void PreviusPhotosButton_Click(object sender, RoutedEventArgs e)
        {
            imagesLineManager.PriviusPage();
        }


        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.OpenPage(new VisitorMainPage());
        }

        private void VisitorButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.OpenPage(new VisitorMainPage());
        }
    }
}
