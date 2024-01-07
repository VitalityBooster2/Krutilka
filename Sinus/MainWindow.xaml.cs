using ScottPlot.Plottable;
using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ellipse = System.Windows.Shapes.Ellipse;
namespace Sinus
{



    public partial class MainWindow : Window
    {




        Point mousePos;
        Krutilka krutilka;

        public MainWindow()
        {
            InitializeComponent();
            krutilka = new Krutilka(new Ellipse() { Margin = new Thickness(500, 300, 0, 0), Stroke = Brushes.Black, Width = 200, Height = 200 }, new Button() { Width = 50, Height = 50 });
            krutilka.SetButtonStyle(FindResource("RoundedButton") as Style);
            krutilka.AddToForm(canvas);

            plot.Plot.AddHorizontalLine(0);
            plot.Plot.AddVerticalLine(0);

            







            mWindow.PreviewMouseMove += UpdateMove;

            mWindow.Loaded += Update;
        }





        private void UpdateMove(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(MainWindow))
                mousePos = e.GetPosition((IInputElement)sender);


            plot.Plot.Add(new MarkerPlot() { X = 0, Y = Math.Sin(0), Color = System.Drawing.Color.Red });

            if (krutilka.IsCaptured)
            {

                krutilka.Update();
                var x = krutilka.ActualAngle.ConvertToRadians();
                var y = Math.Sin(krutilka.ActualAngle.ConvertToRadians());

                if (x > 0)

                    plot.Plot.Add(new MarkerPlot() { X = x, Y = y, Color = System.Drawing.Color.Green, MarkerSize = 2 });
                else plot.Plot.Add(new MarkerPlot() { X = x, Y = y, Color = System.Drawing.Color.Blue, MarkerSize = 2 });


            }


            plot.Refresh();

        }

        private void ClearGraph(object sender, RoutedEventArgs args)
        {
            plot.Plot.Clear();
        }

        private void Update(object sender, RoutedEventArgs e)
        {

            {
                Task.Run(() =>
                {


                    while (true)
                    {
                        Mouse.MouseDirection = new Vector2(0, 0);
                        Mouse.Update(mousePos);



                    }

                });


            };
        }
    }
}