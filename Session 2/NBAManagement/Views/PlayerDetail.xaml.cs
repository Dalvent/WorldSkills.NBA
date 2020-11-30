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

namespace NBAManagement.Views
{
    /// <summary>
    /// Логика взаимодействия для PlayerDetail.xaml
    /// </summary>
    public partial class PlayerDetail : Page
    {
        public const string DATE_INTERVAL = "DATE_INTERVAL";

        private readonly RadioBackgroundControlHighlighter buttonHighlighter;
        private readonly FilteredEnumerable<PlayerStatistics> playerStatistics;
        public PlayerDetail(Player player)
        {
            InitializeComponent();
            DataContext = player;
            buttonHighlighter = new RadioBackgroundControlHighlighter(
                (SolidColorBrush)FindResource("buttonStandartColor"), 
                (SolidColorBrush)FindResource("buttonHighlightColor")
            );


            var startDate = DateTime.Now.AddYears(-10);
            var endDate = DateTime.Now;
            startDatePicker.SelectedDate = startDate;
            endDatePicker.SelectedDate = endDate;
            var sortedPlayerStatistics = new SortedSet<PlayerStatistics>(player.PlayerStatistics);
            playerStatistics = new FilteredEnumerable<PlayerStatistics>(sortedPlayerStatistics);
            playerStatistics.SetFilter(DATE_INTERVAL, new PlayerStatisticDateIntervalFilter(startDate, endDate));

            var chartArea = new ChartArea("Main");
            chartArea.AxisX.Interval = 1;
            statisticChart.ChartAreas.Add(chartArea);
            var currentSeries = new Series();
            currentSeries.ChartType = SeriesChartType.Line;
            currentSeries.BorderWidth = 3;
            statisticChart.Series.Add(currentSeries);

            SwitchDataOnChart(pointsButton, new RoutedEventArgs());
        }

        private void SwitchDataOnChart(object sender, RoutedEventArgs e)
        {
            var senderButton = (Button)sender;
            buttonHighlighter.HighlightControl(senderButton);
            switch(senderButton.Name)
            {
            case "pointsButton":
                UpdateChart(s => s.Point);
                break;
            case "reboundsButton":
                UpdateChart(s => s.Rebound);
                break;
            case "assistsButton":
                UpdateChart(s => s.Assist);
                break;
            case "stealsButton":
                UpdateChart(s => s.Steal);
                break;
            case "blocksButton":
                UpdateChart(s => s.Block);
                break;
            default:
                throw new NotImplementedException();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var startDate = startDatePicker.SelectedDate;
            var endDate = endDatePicker.SelectedDate;

            if(startDate == null || endDate == null)
            {
                MessageBox.Show("Промежуток указан в неверном формате.");
                return;
            }

            playerStatistics.SetFilter(DATE_INTERVAL, 
                new PlayerStatisticDateIntervalFilter(startDate.Value, endDate.Value));
            SwitchDataOnChart(buttonHighlighter.Highlighted, new RoutedEventArgs());
        }

        private void UpdateChart(Func<PlayerStatistics, int> getField)
        {
            var currentSiries = statisticChart.Series.FirstOrDefault();
            currentSiries.Points.Clear();

            foreach(var statistic in playerStatistics)
            {
                currentSiries.Points.AddXY(statistic.Matchup.Starttime.ToString(@"M/d"), getField(statistic));
            }
        }
    }
}
