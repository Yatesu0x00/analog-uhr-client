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


namespace AnalogUhr
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Uhr uhr;
        DispatcherTimer timer;
        Double ticks_old;
        Double ms;
        Double T_Sec;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);

            uhr = new Uhr(C, C.ActualWidth/2, C.ActualHeight/2, 200);
            uhr.draw(C);

            ticks_old = Environment.TickCount;
           
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Double ticks = Environment.TickCount;
            
            SecBerechnung(ticks);
            
            ticks_old = ticks;        
        }

        private void SecBerechnung(double _ticks)
        {
            ms += (_ticks - ticks_old);

            if(ms >= 1000)
            {
                T_Sec++;                
                uhr.updateUhr(T_Sec);
                ms = 0;
            }
        }

        private void C_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Double sx = e.NewSize.Width / e.PreviousSize.Width;
                Double sy = e.NewSize.Height / e.PreviousSize.Height;

                uhr.resize(sx, sy);
            }
            catch { }
        }
    }
}

