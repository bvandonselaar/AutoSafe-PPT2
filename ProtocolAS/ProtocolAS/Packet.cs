using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAS
{
    /* Fields
     * 
     * Magic - 16 bits
     * Indicates the start of a new packet; it has the value 0x0EE0.
     * 
     * Length - 8 bits
     * Represents the number of bytes in the packet (including the header and checksum).
     * 
     * Cat - 3 bits
     * The category of the command (see Categories). A receiver must ignore a packet if it doesn’t recognize the category.
     * 
     * Cmd - 5 bits
     * The number identifying the command within the category (see Commands). A receiver must ignore a packet if it doesn’t recognize the command.
     * 
     * Payload - n × 16 bits
     * Arbitrary number of data bytes carried by the packet. This must be interpreted depending on the command.
     * 
     * Checksum - 16 bits
     * Checksum of the complete packet computed using the Fletcher-16 algorithm. This is used to detect if the packet was corrupted.
     */
    public class Packet
    {
        public byte magic1 { get { return 0x0E; } }
        public byte magic2 { get { return 0xE0; } }
        public byte[] newMessage { get; private set; }
        public int payloadvalue { get; private set; }
        public byte Length { get; private set; }
        public byte ChecksumSum { get; private set; }
        public byte ChecksumDivisor { get { return 0xE0; } }
        public int Checksum { get; private set; }

        byte bitmask = 0xFF;

        /// <summary>
        /// Maakt een heel bericht volgens ons protocol
        /// </summary>
        /// <param name="Magic">Magic - 16 bits, Indicates the start of a new packet; it has the value 0x0EE0.</param>
        /// <param name="Length"> Length - 8 bits, Represents the number of bytes in the packet(including the header and checksum).</param>
        /// <param name="Cat">Cat - 3 bits, The category of the command(see Categories). A receiver must ignore a packet if it doesn’t recognize the category.</param>
        /// <param name="Cmd">Cmd - 5 bits, The number identifying the command within the category(see Commands). A receiver must ignore a packet if it doesn’t recognize the command.</param>
        /// <param name="Payload">Payload - n × 16 bit, Arbitrary number of data bytes carried by the packet.This must be interpreted depending on the command. </param>
        /// <param name="Checksum">Checksum - 16 bits, Checksum of the complete packet computed using the Fletcher-16 algorithm.This is used to detect if the packet was corrupted.</param>
        /// <returns>het gehele bericht</returns>
        public byte[] Serialize(byte Command, byte[] Payload)
        {
            if(Payload == null)
            {
                Length = 6;
            }
            else
            {
                Length = (byte)(6 + Payload.Length);
            }

            //maakt message-array aan
            newMessage = new byte[Length];
            for (int i = 0; i < Length; i++)
            {
                newMessage[i] = 0x00;
            }

            if (Payload != null)
            {
                for (int i = 0; i < Payload.Length; i++)//splitst Payload in losse bytes
                {
                    newMessage[i + 4] = (byte)(bitmask & (Payload[i]));
                }
            }

            //maakt het bericht vanaf hier
            newMessage[0] = magic1;
            newMessage[1] = magic2;
            newMessage[2] = Length;
            newMessage[3] = Command;
            
            Checksum = Fletcher16(newMessage, (UInt16)(Length - 2));
            int check1 = (byte)Checksum;
            int check2 = (byte)Checksum<<8;

            newMessage[Length - 2] = (byte)check1;
            newMessage[Length - 1] = (byte)check2;
            return newMessage; //verstuurt message, message is klaar
        }

        public UInt16 Fletcher16(byte[] data, UInt16 count)
        {
            UInt16 sum1 = 0;
            UInt16 sum2 = 0;
            int index;

            for (index = 0; index < count; ++index)
            {
                sum1 = (UInt16)((sum1 + data[index]) % 255);
                sum2 = (UInt16)((sum2 + sum1) % 255);
            }

            return (UInt16)((sum2 << 8) | sum1);
        }
    }
}
