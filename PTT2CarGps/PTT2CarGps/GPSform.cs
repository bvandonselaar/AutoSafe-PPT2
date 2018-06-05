using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using PTT2CarGps.locationClient;
using PTT2CarGps.Messages;

namespace PTT2CarGps
{
    public partial class GPSform : Form
    {
        SerialPort port;
        LocationClient locClient;
        public List<Car> Cars;
        byte[] Protocol = new byte[100];
        int ProtocolByteCount = 0;
        bool ReadingProtocol = false;
        byte LastByte = 0x00;
        Unpack Unpacker = new Unpack();
        Packet Packet = new Packet();
        Car selectedCar;
        public GPSform()
        {
            InitializeComponent();
            SerialComLB.Items.Add("Not connected");
            Cars = new List<Car>();
            locClient = new LocationClient();
            CarsBrake = new List<Car>();
            CarsEmergencyBrake = new List<Car>();
            groupBox_server.Enabled = false;
            comPortCBB.DataSource = SerialPort.GetPortNames();
            SerialTimer.Start();
        }
        private void AddCars()
        {
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                Car c = new Car(i);
                Point p = new Point(r.Next(0,300), r.Next(0, 200));
                c.Path.AddPosition(p);
                Cars.Add(c);
            }
        }
        private void TestSimulation()
        {
            if (Cars.Count == 0) AddCars();
            Random r = new Random();
            int[] signatures = new int[Cars.Count];
            Point[] points = new Point[Cars.Count];
            for (int i = 0; i < Cars.Count; i++)
            {
                signatures[i] = i;
                Point p;
                if (CarsBrake.Contains(Cars[i]))
                {
                    //Car brake
                    p = new Point
                    {
                        X = Cars[i].Path.Position.X + (int)(Cars[i].Path.Direction.X * 0.8) + r.Next(0, 5) - 2,
                        Y = Cars[i].Path.Position.Y + (int)(Cars[i].Path.Direction.Y * 0.8) + r.Next(0, 5) - 2
                    };
                }
                else if (CarsEmergencyBrake.Contains(Cars[i]))
                {
                    //Car emergencybrake
                    p = new Point
                    {
                        X = Cars[i].Path.Position.X + (int)(Cars[i].Path.Direction.X * 0.5) + r.Next(0, 5) - 2,
                        Y = Cars[i].Path.Position.Y + (int)(Cars[i].Path.Direction.Y * 0.5) + r.Next(0, 5) - 2
                    };
                }
                else
                {
                    //Car keeps driving randomly
                    p = new Point
                    {
                        X = Cars[i].Path.Position.X + Cars[i].Path.Direction.X + r.Next(0, 11) - 5,
                        Y = Cars[i].Path.Position.Y + Cars[i].Path.Direction.Y + r.Next(0, 11) - 5
                    };
                    if (Cars[i].Position.X > 280)
                    {
                        p.X -= 3;
                    }
                    else if (Cars[i].Position.X < 20)
                    {
                        p.X += 3;
                    }
                    if (Cars[i].Position.Y > 180)
                    {
                        p.Y -= 3;
                    }
                    else if (Cars[i].Position.Y < 20)
                    {
                        p.Y += 3;
                    }
                }
                points[i] = p;
            }
            AddPositions(points, signatures);
            DrawPositions();
        }

