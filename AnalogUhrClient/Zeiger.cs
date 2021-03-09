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

namespace AnalogUhrClient
{
    class Zeiger
    {
        Line li;
        public double Length { get; set; }
        public double Angle { get; set; }
        public int StrokeThic;
        public Brush Brush;

        public Zeiger(Ellipse eli, int length, int strokeThic, Brush brush)
        {
            Brush = brush;
            StrokeThic = strokeThic;
            //angle = 0;

            li = new Line();

            li.Stroke = brush;
            li.StrokeThickness = strokeThic;

            li.X1 = Canvas.GetLeft(eli) + eli.Width / 2;
            li.Y1 = Canvas.GetTop(eli) + eli.Height / 2;

            this.Length = eli.Width / 2 - length;
            li.X2 = li.X1;
            li.Y2 = li.Y1 - this.Length;

            Panel.SetZIndex(li, 1);
        }

        public void updateZeiger(double angle)
        {
             Angle = angle;

            li.X2 = li.X1 + Length * Math.Sin(angle);
            li.Y2 = li.Y1 - Length * Math.Cos(angle);
        }

        public void draw(Canvas c)
        {
            if (!c.Children.Contains(li))
            {
                c.Children.Add(li);
            }
        }

        public void undraw(Canvas c)
        {
            if (c.Children.Contains(li))
            {
                c.Children.Remove(li);
            }
        }

        public void resize(double sx, double sy, double _length, double newLength)
        {
            Length = _length - newLength;
            li.X1 = sx;
            li.Y1 = sy;

            li.X2 = li.X1 + Length * Math.Sin(Angle);
            li.Y2 = li.Y1 - Length * Math.Cos(Angle);

            li.Width *= ((sx + sy) / 2);
            li.Height *= ((sx + sy) / 2);

            Canvas.SetTop(li, sy * Canvas.GetTop(li));
            Canvas.SetLeft(li, sx * Canvas.GetLeft(li));
        }
    }
}
