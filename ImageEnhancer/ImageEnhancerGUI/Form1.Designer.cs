namespace ImageEnhancerGUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardFFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inverseFFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.periodicNoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colouriseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.imageBoxOriginal = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageBoxWorking = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxOriginal)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxWorking)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processToolStripMenuItem,
            this.mSEToolStripMenuItem,
            this.colouriseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadImageToolStripMenuItem.Text = "Load Image";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fFTToolStripMenuItem,
            this.noiseToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // fFTToolStripMenuItem
            // 
            this.fFTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forwardFFTToolStripMenuItem,
            this.inverseFFTToolStripMenuItem});
            this.fFTToolStripMenuItem.Name = "fFTToolStripMenuItem";
            this.fFTToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.fFTToolStripMenuItem.Text = "FFT";
            // 
            // forwardFFTToolStripMenuItem
            // 
            this.forwardFFTToolStripMenuItem.Name = "forwardFFTToolStripMenuItem";
            this.forwardFFTToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.forwardFFTToolStripMenuItem.Text = "Forward FFT";
            this.forwardFFTToolStripMenuItem.Click += new System.EventHandler(this.forwardFFTToolStripMenuItem_Click);
            // 
            // inverseFFTToolStripMenuItem
            // 
            this.inverseFFTToolStripMenuItem.Name = "inverseFFTToolStripMenuItem";
            this.inverseFFTToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.inverseFFTToolStripMenuItem.Text = "Inverse FFT";
            this.inverseFFTToolStripMenuItem.Click += new System.EventHandler(this.inverseFFTToolStripMenuItem_Click);
            // 
            // noiseToolStripMenuItem
            // 
            this.noiseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.periodicNoiseToolStripMenuItem,
            this.medianFilterToolStripMenuItem});
            this.noiseToolStripMenuItem.Name = "noiseToolStripMenuItem";
            this.noiseToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.noiseToolStripMenuItem.Text = "Noise";
            // 
            // periodicNoiseToolStripMenuItem
            // 
            this.periodicNoiseToolStripMenuItem.Name = "periodicNoiseToolStripMenuItem";
            this.periodicNoiseToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.periodicNoiseToolStripMenuItem.Text = "Periodic Noise";
            this.periodicNoiseToolStripMenuItem.Click += new System.EventHandler(this.periodicNoiseToolStripMenuItem_Click);
            // 
            // medianFilterToolStripMenuItem
            // 
            this.medianFilterToolStripMenuItem.Name = "medianFilterToolStripMenuItem";
            this.medianFilterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.medianFilterToolStripMenuItem.Text = "Median Filter";
            this.medianFilterToolStripMenuItem.Click += new System.EventHandler(this.medianFilterToolStripMenuItem_Click);
            // 
            // mSEToolStripMenuItem
            // 
            this.mSEToolStripMenuItem.Name = "mSEToolStripMenuItem";
            this.mSEToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.mSEToolStripMenuItem.Text = "MSE";
            this.mSEToolStripMenuItem.Click += new System.EventHandler(this.mSEToolStripMenuItem_Click);
            // 
            // colouriseToolStripMenuItem
            // 
            this.colouriseToolStripMenuItem.Name = "colouriseToolStripMenuItem";
            this.colouriseToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.colouriseToolStripMenuItem.Text = "Colourise";
            this.colouriseToolStripMenuItem.Click += new System.EventHandler(this.colouriseToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.11823F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.881773F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 522);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(744, 506);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.imageBoxOriginal);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(736, 480);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Original Image";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // imageBoxOriginal
            // 
            this.imageBoxOriginal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageBoxOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxOriginal.Location = new System.Drawing.Point(3, 3);
            this.imageBoxOriginal.Name = "imageBoxOriginal";
            this.imageBoxOriginal.Size = new System.Drawing.Size(730, 474);
            this.imageBoxOriginal.TabIndex = 0;
            this.imageBoxOriginal.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.imageBoxWorking);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(736, 480);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Working Image";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageBoxWorking
            // 
            this.imageBoxWorking.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageBoxWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxWorking.Location = new System.Drawing.Point(3, 3);
            this.imageBoxWorking.Name = "imageBoxWorking";
            this.imageBoxWorking.Size = new System.Drawing.Size(730, 474);
            this.imageBoxWorking.TabIndex = 0;
            this.imageBoxWorking.TabStop = false;
            this.imageBoxWorking.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBoxWorking_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "Image Enhancer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxOriginal)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxWorking)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox imageBoxOriginal;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forwardFFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inverseFFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem periodicNoiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianFilterToolStripMenuItem;
        private System.Windows.Forms.PictureBox imageBoxWorking;
        private System.Windows.Forms.ToolStripMenuItem mSEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colouriseToolStripMenuItem;
    }
}