        private void SetSerialPort()
        {
            if(!string.IsNullOrEmpty(baudRateCBB.Text) && !string.IsNullOrEmpty(comPortCBB.Text))
            {
                try
                {
                    if(port != null) port.Close();
                    port = new SerialPort(comPortCBB.Text, Convert.ToInt32(baudRateCBB.Text), Parity.None, 8, StopBits.One);
                    port.Open();
                    port.ReadTimeout = 1000;
                    toolStripStatusLabel1.Text = "Connected";
                    SerialComLB.Items.Add("Connected");
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void ReadSerialTimer_Tick(object sender, EventArgs e)
        {
            if(port == null)
            {
                toolStripStatusLabel1.Text = "Not connected";
                return;
            }
            if (!port.IsOpen)
            {
                toolStripStatusLabel1.Text = "Not connected";
                return;
            }

            while(port.IsOpen && port.BytesToRead > 0)
            {
                try
                {
                    if (ReadingProtocol)
                    {
                        if(ProtocolByteCount < 99)
                        {
                            Protocol[ProtocolByteCount] = (byte)port.ReadByte();
                            ProtocolByteCount++;
                            if ((int)Protocol[2] == ProtocolByteCount)
                            {
                                Array.Resize(ref Protocol, ProtocolByteCount);
                                if (Unpacker.Deserialize(Protocol) == 1)
                                {
                                    Point[] locations = new Point[Unpacker.Payload.Length];
                                    int[] signatures = new int[Unpacker.Payload.Length];
                                    int counter = 0;

                                    for (int i = 0; i < Unpacker.Payload.Length - 4; i += 5)
                                    {
                                        int SignatureID = Unpacker.Payload[i];
                                        int Xvalue = (Unpacker.Payload[i + 1] << 8) | Unpacker.Payload[i + 2];
                                        int Yvalue = (Unpacker.Payload[i + 3] << 8) | Unpacker.Payload[i + 4];
                                        locations[counter] = new Point(Xvalue, Yvalue);
                                        signatures[counter] = SignatureID;
                                        counter++;
                                    }
                                    AddPositions(locations, signatures);
                                }
                                Protocol = new byte[100];
                                ProtocolByteCount = 0;
                                ReadingProtocol = false;
                            }
                        }
                        else
                        {
                            Protocol = new byte[100];
                            ProtocolByteCount = 0;
                            ReadingProtocol = false;
                        }
                    }
                    else
                    {
                        byte b = (byte)port.ReadByte();
                        if(LastByte == 0x0E && b == 0xE0)
                        {
                            ReadingProtocol = true;
                            Protocol[0] = 0x0E;
                            Protocol[1] = 0xE0;
                            ProtocolByteCount = 2;
                        }
                        LastByte = b;
                    }
                }
                catch (TimeoutException)
                {
                    port.Close();
                    toolStripStatusLabel1.Text = "Wrong baud rate";
                }
            }

            if (!port.IsOpen)
            {
                toolStripStatusLabel1.Text = "Not connected";
                if(SerialComLB.Items[SerialComLB.Items.Count - 1].ToString() != "Connection lost")
                {
                    SerialComLB.Items.Add("Connection lost");
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Connected";
            }


            while (SerialComLB.Items.Count > 30)
            {
                SerialComLB.Items.RemoveAt(0);
            }
        }
        

        public void AddPositions(Point[] positions, int[] signatures)
        {
            //Check for errors
            if (positions == null) return;
            if (positions.Length != signatures.Length) return;

            //Update car positions
            for(int i = 0; i < signatures.Length; i++)
            {
                bool found = false;
                //find car with right signature
                foreach(Car c in Cars)
                {
                    if (c.SignatureId == signatures[i])
                    {
                        //Add position
                        found = true;
                        c.AddPosition(positions[i]);
                        break;
                    }
                }
                //Add new car if new signature is found
                if (!found) Cars.Add(new Car(signatures[i]));
            }
            DetectCollision();
            DrawPositions();
            UpdateUiData();
        }

        private void UpdateUiData()
        {
            foreach(Car c in Cars)
            {
                if (!CarsLB.Items.Contains(c))
                {
                    CarsLB.Items.Add(c);
                }
            }
            List<Car> CarsToRemove = new List<Car>();
            foreach(object o in CarsLB.Items)
            {
                if(o is Car)
                {
                    if(!Cars.Contains(o as Car))
                    {
                        CarsToRemove.Add(o as Car);
                    }
                }
            }
            foreach(Car c in CarsToRemove)
            {
                CarsLB.Items.Remove(c);
            }
            if (selectedCar != null)
            {
                CarInfoLBL.Text = selectedCar.ToString() + Environment.NewLine + "X: " + selectedCar.Path.Position.X + Environment.NewLine + "Y: " + selectedCar.Path.Position.Y;
            }
        }

        private void DrawPositions()
        {
            CarLocationsPB.Invalidate();
        }

        private async void button_initiate_Click(object sender, EventArgs e)
        {
            string ip = textBox_ip.Text;
            int port = (int)numeric_port.Value;
            try
            {
                await locClient.Connect(ip, port);
                ReceiveMessages();
                groupBox_initiate.Enabled = false;
                groupBox_server.Enabled = true;
            }
            catch(FormatException)
            {
                MessageBox.Show("Ip format is not correct");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Wrong host Ip");
            }
        }

        private async Task ReceiveMessages()
        {
            bool loop = true;
            while (loop)
            {
                try
                {
                    
                    await locClient.Read();
                    textBox_input.AppendText("Incoming: " + locClient.Bytes[0].ToString());
                    
                    for (int i = 1; i < locClient.Bytes.Length; i++)
                    {
                        textBox_input.AppendText(" - " + locClient.Bytes[i].ToString());
                    }
                    textBox_input.AppendText("\n");
                }
                catch (ObjectDisposedException)
                {
                        loop = false;
                        MessageBox.Show("Disconnected");
                }
            }
        }

        private void ConnectBTTN_Click_1(object sender, EventArgs e)
        {
            SetSerialPort();
        }

        private void refreshComPortBTTN_Click_1(object sender, EventArgs e)
        {
            comPortCBB.DataSource = SerialPort.GetPortNames();
        }

        private void ClearBTTN_Click(object sender, EventArgs e)
        {
            Cars.Clear();
        }

        private void TestBTTN_Click(object sender, EventArgs e)
        {
            TestSimulation();
        }

        private void closeConnectionBTTN_Click_1(object sender, EventArgs e)
        {
            port.Close();
        }

        private void CarLocationsPB_Paint_1(object sender, PaintEventArgs e)

        {
            Graphics Canvas = e.Graphics;
            Canvas.ScaleTransform((float)2, (float)2);
            Random r = new Random(10);
            foreach (Car c in Cars)
            {
                c.Draw(Canvas, Color.FromArgb(r.Next(100, 255), r.Next(100, 255), r.Next(100, 255)));
            }

            Canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
        }

        private void CarsLB_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selectedCar = (Car)CarsLB.SelectedItem;
            UpdateUiData();
        }
        List<Car> CarsBrake;
        List<Car> CarsEmergencyBrake;
        private void DetectCollision()
        {
            CarsBrake.Clear();
            CarsEmergencyBrake.Clear();
            int MinDistanceBrake = 60;
            int MinDistanceEmergencyBrake = 30;
            foreach(Car Car1 in Cars)
            {
                foreach(Car Car2 in Cars)
                {
                    if(Car1 != Car2)
                    {
                        //------------------------------------------------
                        //Predict next car locations
                        //------------------------------------------------
                        Point NextPosCar1 = new Point(
                                Car1.Position.X + Car1.Path.Direction.X * 2,
                                Car1.Position.Y + Car1.Path.Direction.Y * 2
                            );
                        Point NextPosCar2 = new Point(
                                Car2.Position.X + Car2.Path.Direction.X * 2,
                                Car2.Position.Y + Car2.Path.Direction.Y * 2
                            );

                        //------------------------------------------------
                        //Check if distance is decreasing
                        //------------------------------------------------
                        //New Distance
                        //------------------------------------------------
                        int dX = NextPosCar1.X - NextPosCar2.X;
                        int dY = NextPosCar1.Y - NextPosCar2.Y;
                        double NewDistance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
                        //------------------------------------------------
                        //Old Distance
                        //------------------------------------------------
                        dX = Car1.Position.X - Car2.Position.X;
                        dY = Car1.Position.Y - Car2.Position.Y;
                        double OldDistance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
                        //------------------------------------------------
                        //Comparing
                        //------------------------------------------------
                        bool Decreasing = NewDistance < OldDistance;
                        bool Brake = NewDistance < MinDistanceBrake && NewDistance > MinDistanceEmergencyBrake && Decreasing;
                        bool EmergencyBrake = NewDistance < MinDistanceEmergencyBrake && Decreasing;

                        //------------------------------------------------
                        //Check if the cars are driving towards eachother
                        //------------------------------------------------
                        //Car1
                        //------------------------------------------------
                        dX = NextPosCar1.X - Car2.Position.X;
                        dY = NextPosCar1.Y - Car2.Position.Y;
                        double Distance_Car1NewPos_Car2OldPos = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
                        bool Car1DrivesTowardsCar2 = Distance_Car1NewPos_Car2OldPos < OldDistance;
                        //------------------------------------------------
                        //Car2
                        //------------------------------------------------
                        dX = NextPosCar2.X - Car1.Position.X;
                        dY = NextPosCar2.Y - Car1.Position.Y;
                        double Distance_Car2NewPos_Car1OldPos = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));
                        bool Car2DrivesTowardsCar1 = Distance_Car2NewPos_Car1OldPos < OldDistance;

                        if (Brake)
                        {
                            if (!CarsBrake.Contains(Car1) && Car1DrivesTowardsCar2)
                            {
                                CarsBrake.Add(Car1);
                            }
                            if (!CarsBrake.Contains(Car2) && Car2DrivesTowardsCar1)
                            {
                                CarsBrake.Add(Car2);
                            }
                        }
                        else if(EmergencyBrake)
                        {
                            if (!CarsEmergencyBrake.Contains(Car1) && Car1DrivesTowardsCar2)
                            {
                                CarsEmergencyBrake.Add(Car1);
                            }
                            if (!CarsEmergencyBrake.Contains(Car2) && Car2DrivesTowardsCar1)
                            {
                                CarsEmergencyBrake.Add(Car2);
                            }
                        }
                    }
                }
            }
            foreach(Car c in CarsBrake)
            {
                if (!c.IsLost)
                {
                    BrakeWarning bw = new BrakeWarning(DateTime.Now, c.Position);
                    c.SendMessage(bw);
                    //locClient.SendWarning((byte)c.SignatureId);
                }
            }
            foreach (Car c in CarsEmergencyBrake)
            {
                if (!c.IsLost)
                {
                    EmergencyBrakeWarning bw = new EmergencyBrakeWarning(DateTime.Now, c.Position);
                    c.SendMessage(bw);
                    //locClient.SendEmergencyWarning((byte)c.SignatureId);
                }
            }
        }

        private void SetIpBTTN_Click(object sender, EventArgs e)
        {
            if(selectedCar != null)
            {
                
            }
        }
    }
}
