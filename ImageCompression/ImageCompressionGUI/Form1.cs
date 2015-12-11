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
using ImageCompression.Helpers;

namespace ImageCompressionGUI
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// The original image
        /// </summary>
        private StandardImage originalImage { get; set; }

        /// <summary>
        /// Modified pixel data
        /// </summary>
        private ImageCompression.ImageData.PixelMatrix modifiedData { get; set; }

        /// <summary>
        /// Compressed image
        /// </summary>
        private StandardImage compressedImage { get; set; }

        /// <summary>
        /// Compressed form in bytes
        /// </summary>
        private byte[] compressedForm { get; set; }

        /// <summary>
        /// Active compresion flags
        /// </summary>
        private byte compressionFlags { get; set; }

        /// <summary>
        /// Height of image
        /// </summary>
        private int height { get; set; }

        /// <summary>
        /// Width of image
        /// </summary>
        private int width { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set autoscroll for scrolling the images
            this.originalImageTab.AutoScroll = true;
            this.compressedImageTab.AutoScroll = true;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            // Open file dialog
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // If is our custom file type, read it
                if (Path.GetExtension(fileDialog.FileName) == ".miac")
                {
                    // Read the MIAC file
                    var binaryReader = new BinaryReader(File.Open(fileDialog.FileName, FileMode.Open));

                    // Read the generic information
                    this.compressionFlags = binaryReader.ReadByte();
                    this.width = binaryReader.ReadInt32();
                    this.height = binaryReader.ReadInt32();

                    // Decompress
                    this.Decompress(ref binaryReader);

                    // Tidy
                    binaryReader.Dispose();

                    // Load the compressed image
                    this.originalImage = new StandardImage(new Bitmap(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\Output\temp.bmp"));

                    // Load new modified data
                    this.modifiedData = new ImageCompression.ImageData.PixelMatrix(this.originalImage.GetPixelMatrix(RGB.ColorDepth.TwentyFour));
                }
                else
                {
                    // Load the image
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

                // Clear the image box if required
                if (this.originalPictureBox.Image != null)
                {
                    this.originalPictureBox.Image.Dispose();
                }

                // Setup the new imagebox
                this.originalPictureBox.Image = this.originalImage.GetBitmap();
                height = this.originalPictureBox.Height;
                width = this.originalPictureBox.Width;
                // Set file size
                this.originalSizeLabelBytes.Text = (new FileInfo(fileDialog.FileName).Length / 1024) + " KB";
                this.modifiedData = this.modifiedData = new ImageCompression.ImageData.PixelMatrix(this.originalImage.GetPixelMatrix(RGB.ColorDepth.TwentyFour));
            }
        }

        private void Decompress(ref BinaryReader binaryReader)
        {
            var bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;

            // Default to RGB for now
            System.Type colorModel = typeof(ImageCompression.ColorModel.RGB);

            // Set the bit depth in use
            if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.EightBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eight;
            }
            else if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.FifteenBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Fifteen;
            }
            else if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.EighteenBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eighteen;
            }
            else if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.TwentyFourBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;
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

                    if (bitDepth != ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour)
                    {
                        fullDecodedLZ77 = fullDecodedLZ77.ConvertAll(new Converter<RGB, RGB>(x => x.ToDepth(RGB.ColorDepth.TwentyFour)));
                    }
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

        /// <summary>
        /// Save the data to a temporary bitmap
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void saveTempBitmap(List<ImageCompression.ColorModel.RGB> data, int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            var x = 0;
            var y = 0;

            // Loop over and set each pixel
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

            // Save the temp file
            bitmap.Save(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\Output\temp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);            
        }

        private void recompressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set bit depth correctly
            var bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;

            if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.EightBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eight;
            }
            else if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.FifteenBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Fifteen;
            }
            else if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.EighteenBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.Eighteen;
            }
            else if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.TwentyFourBit))
            {
                bitDepth = ImageCompression.ColorModel.RGB.ColorDepth.TwentyFour;
            }

            // Re-Load the modified data
            this.modifiedData.data = this.originalImage.GetPixelMatrix(bitDepth);
            ImageCompression.Interfaces.IEncodable[] data = this.modifiedData.data.Flatten();
            
            // If huffman is enabled
            if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.Huffman))
            {
                var huffmanData = Huffman<ImageCompression.Interfaces.IEncodable>.Encode(data);
            }

            // If run length is enabled
            if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.RunLength))
            {
                data = RunLength.Encode<ImageCompression.Interfaces.IEncodable>(data);
            }

            // If LZ77 is enabled
            if (this.compressionFlags.FlagIsSet((byte)CompressionFlags.LZ77))
            {
                var encodedData = LZ77.Encode(data, 255, 255);
                data = encodedData.ToArray<ImageCompression.Interfaces.IEncodable>();
            }

            // Pack the bytes
            this.compressedForm = ImageCompression.Helpers.BytePacker.Pack(data.ToList());

            // Update compressed size
            this.compressedSizeLabelBytes.Text = ((this.compressedForm.Length + this.compressionFlags) / 1024) + " KB";

            // Update compression radio
            this.compressionRatio.Text = this.CalculateCompressionRatio(
                int.Parse(this.originalSizeLabelBytes.Text.Substring(0, this.originalSizeLabelBytes.Text.Length - 3)),
                int.Parse(this.compressedSizeLabelBytes.Text.Substring(0, this.originalSizeLabelBytes.Text.Length - 3)));

            // Update MSE
            this.meanSquaredError.Text = this.originalImage.MeanSquaredError(this.modifiedData.data).ToString();

            // Update the preview tab with the preview data
            if (this.compressedPictureBox.Image != null)
            {
                this.compressedPictureBox.Image.Dispose();
                this.compressedImage.Dispose();
            }

            // Save the modified data as a temporary image
            var modifiedBitmap = this.modifiedData.GetBitmap();

            modifiedBitmap.Save(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\Output\temp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            this.compressedImage = new StandardImage(new Bitmap(@"D:\GitHub\SE3IA11-Image-Analysis\ImageCompression\TestImage\Output\temp.bmp"));

            this.compressedPictureBox.Image = this.compressedImage.GetBitmap();
        }

        /// <summary>
        /// Calculate the compression ratio
        /// </summary>
        /// <param name="uncompressed">Uncompressed size in kb</param>
        /// <param name="compressed">Compressed size in kb</param>
        /// <returns></returns>
        private string CalculateCompressionRatio(int uncompressed, int compressed)
        {
            if (compressed == 0)
            {
                return "N /A";
            }
            else
            {
                // Return a ratio
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

        private void huffmanEncodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Toggle the enabled/disabled
            this.huffmanEncodingToolStripMenuItem.Checked = this.huffmanEncodingToolStripMenuItem.Checked ? false : true;

            // Add/remove flag if it isn't/is there
            this.compressionFlags = (byte)(this.compressionFlags ^ (byte)CompressionFlags.Huffman);
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
            // Create new save file dialog
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = ".miac";
            saveFileDialog.Filter = "Image File | *.miac";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Write the data to file
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
