using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PTT2CarGps.Messages
{
    public class EmergencyBrakeWarning : Message
    {
        public EmergencyBrakeWarning(DateTime TimeSend, Point Location) : base(TimeSend, Location)
        {

        }

        public override void Draw(Graphics Canvas, Color Color)
        {
            SolidBrush brush = new SolidBrush(Color.Red);
            Point pos = Location;
            pos.Offset(-10, -10);
            Canvas.FillEllipse
                (
                    brush,
                    new Rectangle(pos, new Size(20, 20))
                );
        }
    }
}
