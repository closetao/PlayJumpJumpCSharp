using ADBTool.AdbHelp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayJumpJump
{
    public partial class Form1 : Form
    {
        #region [成员变量]
        private CmdHelp cmdHelp = new CmdHelp();
        private double _WidthCompressRate = 0;
        private double _HeightCompressRate = 0;
        private List<Point> _PointList = new List<Point>();
        private string _LocalImagFileName = System.Environment.CurrentDirectory +  "\\screenshot.png";

        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //注册事件
            CmdHelp.EventReceiveData += new CmdHelp.DelegateReceiveData(cmdHelp_EventReceiveData);
            CmdHelp.EventReceiveThreadData += new CmdHelp.DelegateReceiveThreadData(CmdHelp_EventReceiveThreadData);
            CmdHelp.EventReceiveThreadErrorData += new CmdHelp.DelegateReceiveThreadErrorData(CmdHelp_EventReceiveThreadErrorData);

            if (System.IO.File.Exists(_LocalImagFileName))
            {
                pictureBox1.ImageLocation = _LocalImagFileName;
                Size size = GetImageSize(_LocalImagFileName);
                _WidthCompressRate = (double)size.Width / (double)pictureBox1.Width;
                _HeightCompressRate = (double)size.Height / (double)pictureBox1.Height;
            }
            
            Task.Factory.StartNew(TickFunc);
        }

        //接收到数据
        private void cmdHelp_EventReceiveData(string data)
        {

        }

        //接收错误数据
        private void CmdHelp_EventReceiveThreadErrorData(string data)
        {
        }


        void CmdHelp_EventReceiveThreadData(string data)
        {
        }

        private Size GetImageSize(string fileName)
        {
            Size size;
            using (Bitmap img = new Bitmap(fileName))
            {
                size = img.Size;
            }
            return size;
        }

        private int Distance(Point a, Point b)
        {
            return (int)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        private void TickFunc()
        {
            while (true)
            {
                try
                {
                    string cmdStr = "shell /system/bin/screencap -p /sdcard/screenshot.png";
                    string cmdStr1 = "pull /sdcard/screenshot.png " + _LocalImagFileName;
                    cmdHelp.SendAdbCmd(cmdStr);
                    Thread.Sleep(50);
                    cmdHelp.SendAdbCmd(cmdStr1);
                    Thread.Sleep(50);
                    this.Invoke((EventHandler)(delegate
                    {
                        if (System.IO.File.Exists(_LocalImagFileName))
                        {
                            pictureBox1.ImageLocation = _LocalImagFileName;
                        }
                    }));
                    Thread.Sleep(50);
                }
                catch (Exception)
                {
                    
                }
                
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox picbox = sender as PictureBox;
            Point point = picbox.PointToClient(Control.MousePosition);
            Graphics g = picbox.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿 

            if (_PointList.Count < 1)
            {
                _PointList.Add(point);
                g.DrawEllipse(new Pen(Color.Red, 2), point.X - 4, point.Y - 4, 8, 8);
            }
            else
            {
                _PointList.Add(point);
                g.DrawEllipse(new Pen(Color.Green, 2), point.X - 4, point.Y - 4, 8, 8);
                g.DrawLine(new Pen(Color.Blue, 2), _PointList[0], _PointList[1]);

                int distance = Distance(_PointList[0], _PointList[1]);
                int sec = (int)(distance * Convert.ToDouble( tb_Elasticity.Text.Trim()));
                string cmd = string.Format("shell input swipe {0} {1} {2} {3} {4}",
                    (int)(_PointList[0].X * _WidthCompressRate),
                    (int)(_PointList[0].Y * _HeightCompressRate),
                    (int)(_PointList[1].X * _WidthCompressRate),
                   (int)(_PointList[1].Y * _HeightCompressRate),
                   sec);
                cmdHelp.SendAdbCmd(cmd);
                Console.WriteLine(cmd);


                //string cmdStr = "shell /system/bin/screencap -p /sdcard/screenshot.png";
                //string cmdStr1 = "pull /sdcard/screenshot.png " + _LocalImagFileName;
                ////  ShowSendCmdInfo(cmdStr);
                //cmdHelp.SendAdbCmd(cmdStr);
                //Thread.Sleep(50);
                //cmdHelp.SendAdbCmd(cmdStr1);
                //Thread.Sleep(50);
                //pictureBox1.ImageLocation = _LocalImagFileName;
                
                _PointList.Clear();
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                _PointList.Clear();
            }
        }
    }
}
