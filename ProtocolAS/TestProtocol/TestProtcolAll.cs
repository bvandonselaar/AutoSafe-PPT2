using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtocolAS;

namespace TestProtocol
{
    [TestClass]
    public class TestProtcolAll
    {
        Packet p = new Packet();
        Unpack u = new Unpack();

        [TestMethod]
        public void TestFletcher()
        {
            //0's in array zijn gereserveerde plekken in array zie serialize
            //Test Fletcher Algoritme
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x0A, 0XE1, 0xA1, 0xA2, 0x33, 0x44, 0, 0 };
            UInt16 test = p.Fletcher16(Message);
            
            Assert.AreEqual(0x5596, test);
        }

        [TestMethod]
        public void TestSerializeCorrectComplete()
        {
            //Test een normaal bericht om te versturen, zonder bijzonderheden
            byte Command = 0xE1;
            byte[] Payload = new byte[] { 0xA1, 0xA2, 0x33, 0x44 };
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x0A, 0XE1, 0xA1, 0xA2, 0x33, 0x44, 0x55, 0x96};
            byte[] test = p.Serialize(Command, Payload);

            Assert.AreEqual(4, Payload.Length);
            Assert.AreEqual(Message[0], test[0]);
            Assert.AreEqual(Message[1], test[1]);
            Assert.AreEqual(Message[2], test.Length);
            Assert.AreEqual(Message[2], test[2]);
            Assert.AreEqual(Message[3], test[3]);
            Assert.AreEqual(Message[4], test[4]);
            Assert.AreEqual(Message[5], test[5]);
            Assert.AreEqual(Message[6], test[6]);
            Assert.AreEqual(Message[7], test[7]);
            Assert.AreEqual(Message[8], test[8]);
            Assert.AreEqual(Message[9], test[9]);
        }

        [TestMethod]
        public void TestSerializePayloadNull()
        {
            //Test versturen bericht zonder payload
            byte Command = 0xE1;
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x06, 0XE1};
            byte[] test = p.Serialize(Command, null);

            Assert.AreEqual(Message[0], test[0]);
            Assert.AreEqual(Message[1], test[1]);
            Assert.AreEqual(Message[2], test.Length);
            Assert.AreEqual(Message[2], test[2]);
            Assert.AreEqual(Message[3], test[3]);
            Assert.AreEqual(0xC8, test[4]);
            Assert.AreEqual(0xD6, test[5]);
        }

        [TestMethod]
        public void TestDeserializeCorrect()
        {
            //Test het ontcijferen van een ontvangen bericht die alles heeft en waarbij alles klopt
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x0A, 0XE1, 0xA1, 0xA2, 0x33, 0x44, 0x55, 0x96 };
            int check = u.Deserialize(Message);

            Assert.AreEqual(1, check);
            Assert.AreEqual(0x0E, u.Magic1);
            Assert.AreEqual(0xE0, u.Magic2);
            Assert.AreEqual(0x0A, u.Length);
            Assert.AreEqual(0XE0, u.Cat);
            Assert.AreEqual(0x01, u.Cmd);
            Assert.AreEqual(0xA1, u.Payload[0]);
            Assert.AreEqual(0xA2, u.Payload[1]);
            Assert.AreEqual(0x33, u.Payload[2]);
            Assert.AreEqual(0x44, u.Payload[3]);
            Assert.AreEqual(0x5596, u.Checksum);
        }

        [TestMethod]
        public void TestDeserializeIncorrectMagic()
        {
            //Test het ontcijferen van een ontvangen bericht die alles heeft maar de Magic niet correct heeft
            byte[] Message = new byte[] { 0x0E, 0x0E, 0x0A, 0XE1, 0xA1, 0xA2, 0x33, 0x44, 0x05, 0x30 };
            int check = u.Deserialize(Message);

            Assert.AreEqual(0, check);
            Assert.AreEqual(0x0E, u.Magic1);
            Assert.AreEqual(0x0E, u.Magic2);
        }

        [TestMethod]
        public void TestDeserializeIncorrectChecksum()
        {            
            //Test het ontcijferen van een ontvangen bericht die alles heeft maar de checksum klopt niet
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x0A, 0XE1, 0xA1, 0xA2, 0x33, 0x44, 0x05, 0x30 };
            int check = u.Deserialize(Message);

            Assert.AreEqual(-1, check);
            Assert.AreEqual(0x0E, u.Magic1);
            Assert.AreEqual(0xE0, u.Magic2);
            Assert.AreEqual(0x0A, u.Length);
            Assert.AreEqual(0x0530, u.Checksum);
        }

        [TestMethod]
        public void TestDeserializeNoPayloadCorrect()
        {
            //Test het ontcijferen van een ontvangen bericht die geen payload heeft en waarbij alles klopt
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x06, 0XE1, 0xC8, 0xD6 };
            int check = u.Deserialize(Message);

            Assert.AreEqual(1, check);
            Assert.AreEqual(0x0E, u.Magic1);
            Assert.AreEqual(0xE0, u.Magic2);
            Assert.AreEqual(0x06, u.Length);
            Assert.AreEqual(0XE0, u.Cat);
            Assert.AreEqual(0x01, u.Cmd);
            Assert.AreEqual(0xC8D6, u.Checksum);
        }

        [TestMethod]
        public void TestDeserializeNoPayloadIncorrectChecksum()
        {
            //Test het ontcijferen van een ontvangen bericht die geen payload heeft en de meegegeven checksum niet klopt
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x06, 0XE1, 0x55, 0x96 };
            int check = u.Deserialize(Message);

            Assert.AreEqual(-1, check);
            Assert.AreEqual(0x0E, u.Magic1);
            Assert.AreEqual(0xE0, u.Magic2);
            Assert.AreEqual(0x06, u.Length);
            Assert.AreEqual(0x5596, u.Checksum);
        }

        [TestMethod]
        public void TestSerializeDeserializePayloadCorrectEverthingChecked()
        {
            //Test het versturen en ontcijferen bericht waarbij alles klopt
            byte Command = 0xE1;
            byte[] Payload = new byte[] { 0xA1, 0xA2, 0x33, 0x44 };
            byte[] Message = new byte[] { 0x0E, 0xE0, 0x0A, 0XE1, 0xA1, 0xA2, 0x33, 0x44, 0x55, 0x96 };
            byte[] test = p.Serialize(Command, Payload);
            int check = u.Deserialize(test);

            //tests serialize
            Assert.AreEqual(4, Payload.Length);
            Assert.AreEqual(Message[0], test[0]);
            Assert.AreEqual(Message[1], test[1]);
            Assert.AreEqual(Message[2], test.Length);
            Assert.AreEqual(Message[2], test[2]);
            Assert.AreEqual(Message[3], test[3]);
            Assert.AreEqual(Message[4], test[4]);
            Assert.AreEqual(Message[5], test[5]);
            Assert.AreEqual(Message[6], test[6]);
            Assert.AreEqual(Message[7], test[7]);
            Assert.AreEqual(Message[8], test[8]);
            Assert.AreEqual(Message[9], test[9]);

            //tests deserialize
            Assert.AreEqual(1, check);
            Assert.AreEqual(0x0E, u.Magic1);
            Assert.AreEqual(0xE0, u.Magic2);
            Assert.AreEqual(0x0A, u.Length);
            Assert.AreEqual(0XE0, u.Cat);
            Assert.AreEqual(0x01, u.Cmd);
            Assert.AreEqual(0xA1, u.Payload[0]);
            Assert.AreEqual(0xA2, u.Payload[1]);
            Assert.AreEqual(0x33, u.Payload[2]);
            Assert.AreEqual(0x44, u.Payload[3]);
            Assert.AreEqual(0x5596, u.Checksum);
        }

        [TestMethod]
        public void TestSerializeDeserializePayloadCorrect()
        {
            //Test het versturen en ontcijferen bericht waarbij alles klopt, gekeken naar begin en eindwaarden

            byte Command = 0xE1;
            byte[] Payload = new byte[] { 0xA1, 0xA2, 0x33, 0x44 };
            byte[] test = p.Serialize(Command, Payload);
            int check = u.Deserialize(test);

            //test serialize -> deserialize
            Assert.AreEqual(1, check);
            Assert.AreEqual(test[0], u.Magic1);
            Assert.AreEqual(test[1], u.Magic2);
            Assert.AreEqual(test[2], u.Length);
            Assert.AreEqual(test[3], (u.Cat | u.Cmd));
            Assert.AreEqual(Command, (u.Cat | u.Cmd));
            Assert.AreEqual(test[4], u.Payload[0]);
            Assert.AreEqual(test[5], u.Payload[1]);
            Assert.AreEqual(test[6], u.Payload[2]);
            Assert.AreEqual(test[7], u.Payload[3]);
            Assert.AreEqual(((test[8] << 8) | test[9]), u.Checksum);
        }

        [TestMethod]
        public void TestSerializeDeserializeCorrectNoPayload()
        {            
            //Test het versturen en ontcijferen bericht waarbij alles klopt maar geen payload is
            byte Command = 0xE1;
            byte[] test = p.Serialize(Command, null);
            int check = u.Deserialize(test);

            //test serialize -> deserialize
            Assert.AreEqual(1, check);
            Assert.AreEqual(test[0], u.Magic1);
            Assert.AreEqual(test[1], u.Magic2);
            Assert.AreEqual(test[2], u.Length);
            Assert.AreEqual(test[3], (u.Cat | u.Cmd));
            Assert.AreEqual(Command, (u.Cat | u.Cmd));
            Assert.AreEqual(((test[4] << 8) | test[5]), u.Checksum);
        }
    }
}
