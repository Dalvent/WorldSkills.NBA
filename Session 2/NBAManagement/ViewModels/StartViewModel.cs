using NBAManagement.Command;
using NBAManagement.Models;
using NBAManagement.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.ViewModels
{
    class StartViewModel : INotifyPropertyChanged
    {
        private readonly PagedEnumerable<byte[]> images;
        private const int PAGE_SIZE = 3;
        public IList<byte[]> ImageInPage { get; set; }
        public ICommand NextImagePage { get; set; }
        public ICommand PriviusImagePage { get; set; }
        public ICommand OpenLoginPage { get; set; }
        public ICommand OpenVisitorPage { get; set; }

        public StartViewModel()
        {
            images = new PagedEnumerable<byte[]>(
                NBAContext.Instance.Pictures.Select(p => p.Img).ToArray(),
                PAGE_SIZE
            );
            UpdateImageInPage();
            NextImagePage = new ActionCommand(() =>
            {
                images.NextPage();
                UpdateImageInPage();
            });
            PriviusImagePage = new ActionCommand(() =>
            {
                images.PriviusPage();
                UpdateImageInPage();
            });
            OpenLoginPage = new ActionCommand(() => NavigationManager.OpenPage(new LoginPage()));
            OpenVisitorPage = new ActionCommand(() => NavigationManager.OpenPage(new VisitorMainPage()));
        }

        private void UpdateImageInPage()
        {
            ImageInPage = new List<byte[]>(images.Filtered);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageInPage)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
