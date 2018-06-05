using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCar
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
     * Identifier - 16 bits
     * Identifier of the intended recipient of the packet.
     * 
     * Payload - n × 16 bits
     * Arbitrary number of data bytes carried by the packet. This must be interpreted depending on the command.
     * 
     * Checksum - 16 bits
     * Checksum of the complete packet computed using the Fletcher-16 algorithm. This is used to detect if the packet was corrupted.
     */
    public class Packet
    {
        public byte Magic1 { get { return 0x0E; } }
        public byte Magic2 { get { return 0xE0; } }
        public byte[] newMessage { get; private set; }
        public byte Length { get; private set; }
        public byte Identifier1 { get; private set; }
        public byte Identifier2 { get; private set; }
        public UInt16 Checksum { get; private set; }

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
        public byte[] Serialize(byte Command, UInt16 Identifier, byte[] Payload)
        {
            //De lengte van het bericht is 6 wanneer hij geen payload heeft, anders 6 + de lengte van de payload
            if (Payload == null)
            {
                Length = 8;
            }
            else
            {
                Length = (byte)(8 + Payload.Length);
            }

            //maakt message-array aan
            newMessage = new byte[Length];
            for (int i = 0; i < Length; i++)
            {
                newMessage[i] = 0x00;
            }

            //maakt het bericht vanaf hier
            newMessage[0] = Magic1;
            newMessage[1] = Magic2;
            newMessage[2] = Length;
            newMessage[3] = Command;

            Identifier1 = (byte)((Identifier >> 8) & 0xFF);
            Identifier2 = (byte)(Identifier & 0XFF);

            newMessage[4] = Identifier1;
            newMessage[5] = Identifier2;

            //Als de payload niet null is, dan splitst hij hier de Payload in losse bytes en zet het in het nieuwe bericht
            if (Payload != null)
            {
                for (int i = 0; i < Payload.Length; i++)
                {
                    newMessage[i + 6] = (byte)(bitmask & (Payload[i]));
                }
            }
            //Doet controleren of de waarden kloppen
            Checksum = Fletcher16(newMessage);
            int check1 = (byte)((Checksum >> 8) & 0xFF);
            int check2 = (byte)Checksum & 0XFF;

            newMessage[Length - 2] = (byte)check1; //eerste deel checksum in array
            newMessage[Length - 1] = (byte)check2; //tweede deel checksum in array

            return newMessage; //verstuurt message, message is klaar
        }

        /// <summary>
        /// Het Checksum-gedeelte volgens het Fletcher16-protocol. 
        /// Je pakt voor sum1 steeds de waarde van de data op arraylocatie i, waarna je de rest pakt als je dit deelt door 255
        /// Op sum 2 tel je de huidige Sum2 waarde op bij de sum1 waarde die je net hebt berekend en ook deze deel je dan door 255
        /// Sum2 moet het eerste komen in de waarde en sum 1 als tweede
        /// </summary>
        /// <param name="data">Het meegekregen bericht dat een checksum zal krijgen</param>
        /// <returns></returns>
        public UInt16 Fletcher16(byte[] data)
        {
            UInt16 sum1 = 0;
            UInt16 sum2 = 0;

            for (int i = 0; i < data.Length - 2; i++)
            {
                sum1 = (UInt16)((sum1 + data[i]) % 255);
                sum2 = (UInt16)((sum2 + sum1) % 255);
            }
            return (UInt16)((sum2 << 8) | sum1);
        }
    }
}
