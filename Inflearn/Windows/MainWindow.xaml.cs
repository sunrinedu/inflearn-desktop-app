using InflearnMirror;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Inflearn.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegisterElements();
            RegisterEvents();
        }

        private void RegisterElements()
        {

        }

        private void RegisterEvents()
        {
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCourses();
        }

        private async void LoadCourses()
        {
            var data = await API.GetListAsync();
            foreach (string courseName in data)
            {
                JObject info;
                if (App.courseCache.ContainsKey(courseName))
                    info = App.courseCache[courseName];
                else
                {
                    info = await API.GetInfoAsync(courseName);
                    App.courseCache[courseName] = info;
                }
                ListViewItem item = new ListViewItem();
                item.MouseDoubleClick += Item_MouseDoubleClick;
                item.Content = info["title"].ToString();
                item.DataContext = courseName;
                listView.Items.Add(item);
            }
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CourseWindow(((ListViewItem)sender).DataContext as string).ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://docs.google.com/spreadsheets/d/1rB__bUiP3E50hDdjGtwy3HXgfY0BYfKbEIsioY-BGNw/edit",
                UseShellExecute = true
            });
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://3-159laura.pulsedmedia.com/public-server/",
                UseShellExecute = true
            });
        }
    }
}
