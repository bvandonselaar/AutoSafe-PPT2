using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PTT2CarGps
{
    public class Car
    {
        public int SignatureId { get; private set; }
        public Trail Path { get; private set; }
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
    }
}
