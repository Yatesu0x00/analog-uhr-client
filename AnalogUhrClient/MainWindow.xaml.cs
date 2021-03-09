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
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using System.Threading;

namespace AnalogUhrClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Uhr uhr;
        myDialog dlg;
        Socket s;
        IPAddress host;
        Thread thread_1;
        private bool connectStatus = false;
        byte[] bytes;

        public MainWindow()
        {
            InitializeComponent();

            C.Background = Brushes.Gainsboro;
            disconnect.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uhr = new Uhr(C, 580, C.ActualHeight / 2, 140);
            uhr.draw(C);
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

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            dlg = new myDialog();
            dlg.ShowDialog();

            if(dlg.DialogResult == true)
            {
                disconnect.IsEnabled = true;
                connect.IsEnabled = false;
                connectStatus = true;

                host = IPAddress.Parse(dlg.ipAddress);
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(host, dlg.port);
                thread_1 = new Thread(ThreadFunction);
                thread_1.Start();
            }
        }

        private void ThreadFunction()
        {
            string request = "send_time";

            while (connectStatus == true)
            {
                bytes = new byte[99];

                s.Send(Encoding.ASCII.GetBytes(request));

                s.Receive(bytes);
                string time = Encoding.ASCII.GetString(bytes);

                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    lbTime.Content = " " + time;
                    uhr.UpdateClock(time);
                }));

                Thread.Sleep(50);
            }
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            connectStatus = false;
            s.Close();
            connect.IsEnabled = true;
            disconnect.IsEnabled = false;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(thread_1 != null)
                {
                    thread_1.Abort();
                    s.Close();
                }
                Environment.Exit(0);
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (thread_1 != null)
                {
                    thread_1.Abort();
                    s.Close();
                }
                Environment.Exit(0);
            }
            catch { }
        }
    }
}