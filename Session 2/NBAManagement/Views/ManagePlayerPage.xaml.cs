using NBAManagement.Filter;
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
    /// Логика взаимодействия для ManagePlayerPage.xaml
    /// </summary>
    public partial class ManagePlayerPage : Page
    {
        public ManagePlayerPage()
        {
            InitializeComponent();
            countryComboBox.ItemsSource = WithAllStringFirst(NBAContext.Instance.Country.ToList());
            postionComboBox.ItemsSource = WithAllStringFirst(NBAContext.Instance.PositionName.ToList());
            countryComboBox.SelectedIndex = 0;
            postionComboBox.SelectedIndex = 0;
            UpdateTable();
        }

        private void UpdateTable()
        {
            IEnumerable<Player> players = NBAContext.Instance.Player.ToList();
            if(postionComboBox.SelectedIndex != 0)
            {
                players = players.Where(p => p.MainPositionName == postionComboBox.SelectedItem);
            }
            if(countryComboBox.SelectedIndex != 0)
            {
                players = players.Where(p => p.Country == countryComboBox.SelectedItem);
            }

            players = new StringContainsFilter<Player>(nameTextBox.Text, p => p.FullName).Use(players);
            countTextBlock.Text = "Total players: " +  players.Count().ToString();
            playerDataGrid.ItemsSource = players;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private IEnumerable<object> WithAllStringFirst(IEnumerable<object> collection)
        {
            var result = new List<object>();
            result.Add("All");
            result.AddRange(collection);
            return result;
        }
    }
}
