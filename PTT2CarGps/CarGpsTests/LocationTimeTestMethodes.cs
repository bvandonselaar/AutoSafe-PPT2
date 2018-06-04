using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTT2CarGps;

namespace CarGpsTests
{
    [TestClass]
    public class LocationTimeTestMethodes
    {
        [TestMethod]
        public void TestLocationTimeConstructor()
        {
            Point Location = new Point(10, 20);
            DateTime Time = DateTime.Now;

            LocationTime locationTime = new LocationTime(Location, Time);

            Assert.AreEqual(Location, locationTime.Location);
            Assert.AreEqual(Time, locationTime.Time);
        }
    }
}
