using animationWithBallClass;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Leds;
using Meadow.Hardware;
using Meadow.Units;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;


namespace MeadowBallAnimation
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV2>
    {
        MicroGraphics graphics;

        Ball ball;
        const int displayWidth = 240;
        const int displayHeight = 240;
        Color BackgroundColor = Color.White;
        int radius = 10;
        double positionX = displayWidth / 2;
        double positionY = displayHeight / 2;
        double speedX = 11;
        double speedY = 10;
        Color ballColor = Color.Blue;


        public override Task Initialize() 
        {
            var onboardLed = new RgbPwmLed(
                device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            var config = new SpiClockConfiguration(
                speed: new Frequency(48000, Frequency.UnitType.Kilohertz),
                mode: SpiClockConfiguration.Mode.Mode3);
            var spiBus = Device.CreateSpiBus(
                clock: Device.Pins.SCK,
                copi: Device.Pins.MOSI,
                cipo: Device.Pins.MISO,
                config: config);
            var st7789 = new St7789
            (
                device: Device,
                spiBus: spiBus,
                chipSelectPin: Device.Pins.D02,
                dcPin: Device.Pins.D01,
                resetPin: Device.Pins.D00,
                width: 240, height: 240
            );

            ball = new Ball
           (
           displayWidth: displayWidth,
           displayHeight: displayHeight,
           backgroundColor: BackgroundColor,
           radius: radius,
           positionX: positionX,
           positionY: positionY,
           speedX: speedX,
           speedY: speedY,
           ballColor: ballColor
           );


            graphics = new MicroGraphics(st7789);
            

            onboardLed.SetColor(Color.Green);

            return base.Initialize();
        }


        public override Task Run()
        {
            Console.WriteLine("Run...");

            RunAnimation();
            return base.Run();
        }








        private void RunAnimation()
        {
            while (true)
            {
                graphics.Clear();
                ball.MoveBall();
                ball.BounceBall(displayWidth,displayHeight );
                graphics.DrawCircle(
                    centerX: (int)ball.positionX,
                    centerY: (int)ball.positionY,
                    radius: ball.radius,
                    color: ball.ballColor,
                    filled: false
                    );
                graphics.Show();
                Thread.Sleep(100);
            }
        }


    }
}