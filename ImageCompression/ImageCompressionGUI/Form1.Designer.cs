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
            this.runLengthEncodingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lZ77CompressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.huffmanEncodingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recompressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.originalImageTab = new System.Windows.Forms.TabPage();
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.compressedImageTab = new System.Windows.Forms.TabPage();
            this.compressedPictureBox = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.originalSizeLabel = new System.Windows.Forms.Label();
            this.originalSizeLabelBytes = new System.Windows.Forms.Label();
            this.compressedSizeLabel = new System.Windows.Forms.Label();
            this.compressedSizeLabelBytes = new System.Windows.Forms.Label();
            this.compressionRatioLabel = new System.Windows.Forms.Label();
            this.compressionRatio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.meanSquaredError = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.originalImageTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            this.compressedImageTab.SuspendLayout();
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
            this.saveCompressedToolStripMenuItem.Click += new System.EventHandler(this.saveCompressedToolStripMenuItem_Click);
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
            this.bitDepthComboBox.SelectedIndexChanged += new System.EventHandler(this.bitDepthComboBox_SelectionIndexChanged);
            // 
            // encodingTypeToolStripMenuItem
            // 
            this.encodingTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runLengthEncodingToolStripMenuItem,
            this.lZ77CompressionToolStripMenuItem,
            this.huffmanEncodingToolStripMenuItem});
            this.encodingTypeToolStripMenuItem.Name = "encodingTypeToolStripMenuItem";
            this.encodingTypeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.encodingTypeToolStripMenuItem.Text = "Encoding Type";
            // 
            // runLengthEncodingToolStripMenuItem
            // 
            this.runLengthEncodingToolStripMenuItem.Name = "runLengthEncodingToolStripMenuItem";
            this.runLengthEncodingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.runLengthEncodingToolStripMenuItem.Text = "Run Length Encoding";
            this.runLengthEncodingToolStripMenuItem.Click += new System.EventHandler(this.runLengthEncodingToolStripMenuItem_Click);
            // 
            // lZ77CompressionToolStripMenuItem
            // 
            this.lZ77CompressionToolStripMenuItem.Name = "lZ77CompressionToolStripMenuItem";
            this.lZ77CompressionToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.lZ77CompressionToolStripMenuItem.Text = "LZ77 Compression";
            this.lZ77CompressionToolStripMenuItem.Click += new System.EventHandler(this.lZ77CompressionToolStripMenuItem_Click);
            // 
            // huffmanEncodingToolStripMenuItem
            // 
            this.huffmanEncodingToolStripMenuItem.Enabled = false;
            this.huffmanEncodingToolStripMenuItem.Name = "huffmanEncodingToolStripMenuItem";
            this.huffmanEncodingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.huffmanEncodingToolStripMenuItem.Text = "Huffman Encoding";
            this.huffmanEncodingToolStripMenuItem.Click += new System.EventHandler(this.huffmanEncodingToolStripMenuItem_Click);
            // 
            // recompressToolStripMenuItem
            // 
            this.recompressToolStripMenuItem.Name = "recompressToolStripMenuItem";
            this.recompressToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.recompressToolStripMenuItem.Text = "Preview";
            this.recompressToolStripMenuItem.Click += new System.EventHandler(this.recompressToolStripMenuItem_Click);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 546);
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
            this.tabControl1.Size = new System.Drawing.Size(687, 509);
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
            this.compressedImageTab.Controls.Add(this.compressedPictureBox);
            this.compressedImageTab.Controls.Add(this.panel2);
            this.compressedImageTab.Location = new System.Drawing.Point(4, 22);
            this.compressedImageTab.Name = "compressedImageTab";
            this.compressedImageTab.Padding = new System.Windows.Forms.Padding(3);
            this.compressedImageTab.Size = new System.Drawing.Size(679, 483);
            this.compressedImageTab.TabIndex = 1;
            this.compressedImageTab.Text = "Compressed Image";
            this.compressedImageTab.UseVisualStyleBackColor = true;
            // 
            // compressedPictureBox
            // 
            this.compressedPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.compressedPictureBox.Location = new System.Drawing.Point(6, 6);
            this.compressedPictureBox.Name = "compressedPictureBox";
            this.compressedPictureBox.Size = new System.Drawing.Size(100, 50);
            this.compressedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.compressedPictureBox.TabIndex = 0;
            this.compressedPictureBox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(673, 477);
            this.panel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.originalSizeLabel);
            this.flowLayoutPanel1.Controls.Add(this.originalSizeLabelBytes);
            this.flowLayoutPanel1.Controls.Add(this.compressedSizeLabel);
            this.flowLayoutPanel1.Controls.Add(this.compressedSizeLabelBytes);
            this.flowLayoutPanel1.Controls.Add(this.compressionRatioLabel);
            this.flowLayoutPanel1.Controls.Add(this.compressionRatio);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.meanSquaredError);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 518);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(687, 25);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // originalSizeLabel
            // 
            this.originalSizeLabel.AutoSize = true;
            this.originalSizeLabel.Location = new System.Drawing.Point(5, 5);
            this.originalSizeLabel.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
            this.originalSizeLabel.Name = "originalSizeLabel";
            this.originalSizeLabel.Size = new System.Drawing.Size(68, 13);
            this.originalSizeLabel.TabIndex = 0;
            this.originalSizeLabel.Text = "Original Size:";
            // 
            // originalSizeLabelBytes
            // 
            this.originalSizeLabelBytes.AutoSize = true;
            this.originalSizeLabelBytes.Location = new System.Drawing.Point(79, 5);
            this.originalSizeLabelBytes.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.originalSizeLabelBytes.Name = "originalSizeLabelBytes";
            this.originalSizeLabelBytes.Size = new System.Drawing.Size(13, 13);
            this.originalSizeLabelBytes.TabIndex = 3;
            this.originalSizeLabelBytes.Text = "0";
            // 
            // compressedSizeLabel
            // 
            this.compressedSizeLabel.AutoSize = true;
            this.compressedSizeLabel.Location = new System.Drawing.Point(105, 5);
            this.compressedSizeLabel.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.compressedSizeLabel.Name = "compressedSizeLabel";
            this.compressedSizeLabel.Size = new System.Drawing.Size(91, 13);
            this.compressedSizeLabel.TabIndex = 1;
            this.compressedSizeLabel.Text = "Compressed Size:";
            // 
            // compressedSizeLabelBytes
            // 
            this.compressedSizeLabelBytes.AutoSize = true;
            this.compressedSizeLabelBytes.Location = new System.Drawing.Point(202, 5);
            this.compressedSizeLabelBytes.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.compressedSizeLabelBytes.Name = "compressedSizeLabelBytes";
            this.compressedSizeLabelBytes.Size = new System.Drawing.Size(13, 13);
            this.compressedSizeLabelBytes.TabIndex = 4;
            this.compressedSizeLabelBytes.Text = "0";
            // 
            // compressionRatioLabel
            // 
            this.compressionRatioLabel.AutoSize = true;
            this.compressionRatioLabel.Location = new System.Drawing.Point(228, 5);
            this.compressionRatioLabel.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.compressionRatioLabel.Name = "compressionRatioLabel";
            this.compressionRatioLabel.Size = new System.Drawing.Size(98, 13);
            this.compressionRatioLabel.TabIndex = 2;
            this.compressionRatioLabel.Text = "Compression Ratio:";
            // 
            // compressionRatio
            // 
            this.compressionRatio.Location = new System.Drawing.Point(332, 5);
            this.compressionRatio.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.compressionRatio.Name = "compressionRatio";
            this.compressionRatio.Size = new System.Drawing.Size(172, 13);
            this.compressionRatio.TabIndex = 5;
            this.compressionRatio.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(517, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "MSE:";
            // 
            // meanSquaredError
            // 
            this.meanSquaredError.Location = new System.Drawing.Point(556, 5);
            this.meanSquaredError.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.meanSquaredError.Name = "meanSquaredError";
            this.meanSquaredError.Size = new System.Drawing.Size(100, 13);
            this.meanSquaredError.TabIndex = 7;
            this.meanSquaredError.Text = "N/A";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 585);
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
            this.compressedImageTab.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem compressionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colourDepthToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox bitDepthComboBox;
        private System.Windows.Forms.ToolStripMenuItem saveCompressedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encodingTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runLengthEncodingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recompressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lZ77CompressionToolStripMenuItem;
        private System.Windows.Forms.Label originalSizeLabelBytes;
        private System.Windows.Forms.Label compressedSizeLabelBytes;
        private System.Windows.Forms.Label compressionRatio;
        private System.Windows.Forms.ToolStripMenuItem huffmanEncodingToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label meanSquaredError;
    }
}

