using System;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;



namespace Sinus
{


    public class Krutilka
    {

        #region Fields

        private Label text;

        private double lastAngle;

        private double actualAngle = 0;

        private Button button;

        private Ellipse circle;

        private double angle { get; set; }

        public bool IsCaptured => button.IsMouseCaptured;

        #endregion


        #region Prosps

        public double Radius { get; private set; }

        public double ActualAngle
        {
            get { return actualAngle; }
            private set { actualAngle = value; text.Content = Math.Round(actualAngle,0); }
        }



        #endregion





        public Krutilka(Ellipse ellipse, Button? button )
        {
            circle = ellipse;

            Radius = circle.Width / 2;
            
            this.button = button is not null ? button : new Button() { Width = 30, Height = 30 };

            if (App.Current.FindResource("RoundedButton") != null) SetButtonStyle(App.Current.FindResource("RoundedButton") as Style);

            this.button.Margin = new Thickness((circle.Margin.Left + Radius - this.button.Width / 2) + Radius * Math.Cos(ActualAngle.ConvertToRadians()),
                (Radius + circle.Margin.Top - this.button.Height / 2) + Radius * Math.Sin(ActualAngle.ConvertToRadians()), 0, 0);

            text = new Label() { Margin = new Thickness(circle.Margin.Left + Radius - 20, circle.Margin.Top + Radius - 20, 0, 0), FontSize = 20 };
        }




        public void SetButtonStyle(Style style)
        {
            button.Style = style;
        }

        public void Update()
        {



            angle = Math.Atan2(Mouse.MousePosition.Y - (circle.Margin.Top + circle.Height / 2),
                Mouse.MousePosition.X - (circle.Margin.Left + circle.Width / 2)).ConvertToDegree();

            double deltaAngle = angle - lastAngle;

            if(Math.Abs(deltaAngle) < 300)

            ActualAngle -= deltaAngle;


            button.Margin = new Thickness((circle.Margin.Left + Radius - button.Width / 2) + Radius * Math.Cos(ActualAngle.ConvertToRadians()),
                (Radius + circle.Margin.Top - button.Height / 2) + Radius * Math.Sin(-ActualAngle.ConvertToRadians()), 0, 0);

            lastAngle = angle;
        }





        public void AddToForm(Panel panel)
        {

            panel.Children.Add(circle);
            panel.Children.Add(button);
            panel.Children.Add(text);

        }
    }

    public static class MathEx
    {
        public static double ConvertToRadians(this double angle, int digits)
        {
            return Math.Round((Math.PI / 180) * angle, digits);
        }

        public static double ConvertToDegree(this double rad, int digits)
        {
            return Math.Round((180 / Math.PI) * rad, digits);
        }

        public static double ConvertToRadians(this double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double ConvertToDegree(this double rad)
        {
            return (180 / Math.PI) * rad;
        }
    }
}
