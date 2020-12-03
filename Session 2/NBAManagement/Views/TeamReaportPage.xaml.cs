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

namespace NBAManagement.Views
{
    /// <summary>
    /// Логика взаимодействия для TeamReaportPage.xaml
    /// </summary>
    public partial class TeamReaportPage : Page
    {
        private const int PAGE_SIZE = 20;
        public PagedEnumerable<TeamReport> PagedTeamReprots { get; set; }
        public int PageNum
        {
            get => PagedTeamReprots.CurrentPageNum;
            set
            {
                PagedTeamReprots.CurrentPageNum = value;
                UpdateBindings();
            }
        }
        private MatchupTypeEnum SelectedMatchupType
        {
            get
            {
                switch(matchupTypComboBox.SelectedItem)
                {
                case "Preseason":      return MatchupTypeEnum.Preseason;
                case "Regular Season": return MatchupTypeEnum.Regular;
                case "Post Season":    return MatchupTypeEnum.Post;
                default:               throw new FormatException();
                }
            }
        }
        private Func<TeamReport, double> SelectedGetRankiedField
        {
            get
            {
                switch(rankedByComboBox.SelectedItem)
                {
                case "Points":    return ts => ts.Points;
                case "Rebounds":  return ts => ts.Rebounds;
                case "Assists":   return ts => ts.Assists;
                case "Steals":    return ts => ts.Steals;
                case "Blocks":    return ts => ts.Blocks;
                case "Turnovers": return ts => ts.Turnovers;
                case "Fouls":     return ts => ts.Fouls;
                default: throw new FormatException();
                }
            }
        }

        public TeamReaportPage()
        {
            InitializeComponent();
            matchupTypComboBox.ItemsSource = new string[]
            {
                "Preseason",
                "Regular Season",
                "Post Season"
            };
            matchupTypComboBox.SelectedIndex = 0;
            rankedByComboBox.ItemsSource = new string[]
            {
                "Points",
                "Rebounds",
                "Assists",
                "Steals",
                "Blocks",
                "Turnovers",
                "Fouls",
            };
            rankedByComboBox.SelectedIndex = 0;

            viewByComboBox.ItemsSource = new string[]
            {
                "Avarege",
                "Total"
            };
            viewByComboBox.SelectedIndex = 0;
            UpdateTable();
            DataContext = this;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void UpdateTable()
        {
            var teams = NBAContext.Instance.Team.ToList();
            switch(viewByComboBox.SelectedItem)
            {
            case "Avarege":
                PagedTeamReprots = new PagedEnumerable<TeamReport>(TeamReport.CreateAvarange(teams, SelectedMatchupType, SelectedGetRankiedField), PAGE_SIZE);
                break;
            case "Total":
                PagedTeamReprots = new PagedEnumerable<TeamReport>(TeamReport.CreateTotal(teams, SelectedMatchupType, SelectedGetRankiedField), PAGE_SIZE);
                break;
            }
            UpdateBindings();
        }

        private void UpdateBindings()
        {
            var dataContext = DataContext;
            DataContext = null;
            DataContext = dataContext;
        }

        private void firstPage_Click(object sender, RoutedEventArgs e)
        {
            PageNum = 1;
        }

        private void priviusPage_Click(object sender, RoutedEventArgs e)
        {
            PageNum--;
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            PageNum++;
        }

        private void lastPage_Click(object sender, RoutedEventArgs e)
        {
            PageNum = PagedTeamReprots.PageCount;
        }
    }
}
