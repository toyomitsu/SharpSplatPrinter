namespace SharpSplatPrinter
{
    partial class SharpSplatPrinter
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharpSplatPrinter));
            this.ArduinoIdeLabel = new System.Windows.Forms.Label();
            this.MinGwLabel = new System.Windows.Forms.Label();
            this.PngPictureBox = new System.Windows.Forms.PictureBox();
            this.BoardLabel = new System.Windows.Forms.Label();
            this.ChooseImageButton = new System.Windows.Forms.Button();
            this.InjectButton = new System.Windows.Forms.Button();
            this.LogsTextBox = new System.Windows.Forms.RichTextBox();
            this.RefreshBoardButton = new System.Windows.Forms.Button();
            this.TipLabel = new System.Windows.Forms.Label();
            this.ChooseImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.PngPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ArduinoIdeLabel
            // 
            this.ArduinoIdeLabel.AutoSize = true;
            this.ArduinoIdeLabel.Location = new System.Drawing.Point(12, 9);
            this.ArduinoIdeLabel.Name = "ArduinoIdeLabel";
            this.ArduinoIdeLabel.Size = new System.Drawing.Size(61, 13);
            this.ArduinoIdeLabel.TabIndex = 1;
            this.ArduinoIdeLabel.Text = "Checking...";
            // 
            // MinGwLabel
            // 
            this.MinGwLabel.AutoSize = true;
            this.MinGwLabel.Location = new System.Drawing.Point(12, 22);
            this.MinGwLabel.Name = "MinGwLabel";
            this.MinGwLabel.Size = new System.Drawing.Size(61, 13);
            this.MinGwLabel.TabIndex = 2;
            this.MinGwLabel.Text = "Checking...";
            // 
            // PngPictureBox
            // 
            this.PngPictureBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PngPictureBox.ErrorImage")));
            this.PngPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PngPictureBox.Image")));
            this.PngPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("PngPictureBox.InitialImage")));
            this.PngPictureBox.Location = new System.Drawing.Point(268, 80);
            this.PngPictureBox.Name = "PngPictureBox";
            this.PngPictureBox.Size = new System.Drawing.Size(320, 120);
            this.PngPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PngPictureBox.TabIndex = 3;
            this.PngPictureBox.TabStop = false;
            // 
            // BoardLabel
            // 
            this.BoardLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BoardLabel.AutoSize = true;
            this.BoardLabel.Location = new System.Drawing.Point(12, 35);
            this.BoardLabel.Name = "BoardLabel";
            this.BoardLabel.Size = new System.Drawing.Size(161, 13);
            this.BoardLabel.TabIndex = 4;
            this.BoardLabel.Text = "Looking for your Arduino board...";
            // 
            // ChooseImageButton
            // 
            this.ChooseImageButton.Location = new System.Drawing.Point(267, 206);
            this.ChooseImageButton.Name = "ChooseImageButton";
            this.ChooseImageButton.Size = new System.Drawing.Size(152, 34);
            this.ChooseImageButton.TabIndex = 5;
            this.ChooseImageButton.Text = "choose image";
            this.ChooseImageButton.UseVisualStyleBackColor = true;
            this.ChooseImageButton.Click += new System.EventHandler(this.ChooseImageButton_Click);
            // 
            // InjectButton
            // 
            this.InjectButton.Enabled = false;
            this.InjectButton.Location = new System.Drawing.Point(436, 206);
            this.InjectButton.Name = "InjectButton";
            this.InjectButton.Size = new System.Drawing.Size(152, 34);
            this.InjectButton.TabIndex = 6;
            this.InjectButton.Text = "inject to board";
            this.InjectButton.UseVisualStyleBackColor = true;
            this.InjectButton.Click += new System.EventHandler(this.InjectButton_Click);
            // 
            // LogsTextBox
            // 
            this.LogsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.LogsTextBox.Location = new System.Drawing.Point(14, 80);
            this.LogsTextBox.Name = "LogsTextBox";
            this.LogsTextBox.ReadOnly = true;
            this.LogsTextBox.Size = new System.Drawing.Size(247, 160);
            this.LogsTextBox.TabIndex = 7;
            this.LogsTextBox.Text = "";
            // 
            // RefreshBoardButton
            // 
            this.RefreshBoardButton.Location = new System.Drawing.Point(14, 51);
            this.RefreshBoardButton.Name = "RefreshBoardButton";
            this.RefreshBoardButton.Size = new System.Drawing.Size(88, 23);
            this.RefreshBoardButton.TabIndex = 8;
            this.RefreshBoardButton.Text = "refresh board";
            this.RefreshBoardButton.UseVisualStyleBackColor = true;
            this.RefreshBoardButton.Click += new System.EventHandler(this.RefreshBoardButton_Click);
            // 
            // TipLabel
            // 
            this.TipLabel.AutoSize = true;
            this.TipLabel.Location = new System.Drawing.Point(264, 64);
            this.TipLabel.Name = "TipLabel";
            this.TipLabel.Size = new System.Drawing.Size(324, 13);
            this.TipLabel.TabIndex = 9;
            this.TipLabel.Text = "Tip: Use Ditherlicious to make awesome 320x120 dithered pictures.";
            // 
            // ChooseImageDialog
            // 
            this.ChooseImageDialog.FileName = "image.png";
            this.ChooseImageDialog.Filter = "PNG images|*.png";
            this.ChooseImageDialog.InitialDirectory = "./";
            // 
            // SharpSplatPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 261);
            this.Controls.Add(this.TipLabel);
            this.Controls.Add(this.RefreshBoardButton);
            this.Controls.Add(this.LogsTextBox);
            this.Controls.Add(this.InjectButton);
            this.Controls.Add(this.ChooseImageButton);
            this.Controls.Add(this.BoardLabel);
            this.Controls.Add(this.PngPictureBox);
            this.Controls.Add(this.MinGwLabel);
            this.Controls.Add(this.ArduinoIdeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SharpSplatPrinter";
            this.Text = "SharpSplat Printer";
            this.Load += new System.EventHandler(this.SharpSplatPrinter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PngPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ArduinoIdeLabel;
        private System.Windows.Forms.Label MinGwLabel;
        private System.Windows.Forms.PictureBox PngPictureBox;
        private System.Windows.Forms.Label BoardLabel;
        private System.Windows.Forms.Button ChooseImageButton;
        private System.Windows.Forms.Button InjectButton;
        private System.Windows.Forms.RichTextBox LogsTextBox;
        private System.Windows.Forms.Button RefreshBoardButton;
        private System.Windows.Forms.Label TipLabel;
        private System.Windows.Forms.OpenFileDialog ChooseImageDialog;
        private System.ComponentModel.BackgroundWorker BackgroundWorker1;
    }
}

