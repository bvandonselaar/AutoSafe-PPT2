using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTT2CarGps;
using PTT2CarGps.Messages;

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
        [TestMethod]
        public void TestCarSendMessage()
        {
            int SignatureId = 5;
            Car TestCar = new Car(SignatureId);

            //Send Brakewarning
            BrakeWarning brakeWarning1 = new BrakeWarning(DateTime.Now, new Point(0, 0));
            TestCar.SendMessage(brakeWarning1);
            //Send second brakewarning (wont send to avoid spam)
            BrakeWarning brakeWarning2 = new BrakeWarning(DateTime.Now, new Point(0, 0));
            TestCar.SendMessage(brakeWarning2);
            //Send emergency brake warning (will send)
            EmergencyBrakeWarning emergencyBrakeWarning1 = new EmergencyBrakeWarning(DateTime.Now, new Point(0, 0));
            TestCar.SendMessage(emergencyBrakeWarning1);
            //Send sencond emergency brake (wont send to avoid spam)
            EmergencyBrakeWarning emergencyBrakeWarning2 = new EmergencyBrakeWarning(DateTime.Now, new Point(0, 0));
            TestCar.SendMessage(emergencyBrakeWarning2);

            Assert.AreEqual(brakeWarning1, TestCar.GetMessages[0]);
            Assert.AreEqual(emergencyBrakeWarning1, TestCar.GetMessages[1]);
            Assert.AreEqual(2, TestCar.GetMessages.Count);
        }

        [TestMethod]
        public void TestCarIsLost()
        {
            int SignatureId = 5;
            Car TestCar = new Car(SignatureId);
            Point Position1 = new Point(14, 7);
            Point Position2 = new Point(15, 10);
            Point Position3 = new Point(20, 15);
            Point Position4 = new Point(20, 15);

            TestCar.AddPosition(Position1);
            TestCar.AddPosition(Position2);
            TestCar.AddPosition(Position3);
            TestCar.AddPosition(Position4);

            //Car is lost because the arduino will send the same location twice 
            //if it can't find the new location
            Assert.IsTrue(TestCar.IsLost);
        }
    }
}
