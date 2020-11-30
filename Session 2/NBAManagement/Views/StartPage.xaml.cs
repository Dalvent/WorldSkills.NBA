using NBAManagement.Models;
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

namespace NBAManagement.Views
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private MatchPhotoLineManger matchPhotoLineManger;
        public StartPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            matchPhotoLineManger = new MatchPhotoLineManger(
                new List<Image>() { image1, image2, image3 },
                NBAContext.Instance.Pictures.Select(p => p.Img)
            );
        }

        private void NextPhotosButton_Click(object sender, RoutedEventArgs e)
        {
            matchPhotoLineManger.NextPage();
        }

        private void PreviusPhotosButton_Click(object sender, RoutedEventArgs e)
        {
            matchPhotoLineManger.PriviusPage();
        }


        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not relesed now...");
        }

        private void VisitorButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.OpenPage(new VisitorMainPage());
        }
    }
}
