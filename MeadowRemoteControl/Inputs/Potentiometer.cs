using Meadow.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadowRemoteControl.Types
{
    public class Potentiometer
    {
        IAnalogInputPort pot;

        public Potentiometer(IIODevice device, IPin pin)
        {
            pot = device.CreateAnalogInputPort(pin);
        }

        public uint Value => (uint)pot.Read().Result * 254;
    }
}
