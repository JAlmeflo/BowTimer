using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BowTimer.Resources;

namespace BowTimer
{
    public partial class MainPage : PhoneApplicationPage
    {
        int sec;
        int min;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            min = 0;
            sec = 0;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            sec++;
            if (sec >= 60)
            {
                min++;
                sec = 0;
            }

            string secText = sec.ToString();
            if (sec < 10)
            {
                secText = "0" + secText;
            }

            //TimerText.Text = min.ToString() + ":" + secText;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TimerPage.xaml", UriKind.Relative));
        }
    }
}