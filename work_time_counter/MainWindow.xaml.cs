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


namespace work_time_counter
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, DispatcherTimer> list = new Dictionary<string, DispatcherTimer>();
        public int count = 0;
        public bool start = false;
        public DispatcherTimer dis_1;

        public MainWindow()
        {
            InitializeComponent();
            GetTodayTime();
        }

        // https://stackoverflow.com/questions/16748371/how-to-make-a-wpf-countdown-timer
        // https://docs.microsoft.com/ja-jp/dotnet/api/system.windows.threading.dispatchertimer?view=netframework-4.7.2
        public DispatcherTimer mkDisTime()
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            return dispatcherTimer;
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
            Console.WriteLine(e);
        }


        /// <summary>
        /// get time now event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodayTime_Tick(object sender, EventArgs e)
        {
            clock.Content = DateTime.Now.ToString();
        }
        /// <summary>
        /// get time now
        /// </summary>
        void GetTodayTime()
        {
            clock.Content = DateTime.Now.ToString();
            var dis = mkDisTime();
            dis.Tick += new EventHandler(TodayTime_Tick);
            dis.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            start = !start;

            if (start)
            {
                dis_1 = mkDisTime();
                dis_1.Tick += new EventHandler(TimeCounter);
                ell1.Fill = Brushes.Red;
                dis_1.Start();
            }
            else
            {
                System.Diagnostics.Debug.Assert(dis_1 != null);
                ell1.Fill = Brushes.Green;
                dis_1.Stop();
            }
        }
        private void TimeCounter(object sender, EventArgs e)
        {
            tsk1.Content = new TimeSpan(0, 0, ++count);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            count = 0;
            tsk1.Content = "00:00:00";
            if (!start && dis_1 != null)
            {
                ell1.Fill = Brushes.Black;
                dis_1.Stop();
            }
        }
    }
}
