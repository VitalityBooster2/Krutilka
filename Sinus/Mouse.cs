using System.Numerics;
using System.Threading;
using System.Windows;

namespace Sinus
{
    public static class Mouse
    {
        public static Vector2 MouseDirection { get; set; }


        private static Point lastPosition { get; set; }

        public static Point MousePosition { get; set; }

       
        
        public static void Update(Point position)
        {
            MousePosition = position;
            if (lastPosition.X > position.X) MouseDirection = new Vector2(MouseDirection.X - 1, MouseDirection.Y); 



            if (lastPosition.X < position.X) MouseDirection = new Vector2(MouseDirection.X + 1, MouseDirection.Y);



            if (lastPosition.Y > position.Y) MouseDirection = new Vector2(MouseDirection.X, MouseDirection.Y - 1);
            if (lastPosition.Y < position.Y) MouseDirection = new Vector2(MouseDirection.X, MouseDirection.Y + 1);

            lastPosition = position;


        }

    }
}
