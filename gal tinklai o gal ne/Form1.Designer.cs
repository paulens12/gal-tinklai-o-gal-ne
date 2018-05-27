namespace gal_tinklai_o_gal_ne
{
    partial class Form1
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
            this.goView1 = new Northwoods.Go.GoView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.neighborhoodTab = new System.Windows.Forms.TabPage();
            this.neighborTable = new System.Windows.Forms.DataGridView();
            this.routingTab = new System.Windows.Forms.TabPage();
            this.routingTable = new System.Windows.Forms.DataGridView();
            this.topologyTab = new System.Windows.Forms.TabPage();
            this.topologyTable = new System.Windows.Forms.DataGridView();
            this.originBox = new System.Windows.Forms.ComboBox();
            this.destBox = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.neighborhoodTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neighborTable)).BeginInit();
            this.routingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.routingTable)).BeginInit();
            this.topologyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topologyTable)).BeginInit();
            this.SuspendLayout();
            // 
            // goView1
            // 
            this.goView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goView1.ArrowMoveLarge = 10F;
            this.goView1.ArrowMoveSmall = 1F;
            this.goView1.BackColor = System.Drawing.Color.White;
            this.goView1.Location = new System.Drawing.Point(12, 12);
            this.goView1.Name = "goView1";
            this.goView1.Size = new System.Drawing.Size(958, 360);
            this.goView1.TabIndex = 0;
            this.goView1.Text = "goView1";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.neighborhoodTab);
            this.tabControl1.Controls.Add(this.routingTab);
            this.tabControl1.Controls.Add(this.topologyTab);
            this.tabControl1.Location = new System.Drawing.Point(13, 378);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(957, 223);
            this.tabControl1.TabIndex = 1;
            // 
            // neighborhoodTab
            // 
            this.neighborhoodTab.Controls.Add(this.neighborTable);
            this.neighborhoodTab.Location = new System.Drawing.Point(4, 22);
            this.neighborhoodTab.Name = "neighborhoodTab";
            this.neighborhoodTab.Padding = new System.Windows.Forms.Padding(3);
            this.neighborhoodTab.Size = new System.Drawing.Size(949, 197);
            this.neighborhoodTab.TabIndex = 0;
            this.neighborhoodTab.Text = "Kaimynystės lentelė";
            this.neighborhoodTab.UseVisualStyleBackColor = true;
            // 
            // neighborTable
            // 
            this.neighborTable.AllowUserToAddRows = false;
            this.neighborTable.AllowUserToDeleteRows = false;
            this.neighborTable.AllowUserToResizeRows = false;
            this.neighborTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.neighborTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neighborTable.Location = new System.Drawing.Point(3, 3);
            this.neighborTable.Name = "neighborTable";
            this.neighborTable.ReadOnly = true;
            this.neighborTable.Size = new System.Drawing.Size(943, 191);
            this.neighborTable.TabIndex = 0;
            // 
            // routingTab
            // 
            this.routingTab.Controls.Add(this.routingTable);
            this.routingTab.Location = new System.Drawing.Point(4, 22);
            this.routingTab.Name = "routingTab";
            this.routingTab.Padding = new System.Windows.Forms.Padding(3);
            this.routingTab.Size = new System.Drawing.Size(949, 197);
            this.routingTab.TabIndex = 1;
            this.routingTab.Text = "Maršrutizavimo lentelė";
            this.routingTab.UseVisualStyleBackColor = true;
            // 
            // routingTable
            // 
            this.routingTable.AllowUserToAddRows = false;
            this.routingTable.AllowUserToDeleteRows = false;
            this.routingTable.AllowUserToResizeRows = false;
            this.routingTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.routingTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routingTable.Location = new System.Drawing.Point(3, 3);
            this.routingTable.Name = "routingTable";
            this.routingTable.ReadOnly = true;
            this.routingTable.Size = new System.Drawing.Size(943, 191);
            this.routingTable.TabIndex = 1;
            // 
            // topologyTab
            // 
            this.topologyTab.Controls.Add(this.topologyTable);
            this.topologyTab.Location = new System.Drawing.Point(4, 22);
            this.topologyTab.Name = "topologyTab";
            this.topologyTab.Size = new System.Drawing.Size(949, 197);
            this.topologyTab.TabIndex = 2;
            this.topologyTab.Text = "Topologinė lentelė";
            this.topologyTab.UseVisualStyleBackColor = true;
            // 
            // topologyTable
            // 
            this.topologyTable.AllowUserToAddRows = false;
            this.topologyTable.AllowUserToDeleteRows = false;
            this.topologyTable.AllowUserToResizeRows = false;
            this.topologyTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.topologyTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topologyTable.Location = new System.Drawing.Point(0, 0);
            this.topologyTable.Name = "topologyTable";
            this.topologyTable.ReadOnly = true;
            this.topologyTable.Size = new System.Drawing.Size(949, 197);
            this.topologyTable.TabIndex = 1;
            // 
            // originBox
            // 
            this.originBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.originBox.FormattingEnabled = true;
            this.originBox.Location = new System.Drawing.Point(17, 607);
            this.originBox.Name = "originBox";
            this.originBox.Size = new System.Drawing.Size(304, 21);
            this.originBox.TabIndex = 2;
            // 
            // destBox
            // 
            this.destBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.destBox.FormattingEnabled = true;
            this.destBox.Location = new System.Drawing.Point(327, 608);
            this.destBox.Name = "destBox";
            this.destBox.Size = new System.Drawing.Size(291, 21);
            this.destBox.TabIndex = 3;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshButton.Location = new System.Drawing.Point(624, 607);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(116, 23);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(746, 607);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(117, 23);
            this.sendButton.TabIndex = 5;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 640);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.destBox);
            this.Controls.Add(this.originBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.goView1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.neighborhoodTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neighborTable)).EndInit();
            this.routingTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.routingTable)).EndInit();
            this.topologyTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topologyTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Northwoods.Go.GoView goView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage neighborhoodTab;
        private System.Windows.Forms.DataGridView neighborTable;
        private System.Windows.Forms.TabPage routingTab;
        private System.Windows.Forms.TabPage topologyTab;
        private System.Windows.Forms.DataGridView routingTable;
        private System.Windows.Forms.DataGridView topologyTable;
        private System.Windows.Forms.ComboBox originBox;
        private System.Windows.Forms.ComboBox destBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button sendButton;
    }
}

