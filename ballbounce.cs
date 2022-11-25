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


public Ball(
MicroGraphics graphics,
int displayWidth,
int displayHeight,
Color backgroundColor,
int radius,
int positionX,
int positionY,
int speedX,
int speedY,
Color ballColor
)

graphics.DrawCircle
(
centerX: positionX,
centerY: positionY,
radius: radius,
color: ballColor,
filled: false
);.
.
.
.
.
.
// for timing use:
Thread.Sleep(10);


namespace ballBounce
{
    // public class MeadowApp : App<F7FeatherV1> <- If you have a Meadow F7v1.*
    public class MeadowApp : App<F7FeatherV2>
    {
        

        MicroGraphics graphics;
        int displayWidth, displayHeight;
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

        


        }

        

        }

        public override Task Run()
        {
            DrawShapes();


            return base.Run();
        }
    }
}