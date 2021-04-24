using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeadowRemoteControl.Display
{
    public class OledDisplay
    {
        readonly Ssd1306 display;
        readonly GraphicsLibrary graphics;

        // rendering state and lock
        protected bool isRendering = false;
        protected object renderLock = new object();

        public OledDisplay(II2cBus i2CBus)
        {
            display = new Ssd1306(i2CBus, 0x3c, Ssd1306.DisplayType.OLED128x64);
            graphics = new GraphicsLibrary(display);
        }

        public string HeaderText { get; set; } = "Meadow F7 Remote";

        public double BatteryVoltage { get; set; }

        public string Speed { get; set; } = "High";

        public string MpuStatus { get; set; } = "Off";

        public string NRF24L01Status { get; set; } = "Off";

        public void StartUpdating(int updateInterval = 1000)
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(updateInterval);
                    Render();
                }
            });

            t.Start();
        }

        protected void Render()
        {
            lock (renderLock)
            {   // if we're already rendering, bail out.
                if (isRendering)
                {
                    Console.WriteLine("Already in a rendering loop, bailing out.");
                    return;
                }
                isRendering = true;
            }

            graphics.Clear();

            graphics.CurrentFont = new Font8x12();
            graphics.DrawText(x: 0, y: 3, HeaderText);
            graphics.DrawHorizontalLine(0, 0, 127, true);
            graphics.DrawHorizontalLine(0, 15, 127, true);

            graphics.DrawText(x:0, y: 16, $"Speed: {Speed}");

            graphics.DrawText(x:0, y: 28, $"MPU6050: {MpuStatus}");

            graphics.DrawText(x:0, y: 40, $"NRF24L01: {NRF24L01Status}");

            graphics.DrawText(x: 0, y: 52, $"Battery: {BatteryVoltage:F2}V");

            graphics.Show();
            isRendering = false;
        }
    }
}
