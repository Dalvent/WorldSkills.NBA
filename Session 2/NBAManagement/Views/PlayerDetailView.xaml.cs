using NBAManagement.Models;
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
using System.Windows.Forms.DataVisualization.Charting;
using NBAManagement.Filter;
using NBAManagement.ViewModels;

namespace NBAManagement.Views
{
    /// <summary>
    /// Логика взаимодействия для PlayerDetail.xaml
    /// </summary>
    public partial class PlayerDetailView : Page
    {
        private readonly RadioBackgroundControlHighlighter buttonHighlighter;
        public PlayerDetailView(PlayerInTeam player)
        {
            InitializeComponent();
            DataContext = new PlayerDetailViewModel(player);
            buttonHighlighter = new RadioBackgroundControlHighlighter(
                (SolidColorBrush)FindResource("buttonStandartColor"), 
                (SolidColorBrush)FindResource("buttonHighlightColor")
            );
        }

        private void SwitchDataOnChart(object sender, RoutedEventArgs e)
        {
            var senderButton = (Button)sender;
            buttonHighlighter.HighlightControl(senderButton);
        }
    }
}
