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
