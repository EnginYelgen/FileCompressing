﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCompressing
{
    public partial class Form1 : Form
    {
        private FolderBrowserDialog folderBrowserDialog1, folderBrowserDialogNew;
        private string folderName, folderNameNew;
        private DateTime startTime;
        private CompressFile compressFile = new CompressFile();
        private static long compressionLevel;
        private static float shrinkLevel;

        public Form1()
        {
            InitializeComponent();

            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogNew = new System.Windows.Forms.FolderBrowserDialog();

            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;

            label_FolderPath.Text = label_NewPath.Text = label_Timer.Text = label_Count.Text = string.Empty;
            label_CompressLevel.Text = trackBar_CompressLevel.Value.ToString();
            label_ShrinkLevel.Text = trackBar_ShrinkLevel.Value.ToString();

            compressionLevel = Convert.ToByte(100 - trackBar_CompressLevel.Value);
            shrinkLevel = Convert.ToSingle(100 - trackBar_ShrinkLevel.Value) / 100;

            progressBar1.Minimum = 0;
            progressBar1.Step = 1;

            button_Export.Enabled = false;

            compressFile.CompressFileDetails = new List<CompressFileDetail>();
            dataGridView_Files.DataSource = compressFile.CompressFileDetails;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now - startTime;

            label_Timer.Text = timeSpan.ToString(@"hh\:mm\:ss");
        }

        private void button_Compress_Click(object sender, EventArgs e)
        {
            StartProgress();

            bool overwriteEnabled = checkBox_Overwrite.Checked;

            ThreadStart threadStart;

            if (overwriteEnabled)
                threadStart = new ThreadStart(OverwriteFiles);
            else
                threadStart = new ThreadStart(CreateNewFiles);

            threadStart += () =>
            {
                StopProgress();
            };

            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void StartProgress()
        {
            timer1.Enabled = true;
            startTime = DateTime.Now;
            button_Compress.Enabled = button_Export.Enabled = false;
            compressFile = new CompressFile();
            compressFile.StartDate = DateTime.Now;
            compressFile.CompressFileDetails = new List<CompressFileDetail>();
            label_Count.Text = string.Empty;
            progressBar1.Value = 0;

            dataGridView_Files.DataSource = null;
            dataGridView_Files.DataSource = compressFile.CompressFileDetails;
        }

        private void StopProgress()
        {
            timer1.Enabled = false;
            compressFile.EndDate = DateTime.Now;
            button_Compress.Invoke(new Action(() => { button_Compress.Enabled = true; }));
            button_Export.Invoke(new Action(() =>
            {
                if (compressFile.CompressFileDetails.Count > 0)
                    button_Export.Enabled = true;
            }));
            label_Count.Invoke(new Action(() => { label_Count.Text = "Count: " + compressFile.CompressFileDetails.Count; }));
        }

        private void OverwriteFiles()
        {
            if (String.IsNullOrEmpty(folderName))
            {
                MessageBox.Show("Lütfen sıkıştırma yapacağınız dosyaların bulunduğu klasörü seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] filePaths = Directory.GetFiles(folderName, "*.pdf", SearchOption.AllDirectories);

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Maximum = filePaths.Length;
            }));

            compressFile.Method = "Overwrite";
            compressFile.FileTypeName = "pdf";

            bool logFile = false;

            //Bind a reader to our large PDF
            for (int j = 0; j < filePaths.Length; j++)
            {
                logFile = false;
                byte[] memoryBytes = null;
                long fileLength = 0;
                try
                {
                    //Now we're going to open the above PDF and compress things
                    using (PdfReader reader = new PdfReader(filePaths[j]))
                    {
                        fileLength = reader.FileLength;

                        //Create our output PDF
                        using (MemoryStream ms = new MemoryStream())
                        {
                            //Bind a stamper to the file and our reader
                            using (PdfStamper stamper = new PdfStamper(reader, ms))
                            {
                                //NOTE: This code only deals with page 1, you'd want to loop more for your code
                                //Get page 1
                                PdfDictionary page;
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    page = reader.GetPageN(i);

                                    //Get the xobject structure
                                    PdfDictionary resources = (PdfDictionary)PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES));
                                    PdfDictionary xobject = (PdfDictionary)PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT));
                                    if (xobject != null)
                                    {
                                        PdfObject obj;
                                        //Loop through each key
                                        foreach (PdfName name in xobject.Keys)
                                        {
                                            obj = xobject.Get(name);
                                            if (obj.IsIndirect())
                                            {
                                                //Get the current key as a PDF object
                                                PdfDictionary imgObject = (PdfDictionary)PdfReader.GetPdfObject(obj);
                                                //See if its an image
                                                if (imgObject != null && imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
                                                {
                                                    if (imgObject.Get(PdfName.FILTER) != null)
                                                    {
                                                        //NOTE: There's a bunch of different types of filters, I'm only handing the simplest one here which is basically raw JPG, you'll have to research others
                                                        if (imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE))
                                                        {
                                                            logFile = true;
                                                            Compress(imgObject, stamper, obj);
                                                        }
                                                        else if (imgObject.Get(PdfName.FILTER).IsArray())
                                                        {
                                                            PdfArray pdfArray = (PdfArray)imgObject.Get(PdfName.FILTER);
                                                            List<PdfObject> pdfObjects = pdfArray.ArrayList;

                                                            for (int k = 0; k < pdfObjects.Count; k++)
                                                            {
                                                                if (pdfObjects[k].Equals(PdfName.DCTDECODE))
                                                                {
                                                                    logFile = true;
                                                                    Compress(imgObject, stamper, obj);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            memoryBytes = ms.ToArray();
                        }
                    }

                    if (logFile)
                    {
                        File.WriteAllBytes(filePaths[j], memoryBytes);

                        compressFile.OldSize += fileLength;
                        compressFile.NewSize += memoryBytes.Length;

                        compressFile.CompressFileDetails.Add(new CompressFileDetail
                        {
                            OldFilePath = filePaths[j],
                            NewFilePath = filePaths[j],
                            NewSize = memoryBytes.Length,
                            OldSize = fileLength
                        });

                        dataGridView_Files.Invoke(new Action(() =>
                        {
                            dataGridView_Files.DataSource = null;
                            dataGridView_Files.DataSource = compressFile.CompressFileDetails;
                        }));
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.PerformStep();
                    }));
                }
            }
        }

        private void CreateNewFiles()
        {
            if (String.IsNullOrEmpty(folderName))
            {
                MessageBox.Show("Lütfen sıkıştırma yapacağınız dosyaların bulunduğu klasörü seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrEmpty(folderNameNew))
            {
                MessageBox.Show("Lütfen sıkıştırılan dosyaların aktarılacağı klasörü seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] filePaths = Directory.GetFiles(folderName, "*.pdf", SearchOption.AllDirectories);

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Maximum = filePaths.Length;
            }));

            compressFile.Method = "CreateNew";
            compressFile.FileTypeName = "pdf";

            bool logFile = false;
            
            //Bind a reader to our large PDF
            PdfReader reader;
            string fileName = string.Empty, newFileName = string.Empty;
            long fileLength = 0;
            FileInfo fileInfo;
            for (int j = 0; j < filePaths.Length; j++)
            {
                logFile = false;
                try
                {
                    //Now we're going to open the above PDF and compress things
                    reader = new PdfReader(filePaths[j]);
                    fileLength = reader.FileLength;
                    newFileName = folderNameNew + filePaths[j].Replace(folderName, "");

                    if (!Directory.Exists(Path.GetDirectoryName(newFileName)))
                        Directory.CreateDirectory(Path.GetDirectoryName(newFileName));

                    //Create our output PDF
                    using (FileStream fs = new FileStream(newFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        //Bind a stamper to the file and our reader
                        using (PdfStamper stamper = new PdfStamper(reader, fs))
                        {
                            //NOTE: This code only deals with page 1, you'd want to loop more for your code
                            //Get page 1
                            PdfDictionary page;
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                page = reader.GetPageN(i);

                                //Get the xobject structure
                                PdfDictionary resources = (PdfDictionary)PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES));
                                PdfDictionary xobject = (PdfDictionary)PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT));
                                if (xobject != null)
                                {
                                    PdfObject obj;
                                    //Loop through each key
                                    foreach (PdfName name in xobject.Keys)
                                    {
                                        obj = xobject.Get(name);
                                        if (obj.IsIndirect())
                                        {
                                            //Get the current key as a PDF object
                                            PdfDictionary imgObject = (PdfDictionary)PdfReader.GetPdfObject(obj);
                                            //See if its an image
                                            if (imgObject != null && imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
                                            {
                                                if (imgObject.Get(PdfName.FILTER) != null)
                                                {
                                                    //NOTE: There's a bunch of different types of filters, I'm only handing the simplest one here which is basically raw JPG, you'll have to research others
                                                    if (imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE))
                                                    {
                                                        logFile = true;
                                                        Compress(imgObject, stamper, obj);
                                                    }
                                                    else if (imgObject.Get(PdfName.FILTER).IsArray())
                                                    {
                                                        PdfArray pdfArray = (PdfArray)imgObject.Get(PdfName.FILTER);
                                                        List<PdfObject> pdfObjects = pdfArray.ArrayList;

                                                        for (int k = 0; k < pdfObjects.Count; k++)
                                                        {
                                                            if (pdfObjects[k].Equals(PdfName.DCTDECODE))
                                                            {
                                                                logFile = true;
                                                                Compress(imgObject, stamper, obj);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (logFile)
                    {
                        fileInfo = new FileInfo(newFileName);

                        compressFile.OldSize += fileLength;
                        compressFile.NewSize += fileInfo.Length;

                        compressFile.CompressFileDetails.Add(new CompressFileDetail
                        {
                            OldFilePath = filePaths[j],
                            NewFilePath = newFileName,
                            NewSize = fileInfo.Length,
                            OldSize = fileLength
                        });

                        dataGridView_Files.Invoke(new Action(() =>
                        {
                            dataGridView_Files.DataSource = null;
                            dataGridView_Files.DataSource = compressFile.CompressFileDetails;
                        }));
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.PerformStep();
                    }));
                }
            }
        }

        private void Compress(PdfDictionary imgObject, PdfStamper stamper, PdfObject obj)
        {
            //Get the raw bytes of the current image
            byte[] oldBytes = PdfReader.GetStreamBytesRaw((PRStream)imgObject);
            //Will hold bytes of the compressed image later
            byte[] newBytes;
            //Wrap a stream around our original image
            using (MemoryStream sourceMS = new MemoryStream(oldBytes))
            {
                //Convert the bytes into a .Net image
                using (System.Drawing.Image oldImage = Bitmap.FromStream(sourceMS))
                {
                    //Shrink the image to 90% of the original
                    using (System.Drawing.Image newImage = ShrinkImage(oldImage))
                    {
                        //Convert the image to bytes using JPG at 85%
                        newBytes = ConvertImageToBytes(newImage);
                    }
                }
            }
            //Create a new iTextSharp image from our bytes
            iTextSharp.text.Image compressedImage = iTextSharp.text.Image.GetInstance(newBytes);
            //Kill off the old image
            PdfReader.KillIndirect(obj);
            //Add our image in its place
            stamper.Writer.AddDirectImageSimple(compressedImage, (PRIndirectReference)obj);
        }

        //Standard image save code from MSDN, returns a byte array
        private static byte[] ConvertImageToBytes(System.Drawing.Image image)
        {
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, compressionLevel);
            myEncoderParameters.Param[0] = myEncoderParameter;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, jgpEncoder, myEncoderParameters);
                return ms.ToArray();
            }
        }
        //standard code from MSDN
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        //Standard high quality thumbnail generation from http://weblogs.asp.net/gunnarpeipman/archive/2009/04/02/resizing-images-without-loss-of-quality.aspx
        private static System.Drawing.Image ShrinkImage(System.Drawing.Image sourceImage)
        {
            int newWidth = Convert.ToInt32(sourceImage.Width * shrinkLevel);
            int newHeight = Convert.ToInt32(sourceImage.Height * shrinkLevel);

            var thumbnailBitmap = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(thumbnailBitmap))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                g.DrawImage(sourceImage, imageRectangle);
            }
            return thumbnailBitmap;
        }

        private void checkBox_Overwrite_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                button_BrowseNewFolder.Enabled = false;
                label_NewPath.Text = string.Empty;
            }
            else
            {
                button_BrowseNewFolder.Enabled = true;
            }
        }

        private void button_Export_Click(object sender, EventArgs e)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(CompressFile));

            DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//CompressedFiles_" + directoryInfo.Name + ".xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, compressFile);
            file.Close();

            MessageBox.Show("Dosya oluşturulmuştur. Aşağıdaki dizinden erişebilirsiniz.\n\n" + path, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;

            switch (trackBar.Name)
            {
                case "trackBar_CompressLevel":
                    {
                        label_CompressLevel.Text = trackBar.Value.ToString();
                        compressionLevel = Convert.ToByte(100 - trackBar.Value);
                    }
                    break;
                case "trackBar_ShrinkLevel":
                    {
                        label_ShrinkLevel.Text = trackBar.Value.ToString();
                        shrinkLevel = Convert.ToSingle(100 - trackBar.Value) / 100;
                    }
                    break;
                default:
                    break;
            }
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                label_FolderPath.Text = folderName = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button_BrowseNewFolder_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialogNew.ShowDialog();
            if (result == DialogResult.OK)
            {
                label_NewPath.Text = folderNameNew = folderBrowserDialogNew.SelectedPath;
            }
        }
    }

    public class CompressFile
    {
        public string Method { get; set; }
        public string FileTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long OldSize { get; set; }
        public long NewSize { get; set; }
        public List<CompressFileDetail> CompressFileDetails { get; set; }

        public CompressFile()
        {

        }
    }

    public class CompressFileDetail
    {
        public string OldFilePath { get; set; }
        public string NewFilePath { get; set; }
        public long OldSize { get; set; }
        public long NewSize { get; set; }

        public CompressFileDetail()
        {

        }
    }
}
