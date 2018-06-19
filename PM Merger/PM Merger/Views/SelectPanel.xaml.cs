using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PM_Merger;
using PM_Merger.Controls;
using static PM_Merger.Models.Select_Models;

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
            Article_Listbox.ItemsSource = s_control.FolderReadOut();
        }

        private void Options_btn_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(editPanel = new EditPanel(grid));
        }

        private void OpenFoldder(object sender, RoutedEventArgs e)
        {
            s_control.OpenFolderDialog_URL();
            Article_Listbox.ItemsSource = s_control.FolderReadOut();
        }

        private void Merge_btn_Click(object sender, RoutedEventArgs e)
        {
            Object lb_item;
            List<string> Urls = new List<string>();
            foreach (var item in Article_Listbox.Items)
            {
                lb_item = item as object;
                Article_Struct structed_item = lb_item as Article_Struct;
                if (structed_item.Checked)
                    Urls.Add(structed_item.path);
            }
            try
            {
                s_control.PDF_Merger(Urls, Printed_Name_tbx.Text);
                Printed_Name_tbx.Text = "";
            }
            catch (Exception)
            {
                //MessageBox.Show("Sikertelen művelet!\n\n" + exc);
            }
        }

        private void OpenInExplorer(object sender, RoutedEventArgs e)
        {
            try
            {
                s_control.OpenExplorer();
            }
            catch (Exception)
            {
            }
        }
        private void TextBox_PlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string Tbx_name = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBlock)this.FindName(Tbx_name);

            if (((TextBox)sender).Text == "")
                Tbx.Visibility = Visibility.Hidden;
        }
        private void TextBox_PlaceHolder_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string Tbx_name = ((TextBox)sender).Tag.ToString();
            var Tbx = (TextBlock)this.FindName(Tbx_name);

            if (((TextBox)sender).Text == "")
            {
                Tbx.Visibility = Visibility.Visible;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            Article_Struct item = mi.DataContext as Article_Struct;
            s_control.OpenPDF(item.path);
        }
    }
}
