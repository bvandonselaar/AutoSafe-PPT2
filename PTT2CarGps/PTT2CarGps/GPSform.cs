﻿using System;
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

namespace PTT2CarGps
{
    public partial class GPSform : Form
    {
        SerialPort port;
        public List<Car> Cars;
        public GPSform()
        {
            InitializeComponent();
            Cars = new List<Car>();
            comPortCBB.DataSource = SerialPort.GetPortNames();
            SerialTimer.Start();
        }
        private void AddCars()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                Car c = new Car(i);
                Point p = new Point(r.Next(0, 500) + 50, r.Next(0, 300) + 50);
                c.Path.AddPosition(p);
                Cars.Add(c);
            }
        }
        private void TestTrailDrawing()
        {
            if (Cars.Count == 0) AddCars();
            Random r = new Random();
            int[] signatures = new int[Cars.Count];
            Point[] points = new Point[Cars.Count];
            for (int i = 0; i < Cars.Count; i++)
            {
                signatures[i] = i;
                Point p = new Point
                {
                    X = Cars[i].Path.Position.X + Cars[i].Path.Speed.X + r.Next(0, 5) - 2,
                    Y = Cars[i].Path.Position.Y + Cars[i].Path.Speed.Y + r.Next(0, 5) - 2
                };
                points[i] = p;
            }
            AddPositions(points, signatures);
            DrawPositions();
        }

        private void refreshComPortBTTN_Click(object sender, EventArgs e)
        {
            comPortCBB.DataSource = SerialPort.GetPortNames();
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
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (System.UnauthorizedAccessException ex)
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

            port.Write("send data");

            while(port.IsOpen && port.BytesToRead > 0)
            {
                toolStripStatusLabel1.Text = "Incomming data...";
                char[] data;
                try
                {
                    data = port.ReadExisting().ToCharArray();
                    SerialComLB.Items.Add(new String(data));
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
            }
            else
            {
                toolStripStatusLabel1.Text = "Connected";
            }


            while (SerialComLB.Items.Count > 35)
            {
                SerialComLB.Items.RemoveAt(0);
            }
        }

        private void ConnectBTTN_Click(object sender, EventArgs e)
        {
            SetSerialPort();
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
                        c.Path.AddPosition(positions[i]);
                        break;
                    }
                }
                //Add new car if new signature is found
                if (!found) Cars.Add(new Car(signatures[i]));
            }

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
        }

        private void DrawPositions()
        {
            CarLocationsPB.Invalidate();
        }

        private void CarLocationsPB_Paint(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Random r = new Random(10 );
            foreach (Car c in Cars)
            {
                Pen pen = new Pen(Color.FromArgb(r.Next(100,255),r.Next(100,255),r.Next(100,255)));
                Point pos = c.Path.GetPositions[c.Path.GetPositions.Count - 1].Location;
                pos.Offset(-15, -15);
                Canvas.DrawEllipse
                    (
                        pen,
                        new Rectangle(pos, new Size(30, 30))
                    );
                if (c.Path.GetPoints.Length > 1)
                {
                    Canvas.DrawLines(pen, c.Path.GetPoints);
                    Canvas.DrawLine
                        (
                            new Pen(Color.White, 2),
                            c.Path.GetPositions[c.Path.GetPositions.Count - 1].Location,
                            new Point
                            (
                                c.Path.GetPositions[c.Path.GetPositions.Count - 1].Location.X + c.Path.Speed.X,
                                c.Path.GetPositions[c.Path.GetPositions.Count - 1].Location.Y + c.Path.Speed.Y
                            )
                        );
                }
                Canvas.DrawString("ID:" + c.SignatureId, DefaultFont, new SolidBrush(Color.White), pos);
            }
            Canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestTrailDrawing();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cars.Clear();
        }

        private void CarsLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Car selectedCar = (Car)CarsLB.SelectedItem;
            CarInfoLBL.Text = selectedCar.ToString();
        }
    }
}
