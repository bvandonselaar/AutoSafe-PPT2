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
    public class Unpack
    {
        public byte Magic1 { get; private set; }
        public byte Magic2 { get; private set; }
        public byte Length { get; private set; }
        public byte Cat { get; private set; }
        public byte Cmd { get; private set; }
        public byte[] Payload { get; private set; }
        public UInt16 Checksum { get; private set; }
        public UInt16 ControlChecksum { get; private set; }

        /// <summary>
        /// Ontcijfert het bericht dat hij meekrijgt volgens ons protocol
        /// </summary>
        /// <param name="message">het bericht dat moet worden ontcijferd</param>
        public int Deserialize(byte[] message)
        {
            if (message[2] == message.Length)
            {
                Magic1 = message[0]; //1e deel magic
                Magic2 = message[1]; //2e deel magic

                //Als Magic niet onze 2 bepaalde waarden heeft, wordt hij niet herkend
                if (Magic1 == 0x0E && Magic2 == 0xE0)
                {
                    Length = message[2]; //lengte van bericht

                    //Controleert Checksum volgens Fletcher16
                    ControlChecksum = Fletcher16(message);

                    //Maakt Checksum met de meegekregen waarden op de locaties waar deze geschreven staan
                    Checksum = (UInt16)((message[Length - 2] << 8) | message[Length - 1]);

                    //Als de Fletcher16 en de zelf geschoven Checksum hetzelfde zijn, dan is het bericht goed en mag het worden ontcijferd
                    if (ControlChecksum == Checksum)
                    {
                        if (message[3] != null)
                        {
                            Cat = (byte)(0b11100000 & (message[3])); //zet categorie
                            Cmd = (byte)(0b00011111 & (message[3])); //zet bericht

                            if (Length > 0x06)
                            {
                                //maakt payloadarray aan
                                Payload = new byte[100];
                                for (int i = 4; i < Length; i++)
                                {
                                    Payload[i - 4] = message[i];
                                }
                            }
                            return 1;
                        }
                        return -2;
                    }
                    //Als Fletcher16 en Checksum niet gelijk zijn, mag het bericht niet worden gemaakt, vraagt om nieuw bericht
                    else return -1;
                }
                //Magic heeft niet onze 2 bepaalde waarden, vraagt om nieuw bericht
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }

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
