using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControlCar;
using System.Threading.Tasks;
using System.Text;

namespace ControlCarTest
{
    [TestClass]
    public class ControlClientTest
    {
        ControlClient c = new ControlClient();
        [TestMethod]
        public void ConstructorTest()
        {
            Assert.AreEqual("0", c.IP);
            Assert.AreEqual(0, c.Port);
        }
        [TestMethod]
        public void TestDisconnect()
        {
            c.Disconnect();
            Assert.AreEqual("0", c.IP);
            Assert.AreEqual(0, c.Port);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void TestSend()
        {
            string command = "Hoi";
            byte[] data = Encoding.ASCII.GetBytes(command);
            c.Send(command);
        }
        /// <summary>
        /// Test faalt omdat er geen verbinding gemaakt kan worden in Unit test, als dit wel kon was dit de test
        /// </summary>
        /*[TestMethod]
        public void TestSendSpeed()
        {
            Assert.AreEqual(1, c.SendSpeed(1, 1, 200));
        }*/

        [TestMethod]
        public void TestSendSpeedNoConnection()
        {
            Assert.AreEqual(0, c.SendSpeed(1, 1, 200));
        }

        [TestMethod]
        public void TestConnect()
        {
            c.Connect("145.10.233.20", 3);
            Assert.AreEqual("145.10.233.20", c.IP);
            Assert.AreEqual(3, c.Port);
        }

        [TestMethod]
        public void TestToString()
        {
            //False want hij kan niet verbinden in een Unit Test
            string expected = "Connected: " + "False" + "\nIP: " + "145.10.233.20" + "\nPort: " + 3;
            c.Connect("145.10.233.20", 3);
            Assert.AreEqual(expected, c.ToString());

        }
    }
}
