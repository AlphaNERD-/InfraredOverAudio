namespace IRSignalGenerator
{
    partial class frmIRSignalGenerator
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudIRFrequency = new System.Windows.Forms.NumericUpDown();
            this.tbxIRdata = new System.Windows.Forms.TextBox();
            this.lblIRfrequency = new System.Windows.Forms.Label();
            this.lblIRdata = new System.Windows.Forms.Label();
            this.btnPlaySignal = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrv = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudIRFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // nudIRFrequency
            // 
            this.nudIRFrequency.Location = new System.Drawing.Point(106, 7);
            this.nudIRFrequency.Maximum = new decimal(new int[] {
            45000,
            0,
            0,
            0});
            this.nudIRFrequency.Minimum = new decimal(new int[] {
            35000,
            0,
            0,
            0});
            this.nudIRFrequency.Name = "nudIRFrequency";
            this.nudIRFrequency.Size = new System.Drawing.Size(120, 22);
            this.nudIRFrequency.TabIndex = 0;
            this.nudIRFrequency.Value = new decimal(new int[] {
            35000,
            0,
            0,
            0});
            // 
            // tbxIRdata
            // 
            this.tbxIRdata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxIRdata.Location = new System.Drawing.Point(106, 35);
            this.tbxIRdata.Multiline = true;
            this.tbxIRdata.Name = "tbxIRdata";
            this.tbxIRdata.Size = new System.Drawing.Size(477, 176);
            this.tbxIRdata.TabIndex = 1;
            // 
            // lblIRfrequency
            // 
            this.lblIRfrequency.AutoSize = true;
            this.lblIRfrequency.Location = new System.Drawing.Point(12, 9);
            this.lblIRfrequency.Name = "lblIRfrequency";
            this.lblIRfrequency.Size = new System.Drawing.Size(88, 17);
            this.lblIRfrequency.TabIndex = 2;
            this.lblIRfrequency.Text = "IR frequency";
            // 
            // lblIRdata
            // 
            this.lblIRdata.AutoSize = true;
            this.lblIRdata.Location = new System.Drawing.Point(12, 38);
            this.lblIRdata.Name = "lblIRdata";
            this.lblIRdata.Size = new System.Drawing.Size(53, 17);
            this.lblIRdata.TabIndex = 3;
            this.lblIRdata.Text = "IR data";
            // 
            // btnPlaySignal
            // 
            this.btnPlaySignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaySignal.Location = new System.Drawing.Point(474, 217);
            this.btnPlaySignal.Name = "btnPlaySignal";
            this.btnPlaySignal.Size = new System.Drawing.Size(109, 27);
            this.btnPlaySignal.TabIndex = 4;
            this.btnPlaySignal.Text = "Play Signal";
            this.btnPlaySignal.UseVisualStyleBackColor = true;
            this.btnPlaySignal.Click += new System.EventHandler(this.btnPlaySignal_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(292, 217);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 27);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrv
            // 
            this.btnPrv.Location = new System.Drawing.Point(106, 217);
            this.btnPrv.Name = "btnPrv";
            this.btnPrv.Size = new System.Drawing.Size(75, 27);
            this.btnPrv.TabIndex = 6;
            this.btnPrv.Text = "Previous";
            this.btnPrv.UseVisualStyleBackColor = true;
            this.btnPrv.Click += new System.EventHandler(this.btnPrv_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(187, 222);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(67, 17);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Waiting...";
            // 
            // frmIRSignalGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 256);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnPrv);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPlaySignal);
            this.Controls.Add(this.lblIRdata);
            this.Controls.Add(this.lblIRfrequency);
            this.Controls.Add(this.tbxIRdata);
            this.Controls.Add(this.nudIRFrequency);
            this.Name = "frmIRSignalGenerator";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudIRFrequency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudIRFrequency;
        private System.Windows.Forms.TextBox tbxIRdata;
        private System.Windows.Forms.Label lblIRfrequency;
        private System.Windows.Forms.Label lblIRdata;
        private System.Windows.Forms.Button btnPlaySignal;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrv;
        private System.Windows.Forms.Label lblInfo;
    }
}

