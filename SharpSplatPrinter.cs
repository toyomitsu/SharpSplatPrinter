using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Management;

namespace SharpSplatPrinter
{
    public partial class SharpSplatPrinter : Form
    {
        public bool ArduinoIdeInstalled = false;
        public bool MinGwInstalled = false;
        public bool ArduinoBoardFound = false;

        public string ArduinoBoardType = "none";
        public string ArduinoComPort = "none";

        public SharpSplatPrinter()
        {
            InitializeComponent();
        }

        private void SharpSplatPrinter_Load(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Program Files (x86)\Arduino\hardware\tools\avr\bin\avrdude.exe") || File.Exists(@"C:\Program Files\Arduino\hardware\tools\avr\bin\avrdude.exe"))
            {
                ArduinoIdeInstalled = true;
                ArduinoIdeLabel.ForeColor = Color.Green;
                ArduinoIdeLabel.Text = "Arduino IDE is installed in your system.";

            }
            else
            {
                ArduinoIdeLabel.ForeColor = Color.Red;
                ArduinoIdeLabel.Text = "Arduino IDE is not installed correctly in your system. Please install it and reopen the tool.";
            }

            if (File.Exists(@"C:\MinGW\bin\mingw32-make.exe") || File.Exists(@"C:\MinGW\bin\make.exe"))
            {
                MinGwInstalled = true;
                MinGwLabel.ForeColor = Color.Green;
                MinGwLabel.Text = "MinGW is installed in your system.";
            }
            else
            {
                ArduinoIdeLabel.ForeColor = Color.Red;
                ArduinoIdeLabel.Text = "MinGW is not correctly installed in your system. Please install it and reopen the tool.";
            }

            SearchForArduinoBoard();
        }

        public void SearchForArduinoBoard()
        {
            ArduinoBoardFound = false;
            ArduinoBoardType = "none";
            ArduinoComPort = "none";

            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort WHERE Caption LIKE '%Leonardo%'");
            foreach (ManagementObject QueryObj in Searcher.Get())
            {
                if (((string)QueryObj["DeviceID"]).Contains("COM"))
                {
                    ArduinoBoardFound = true;
                    ArduinoBoardType = "atmega32u4";
                    ArduinoComPort = (string)QueryObj["DeviceID"];
                }
            }

            if (ArduinoBoardFound)
            {
                BoardLabel.ForeColor = Color.Green;
                BoardLabel.Text = "An atmega32u4 board was found at port " + ArduinoComPort + ".";
            }
            else
            {
                BoardLabel.ForeColor = Color.Red;
                BoardLabel.Text = "An atmega32u4 was not found. Try using the reset button.";
            }

            if (ArduinoIdeInstalled == true && MinGwInstalled == true && ArduinoBoardFound == true)
            {
                InjectButton.Enabled = true;

                LogsTextBox.AppendText("Ready. \n");
            }
            else
            {
                InjectButton.Enabled = false;
            }
        }

        private void RefreshBoardButton_Click(object sender, EventArgs e)
        {
            SearchForArduinoBoard();
        }
    }
}
