using InflearnMirror;
using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace Inflearn.Windows
{
    /// <summary>
    /// CourseWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CourseWindow : Window
    {
        private string courseName;
        private JObject info;
        private JObject map;
        public CourseWindow(string courseName)
        {
            InitializeComponent();
            RegisterElements();
            RegisterEvents();
            (this.courseName) = (courseName);
        }

        private void RegisterElements()
        {

        }

        private void RegisterEvents()
        {
            this.Loaded += CourseWindow_Loaded; ;
        }

        private async void CourseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            map = await API.GetMapAsync(courseName);
            if (App.courseCache.ContainsKey(courseName))
                info = App.courseCache[courseName];
            else
            {
                info = await API.GetInfoAsync(courseName);
                App.courseCache[courseName] = info;
            }
            this.Title = info["title"].ToString();
            foreach (var lecture in info["_"]["curriculum"])
            {
                ListViewItem item = new ListViewItem();
                if (lecture["type"].ToString() == "section")
                    item.Content = new Components.Section(lecture["title"].ToString()) { HorizontalAlignment = HorizontalAlignment.Stretch };
                else
                    item.Content = lecture["title"].ToString();
                item.DataContext = lecture;
                item.HorizontalAlignment = HorizontalAlignment.Stretch;
                if (lecture["type"].ToString() == "lecture")
                    item.MouseDoubleClick += Item_MouseDoubleClick;
                curriculum.Items.Add(item);
            }
        }

        private async void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem me = (ListViewItem)sender;
            JObject lecture = (JObject)me.DataContext;
            JToken mapdata = map[lecture["id"].ToString()];
            string filename = mapdata["filename"].ToString();
            string url = API.GetVideoLocationAsync(courseName, filename);
            player.mediaMain.Source = new Uri(url);
            player.mediaMain.Play();
            string html = await API.GetHtmlAsync(courseName, filename);
            if (!string.IsNullOrWhiteSpace(html))
                contentBrower.NavigateToString(html);
            else
                contentBrower.Navigate((Uri)null);
        }
    }
}
