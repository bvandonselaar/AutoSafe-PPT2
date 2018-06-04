using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTT2CarGps;

namespace CarGpsTests
{
    [TestClass]
    public class CarGpsTests
    {
        [TestMethod]
        public void TestCarConstructor()
        {
            int SignatureId = 5;

            Car TestCar = new Car(SignatureId);

            Assert.AreEqual(SignatureId, TestCar.SignatureId);
        }
        [TestMethod]
        public void TestCarPositionAdding()
        {
            int SignatureId = 5;
            Car TestCar = new Car(SignatureId);
            Point OldPosition = new Point(10, 5);
            Point NewPosition = new Point(15, 10);

            TestCar.AddPosition(OldPosition);
            TestCar.AddPosition(NewPosition);

            Assert.AreEqual(2, TestCar.Path.GetPositions.Count);
            Assert.AreEqual(NewPosition, TestCar.Position);
        }
        [TestMethod]
        public void TestCarDefaultPosition()
        {
            int SignatureId = 5;
            Car TestCar = new Car(SignatureId);

            Assert.AreEqual(0, TestCar.Position.X);
            Assert.AreEqual(0, TestCar.Position.Y);
        }
        [TestMethod]
        public void TestCarToString()
        {
            int SignatureId = 5;

            Car TestCar = new Car(SignatureId);

            Assert.AreEqual("Car5", TestCar.ToString());
        }
    }
}
