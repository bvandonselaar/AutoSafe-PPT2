using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PTT2CarGps.Messages
{
    public class BrakeWarning : Message
    {
        public BrakeWarning(DateTime TimeSend, Point Location) : base(TimeSend, Location)
        {

        }

        public override void Draw(Graphics Canvas, Color Color)
        {
            SolidBrush brush = new SolidBrush(Color.Orange);
            Point pos = Location;
            pos.Offset(-5, -5);
            Canvas.FillEllipse
                (
                    brush,
                    new Rectangle(pos, new Size(10, 10))
                );
        }
    }
}
