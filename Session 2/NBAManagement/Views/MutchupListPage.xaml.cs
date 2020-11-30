using NBAManagement.Models;
using NBAManagement.Filter;
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
    /// Логика взаимодействия для MutchupListPage.xaml
    /// </summary>
    public partial class MutchupListPage : Page
    {
        private readonly FilteredEnumerable<Matchup> matchups;
        public MutchupListPage()
        {
            InitializeComponent();
            matchups = new FilteredEnumerable<Matchup>(NBAContext.Instance
                .Matchup
                .OrderBy(s => s.Status)
                .ToList());
            gameDatePicker.SelectedDate = new DateTime(2017, 3, 30);
            nearestDateContainer.DataContext = GetMatchNearestMatchup(matchups);
        }

        private Matchup GetMatchNearestMatchup(IEnumerable<Matchup> matchups)
        {
            var nearestMatchup = matchups.Where(m => (m.Status == -1)).Min();
            if(nearestMatchup != null)
            {
                return nearestMatchup;
            }
            else
            {
                return matchups.Max();
            }
        }

        private void nextDateButton_Click(object sender, RoutedEventArgs e)
        {
            if(!gameDatePicker.SelectedDate.HasValue)
                return;

            var nextDayDate = gameDatePicker.SelectedDate + new TimeSpan(1, 0, 0, 0);
            gameDatePicker.SelectedDate = nextDayDate;
        }

        private void previousDateButton_Click(object sender, RoutedEventArgs e)
        {
            if(!gameDatePicker.SelectedDate.HasValue)
                return;

            var nextDayDate = gameDatePicker.SelectedDate + new TimeSpan(-1, 0, 0, 0);
            gameDatePicker.SelectedDate = nextDayDate;
        }

        private void gameDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!gameDatePicker.SelectedDate.HasValue) return;

            matchupListView.ItemsSource = matchups
                .Where(m => m.Starttime.Year == gameDatePicker.SelectedDate.Value.Year
                 && m.Starttime.Month == gameDatePicker.SelectedDate.Value.Month
                 && m.Starttime.Day == gameDatePicker.SelectedDate.Value.Day);
        }

        private void OpenDetails(object sender, RoutedEventArgs e)
        {
            var senderControl = (Control)sender;
            FrameManager.OpenPage(new MatchupDetailPage((Matchup)senderControl.DataContext));
        }
    }
}
