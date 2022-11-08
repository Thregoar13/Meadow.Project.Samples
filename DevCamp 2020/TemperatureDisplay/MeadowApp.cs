﻿using System;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Atmospheric;
using Meadow.Hardware;
using Meadow.Units;

namespace TemperatureDisplay
{
    // public class MeadowApp : App<F7FeatherV1> <- If you have a Meadow F7v1.*
    public class MeadowApp : App<F7FeatherV2>
    {
        Bmp180 sensor;
        RgbPwmLed onboardLed;
        MicroGraphics graphics;
        IDigitalInputPort button;

        bool isMetric = true;

        public override Task Initialize()
        {
            onboardLed = new RgbPwmLed(device: Device,
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
            var display = new St7789(
                device: Device,
                spiBus: spiBus,
                chipSelectPin: Device.Pins.D02,
                dcPin: Device.Pins.D01,
                resetPin: Device.Pins.D00,
                width: 240, height: 240);
            graphics = new MicroGraphics(display);
            graphics.CurrentFont = new Font12x20();

            button = Device.CreateDigitalInputPort(Device.Pins.D12, InterruptMode.EdgeRising, ResistorMode.Disabled);
            button.Changed += Button_Changed;

            sensor = new Bmp180(Device.CreateI2cBus());
            sensor.Updated += SensorUpdated;
            sensor.StartUpdating();

            onboardLed.SetColor(Color.Green);

            return base.Initialize();
        }

        private void SensorUpdated(object sender, IChangeResult<(Meadow.Units.Temperature? Temperature, Meadow.Units.Pressure? Pressure)> result)
        {
            Console.WriteLine($"{result.New.Temperature?.Celsius}");

            UpdateDisplayFancy(result.New.Temperature.Value);

            UpdateLed(result.New.Temperature.Value.Celsius);
        }

        private void Button_Changed(object sender, DigitalPortResult e)
        {
            Console.WriteLine("Button pressed");
            isMetric = !isMetric;
        }

        void UpdateDisplayFancy(Temperature? temperature)
        {
            if (temperature is { } temp)
            {
                graphics.Clear();

                var primaryColor = Color.FromRgb(238, 243, 189);
                var accentColor = Color.FromRgb(26, 128, 170);

                var tempText = GetTemperatureString(temperature);
                var xTempPos = graphics.Width / 2 - tempText.Length * 12;

                graphics.DrawCircle(120, 84, 80, accentColor, true);
                graphics.DrawCircle(120, 84, 80, primaryColor);
                graphics.DrawText((int)xTempPos, 70, tempText, primaryColor, ScaleFactor.X2);

                double barWidth = sensor.Pressure.Value.Bar;
                graphics.DrawRectangle(10, 190, (int)barWidth, 30, accentColor, true);
                graphics.DrawRectangle(10, 190, 220, 30, primaryColor);
                graphics.DrawText(20, 196, sensor.Pressure.Value.Bar.ToString(), primaryColor);

                graphics.Show(); 
            }
        }

        string GetTemperatureString(Temperature? celsius)
        {
            if (isMetric)
            {
                return $"{celsius.Value.Celsius}°C";
            }
            else
            {
                return $"{celsius.Value.Fahrenheit}°F"; 
            }
        }

        double oldTemp = 0;
        void UpdateLed(double newTemp)
        {
            if (Math.Abs(newTemp - oldTemp) < 0.01)
            {
                onboardLed.SetColor(Color.Black);
            }
            else if (oldTemp > newTemp)
            {
                onboardLed.SetColor(Color.Blue);
            }
            else
            {
                onboardLed.SetColor(Color.Red);
            }
            oldTemp = newTemp;
        }
    }
}