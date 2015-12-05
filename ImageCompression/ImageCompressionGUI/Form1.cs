using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageCompression;
using System.IO;
using ImageCompression.Encoders;
using ImageCompression.ColorModel;
using ImageCompression.ExtensionMethods;

namespace ImageCompressionGUI
{
    public partial class Form1 : Form
    {
        private StandardImage originalImage { get; set; }

        private byte[] compressedForm { get; set; }

        private byte compressionFlags { get; set; }

        private int height { get; set; }
        private int width { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.originalImageTab.AutoScroll = true;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(fileDialog.FileName) == ".miac")
                {
                    // Read the MIAC file
                    var binaryReader = new BinaryReader(File.Open(fileDialog.FileName, FileMode.Open));

                    this.compressionFlags = binaryReader.ReadByte();
                    this.width = binaryReader.ReadInt32();
                    this.height = binaryReader.ReadInt32();

                    this.Decompress(ref binaryReader);

                    // Tidy
                    binaryReader.Dispose();

                    this.originalImage = new StandardImage(new Bitmap(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\Output\temp.bmp"));
                }
                else
                {
                    this.originalImage = new StandardImage(new Bitmap(fileDialog.FileName));

                    // Clear any currently set compression bit depth
                    this.compressionFlags = (byte)(this.compressionFlags >> 4);
                    this.compressionFlags = (byte)(this.compressionFlags << 4);

                    var originalPixelFormat = this.originalImage.GetBitmap().PixelFormat;
                    if (originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppArgb1555 ||
                        originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb555 ||
                        originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565 ||
                        originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
                    {
                        this.bitDepthComboBox.SelectedIndex = 1;
                        // Set the bitmask correctly
                        this.compressionFlags = (byte)(this.compressionFlags | (byte)CompressionFlags.FifteenBit);
                    }
                    else if (originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb ||
                             originalPixelFormat == System.Drawing.Imaging.PixelFormat.Canonical ||
                             originalPixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                    {
                        this.bitDepthComboBox.SelectedIndex = 3;
                        // Set the bitmask correctly
                        this.compressionFlags = (byte)(this.compressionFlags | (byte)CompressionFlags.TwentyFourBit);
                    }
                    else
                    {
                        this.bitDepthComboBox.SelectedIndex = 3;
                        // Set the bitmask correctly
                        this.compressionFlags = (byte)(this.compressionFlags | (byte)CompressionFlags.TwentyFourBit);
                    }
                }

                if (this.originalPictureBox.Image != null)
                {
                    this.originalPictureBox.Image.Dispose();
                }

                this.originalPictureBox.Image = this.originalImage.GetBitmap();
                height = this.originalPictureBox.Height;
                width = this.originalPictureBox.Width;
                this.originalSizeLabelBytes.Text = new FileInfo(fileDialog.FileName).Length.ToString();
            }
        }

        private void Decompress(ref BinaryReader binaryReader)
        {
            var bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;

            // Default to RGB for now
            System.Type colorModel = typeof(ImageCompression.ColorModel.RGB);

            switch (this.compressionFlags ^ ((byte)CompressionFlags.LZ77 | (byte)CompressionFlags.RunLength))
            {
                case (byte)CompressionFlags.EightBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eight;
                    break;
                case (byte)CompressionFlags.FifteenBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Fifteen;
                    break;
                case (byte)CompressionFlags.EighteenBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eighteen;
                    break;
                case (byte)CompressionFlags.TwentyFourBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;
                    break;
            }

            var decodedData = new List<ImageCompression.Interfaces.IEncodable>();

            // If LZ77 is enabled
            if ((this.compressionFlags & (byte)CompressionFlags.LZ77) == (byte)CompressionFlags.LZ77)
            {
                // Decode LZ77
                var decodedLZ77 = LZ77.DecodeBinaryStream(ref binaryReader, bitDepth);

                // If run length is enabled
                if ((this.compressionFlags & (byte)CompressionFlags.RunLength) == (byte)CompressionFlags.RunLength)
                {
                    var fullDecodedLZ77 = LZ77.Decode<RunLengthStore<ImageCompression.ColorModel.RGB>>(decodedLZ77).ToList();
                }
                else
                {
                    var fullDecodedLZ77 = LZ77.Decode<ImageCompression.ColorModel.RGB>(decodedLZ77).ToList();

                    this.saveTempBitmap(fullDecodedLZ77, width, height);
                }
            }

            // If run length is enabled
            if ((this.compressionFlags & (byte)CompressionFlags.RunLength) == (byte)CompressionFlags.RunLength)
            {
                
            }
            else
            {
                
            }

            // All decoded
        }

        private void saveTempBitmap(List<ImageCompression.ColorModel.RGB> data, int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            var x = 0;
            var y = 0;

            for (var i = 0; i < data.Count; i++)
            {
                bitmap.SetPixel(x, y, data[i].ToColor());

                y++;

                if (y == height)
                {
                    y = 0;
                    x++;
                }
            }

            bitmap.Save(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\Output\temp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);            
        }

        private void recompressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;

            switch (this.compressionFlags ^ ((byte)CompressionFlags.LZ77 | (byte)CompressionFlags.RunLength))
            {
                case (byte)CompressionFlags.EightBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eight;
                    break;
                case (byte)CompressionFlags.FifteenBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Fifteen;
                    break;
                case (byte)CompressionFlags.EighteenBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eighteen;
                    break;
                case (byte)CompressionFlags.TwentyFourBit:
                    bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;
                    break;
            }

            var data = new List<ImageCompression.Interfaces.IEncodable>();

            // If run length is enabled
            if ((this.compressionFlags & (byte)CompressionFlags.RunLength) == (byte)CompressionFlags.RunLength)
            {
                data = RunLength.Encode(this.originalImage.GetPixelMatrix(bitDepth));
            }
            else
            {
                var flattenedData = this.originalImage.GetPixelMatrix(bitDepth).Flatten();
                data = flattenedData.ToList<ImageCompression.Interfaces.IEncodable>();
            }

            // If LZ77 is enabled
            if ((this.compressionFlags & (byte)CompressionFlags.LZ77) == (byte)CompressionFlags.LZ77)
            {
                var encodedData = LZ77.Encode(data, 255, 255);
                data = encodedData.ToList<ImageCompression.Interfaces.IEncodable>();
            }

            // Make into variableByte array
            var byteArray = data.ConvertAll(new Converter<ImageCompression.Interfaces.IEncodable, VariableByte[]>(x => x.ToByteArray()));

            // Pack the bytes
            this.compressedForm = ImageCompression.Helpers.BytePacker.Pack(byteArray);

            // Update compressed size
            this.compressedSizeLabelBytes.Text = (this.compressedForm.Length + this.compressionFlags).ToString();

            // Update compression radio
            this.compressionRatio.Text = this.CalculateCompressionRatio(int.Parse(this.originalSizeLabelBytes.Text), int.Parse(this.compressedSizeLabelBytes.Text));
        }

        private string CalculateCompressionRatio(int uncompressed, int compressed)
        {
            if (compressed == 0)
            {
                return "N/A";
            }
            else
            {
                var ratio = (double)uncompressed / (double)compressed;
                return ratio.ToString() + ":1";
            }
        }

        private void runLengthEncodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Toggle the enabled/disabled
            this.runLengthEncodingToolStripMenuItem.Checked = this.runLengthEncodingToolStripMenuItem.Checked ? false : true;

            // Add/remove flag if it isn't/is there
            this.compressionFlags = (byte)(this.compressionFlags ^ (byte)CompressionFlags.RunLength);
        }

