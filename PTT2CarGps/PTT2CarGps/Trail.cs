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
        public Point Direction
        {
            get
            {
                if (Positions.Count > 1)
                {
                    LocationTime pos1 = GetPositions[GetPositions.Count - 2];
                    LocationTime pos2 = GetPositions[GetPositions.Count - 1];
                    TimeSpan timeDifference = pos2.Time.Subtract(pos1.Time);
                    
                    Point Vector = new Point
                    {
                        X = (pos2.Location.X - pos1.Location.X),
                        Y = (pos2.Location.Y - pos1.Location.Y)
                    };
                    return Vector;
                    
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
        /// <summary>
        /// Returns the average position of the last so many positions
        /// </summary>
        /// <param name="SampleSize"></param>
        /// <returns></returns>
        public Point MovingAverageSample(int SampleSize)
        {
            if (SampleSize > Positions.Count) SampleSize = Positions.Count;
            int X = 0;
            int Y = 0;
            for(int i = Positions.Count - SampleSize; i < Positions.Count; i++)
            {
                X += Positions[i].Location.X;
                Y += Positions[i].Location.Y;
            }
            return new Point(X / SampleSize, Y / SampleSize);
        }
        /// <summary>
        /// Returns the average position of all the points within the time range
        /// </summary>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        public Point MovingAverageTime(int Seconds)
        {
            DateTime time = DateTime.Now;
            int X = 0;
            int Y = 0;
            int Count = 0;

            foreach (LocationTime Lt in Positions)
            {
                if (time.Subtract(Lt.Time).Seconds <= Seconds)
                {
                    Count++;
                    X += Lt.Location.X;
                    Y += Lt.Location.Y;
                }
            }

            return new Point(X / Count, Y / Count);
        }
    }
}
