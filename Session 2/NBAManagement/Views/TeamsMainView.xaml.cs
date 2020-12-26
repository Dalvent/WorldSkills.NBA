using NBAManagement.Models;
using NBAManagement.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TeamsMainPage.xaml
    /// </summary>
    public partial class TeamsMainView : Page
    {
        public TeamsMainView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var viewModel = await Task.Run(() => TeamsMainViewModel.Create());
                DataContext = viewModel;
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить данные из базы данных");
                NavigationManager.GoBack();
            }
        }
    }
}
