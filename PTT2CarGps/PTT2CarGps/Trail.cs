using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PTT2CarGps
{
    public class Trail
    {
        private List<LocationTime> Positions;
        public int MaxPositions { get; private set; }

        public List<LocationTime> GetPositions { get { return new List<LocationTime>(Positions); } }
        public Point Position { get { if (GetPositions.Count > 0) return GetPositions[GetPositions.Count - 1].Location; else return new Point(0, 0); } }
        public Point Speed
        {
            get
            {
                if (Positions.Count > 1)
                {
                    LocationTime pos1 = GetPositions[GetPositions.Count - 2];
                    LocationTime pos2 = GetPositions[GetPositions.Count - 1];
                    TimeSpan timeDifference = pos2.Time.Subtract(pos1.Time);
                    
                    Point SpeedVector = new Point
                    {
                        X = (pos2.Location.X - pos1.Location.X),
                        Y = (pos2.Location.Y - pos1.Location.Y)
                    };
                    return SpeedVector;
                    
                }
                else return new Point(0,0);
            }
        }
        public Point[] GetPoints
        {
            get
            {
                Point[] Points = new Point[Positions.Count];
                for(int i = 0; i < Positions.Count; i++)
                {
                    Points[i] = Positions[i].Location;
                }
                return Points;
            }
        }

        public Trail()
        {
            Positions = new List<LocationTime>();
            MaxPositions = 100;
        }
        public Trail(int maxPositions)
        {
            Positions = new List<LocationTime>();
            MaxPositions = maxPositions;
        }

        public void AddPosition(Point Position)
        {
            Positions.Add(new LocationTime(Position, DateTime.Now));
            while(Positions.Count > MaxPositions)
            {
                Positions.RemoveAt(0);
            }
        }
    }
}
