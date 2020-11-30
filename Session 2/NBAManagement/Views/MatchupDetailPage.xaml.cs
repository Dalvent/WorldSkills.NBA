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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NBAManagement.Views
{
    /// <summary>
    /// Логика взаимодействия для MatchupDetailPage.xaml
    /// </summary>
    public partial class MatchupDetailPage : Page
    {
        private Matchup MatchupContext => (Matchup)DataContext;
        public MatchupDetailPage(Matchup matchup)
        {
            InitializeComponent();
            DataContext = matchup;
            quaterComboBox.ItemsSource = MatchupContext.MatchupDetail;
            quaterComboBox.SelectedIndex = 0;
            SetFilteredByQuaterLog();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            SetFilteredByQuaterLog();
        }

        private void SetFilteredByQuaterLog()
        {
            var currentPart = (MatchupDetail)quaterComboBox.SelectedItem;
            logDataGrid.ItemsSource = MatchupContext.MatchupLog.Where(ml => ml.Quarter == currentPart.Quarter);
        }
    }
}
