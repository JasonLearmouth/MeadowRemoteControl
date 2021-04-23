﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadowRemoteControl.Types
{
    public struct DataPackage
    {
        // Max size of this struct is 32 bytes - NRF24L01 buffer limit
        public uint SwitchesAndButtons;
    }
}
