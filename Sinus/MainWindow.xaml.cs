using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
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

        private TwoColorVerticalLine function; 
        private Point mousePos;
        private Krutilka krutilka;
        private PlotModel model = new PlotModel() { Title = "Plot"};


        public MainWindow()
        {
            InitializeComponent();
            krutilka = new Krutilka(new Ellipse() { 
                Margin = new Thickness(500, 300, 0, 0), 
                Stroke = Brushes.Black, 
                Width = 200, Height = 200 }, 
                new Button() { Width = 50, Height = 50 });

            krutilka.SetButtonStyle(FindResource("RoundedButton") as Style);

            krutilka.AddToForm(canvas);

            function = new TwoColorVerticalLine() {Color = OxyColors.Black};
            

            model.Series.Add(function);
            function.Limit = 0;


            model.Annotations.Add(new LineAnnotation() { X = 0, Y = 0 , StrokeThickness = 2, Color = OxyColors.Black, Type = LineAnnotationType.Vertical});
            model.Annotations.Add(new LineAnnotation() { X = 0, Y = 0 , StrokeThickness = 2, Color = OxyColors.Black, Type = LineAnnotationType.Horizontal});




            mWindow.PreviewMouseMove += UpdateMove;

            mWindow.Loaded += Update;
        }





        private void UpdateMove(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(MainWindow))
                mousePos = e.GetPosition((IInputElement)sender);




            if (krutilka.IsCaptured)
            {
                krutilka.Update();

                var x = krutilka.ActualAngle.ConvertToRadians();
                var y = Math.Sin(krutilka.ActualAngle.ConvertToRadians());


                function.Points.Add(new (x,y));

                
                plot.Model = model;
                
                plot.InvalidatePlot();


            }
            



        }

        private void ClearGraph(object sender, RoutedEventArgs args)
        {
            plot.Model = new PlotModel();
            model = new PlotModel();
            function = new TwoColorVerticalLine();
            
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