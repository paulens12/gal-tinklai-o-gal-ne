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
            this.routingTab = new System.Windows.Forms.TabPage();
            this.topologyTab = new System.Windows.Forms.TabPage();
            this.neighborTable = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.neighborhoodTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neighborTable)).BeginInit();
            this.SuspendLayout();
            // 
            // goView1
            // 
            this.goView1.ArrowMoveLarge = 10F;
            this.goView1.ArrowMoveSmall = 1F;
            this.goView1.BackColor = System.Drawing.Color.White;
            this.goView1.Location = new System.Drawing.Point(12, 12);
            this.goView1.Name = "goView1";
            this.goView1.Size = new System.Drawing.Size(958, 286);
            this.goView1.TabIndex = 0;
            this.goView1.Text = "goView1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.neighborhoodTab);
            this.tabControl1.Controls.Add(this.routingTab);
            this.tabControl1.Controls.Add(this.topologyTab);
            this.tabControl1.Location = new System.Drawing.Point(13, 305);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(957, 296);
            this.tabControl1.TabIndex = 1;
            // 
            // neighborhoodTab
            // 
            this.neighborhoodTab.Controls.Add(this.neighborTable);
            this.neighborhoodTab.Location = new System.Drawing.Point(4, 22);
            this.neighborhoodTab.Name = "neighborhoodTab";
            this.neighborhoodTab.Padding = new System.Windows.Forms.Padding(3);
            this.neighborhoodTab.Size = new System.Drawing.Size(949, 270);
            this.neighborhoodTab.TabIndex = 0;
            this.neighborhoodTab.Text = "Kaimynystės lentelė";
            this.neighborhoodTab.UseVisualStyleBackColor = true;
            // 
            // routingTab
            // 
            this.routingTab.Location = new System.Drawing.Point(4, 22);
            this.routingTab.Name = "routingTab";
            this.routingTab.Padding = new System.Windows.Forms.Padding(3);
            this.routingTab.Size = new System.Drawing.Size(949, 270);
            this.routingTab.TabIndex = 1;
            this.routingTab.Text = "Maršrutizavimo lentelė";
            this.routingTab.UseVisualStyleBackColor = true;
            // 
            // topologyTab
            // 
            this.topologyTab.Location = new System.Drawing.Point(4, 22);
            this.topologyTab.Name = "topologyTab";
            this.topologyTab.Size = new System.Drawing.Size(949, 270);
            this.topologyTab.TabIndex = 2;
            this.topologyTab.Text = "Topologinė lentelė";
            this.topologyTab.UseVisualStyleBackColor = true;
            // 
            // neighborTable
            // 
            this.neighborTable.AllowUserToAddRows = false;
            this.neighborTable.AllowUserToDeleteRows = false;
            this.neighborTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.neighborTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neighborTable.Location = new System.Drawing.Point(3, 3);
            this.neighborTable.Name = "neighborTable";
            this.neighborTable.ReadOnly = true;
            this.neighborTable.Size = new System.Drawing.Size(943, 264);
            this.neighborTable.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 613);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.goView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.neighborhoodTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neighborTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Northwoods.Go.GoView goView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage neighborhoodTab;
        private System.Windows.Forms.DataGridView neighborTable;
        private System.Windows.Forms.TabPage routingTab;
        private System.Windows.Forms.TabPage topologyTab;
    }
}

