using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Leds;
using Meadow.Hardware;
using Meadow.Units;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ballAnimation
{
    // public class MeadowApp : App<F7FeatherV1> <- If you have a Meadow F7v1.*
    public class MeadowApp : App<F7FeatherV2>
    {
        readonly Color WatchBackgroundColor = Color.White;

        MicroGraphics graphics;


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
            displayWidth = Convert.ToInt32(st7789.Width);
            displayHeight = Convert.ToInt32(st7789.Height);

            graphics = new MicroGraphics(st7789);
            //graphics.Rotation = RotationType._270Degrees;

            onboardLed.SetColor(Color.Green);

            return base.Initialize();
        }

        void DrawShapes()
        {
            Random rand = new Random();

            graphics.Clear(true);

            int radius = 10;
            int originX = displayWidth / 2;
            int originY = displayHeight / 2;

            for (int i = 1; i < 5; i++)
            {
                graphics.DrawCircle
                (
                    centerX: originX,
                    centerY: originY,
                    radius: radius,
                    color: Color.FromRgb(rand.Next(128, 255), rand.Next(128, 255), rand.Next(128, 255))
                );
                graphics.Show();
                radius += 30;
            }

            int sideLength = 30;
            for (int i = 1; i < 5; i++)
            {
                graphics.DrawRectangle
                (
                    x: (displayWidth - sideLength) / 2,
                    y: (displayHeight - sideLength) / 2,
                    width: sideLength,
                    height: sideLength,
                    color: Color.FromRgb(rand.Next(128, 255), rand.Next(128, 255), rand.Next(128, 255))
                );
                graphics.Show();
                sideLength += 60;
            }

            graphics.DrawLine(0, displayHeight / 2, displayWidth, displayHeight / 2,
                Color.FromRgb(rand.Next(128, 255), rand.Next(128, 255), rand.Next(128, 255)));
            graphics.DrawLine(displayWidth / 2, 0, displayWidth / 2, displayHeight,
                Color.FromRgb(rand.Next(128, 255), rand.Next(128, 255), rand.Next(128, 255)));
            graphics.DrawLine(0, 0, displayWidth, displayHeight,
                Color.FromRgb(rand.Next(128, 255), rand.Next(128, 255), rand.Next(128, 255)));
            graphics.DrawLine(0, displayHeight, displayWidth, 0,
                Color.FromRgb(rand.Next(128, 255), rand.Next(128, 255), rand.Next(128, 255)));
            graphics.Show();

            //Thread.Sleep(5000);
        }

       

        public override Task Run()
        {
            DrawShapes();
           

            return base.Run();
        }
    }
}