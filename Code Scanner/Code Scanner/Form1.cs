using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.ComplexFilters;
using AForge.Imaging.ColorReduction;
using AForge.Controls;
using AForge.Genetic;
using AForge.Math;
using AForge.Math.Geometry;
using AForge.Math.Metrics;
using AForge.Math.Random;
using AForge.Neuro;
using AForge.Neuro.Learning;
using AForge.Vision;
using AForge.Vision.Motion;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using ZXing.Common;
using ZXing.QrCode;

namespace Code_Scanner
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //Discover all capture devices and add to the combo box
                CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach(FilterInfo Device in CaptureDevice)
                {
                    CamCombo.Items.Add(Device.Name);
                }
                CamCombo.SelectedIndex = 0;
            }
            catch
            {
                CamCombo.Items.Add("No Camera Device Available!");
                CamCombo.SelectedIndex = 0;
            }
            FinalFrame = new VideoCaptureDevice();

            /* //Here for encoding options
            string[] type = new string[20] {"AZTEC", "CODABAR", "CODE_39", "CODE_93", "CODE_128", "DATA_MATRIX", "EAN_8", "EAN_13", "ITF", "MAXICODE", "PDF_417", "QR_CODE", "RSS_14", "RSS_EXPANDED", "UPC_A", "UPC_E", "ALL_1D", "UPC_EAN_EXTENSION", "MSI", "PLESSEY" };
            for (int i = 0; i < 20; i++)
            {
                CodeCombo.Items.Add(type[i]);
            }
             
            string[] type = new string[2] { "CODE_128", "QR_CODE" }; //First two encoding options working
            for (int i = 0; i < 2; i++)
            {
                CodeCombo.Items.Add(type[i]);
            }
            */
        }


        private void btnStart_Click(object sender, EventArgs e) //Start video capture device and show stream in picture box
        {
            try
            {
                FinalFrame = new VideoCaptureDevice(CaptureDevice[CamCombo.SelectedIndex].MonikerString);
                FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
                pictureBox1.Visible = true;
                FinalFrame.Start();
                //timer1.Enabled = false;
                //Stop the decode thread here
            }
            catch
            {
                MessageBox.Show("Please select a camera device!");
            }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            //throw new NotImplementedException();
        }

        private void btnStop_Click(object sender, EventArgs e) //Stop video capture device and decoding function
        {
            pictureBox1.Image = null;
            FinalFrame.Stop();
            //timer1.Enabled = false;
        }

        private void btnEncode_Click(object sender, EventArgs e) //Take selected information and command encoding of info, Possibly do this in a different form.
        {

        }

        private void btnDecode_Click(object sender, EventArgs e) //Start decoding function only if video capture device is available or if image is loaded into picture box
        {

        }

        private void btnSave_Click(object sender, EventArgs e) //Save current frame image
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|GIF|*.gif";
                if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pictureBox1.Image.Save(s.FileName);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) //To be determined for use!!!!!!!
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) //Open image for decoding
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)//Exit Application, verify releasing of memory, closing of video stream, and closing of decoding/encoding functions
        {
            FinalFrame.Stop();
            Application.Exit();
        }
    }
}
