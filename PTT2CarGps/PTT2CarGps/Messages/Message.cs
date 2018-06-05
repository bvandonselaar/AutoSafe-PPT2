using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PTT2CarGps
{
    abstract public class Message : IDrawable
    {
        public DateTime TimeSend { get; private set; }
        public Point Location { get; private set; }

        public Message(DateTime TimeSend, Point Location)
        {
            this.TimeSend = TimeSend;
            this.Location = Location;
        }

        public abstract void Draw(Graphics Canvas, Color Color);
    }
}
