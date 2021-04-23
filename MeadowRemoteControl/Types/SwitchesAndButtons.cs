using Meadow.Foundation.Sensors.Buttons;
using Meadow.Foundation.Sensors.Switches;
using Meadow.Hardware;
using Meadow.Devices;
using Meadow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadowRemoteControl.Types
{
    /// <summary>
    /// Represents all the switches and buttons wired up to the Meadow F7
    /// </summary>
    public class SwitchesAndButtons
    {
        public SwitchesAndButtons(IIODevice device, IPin switch2, IPin switch3, IPin switch4, IPin switch5, IPin switch6, IPin switch7, IPin switch8, IPin switch9)
        {
            Switch2 = new PushButton(device, switch2, ResistorMode.InternalPullDown);
            Switch3 = new PushButton(device, switch3, ResistorMode.InternalPullDown);
            Switch4 = new PushButton(device, switch4, ResistorMode.InternalPullDown);
            Switch5 = new PushButton(device, switch5, ResistorMode.InternalPullDown);
            Switch6 = new PushButton(device, switch6, ResistorMode.InternalPullDown);
            Switch7 = new PushButton(device, switch7, ResistorMode.InternalPullDown);
            Switch8 = new PushButton(device, switch8, ResistorMode.InternalPullDown);
            Switch9 = new PushButton(device, switch9, ResistorMode.InternalPullDown);
        }
        // The property names are aliged with the labels on the PCB.
        // I could have done a better job of labelling the PCB in some logical way - sorry.

        // Toggle Switches are of type PushButton because the board is wired to take advantage of the InternalPulldown resistor.
        // Only one side of the switch is connected, so it's acting like a SPST switch with ON/OFF only.

        // Toggle Switches on the left side of the board
        public PushButton Switch2 { get; private set; }
        public PushButton Switch3 { get; private set; }

        // Push Buttons on the left side of the board
        public PushButton Switch5 { get; private set; }
        public PushButton Switch6 { get; private set; }
        public PushButton Switch7 { get; private set; }

        // Toggle Switches on the right side of the board
        public PushButton Switch8 { get; private set; }
        public PushButton Switch9 { get; private set; }

        // Push Button on the right side of the board
        public PushButton Switch4 { get; private set; }

        /// <summary>
        /// Bundle the state of each switch into a single byte of bit flags
        /// </summary>
        /// <returns>
        /// unit with each switch/button state represented by a singl bit
        /// </returns>
        public uint AsBitFlags()
        {
            uint values;

            var s9 = Switch9.State ? (uint)1 : 0;
            var s8 = Switch8.State ? (uint)1 : 0;
            var s7 = Switch8.State ? (uint)1 : 0;
            var s6 = Switch8.State ? (uint)1 : 0;
            var s5 = Switch8.State ? (uint)1 : 0;
            var s4 = Switch8.State ? (uint)1 : 0;
            var s3 = Switch8.State ? (uint)1 : 0;
            var s2 = Switch8.State ? (uint)1 : 0;

            values = s9 << 7 | s8 << 6 | s7 << 5 | s6 << 4 | s5 << 3 | s4 << 2 | s3 << 1 | s2;

            return values;
        }
    }
}
