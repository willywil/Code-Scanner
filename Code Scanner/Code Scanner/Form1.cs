using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MessagingToolkit.QRCode.Codec;
//using MessagingToolkit.QRCode.Codec.Data;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
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
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        
    }
}
