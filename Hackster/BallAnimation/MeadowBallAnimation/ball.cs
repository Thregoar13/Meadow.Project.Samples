using System;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;





namespace animationWithBallClass
{
    class Ball
    {
        MicroGraphics graphics;

        public int radius { get; }//ball variables
        public double positionX { get; set; }
        public double positionY { get; set; }
        public double speedX { get; set; }
        public double speedY { get; set; }
        public Color backgroundColor { get; }
        public Color ballColor { get; }
        public int displayWidth { get; }
        public int displayHeight { get; }




        public Ball( //create instance of ball
            int radius,
            double positionX,
            double positionY,
            double speedX,
            double speedY,
            Color ballColor,
            Color backgroundColor,
            int displayWidth,
            int displayHeight)
        {
             // set the variables
            this.radius = radius;
            this.positionX = positionX;
            this.positionY = positionY;
            this.speedX = speedX;
            this.speedY = speedY;
            this.ballColor = ballColor;
            this.backgroundColor = backgroundColor;
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
         
        }



        public void MoveBall()
        {
            positionX += speedX;
            positionY += speedY;
        }
        public void BounceBall(int displayWidth, int displayHeight)
        {
            if ((positionX + radius + speedX) > displayWidth) 
            {
                speedX *= -1;
                positionX = displayWidth - radius;

            }
            if ((positionY + radius + speedY) > displayHeight)
            {
                speedY *= -1;
                positionY = displayHeight - radius;
            }
            if ((positionX - radius - speedX) < 0)
            {
                speedX *= -1;
                positionX = radius;
            }
            if ((positionY - speedY - radius) < 0)
            {
                speedY *= -1;
                positionY = radius;
            }

        }

        public void DrawBall()
        {
            graphics.Clear();
            graphics.DrawCircle(centerX: (int)positionX, centerY: (int)positionY, radius: radius, color: ballColor);

        }
    }
}




