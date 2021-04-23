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
using Meadow.Foundation.Sensors.Hid;

namespace MeadowRemoteControl
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        RgbPwmLed onboardLed;
        SwitchesAndButtons switchesAndButtons;
        AnalogJoystickWithPushButton leftJoystick;
        AnalogJoystickWithPushButton rightJoystick;

        // Buttons
        readonly IPin sw2Pin = Device.Pins.D13;
        readonly IPin sw3Pin = Device.Pins.D12;
        readonly IPin sw4Pin = Device.Pins.D10;
        readonly IPin sw5Pin = Device.Pins.D09;
        readonly IPin sw6Pin = Device.Pins.D06;
        readonly IPin sw7Pin = Device.Pins.D05;
        readonly IPin sw8Pin = Device.Pins.D15;
        readonly IPin sw9Pin = Device.Pins.D14;

        // Joysticks
        readonly IPin leftJoystickHorizontalPin = Device.Pins.A03;
        readonly IPin leftJoystickVerticalPin = Device.Pins.A04;
        readonly IPin leftJoystickButtonPin = Device.Pins.D03;
        readonly IPin rightJoystickHorizontalPin = Device.Pins.A01;
        readonly IPin rightJoystickVerticalPin = Device.Pins.A00;
        readonly IPin rightJoystickButtonPin = Device.Pins.D04;


        public MeadowApp()
        {
            Initialize();
            TransmitControlState();
        }

        void Initialize()
        {
            Console.WriteLine("Initialize hardware...");

            onboardLed = new RgbPwmLed(device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue,
                3.3f, 3.3f, 3.3f,
                Meadow.Peripherals.Leds.IRgbLed.CommonType.CommonAnode);
            
            onboardLed.SetColor(Color.Blue);


            switchesAndButtons = new SwitchesAndButtons(Device, sw2Pin, sw3Pin, sw4Pin, sw5Pin, sw6Pin, sw7Pin, sw8Pin, sw9Pin);

            leftJoystick = new AnalogJoystickWithPushButton(Device, leftJoystickHorizontalPin, leftJoystickVerticalPin, leftJoystickButtonPin);
            rightJoystick = new AnalogJoystickWithPushButton(Device, rightJoystickHorizontalPin, rightJoystickVerticalPin, rightJoystickButtonPin);

            onboardLed.SetColor(Color.Green);
        }

        DataPackage BuildDataPackage()
        {
            var dataPackage = new DataPackage
            {
                SwitchesAndButtons = switchesAndButtons.AsBitFlags(),

               
                LeftJoystickHorizontal = leftJoystick.HorizontalValue,
                LeftJoystickVertical = leftJoystick.VerticalValue,
                LeftJoystickButton = leftJoystick.ButtonState,

                RightJoystickHorizontal = rightJoystick.HorizontalValue,
                RightJoystickVertical = rightJoystick.VerticalValue,
                RightJoystickButton = rightJoystick.ButtonState
            };

            return dataPackage;
        }

        void TransmitControlState()
        {
            var dataPackage = BuildDataPackage();

        }
    }
}
