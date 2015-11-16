namespace ImageCompressionGUI
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
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCompressedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colourDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitDepthComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.encodingTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.huffmanEncodingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runLengthEncodingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.originalImageTab = new System.Windows.Forms.TabPage();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.compressedImageTab = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.compressedPictureBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.originalSizeLabel = new System.Windows.Forms.Label();
            this.originalSizeLabelBytes = new System.Windows.Forms.Label();
            this.compressedSizeLabel = new System.Windows.Forms.Label();
            this.compressionRatioLabel = new System.Windows.Forms.Label();
            this.recompressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.originalImageTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            this.compressedImageTab.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressedPictureBox)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.compressionSettingsToolStripMenuItem,
            this.recompressToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(693, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveCompressedToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveCompressedToolStripMenuItem
            // 
            this.saveCompressedToolStripMenuItem.Name = "saveCompressedToolStripMenuItem";
            this.saveCompressedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.saveCompressedToolStripMenuItem.Text = "Save Compressed";
            // 
            // compressionSettingsToolStripMenuItem
            // 
            this.compressionSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colourDepthToolStripMenuItem,
            this.encodingTypeToolStripMenuItem});
            this.compressionSettingsToolStripMenuItem.Name = "compressionSettingsToolStripMenuItem";
            this.compressionSettingsToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.compressionSettingsToolStripMenuItem.Text = "Compression Settings";
            // 
            // colourDepthToolStripMenuItem
            // 
            this.colourDepthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bitDepthComboBox});
            this.colourDepthToolStripMenuItem.Name = "colourDepthToolStripMenuItem";
            this.colourDepthToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.colourDepthToolStripMenuItem.Text = "Colour Depth";
            this.colourDepthToolStripMenuItem.Click += new System.EventHandler(this.colourDepthToolStripMenuItem_Click);
            // 
            // bitDepthComboBox
            // 
            this.bitDepthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bitDepthComboBox.Items.AddRange(new object[] {
            "8 Bit",
            "15 Bit",
            "18 Bit",
            "24 Bit"});
            this.bitDepthComboBox.Name = "bitDepthComboBox";
            this.bitDepthComboBox.Size = new System.Drawing.Size(121, 23);
            this.bitDepthComboBox.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // encodingTypeToolStripMenuItem
            // 
            this.encodingTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.huffmanEncodingToolStripMenuItem,
            this.runLengthEncodingToolStripMenuItem});
            this.encodingTypeToolStripMenuItem.Name = "encodingTypeToolStripMenuItem";
            this.encodingTypeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.encodingTypeToolStripMenuItem.Text = "Encoding Type";
            // 
            // huffmanEncodingToolStripMenuItem
            // 
            this.huffmanEncodingToolStripMenuItem.Name = "huffmanEncodingToolStripMenuItem";
            this.huffmanEncodingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.huffmanEncodingToolStripMenuItem.Text = "Huffman Encoding";
            // 
            // runLengthEncodingToolStripMenuItem
            // 
            this.runLengthEncodingToolStripMenuItem.Name = "runLengthEncodingToolStripMenuItem";
            this.runLengthEncodingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.runLengthEncodingToolStripMenuItem.Text = "Run Length Encoding";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 509);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.originalImageTab);
            this.tabControl1.Controls.Add(this.compressedImageTab);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(687, 471);
            this.tabControl1.TabIndex = 2;
            // 
            // originalImageTab
            // 
            this.originalImageTab.Controls.Add(this.originalPictureBox);
            this.originalImageTab.Controls.Add(this.panel1);
            this.originalImageTab.Location = new System.Drawing.Point(4, 22);
            this.originalImageTab.Name = "originalImageTab";
            this.originalImageTab.Padding = new System.Windows.Forms.Padding(3);
            this.originalImageTab.Size = new System.Drawing.Size(679, 445);
            this.originalImageTab.TabIndex = 0;
            this.originalImageTab.Text = "Original Image";
            this.originalImageTab.UseVisualStyleBackColor = true;
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.originalPictureBox.Location = new System.Drawing.Point(6, 6);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(100, 50);
            this.originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.originalPictureBox.TabIndex = 0;
            this.originalPictureBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 439);
            this.panel1.TabIndex = 0;
            // 
            // compressedImageTab
            // 
            this.compressedImageTab.Controls.Add(this.panel2);
            this.compressedImageTab.Location = new System.Drawing.Point(4, 22);
            this.compressedImageTab.Name = "compressedImageTab";
            this.compressedImageTab.Padding = new System.Windows.Forms.Padding(3);
            this.compressedImageTab.Size = new System.Drawing.Size(679, 445);
            this.compressedImageTab.TabIndex = 1;
            this.compressedImageTab.Text = "Compressed Image";
            this.compressedImageTab.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.compressedPictureBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(673, 439);
            this.panel2.TabIndex = 0;
            // 
            // compressedPictureBox
            // 
            this.compressedPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.compressedPictureBox.Location = new System.Drawing.Point(3, 3);
            this.compressedPictureBox.Name = "compressedPictureBox";
            this.compressedPictureBox.Size = new System.Drawing.Size(100, 50);
            this.compressedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.compressedPictureBox.TabIndex = 0;
            this.compressedPictureBox.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.originalSizeLabel);
            this.flowLayoutPanel1.Controls.Add(this.originalSizeLabelBytes);
            this.flowLayoutPanel1.Controls.Add(this.compressedSizeLabel);
            this.flowLayoutPanel1.Controls.Add(this.compressionRatioLabel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 480);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(687, 26);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // originalSizeLabel
            // 
            this.originalSizeLabel.AutoSize = true;
            this.originalSizeLabel.Location = new System.Drawing.Point(5, 5);
            this.originalSizeLabel.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.originalSizeLabel.Name = "originalSizeLabel";
            this.originalSizeLabel.Size = new System.Drawing.Size(102, 13);
            this.originalSizeLabel.TabIndex = 0;
            this.originalSizeLabel.Text = "Original Size (bytes):";
            // 
            // originalSizeLabelBytes
            // 
            this.originalSizeLabelBytes.AutoSize = true;
            this.originalSizeLabelBytes.Location = new System.Drawing.Point(113, 5);
            this.originalSizeLabelBytes.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.originalSizeLabelBytes.Name = "originalSizeLabelBytes";
            this.originalSizeLabelBytes.Size = new System.Drawing.Size(13, 13);
            this.originalSizeLabelBytes.TabIndex = 3;
            this.originalSizeLabelBytes.Text = "0";
            // 
            // compressedSizeLabel
            // 
            this.compressedSizeLabel.AutoSize = true;
            this.compressedSizeLabel.Location = new System.Drawing.Point(139, 5);
            this.compressedSizeLabel.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.compressedSizeLabel.Name = "compressedSizeLabel";
            this.compressedSizeLabel.Size = new System.Drawing.Size(125, 13);
            this.compressedSizeLabel.TabIndex = 1;
            this.compressedSizeLabel.Text = "Compressed Size (bytes):";
            // 
            // compressionRatioLabel
            // 
            this.compressionRatioLabel.AutoSize = true;
            this.compressionRatioLabel.Location = new System.Drawing.Point(277, 5);
            this.compressionRatioLabel.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.compressionRatioLabel.Name = "compressionRatioLabel";
            this.compressionRatioLabel.Size = new System.Drawing.Size(98, 13);
            this.compressionRatioLabel.TabIndex = 2;
            this.compressionRatioLabel.Text = "Compression Ratio:";
            // 
            // recompressToolStripMenuItem
            // 
            this.recompressToolStripMenuItem.Name = "recompressToolStripMenuItem";
            this.recompressToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.recompressToolStripMenuItem.Text = "Recompress";
            this.recompressToolStripMenuItem.Click += new System.EventHandler(this.recompressToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 548);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.originalImageTab.ResumeLayout(false);
            this.originalImageTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            this.compressedImageTab.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressedPictureBox)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage compressedImageTab;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label originalSizeLabel;
        private System.Windows.Forms.Label compressedSizeLabel;
        private System.Windows.Forms.Label compressionRatioLabel;
        private System.Windows.Forms.TabPage originalImageTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox compressedPictureBox;
        private System.Windows.Forms.Label originalSizeLabelBytes;
        private System.Windows.Forms.ToolStripMenuItem compressionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colourDepthToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox bitDepthComboBox;
        private System.Windows.Forms.ToolStripMenuItem saveCompressedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodingTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem huffmanEncodingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runLengthEncodingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recompressToolStripMenuItem;
    }
}

