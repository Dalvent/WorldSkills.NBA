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
using Excel = Microsoft.Office.Interop.Excel;

namespace NBAManagement.Views
{
    /// <summary>
    /// Логика взаимодействия для ManageMachupsPage.xaml
    /// </summary>
    public partial class ManageMachupsPage : Page
    {
        public ManageMachupsPage()
        {
            InitializeComponent();
            seasonFilterComboBox.ItemsSource = NBAContext.Instance.Season.ToList();
            seasonFilterComboBox.SelectedIndex = 0;
            UpdateTables();
        }

        private void updateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Update Matchup – Future Add-on",
                "The feature would be a future add - on to the current system.",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void deleteEntityButton_Click(object sender, RoutedEventArgs e)
        {
            var senderDataContext = (Matchup)((Button)sender).DataContext;
            try
            {
                NBAContext.Instance.Matchup.Remove(senderDataContext);
                NBAContext.Instance.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateTables();
        }

        private void addNewMatchup_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.OpenPage(new AddNewMatchupForRegularSeasonPage());
        }

        private void exportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(preseasonDataGrid);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTables();
        }

        private void UpdateTables()
        {
            var matchups = NBAContext.Instance.Matchup
                .ToList()
                .Where(m => m.Season == seasonFilterComboBox.SelectedItem);

            if(currentDataFilterCheckBox.IsChecked == true)
            {
                matchups = matchups.Where(m => m.Starttime == currentDateFilterDatePicker.DisplayDate)
                    .ToList();
            }

            preseasonDataGrid.ItemsSource = matchups;
            regularDataGrid.ItemsSource = matchups.Where(m => m.MatchupTypeEnum == MatchupTypeEnum.Regular);
        }
    
        private void ExportToExcel(DataGrid dataGrid)
        {
            var excel = new Excel.Application();
            var book = excel.Workbooks.Add(Type.Missing);
            var sheet = (Excel.Worksheet)excel.Worksheets[1];
            int columnIndex = 1;
            foreach(var columnHeader in dataGrid.Columns.Select(c => c.Header))
            {
                if(dataGrid.Columns.Count - 1 <= columnIndex)
                    break;
                
                sheet.Cells[1, columnIndex++] = columnHeader;
            }

            int rowIndex = 2;
            foreach(Matchup matchup in preseasonDataGrid.ItemsSource)
            {sheet.Cells[rowIndex, 1] = matchup.Starttime.ToString("yyyy/MM/dd");
                sheet.Cells[rowIndex, 2] = matchup.TeamAway.TeamName;
                sheet.Cells[rowIndex, 3] = matchup.TeamHome.TeamName;
                sheet.Cells[rowIndex, 4] = matchup.Starttime.ToString("hh:mm");
                sheet.Cells[rowIndex, 5] = matchup.Location;
                sheet.Cells[rowIndex, 6] = matchup.IsFinished ? "Yes" : "No";
                rowIndex++;
            }
            
            excel.Visible = true;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                NBAContext.Instance.ChangeTracker.Entries<Matchup>().ToList().ForEach(entity => entity.Reload());
                NBAContext.Instance.ChangeTracker.Entries<MatchupDetail>().ToList().ForEach(entity => entity.Reload());
                NBAContext.Instance.ChangeTracker.Entries<MatchupLog>().ToList().ForEach(entity => entity.Reload());
                UpdateTables();
            }
        }
    }
}
