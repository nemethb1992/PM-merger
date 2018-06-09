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
using PM_Merger;
using PM_Merger.Controls;

namespace PM_Merger.Views
{
    /// <summary>
    /// Interaction logic for SelectPanel.xaml
    /// </summary>
    public partial class SelectPanel : UserControl
    {
        private Grid grid;
        Select_Control s_control = new Select_Control();
        private EditPanel editPanel;
        public SelectPanel(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            Article_Listbox.ItemsSource = s_control.Article_Datasource();
        }

        private void Options_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(editPanel = new EditPanel(grid));
        }

        //private void Exit_Application(object sender, RoutedEventArgs e)
        //{
        //    Environment.Exit(0);
        //}
    }
}
