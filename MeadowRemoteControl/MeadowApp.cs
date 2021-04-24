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
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using MeadowRemoteControl.Display;

namespace MeadowRemoteControl
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        #region Pin Assignments
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

        //Potentiometers
        readonly IPin leftPotPin = Device.Pins.A05;
        readonly IPin rightPotPin = Device.Pins.A02;
        #endregion

        // Inputs
        SwitchesAndButtons switchesAndButtons;
        AnalogJoystickWithButton leftJoystick;
        AnalogJoystickWithButton rightJoystick;
        Potentiometer leftPot;
        Potentiometer rightPot;

        // Outputs
        RgbPwmLed onboardLed;
        OledDisplay display;

        public MeadowApp()
        {
            Initialize();
            //TransmitControlState();
            
        }

        void Initialize()
        {
            Console.WriteLine("Initialize hardware...");
            Console.WriteLine($"Battery Voltage: {Device.GetBatteryLevel():F2}V");

            display = new OledDisplay(Device.CreateI2cBus())
            {
                HeaderText = "Initialising...",
                BatteryVoltage = Device.GetBatteryLevel()
            };
            display.StartUpdating();

            onboardLed = new RgbPwmLed(device: Device,
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue,
                3.3f, 3.3f, 3.3f,
                Meadow.Peripherals.Leds.IRgbLed.CommonType.CommonAnode);
            
            onboardLed.SetColor(Color.Blue);


            switchesAndButtons = new SwitchesAndButtons(Device, sw2Pin, sw3Pin, sw4Pin, sw5Pin, sw6Pin, sw7Pin, sw8Pin, sw9Pin);

            leftJoystick = new AnalogJoystickWithButton(Device, leftJoystickHorizontalPin, leftJoystickVerticalPin, leftJoystickButtonPin);
            rightJoystick = new AnalogJoystickWithButton(Device, rightJoystickHorizontalPin, rightJoystickVerticalPin, rightJoystickButtonPin);

            leftPot = new Potentiometer(Device, leftPotPin);
            rightPot = new Potentiometer(Device, rightPotPin);


            onboardLed.SetColor(Color.Green);
            display.HeaderText = "Meadow F7 Remote";
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
                RightJoystickButton = rightJoystick.ButtonState,

                LeftPot = leftPot.Value,
                RightPot = rightPot.Value
            };

            return dataPackage;
        }

        void TransmitControlState()
        {
            var dataPackage = BuildDataPackage();

        }
    }
}
