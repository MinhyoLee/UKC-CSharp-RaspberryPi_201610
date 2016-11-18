using System;
using System.Net; // Added for httpwebrequest();
using System.IO; // Added for streamreader();
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PiFace_Controller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer0.Interval = 100;
            timer0.Start();
        }

        private void timer0_Tick(object sender, EventArgs e)
        {
            HttpWebRequest oreq = (HttpWebRequest)WebRequest.Create("https://data.drake.kr/piweb/status.dat");
            HttpWebResponse ores = (HttpWebResponse)oreq.GetResponse();
            StreamReader reader = new StreamReader(ores.GetResponseStream());
            String str = reader.ReadToEnd();
            if (str.Length >= 12)
            {
                lbDebug.Text = str.Length.ToString() + "," + str[0].ToString() + str[1].ToString() + str[2].ToString() + str[3].ToString() + str[4].ToString() + str[5].ToString() + str[6].ToString() + str[7].ToString();
                if (str[0] > '0') cbled0.Checked = true; else cbled0.Checked = false;
                if (str[1] > '0') cbled1.Checked = true; else cbled1.Checked = false;
                if (str[2] > '0') cbled2.Checked = true; else cbled2.Checked = false;
                if (str[3] > '0') cbled3.Checked = true; else cbled3.Checked = false;
                if (str[4] > '0') cbled4.Checked = true; else cbled4.Checked = false;
                if (str[5] > '0') cbled5.Checked = true; else cbled5.Checked = false;
                if (str[6] > '0') cbled6.Checked = true; else cbled6.Checked = false;
                if (str[7] > '0') cbled7.Checked = true; else cbled7.Checked = false;
                if (str[8] > '0') cbbtn0.Checked = true; else cbbtn0.Checked = false;
                if (str[9] > '0') cbbtn1.Checked = true; else cbbtn1.Checked = false;
                if (str[10] > '0') cbbtn2.Checked = true; else cbbtn2.Checked = false;
                if (str[11] > '0') cbbtn3.Checked = true; else cbbtn3.Checked = false;
            }
            ores.Close();
        }

        private void cbled0_CheckedChanged(object sender, EventArgs e)
        {
            String str = "https://data.drake.kr/piweb/led.php";
            if (cbled0.Checked)
                str += "?led0=on";
            else str += "?";
            if (cbled1.Checked)
                str += "&led1=on";
            if (cbled2.Checked)
                str += "&led2=on";
            if (cbled3.Checked)
                str += "&led3=on";
            if (cbled4.Checked)
                str += "&led4=on";
            if (cbled5.Checked)
                str += "&led5=on";
            if (cbled6.Checked)
                str += "&led6=on";
            if (cbled7.Checked)
                str += "&led7=on";
            if (cbbtn0.Checked)
                str += "&btn0=on";
            if (cbbtn1.Checked)
                str += "&btn1=on";
            if (cbbtn2.Checked)
                str += "&btn2=on";
            if (cbbtn3.Checked)
                str += "&btn3=on";
            lbDebug.Text = str;
            HttpWebRequest oreq = (HttpWebRequest)WebRequest.Create(str);
            HttpWebResponse ores = (HttpWebResponse)oreq.GetResponse();
            StreamReader reader = new StreamReader(ores.GetResponseStream());
            str = reader.ReadToEnd();
            Thread.Sleep(1000);
            ores.Close();
        }
    }
}
