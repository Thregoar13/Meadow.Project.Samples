﻿using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Rotary;
using Meadow.Foundation.Servos;
using Meadow.Units;
using System.Threading.Tasks;
using AU = Meadow.Units.Angle.UnitType;

namespace RotaryServo
{
    // public class MeadowApp : App<F7FeatherV1> <- If you have a Meadow F7v1.*
    public class MeadowApp : App<F7FeatherV2>
    {
        Angle angle = new Angle(0, AU.Degrees);
        Servo servo;
        RotaryEncoder rotaryEncoder;

        public override async Task Initialize()
        {
            var onboardLed = new RgbPwmLed(
                device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            servo = new Servo(Device, Device.Pins.D08, NamedServoConfigs.SG90);
            await servo.RotateTo(new Angle(0, AU.Degrees));

            rotaryEncoder = new RotaryEncoder(Device, Device.Pins.D01, Device.Pins.D03);
            rotaryEncoder.Rotated += RotaryEncoderRotated;

            onboardLed.SetColor(Color.Green);
        }

        void RotaryEncoderRotated(object sender, Meadow.Peripherals.Sensors.Rotary.RotaryChangeResult e)
        {
            if (e.New == Meadow.Peripherals.Sensors.Rotary.RotationDirection.Clockwise)
            {
                angle += new Angle(1, AU.Degrees);
            }
            else
            {
                angle -= new Angle(1, AU.Degrees);
            }

            if (angle > new Angle(180, AU.Degrees)) angle = new Angle(180, AU.Degrees);
            else if (angle < new Angle(0, AU.Degrees)) angle = new Angle(0, AU.Degrees);

            servo.RotateTo(angle);
        }
    }
}