namespace gal_tinklai_o_gal_ne
{
    partial class InputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OKbutton = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.inputBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OKbutton
            // 
            this.OKbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKbutton.Location = new System.Drawing.Point(216, 11);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 21);
            this.OKbutton.TabIndex = 0;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(12, 12);
            this.inputBox.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.inputBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(198, 20);
            this.inputBox.TabIndex = 1;
            this.inputBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // InputForm
            // 
            this.AcceptButton = this.OKbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 43);
            this.ControlBox = false;
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.OKbutton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InputForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.inputBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button OKbutton;
        public System.Windows.Forms.NumericUpDown inputBox;
    }
}