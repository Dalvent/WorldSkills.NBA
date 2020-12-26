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

namespace NBAManagement.Controlls
{
    /// <summary>
    /// Логика взаимодействия для LineupListControl.xaml
    /// </summary>
    public partial class LineupListControl : UserControl
    {
        public static DependencyProperty LineupNameProperty = 
            DependencyProperty.Register("LineupName",
                typeof(string),
                typeof(LineupListControl),
                new PropertyMetadata(String.Empty));

        public static DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource",
                typeof(IEnumerable<object>),
                typeof(LineupListControl),
                new PropertyMetadata(new List<object>()));

        public string LineupName
        {
            get => (string)GetValue(LineupNameProperty);
            set
            {
                SetValue(LineupNameProperty, value);
            }
        }
        public IEnumerable<object> ItemSource
        {
            get => (IEnumerable<object>)GetValue(ItemSourceProperty);
            set
            {
                SetValue(ItemSourceProperty, value);

            }
        }
        public LineupListControl()
        {
            InitializeComponent();
        }
    }
}