        private void lZ77CompressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Toggle the enabled/disabled
            this.lZ77CompressionToolStripMenuItem.Checked = this.lZ77CompressionToolStripMenuItem.Checked ? false : true;

            // Add/remove flag if it isn't/is there
            this.compressionFlags = (byte)(this.compressionFlags ^ (byte)CompressionFlags.LZ77);
        }

        private void bitDepthComboBox_SelectionIndexChanged(object sender, EventArgs e)
        {
            // Clear any currently set compression bit depth
            this.compressionFlags = (byte)(this.compressionFlags >> 4);
            this.compressionFlags = (byte)(this.compressionFlags << 4);

            // Set bitmask based on the selection
            switch (((ToolStripComboBox)sender).SelectedIndex)
            {
                case 0:
                    this.compressionFlags = (byte)(this.compressionFlags | (byte)CompressionFlags.EightBit);
                    break;
                case 1:
                    this.compressionFlags = (byte)(this.compressionFlags | (byte)CompressionFlags.FifteenBit);
                    break;
                case 2:
                    this.compressionFlags = (byte)(this.compressionFlags | (byte)CompressionFlags.EighteenBit);
                    break;
                case 3:
                    this.compressionFlags = (byte)(this.compressionFlags | (byte)CompressionFlags.TwentyFourBit);
                    break;
            }
        }

        private void saveCompressedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = ".miac";
            saveFileDialog.Filter = "Image File | *.miac";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var byteWriter = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                {
                    byteWriter.WriteByte(this.compressionFlags);
                    byteWriter.Write(BitConverter.GetBytes(this.width), 0, BitConverter.GetBytes(this.width).Length);
                    byteWriter.Write(BitConverter.GetBytes(this.height), 0, BitConverter.GetBytes(this.height).Length);
                    byteWriter.Write(this.compressedForm, 0, this.compressedForm.Length);
                }
            }
        }
    }
}
