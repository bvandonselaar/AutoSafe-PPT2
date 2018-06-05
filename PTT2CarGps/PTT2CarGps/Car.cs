using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PTT2CarGps.Messages;

namespace PTT2CarGps
{
    public class Car : IDrawable
    {
        public int SignatureId { get; private set; }
        public Trail Path { get; private set; }
        private List<Message> Messages;
        public List<Message> GetMessages { get { return new List<Message>(Messages); } }
        public bool IsLost
        { get
            {
                Point[] locations = Path.GetPoints;
                if (locations.Length < 2) return true;
                return
                    locations[locations.Length - 1].X == locations[locations.Length - 2].X &&
                    locations[locations.Length - 1].Y == locations[locations.Length - 2].Y;
            }
        }

        public Car(int SignatureId)
        {
            this.SignatureId = SignatureId;
            Path = new Trail();
            Messages = new List<Message>();
        }
        public void AddPosition(Point position)
        {
            Path.AddPosition(position);
        }
        public Point Position
        {
            get
            {
                return Path.Position;
            }
        }
        public override string ToString()
        {
            return "Car" + SignatureId;
        }
        public void Draw(Graphics Canvas, Color Color)
        {
            List<Message> OldMessages = new List<Message>();
            foreach (Message m in Messages)
            {
                if (DateTime.Now.Subtract(m.TimeSend).Seconds < 5)
                {
                    m.Draw(Canvas, Color);
                }
                else OldMessages.Add(m);
            }
            foreach (Message m in OldMessages)
            {
                Messages.Remove(m);
            }
            if (Path.GetPositions.Count > 0 && !(Path.Position.X == 0 && Path.Position.Y == 0))
            {
                Pen pen = new Pen(Color);
                Point pos = Path.Position;
                pos.Offset(-15, -15);
                Canvas.DrawEllipse
                    (
                        pen,
                        new Rectangle(pos, new Size(30, 30))
                    );
                Path.Draw(Canvas, Color);
                if (!IsLost) Canvas.DrawString("ID:" + SignatureId, System.Windows.Forms.Control.DefaultFont, new SolidBrush(Color.White), pos);
                else Canvas.DrawString("ID:" + SignatureId + "", System.Windows.Forms.Control.DefaultFont, new SolidBrush(Color.Gray), pos);
            }
        }

        public void SendMessage(Message message)
        {
            if(message is EmergencyBrakeWarning)
            {
                foreach (Message m in Messages)
                {
                    if(m is EmergencyBrakeWarning && DateTime.Now.Subtract(m.TimeSend).Seconds < 3)
                    {
                        return;
                    }
                }
            }
            else
            {
                foreach (Message m in Messages)
                {
                    if (DateTime.Now.Subtract(m.TimeSend).Seconds < 1)
                    {
                        return;
                    }
                }
            }
            Messages.Add(message);
        }
    }
}
