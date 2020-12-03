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
    /// Логика взаимодействия для PlayersMainPage.xaml
    /// </summary>
    public partial class PlayersMainPage : Page
    {
        private const int MAX_PAGE_SIZE = 4; 
        /// <summary>
        /// Содержит коллекцию FilteredEnumerable.
        /// </summary>
        private readonly PagedEnumerable<PlayerInTeam> pagedFilteredPlayers;
        private readonly RadioForegroundControlHighlighter buttonHighlighter = 
            new RadioForegroundControlHighlighter(Brushes.Yellow, Brushes.White);
        public int PageCount => pagedFilteredPlayers.PageCount;
        public int ItemsCount => ((FilteredEnumerable<PlayerInTeam>)pagedFilteredPlayers
            .Original).Original.Count();
        public int ItemsOnPageCount => pagedFilteredPlayers.Filtered.Count();
        public int CurrentPageNum
        {
            get => pagedFilteredPlayers.CurrentPageNum;
            set
            {
                pagedFilteredPlayers.CurrentPageNum = value;
                UpdateBindings();
            }
        }
        public IEnumerable<PlayerInTeam> PlayersOnPage => pagedFilteredPlayers.Filtered;
        public PlayersMainPage()
        {
            InitializeComponent();
            var filteredPlayers = new FilteredEnumerable<PlayerInTeam>(NBAContext.Instance.PlayerInTeam.ToList());
            pagedFilteredPlayers = new PagedEnumerable<PlayerInTeam>(filteredPlayers, MAX_PAGE_SIZE);
            seasonComboBox.ItemsSource = NBAContext.Instance.Season.ToList();
            teamComboBox.ItemsSource = NBAContext.Instance.Team.OrderBy(team => team.TeamName).ToList();
            DataContext = this;
        }
        private void UpdateBindings()
        {
            playersDataGrid.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
            pageCountTextBlock.GetBindingExpression(TextBlock.TextProperty).UpdateSource();
            pageIndexTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            pageCountTextBlock.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            itemsOnPageCountTextBlock.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            itemsCountTextBlock.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
        }

        private void AllLettersButton_Click(object sender, RoutedEventArgs e)
        {
            var senderButton = (Button)sender;
            buttonHighlighter.HighlightControl(senderButton);
            SetFilter("InitialsFilter", new NullObjectFilter<PlayerInTeam>());
        }

        private void LetterButton_Click(object sender, RoutedEventArgs e)
        {
            var senderButton = (Button)sender;
            buttonHighlighter.HighlightControl(senderButton);
            SetFilter("InitialsFilter", new InitialsFilter(senderButton.Content.ToString()[0]));
        }

        private void priviusPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageNum--;
        }

        private void firstPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageNum = 0;
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageNum++;
        }

        private void lastPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageNum = PageCount;
        }

        private void SetFilter(string key, IFilter<PlayerInTeam> filter)
        {
            ((FilteredEnumerable<PlayerInTeam>)pagedFilteredPlayers.Original).SetFilter(key, filter);
            CurrentPageNum = 1;
            UpdateBindings();
        }


        private void seasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            SetFilter("Season", new SeasonParticipantFilter<PlayerInTeam>((Season)comboBox.SelectedItem));
        }

        private void teamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            SetFilter("Team", new PlayerTeamFilter((Team)comboBox.SelectedItem));
        }

        private void playerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilter("Name", new StringContainsFilter<PlayerInTeam>(playerNameTextBox.Text, item => item.Player.FullName));
        }

        private void playersDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedItem = playersDataGrid.SelectedItem;
            if(selectedItem == null)
                return;

            var selectedPlayer = ((PlayerInTeam)selectedItem).Player;
            NavigationManager.OpenPage(new PlayerDetail(selectedPlayer));
        }
    }
}
