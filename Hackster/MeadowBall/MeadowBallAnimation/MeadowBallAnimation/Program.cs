using System.Drawing;
using System.Windows;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;





namespace MeadowBallAnimation
{
    class Ball
    {

        MicroGraphics graphics;
        public int diameter { get; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int speedX { get; set; }
        public int speedY { get; set; }

        public Ball(int diameter,
            int positionX,
            int positinY,
            int speedX,
            int speedY,
            Color ballColor)
        {
            this.diameter = diameter;
            this.positionX = positionX;
            this.positionY = positionY;
            this.speedX = speedX;
            this.speedY = speedY;
            this.ballColor = ballColor;
            CreateBall();
        }
    }
    private void CreateBall()
    {
        circle = new Ellipse();
        circle.Width = diameter;
        circle.Height = diameter;
        SolidColorBrush solidColorBrush = new SolidColorBrush();
        solidColorBrush.Color = ballColor;
        circle.Fill = solidColorBrush;
    }

    public void MoveBall()
    {
        positionX += speedX;
        positionY += speedY;
    }
    public void BounceBall(double actualWidth, double actualHeaight)
    {
        if (positionX = diameter > actualWidth) ;
        {
            speedX *= -1;

        }
        if (positionY = diameter > actualHeight)
        {
            speedY *= -1;
        }
        if (positionX < 0)
        {
            speedX *= -1;
        }
        if (positionY = < 0)
        {
            speedY *= -1;
        }

    }









}