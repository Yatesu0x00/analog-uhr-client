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

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer countDown;
        int count = 3, ticks, ticks_old, ticks_new;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            countDown = new DispatcherTimer();
            countDown.Tick += new EventHandler(countDown_Tick);
            countDown.Interval = new TimeSpan(0, 0, 0, 1, 0);          
        }

        private void countDown_Tick(object sender, EventArgs e)
        {
            ticks = Environment.TickCount;

            count++;

            if (count <= 10)
            {
                lbZahl.Content = count.ToString();
            }

            ticks_old = ticks;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            countDown.Start();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            countDown.Stop();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            lbZahl.Content = 3;
            count = 3;
            
            countDown.Start();
            countDown.Stop();
        }
    }
}
