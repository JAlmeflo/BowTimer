using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace BowTimer
{
    public partial class TimerPage : PhoneApplicationPage
    {
        bool setupTime;
        int min, sec;
        System.Windows.Threading.DispatcherTimer timer;

        public TimerPage()
        {
            InitializeComponent();

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);

            min = 0;
            sec = 0;
            DisplayTimerText(min, sec);
            setupTime = true;

            ChangeLightColor(255, 0, 0);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            // Tick down timer
            sec--;
            if (sec >= 60)
            {
                min++;
                sec = 0;
            }
            if (sec < 0)
            {
                min--;
                sec = 59;
            }
            DisplayTimerText(min, sec);

            // Check if we need to stop timer
            if (min <= 0 && sec <= 0)
            {
                if (setupTime)
                {
                    setupTime = false;
                    min = 2;
                    sec = 0;
                    DisplayTimerText(min, sec);
                }
                else
                {
                    StopTimer();
                    return;
                }
            }

            // Change light color
            if (min <= 0 && sec <= 30)
            {
                ChangeLightColor(255, 255, 0);
            }
            else
            {
                ChangeLightColor(0, 255, 0);
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }

        private void DisplayTimerText(int p_min, int p_sec)
        {
            string secText = p_sec.ToString();
            if (p_sec < 10)
            {
                secText = "0" + secText;
            }

            TimerText.Text = p_min.ToString() + ":" + secText;
        }

        private void StartTimer()
        {
            // Reset time
            min = 0;
            sec = 30;
            DisplayTimerText(min, sec);

            PlayButton.Content = "Stop";

            timer.Start();

            ChangeLightColor(255, 255, 0);
        }

        private void StopTimer()
        {
            // Reset time
            min = 0;
            sec = 0;
            DisplayTimerText(min, sec);

            PlayButton.Content = "Start";
            timer.Stop();

            ChangeLightColor(255, 0, 0);

            setupTime = true;
        }

        private void ChangeLightColor(byte p_r, byte p_g, byte p_b)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, p_r, p_g, p_b);

            LightRectangle.Fill = brush;
        }
    }
}