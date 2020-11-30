using Microsoft.Win32;
using NBAManagement.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
    /// Логика взаимодействия для PhotosPage.xaml
    /// </summary>
    public partial class PhotosPage : Page
    {
        private const int PAGE_ELEMENT_COUNT = 12;

        public PagedEnumerable<Pictures> PagedPictures { get; private set; }
        public int PageNum
        {
            get => PagedPictures.CurrentPageNum;
            set
            {
                PagedPictures.CurrentPageNum = value;
                UpdateBindings();
            }
        }
        public PhotosPage()
        {
            InitializeComponent();
            PagedPictures = new PagedEnumerable<Pictures>(NBAContext.Instance.Pictures.OrderByDescending(p => p.CreateTime).ToList(), PAGE_ELEMENT_COUNT);
            DataContext = this;
        }

        public void UpdateBindings()
        {
            var dataContext = DataContext;
            DataContext = null;
            DataContext = dataContext;
        }

        private void FirstPage(object sender, RoutedEventArgs e)
        {
            PageNum = 1;
        }

        private void PriveusPage(object sender, RoutedEventArgs e)
        {
            PageNum--;
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            PageNum++;
        }

        private void LastPage(object sender, RoutedEventArgs e)
        {
            PageNum = PagedPictures.PageCount;
        }

        private Pictures pictureToDownload; 
        private void DownloadImage(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Png | *.png";
            downloadPoput.IsOpen = false;
            if(saveFileDialog.ShowDialog() != true)
                return;

            if(!saveFileDialog.CheckPathExists)
            {
                MessageBox.Show("Путь несуществует", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            pictureToDownload.SaveAsFile(saveFileDialog.FileName, ImageFormat.Png);
        }

        private void DownloadPageImages(object sender, RoutedEventArgs e)
        {
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if(folderDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            int i = 0;
            foreach(var item in PagedPictures)
            {
                item.SaveAsFile($@"{folderDialog.SelectedPath}/picture{++i}.png", ImageFormat.Png);
            }

            MessageBox.Show("Файлы успешно записаны на диск");
        }

        private void ShowDownloadPoput(object sender, MouseButtonEventArgs e)
        {
            pictureToDownload = (Pictures)((Button)sender).DataContext;
            downloadPoput.IsOpen = true;
        }
    }
}
