using Meadow.Foundation.Sensors.Buttons;
using Meadow.Foundation.Sensors.Hid;
using Meadow.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meadow.Foundation.Sensors.Hid.AnalogJoystick;

namespace MeadowRemoteControl.Types
{
    public class AnalogJoystickWithButton
    {
        public AnalogJoystickWithButton(IIODevice device, IPin horizontalPin, IPin verticalPin, IPin buttonInputPin, JoystickCalibration calibration = null, bool isInverted = false, ResistorMode buttonResistorMode = ResistorMode.InternalPullUp)
        {
            Joystick = new AnalogJoystick(device.CreateAnalogInputPort(horizontalPin), device.CreateAnalogInputPort(horizontalPin), calibration, isInverted);
            Button = new PushButton(device, buttonInputPin, buttonResistorMode);
        }

        public AnalogJoystick Joystick { get; private set; }

        public PushButton Button { get; private set; }

        // Joystick returns a float from 0.0 - 1.0
        // We convert to a single byte 0 -> 254
        public uint HorizontalValue => (uint)Joystick.GetHorizontalValue().Result * 254;
        public uint VerticalValue => (uint)Joystick.GetVerticalValue().Result * 254;

        public uint ButtonState => Button.State ? (uint)1 : 0;
        
    }
}
