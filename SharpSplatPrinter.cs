using SharpSplatPrinter.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace SharpSplatPrinter
{
    public partial class SharpSplatPrinter : Form
    {
        public bool ArduinoIdeInstalled = false;
        public bool MinGwInstalled = false;
        public bool ArduinoBoardFound = false;
        public string ArduinoBoardType = "none";
        public string ArduinoComPort = "none";

        public Bitmap ImageFile;
        
        public SharpSplatPrinter()
        {
            InitializeComponent();

            BackgroundWorker1.DoWork += new DoWorkEventHandler(DoBackgroundWork);
        }

        private void SharpSplatPrinter_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("./util/"))
            {
                MessageBox.Show("The util folder needs to exist, otherwise I'm going to fail a lot D:");
            }

            ImageFile = new Bitmap(PngPictureBox.Image);

            if (File.Exists(@"C:\Program Files (x86)\Arduino\hardware\tools\avr\bin\avrdude.exe") || File.Exists(@"C:\Program Files\Arduino\hardware\tools\avr\bin\avrdude.exe"))
            {
                ArduinoIdeInstalled = true;
                ArduinoIdeLabel.ForeColor = Color.Green;
                ArduinoIdeLabel.Text = "Arduino IDE is installed in your system.";
            }
            else
            {
                ArduinoIdeLabel.ForeColor = Color.Red;
                ArduinoIdeLabel.Text = "Arduino IDE is not installed correctly in your system. Please install it and make sure you have avrdude in the right path.";
            }

            if (File.Exists(@"C:\MinGW\bin\mingw32-make.exe"))// || File.Exists(@"C:\MinGW\bin\make.exe"))
            {
                MinGwInstalled = true;
                MinGwLabel.ForeColor = Color.Green;
                MinGwLabel.Text = "MinGW is installed in your system.";
            }
            else
            {
                ArduinoIdeLabel.ForeColor = Color.Red;
                ArduinoIdeLabel.Text = "MinGW is not installed in your system. Please install it and make sure make is in the right path.";
            }

            SearchForArduinoBoard();
        }

        public void SearchForArduinoBoard(bool AppendReady = true)
        {
            ArduinoBoardFound = false;
            ArduinoBoardType = "none";
            ArduinoComPort = "none";

            ManagementObjectSearcher Searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort WHERE (Caption LIKE '%Arduino Leonardo%' OR Caption LIKE '%Arduino Micro%' OR Caption LIKE '%Arduino Pro Micro%')");
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
                BoardLabel.Text = "Your Arduino board was found at port " + ArduinoComPort + ".";
            }
            else
            {
                BoardLabel.ForeColor = Color.Red;
                BoardLabel.Text = "An Arduino atmega32u4 was not found. Try using the board's reset button.";
            }

            if (ArduinoIdeInstalled == true && MinGwInstalled == true && ArduinoBoardFound == true)
            {
                InjectButton.Invoke((MethodInvoker)delegate { InjectButton.Enabled = true; });

                if (AppendReady)
                {
                    LogsTextBox.AppendText("Ready. \n");
                }
            }
            else
            {
                InjectButton.Invoke((MethodInvoker)delegate { InjectButton.Enabled = false; });
            }
        }

        private void RefreshBoardButton_Click(object sender, EventArgs e)
        {
            SearchForArduinoBoard();
        }

        private void ChooseImageButton_Click(object sender, EventArgs e)
        {
            if (ChooseImageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap NewImage = new Bitmap(ChooseImageDialog.FileName);
                    if (NewImage.Height != 120 && NewImage.Width != 320)
                    {
                        MessageBox.Show("This is not a 320x120 image!");
                        return;
                    }

                    this.ImageFile = NewImage;
                    this.PngPictureBox.Image = Image.FromFile(ChooseImageDialog.FileName);
                }
                catch (Exception E)
                {
                    MessageBox.Show("An error has occurred with your image file:\n" + E.ToString(), "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InjectButton_Click(object sender, EventArgs e)
        {
            BackgroundWorker1.RunWorkerAsync();
        }

        private void DoBackgroundWork(object Sender, DoWorkEventArgs E)
        {
            string UtilFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "util\\");
            
            if (!ArduinoIdeInstalled || !MinGwInstalled || !ArduinoBoardFound)
            {
                // Nothing to do here!
                LogsTextBox.AppendText("I am not supposed to do this.\n");
                goto End;
            }

            RefreshBoardButton.Invoke((MethodInvoker)delegate { RefreshBoardButton.Enabled = false; });
            InjectButton.Invoke((MethodInvoker)delegate { InjectButton.Enabled = false; });
            
            string Data = string.Empty;
            string Error = string.Empty;

            byte ImageError = 0;

            string MinGwLocation = @"C:\MinGW\bin\mingw32-make.exe";

            // Build image.c
            LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("Building image.c file...\n"); });
            if (Png2C.Convert(ImageFile, out ImageError) == false)
            {
                switch (ImageError)
                {
                    case 1:
                        LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("ERROR: The image is not valid.\n"); });
                        break;

                    case 2:
                        LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("ERROR: The image is not 320x120.\n"); });
                        break;
                }

                LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("Aborting...\n"); });
                goto End;
            }

            // Run mingw32-make
            LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("Running MinGw32...\n"); });
            ProcessRunner.RunBatch(MinGwLocation, out Data, out Error, UtilFolder);

            if (!string.IsNullOrEmpty(Data))
            {
                if (Data.Contains("failed") || Data.Contains("error"))
                {
                    LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText(Data + "\n"); });
                }
            }
            if (!string.IsNullOrWhiteSpace(Error))
            {
                if (!Error.Contains("grep")) // Grep not found will always throw a warning and honestly we actually don't need that.
                {
                    LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText(Error + "\n"); });
                }
            }

            // Try to reset the arduino board.
            LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("Trying to quick reset Arduino...\n"); });
            ProcessRunner.RunBatch("mode " + ArduinoComPort + ": baud=12 > nul\ntimeout 2 > nul", out Data, out Error);

            //if (!string.IsNullOrWhiteSpace(Data))
            //{
            //    LogsTextBox.AppendText(Data + "\n");
            //}
            //if (!string.IsNullOrWhiteSpace(Error))
            //{
            //    LogsTextBox.AppendText(Error + "\n");
            //}

            // Reidentify Arduino board
            SearchForArduinoBoard(false);

            // Try to inject the .hex file
            LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("Trying to inject the .hex file...\n"); });

            string JoystickHexPath = Path.Combine(UtilFolder, "Joystick.hex");
            string Command = "\"";
            Command += @"C:\Program Files (x86)\Arduino\hardware\tools\avr\bin\avrdude.exe";
            Command += "\" -C\"";
            Command += @"C:\Program Files (x86)\Arduino\hardware\tools\avr\etc\avrdude.conf";
            Command += "\" -v -patmega32u4 -cavr109 -P";
            Command += ArduinoComPort;
            Command += " -b57600 -D -Uflash:w:";
            Command += "\"";
            Command += JoystickHexPath;
            Command += "\":i";

            LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText(Command); });

            ProcessRunner.RunBatch(Command, out Data, out Error, UtilFolder);
            if (!string.IsNullOrEmpty(Data))
            {
                LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText(Data + "\n"); });
            }
            if (!string.IsNullOrWhiteSpace(Error))
            {
                LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText(Error + "\n"); });
            }

            End:
            LogsTextBox.Invoke((MethodInvoker)delegate { LogsTextBox.AppendText("Process ended.\n"); });

            RefreshBoardButton.Invoke((MethodInvoker)delegate { RefreshBoardButton.Enabled = true; });
            InjectButton.Invoke((MethodInvoker)delegate { InjectButton.Enabled = true; });

            // It's not even necessary to delete these but they annoy me.
            try
            {
                Directory.Delete(Path.Combine(UtilFolder, "obj"), true);
                Directory.Delete(Path.Combine(UtilFolder, "-p"), true);
                File.Delete(Path.Combine(UtilFolder, "Joystick.sym"));
                File.Delete(Path.Combine(UtilFolder, "Joystick.lss"));
                File.Delete(Path.Combine(UtilFolder, "Joystick.bin"));
                File.Delete(Path.Combine(UtilFolder, "Joystick.eep"));
                File.Delete(Path.Combine(UtilFolder, "Joystick.hex"));
                File.Delete(Path.Combine(UtilFolder, "Joystick.map"));
                File.Delete(Path.Combine(UtilFolder, "Joystick.elf"));
            }
            catch { }
        }
    }
}
