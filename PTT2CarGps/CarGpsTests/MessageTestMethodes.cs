using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using PTT2CarGps.Messages;

namespace CarGpsTests
{
    [TestClass]
    public class MessageTestMethodes
    {
        [TestMethod]
        public void TestBrakeWarningConstructor()
        {
            DateTime TimeSend = DateTime.Now;
            Point Location = new Point(1, 5);

            BrakeWarning warning = new BrakeWarning(TimeSend, Location);

            Assert.AreEqual(TimeSend, warning.TimeSend);
            Assert.AreEqual(Location, warning.Location);
        }

        [TestMethod]
        public void TestEmergencyBrakeWarningConstructor()
        {
            DateTime TimeSend = DateTime.Now;
            Point Location = new Point(1, 5);

            EmergencyBrakeWarning warning = new EmergencyBrakeWarning(TimeSend, Location);

            Assert.AreEqual(TimeSend, warning.TimeSend);
            Assert.AreEqual(Location, warning.Location);
        }
    }
}
