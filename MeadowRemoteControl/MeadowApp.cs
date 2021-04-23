using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using System;
using System.Threading;
using Meadow.Foundation.Sensors.Buttons;
using Meadow.Hardware;
using System.Collections.Generic;
using MeadowRemoteControl.Types;

namespace MeadowRemoteControl
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        RgbPwmLed onboardLed;
        SwitchesAndButtons switchesAndButtons;

        // Buttons
        readonly IPin sw2 = Device.Pins.D13;
        readonly IPin sw3 = Device.Pins.D12;
        readonly IPin sw4 = Device.Pins.D10;
        readonly IPin sw5 = Device.Pins.D09;
        readonly IPin sw6 = Device.Pins.D06;
        readonly IPin sw7 = Device.Pins.D05;
        readonly IPin sw8 = Device.Pins.D15;
        readonly IPin sw9 = Device.Pins.D14;


        public MeadowApp()
        {
            Initialize();
            TransmitControlState();
        }

        void Initialize()
        {
            Console.WriteLine("Initialize hardware...");
            onboardLed.SetColor(Color.Blue);

            onboardLed = new RgbPwmLed(device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue,
                3.3f, 3.3f, 3.3f,
                Meadow.Peripherals.Leds.IRgbLed.CommonType.CommonAnode);

            switchesAndButtons = new SwitchesAndButtons(Device, sw2, sw3, sw4, sw5, sw6, sw7, sw8, sw9);

            onboardLed.SetColor(Color.Green);
        }

        DataPackage BuildDataPackage()
        {
            var dataPackage = new DataPackage
            {
                SwitchesAndButtons = switchesAndButtons.AsBitFlags()
            };

            return dataPackage;
        }

        void TransmitControlState()
        {
            var dataPackage = BuildDataPackage();

        }
    }
}
