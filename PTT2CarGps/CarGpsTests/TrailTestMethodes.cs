using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTT2CarGps;

namespace CarGpsTests
{
    [TestClass]
    public class TrailTestMethodes
    {
        [TestMethod]
        public void TestTrailConstructor()
        {
            int maxPositions = 10;

            Trail trail = new Trail(maxPositions);

            Assert.AreEqual(maxPositions, trail.MaxPositions);
        }
        [TestMethod]
        [ExpectedException(typeof(TrailCountNegativeException))]
        public void TestTrailConstructorNegativeCount()
        {
            int maxPositions = -10;

            Trail trail = new Trail(maxPositions);
        }
        [TestMethod]
        public void TestPositionAdding()
        {
            Trail trail = new Trail();
            Point OldPosition = new Point(10, 5);
            Point NewPosition = new Point(15, 10);

            trail.AddPosition(OldPosition);
            trail.AddPosition(NewPosition);

            Assert.AreEqual(2, trail.GetPositions.Count);
            Assert.AreEqual(NewPosition, trail.Position);
        }
        [TestMethod]
        public void TestDirection()
        {
            Trail trail = new Trail();
            Point OldPosition = new Point(10, 5);
            Point NewPosition = new Point(15, 10);

            trail.AddPosition(OldPosition);
            trail.AddPosition(NewPosition);

            Assert.AreEqual(5, trail.Direction.X);
            Assert.AreEqual(5, trail.Direction.Y);
        }
        [TestMethod]
        public void TestMaxPositions()
        {
            int MaxPositions = 10;
            Trail trail = new Trail(MaxPositions);

            for(int i = 0; i < 20; i++)
            {
                trail.AddPosition(new Point(i, i));
            }

            Assert.AreEqual(MaxPositions, trail.GetPoints.Length);
            Assert.AreEqual(19, trail.Position.X);
        }
        [TestMethod]
        public void TestMovingAverageSample()
        {
            Trail trail = new Trail();
            Point OldPosition = new Point(10, 0);
            Point NewPosition = new Point(20, 10);

            trail.AddPosition(OldPosition);
            trail.AddPosition(NewPosition);

            //Average of the last 2 samples
            Assert.AreEqual(15, trail.MovingAverageSample(2).X);
            Assert.AreEqual(5, trail.MovingAverageSample(2).Y);
        }

        [TestMethod]
        public void TestMovingAverageSeconds()
        {
            DateTime StartTime = DateTime.Now;
            Trail trail = new Trail();
            Point Position1 = new Point(10, 0);
            Point Position2 = new Point(20, 10);

            trail.AddPosition(Position1);
            while (DateTime.Now.Subtract(StartTime).Seconds < 5) { }
            trail.AddPosition(Position2);

            //Average position of last second
            Assert.AreEqual(20, trail.MovingAverageTime(1).X);
            Assert.AreEqual(10, trail.MovingAverageTime(1).Y);
            //Average position of last 7 seconds
            Assert.AreEqual(15, trail.MovingAverageTime(7).X);
            Assert.AreEqual(5, trail.MovingAverageTime(7).Y);
        }
    }
}
