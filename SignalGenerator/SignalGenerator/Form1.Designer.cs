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
            this.nudBitLength = new System.Windows.Forms.NumericUpDown();
            this.nudPauseLength = new System.Windows.Forms.NumericUpDown();
            this.lblBitLength = new System.Windows.Forms.Label();
            this.lblPauseLength = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudIRFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBitLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPauseLength)).BeginInit();
            this.SuspendLayout();
            // 
            // nudIRFrequency
            // 
            this.nudIRFrequency.Location = new System.Drawing.Point(80, 6);
            this.nudIRFrequency.Margin = new System.Windows.Forms.Padding(2);
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
            this.nudIRFrequency.Size = new System.Drawing.Size(90, 20);
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
            this.tbxIRdata.Location = new System.Drawing.Point(80, 28);
            this.tbxIRdata.Margin = new System.Windows.Forms.Padding(2);
            this.tbxIRdata.Multiline = true;
            this.tbxIRdata.Name = "tbxIRdata";
            this.tbxIRdata.Size = new System.Drawing.Size(359, 148);
            this.tbxIRdata.TabIndex = 1;
            this.tbxIRdata.Text = "+100 +200 +300 +400 +500 +600 +700 +800 +900 +1000";
            // 
            // lblIRfrequency
            // 
            this.lblIRfrequency.AutoSize = true;
            this.lblIRfrequency.Location = new System.Drawing.Point(9, 7);
            this.lblIRfrequency.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIRfrequency.Name = "lblIRfrequency";
            this.lblIRfrequency.Size = new System.Drawing.Size(68, 13);
            this.lblIRfrequency.TabIndex = 2;
            this.lblIRfrequency.Text = "IR frequency";
            // 
            // lblIRdata
            // 
            this.lblIRdata.AutoSize = true;
            this.lblIRdata.Location = new System.Drawing.Point(9, 31);
            this.lblIRdata.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIRdata.Name = "lblIRdata";
            this.lblIRdata.Size = new System.Drawing.Size(42, 13);
            this.lblIRdata.TabIndex = 3;
            this.lblIRdata.Text = "IR data";
            // 
            // btnPlaySignal
            // 
            this.btnPlaySignal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaySignal.Location = new System.Drawing.Point(356, 232);
            this.btnPlaySignal.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlaySignal.Name = "btnPlaySignal";
            this.btnPlaySignal.Size = new System.Drawing.Size(82, 22);
            this.btnPlaySignal.TabIndex = 4;
            this.btnPlaySignal.Text = "Play Signal";
            this.btnPlaySignal.UseVisualStyleBackColor = true;
            this.btnPlaySignal.Click += new System.EventHandler(this.btnPlaySignal_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.Location = new System.Drawing.Point(219, 232);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(56, 22);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrv
            // 
            this.btnPrv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrv.Location = new System.Drawing.Point(80, 232);
            this.btnPrv.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrv.Name = "btnPrv";
            this.btnPrv.Size = new System.Drawing.Size(56, 22);
            this.btnPrv.TabIndex = 6;
            this.btnPrv.Text = "Previous";
            this.btnPrv.UseVisualStyleBackColor = true;
            this.btnPrv.Click += new System.EventHandler(this.btnPrv_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(140, 236);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(52, 13);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Waiting...";
            // 
            // nudBitLength
            // 
            this.nudBitLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudBitLength.Location = new System.Drawing.Point(80, 181);
            this.nudBitLength.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudBitLength.Name = "nudBitLength";
            this.nudBitLength.Size = new System.Drawing.Size(90, 20);
            this.nudBitLength.TabIndex = 8;
            this.nudBitLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // nudPauseLength
            // 
            this.nudPauseLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudPauseLength.Location = new System.Drawing.Point(80, 207);
            this.nudPauseLength.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudPauseLength.Name = "nudPauseLength";
            this.nudPauseLength.Size = new System.Drawing.Size(90, 20);
            this.nudPauseLength.TabIndex = 9;
            this.nudPauseLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lblBitLength
            // 
            this.lblBitLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBitLength.AutoSize = true;
            this.lblBitLength.Location = new System.Drawing.Point(9, 183);
            this.lblBitLength.Name = "lblBitLength";
            this.lblBitLength.Size = new System.Drawing.Size(51, 13);
            this.lblBitLength.TabIndex = 10;
            this.lblBitLength.Text = "Bit length";
            // 
            // lblPauseLength
            // 
            this.lblPauseLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPauseLength.AutoSize = true;
            this.lblPauseLength.Location = new System.Drawing.Point(9, 209);
            this.lblPauseLength.Name = "lblPauseLength";
            this.lblPauseLength.Size = new System.Drawing.Size(69, 13);
            this.lblPauseLength.TabIndex = 11;
            this.lblPauseLength.Text = "Pause length";
            // 
            // frmIRSignalGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 264);
            this.Controls.Add(this.lblPauseLength);
            this.Controls.Add(this.lblBitLength);
            this.Controls.Add(this.nudPauseLength);
            this.Controls.Add(this.nudBitLength);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnPrv);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPlaySignal);
            this.Controls.Add(this.lblIRdata);
            this.Controls.Add(this.lblIRfrequency);
            this.Controls.Add(this.tbxIRdata);
            this.Controls.Add(this.nudIRFrequency);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmIRSignalGenerator";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudIRFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBitLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPauseLength)).EndInit();
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
        private System.Windows.Forms.NumericUpDown nudBitLength;
        private System.Windows.Forms.NumericUpDown nudPauseLength;
        private System.Windows.Forms.Label lblBitLength;
        private System.Windows.Forms.Label lblPauseLength;
    }
}

