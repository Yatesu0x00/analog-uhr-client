﻿using System;
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

namespace AnalogUhrClient
{
    class Uhr
    {
        Ellipse eli;
        Zeiger secZeiger, minZeiger, stdZeiger;
        public Double X, Y;

        public Uhr(Canvas _cvs, double X = 80, double Y = 80, double radius = 40)
        {
            this.X = X;
            this.Y = Y;

            eli = new Ellipse();

            eli.Width = 2 * radius;
            eli.Height = 2 * radius;

            eli.StrokeThickness = 3;
            eli.Fill = Brushes.Gainsboro;
            eli.Stroke = Brushes.Black;

            Canvas.SetLeft(eli, X - radius);
            Canvas.SetTop(eli, Y - radius);

            secZeiger = new Zeiger(eli, 0, 2, Brushes.Black);
            secZeiger.draw(_cvs);
            minZeiger = new Zeiger(eli, 15, 4, Brushes.Red);
            minZeiger.draw(_cvs);
            stdZeiger = new Zeiger(eli, 30, 6, Brushes.Blue);
            stdZeiger.draw(_cvs);
        }

        public void UpdateClock(string time)
        {
            String[] hms = time.Split(new char[] { ' ' })[0].Split(':');

            double h = Convert.ToDouble(hms[0]);
            double m = Convert.ToDouble(hms[1]);
            double s = Convert.ToDouble(hms[2]);

            secZeiger.updateZeiger(Math.PI * (s / 30)); //2 * Math.PI * (h + m/ 60) / 12
            minZeiger.updateZeiger(Math.PI * (m / 30)); //2 * Math.PI * (m + s/ 60) / 60
            stdZeiger.updateZeiger(Math.PI * (h / 6)); //2 * Math.PI * s / 60
        }

        public void draw(Canvas c)
        {
            if (!c.Children.Contains(eli))
            {
                c.Children.Add(eli);
            }
        }

        public void undraw(Canvas c)
        {
            if (c.Children.Contains(eli))
            {
                c.Children.Remove(eli);
            }
        }

        public void resize(double sx, double sy)
        {
            eli.Width *= (sx + sy) / 2;
            eli.Height *= (sx + sy) / 2;

            Canvas.SetLeft(eli, sx * Canvas.GetLeft(eli));
            Canvas.SetTop(eli, sy * Canvas.GetTop(eli));

            secZeiger.resize(Canvas.GetLeft(eli) + eli.Width / 2, Canvas.GetTop(eli) + eli.Width / 2, eli.Width / 2, 0);
            minZeiger.resize(Canvas.GetLeft(eli) + eli.Width / 2, Canvas.GetTop(eli) + eli.Width / 2, eli.Width / 2, 15);
            stdZeiger.resize(Canvas.GetLeft(eli) + eli.Width / 2, Canvas.GetTop(eli) + eli.Width / 2, eli.Width / 2, 30);
        }
    }
}
