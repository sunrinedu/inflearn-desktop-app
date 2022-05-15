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
using System.Windows.Threading;

namespace Inflearn.Components
{
    /// <summary>
    /// Player.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer() {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick; ;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaMain.Source == null || !mediaMain.NaturalDuration.HasTimeSpan) {
                lblPlayTime.Content = "00:00:00 / 00:00:00"; return;
            }
            sldrPlayTime.Value = mediaMain.Position.TotalSeconds;
        }

        private void mediaMain_MediaOpened(object sender, RoutedEventArgs e)
        {
            sldrPlayTime.Minimum = 0;
            sldrPlayTime.Maximum = mediaMain.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void mediaMain_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaMain.Stop();
        }

        private void mediaMain_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //MessageBox.Show("동영상 재생 실패 : " + e.ErrorException.Message.ToString());
        }

        private void sldrPlayTime_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            mediaMain.Pause();
        }

        private void sldrPlayTime_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mediaMain.Position = TimeSpan.FromSeconds(sldrPlayTime.Value);
        }

        private void sldrPlayTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaMain.Source == null) return;
            lblPlayTime.Content = String.Format("{0} / {1}", mediaMain.Position.ToString(@"mm\:ss"), mediaMain.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (mediaMain.Source == null) return;
            mediaMain.Play();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (mediaMain.Source == null) return;
            mediaMain.Stop();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (mediaMain.Source == null) return;
            mediaMain.Pause();
        }

        private void sldrVolume_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {

        }

        private void sldrVolume_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mediaMain.Volume = sldrPlayTime.Value;
        }
    }
}
