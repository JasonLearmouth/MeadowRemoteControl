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
        private readonly IDigitalInputPort button;
        private readonly IAnalogInputPort horizontalInput;
        private readonly IAnalogInputPort verticalInput;



        public AnalogJoystickWithButton(IIODevice device, IPin horizontalPin, IPin verticalPin, IPin buttonInputPin)
        {
            button = device.CreateDigitalInputPort(buttonInputPin, InterruptMode.None, ResistorMode.InternalPullDown);
            horizontalInput = device.CreateAnalogInputPort(horizontalPin);
            verticalInput = device.CreateAnalogInputPort(verticalPin);
        }

        // Analogue inputs returns a float from 0.0 - 1.0
        // We convert to a single byte 0 -> 254
        public uint HorizontalValue => (uint)horizontalInput.Read().Result * 254;
        public uint VerticalValue => (uint)verticalInput.Read().Result * 254;

        public uint ButtonState => button.State ? (uint)1 : 0;
        
    }
}
