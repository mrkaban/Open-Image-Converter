using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;


namespace SimpleImageResizer
{
    public partial class ImageResizer : Form
    {

        #region [ Global declarations ]

        int resizedImagesCount = 1;
        int finishedThreadsCounter = 0;
        string[] imagesToResize = new string[0];
        string currentTempFile = string.Empty;

        DateTime startingTime;

        System.Threading.Thread _threadFirstHalf;
        System.Threading.Thread _threadSecondHalf;
        System.Threading.Thread _threadThirdHalf;
        System.Threading.Thread _threadFourthHalf;

        float cropX;
        float cropY;
        float cropWidth;
        float cropHeight;

        Enums.EditMode editMode = Enums.EditMode.Scroll;

        float previewRatio = 1.0F;
        Image _originalThumbnailImage;
        Bitmap _cropBitmap;

        TimeSpan _totalEstimatedTimeRemaining;

        System.Timers.Timer remainingTimeTimer = new System.Timers.Timer();

        double[] intervals = new double[50];
        int counter = 0;
        double _totalTime = 0.0;
        string _projectedOutputDirSize = "Вычисление...";

        Helper helper = null;
        TxtFileLogger _logger = null;

        private Image _originalImage;

        private int _presetCropWidth = 0;
        private int _presetCropHeight = 0;

        #endregion

        #region [ Properties ]
        private ImageFormat _imageFileFormat = ImageFormat.Jpeg;
        public ImageFormat ImageFileFormat
        {
            get
            {
                return _imageFileFormat;
            }
            set
            {
                _imageFileFormat = value;
            }
        }
        #endregion

        #region [ Constructors ]
        public ImageResizer()
        {
            InitializeComponent();
            FillImageFormatsDropDown();
            labelImageQuality.Text = string.Format("{0}%", trackBarQuality.Value.ToString());
            trackBarQuality_Scroll(null, EventArgs.Empty);
            helper = new Helper();

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            _logger = new TxtFileLogger(@"logs", false);

            toolTip1.SetToolTip(textBoxDestinationImagePath, "Двойной щелчок, чтобы открыть папку.");
            toolTip1.SetToolTip(textBoxSourceImagesPath, "Двойной щелчок, чтобы открыть папку.");
        }




        #endregion

        #region [ ImageResizer_Load ]
        private void ImageResizer_Load(object sender, EventArgs e)
        {
            comboBoxResolution.SelectedIndex = comboBoxResolution.FindStringExact("1024x768");
            remainingTimeTimer.Interval = 1000;
            remainingTimeTimer.Elapsed += new System.Timers.ElapsedEventHandler(remainingTimeTimer_Elapsed);
            CenterImageInPanel();
        }

        void remainingTimeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _totalTime = _totalTime - 1000;
        }
        #endregion





        // REBINDS the DataGridView
        #region [ RebindGrid ]
        private void RebindGrid(string[] dataSource)
        {
            dataGridView1.DataSource = imagesToResize.ToList<string>().Select(x => new { Value = x }).ToList();
            dataGridView1.Columns[0].Width = this.Width;

            dataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
            dataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = Color.DarkGreen;
            dataGridView1.Columns[0].DefaultCellStyle.ForeColor = Color.DarkBlue;
            dataGridView1.Columns[0].HeaderText = "Преобразование выбранных изображений: (дважды щелкните по строке для предварительного просмотра)";

            StatusLabel.Text = String.Format("{0} файлов было выбрано.", imagesToResize.Length);
        }
        #endregion

        //BUTTON START RESIZE
        #region [ buttonStartResize_Click ]
        private void buttonStartResize_Click(object sender, EventArgs e)
        {
            if (!CheckAndSetSourcePath())
                return;

            if (!CheckAndSetDestinationPath())
                return;

            if (CheckIfPathsAreSame())
            {
                MessageBox.Show("Пути источника и назначения совпадают.", "Пути источника и назначения совпадают.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (dataGridView1.Rows.Count == 0)
                buttonOpenAllInSource_Click(sender, e);

            DialogResult result = MessageBox.Show(string.Format("Копировать все изображения из:\n{0}\nв\n{1}\nи изменить размер?", textBoxSourceImagesPath.Text, textBoxDestinationImagePath.Text), "Изменить изображения?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                progressBar1.Maximum = imagesToResize.Length;
                progressBar1.Step = 1;

                StartResize();
            }

        }
        #endregion

        // START RESIZE <>--------------<<<
        #region [ StartResize ]
        private void StartResize()
        {
            startingTime = DateTime.Now;

            CheckForIllegalCrossThreadCalls = false;



            string[] res = new string[2];
            if (comboBoxResolution.SelectedItem != null && comboBoxResolution.SelectedItem.ToString().Contains("x"))
            {
                res = comboBoxResolution.SelectedItem.ToString().Split('x');
            }
            else
            {               
                MessageBox.Show("Укажите допустимые размеры ширины и высоты", "Укажите ширину и высоту", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxResolution.Focus();
                comboBoxResolution.DroppedDown = true;
                return;
            }

            int width = 0;
            int height = 0;

            if (!int.TryParse(res[0], out width) || !int.TryParse(res[1], out height))
            {               
                MessageBox.Show("Укажите допустимые размеры ширины и высоты", "Укажите ширину и высоту", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxResolution.Focus();
                comboBoxResolution.DroppedDown = true;
                return;
            }



            if (radioButtonMultiThread.Checked && imagesToResize.Length > 4)
            {

                string[] firstHalf = new string[imagesToResize.Length / 4];
                string[] secondHalf = new string[imagesToResize.Length / 4];
                string[] thirdHalf = new string[imagesToResize.Length / 4];
                string[] fourthHalf = new string[imagesToResize.Length - imagesToResize.Length / 4 * 3];

                for (int i = 0; i < imagesToResize.Length / 4; i++)
                {
                    firstHalf[i] = imagesToResize[i];
                }

                int j = 0;
                for (int i = firstHalf.Length; i < imagesToResize.Length / 4 * 2; i++)
                {
                    secondHalf[j] = imagesToResize[i];
                    j++;
                }

                j = 0;

                for (int i = firstHalf.Length + secondHalf.Length; i < imagesToResize.Length / 4 * 3; i++)
                {
                    thirdHalf[j] = imagesToResize[i];
                    j++;
                }

                j = 0;

                for (int i = firstHalf.Length + secondHalf.Length + thirdHalf.Length; i < imagesToResize.Length; i++)
                {
                    fourthHalf[j] = imagesToResize[i];
                    j++;
                }

                Resizer r1 = new Resizer();
                r1.RootSourceFolder = textBoxSourceImagesPath.Text;
                r1.ImageQuality = trackBarQuality.Value;
                r1.ImageFileFormat = ImageFileFormat;
                r1.ResizeCompleted += new Resizer.ImageResized(r_ResizeCompleted);
                r1.AllImagesResized += new Resizer.AllImagesResizeCompleted(r2_AllImagesResized);
                r1.NewWidth = width;
                r1.NewHeight = height;
                r1.DestionationFolder = textBoxDestinationImagePath.Text;
                r1.FilesToResize = firstHalf;

                Resizer r2 = new Resizer();
                r2.RootSourceFolder = textBoxSourceImagesPath.Text;
                r2.ImageQuality = trackBarQuality.Value;
                r2.ImageFileFormat = ImageFileFormat;
                r2.ResizeCompleted += new Resizer.ImageResized(r_ResizeCompleted);
                r2.AllImagesResized += new Resizer.AllImagesResizeCompleted(r2_AllImagesResized);
                r2.NewWidth = width;
                r2.NewHeight = height;
                r2.DestionationFolder = textBoxDestinationImagePath.Text;
                r2.FilesToResize = secondHalf;

                Resizer r3 = new Resizer();
                r3.RootSourceFolder = textBoxSourceImagesPath.Text;
                r3.ImageQuality = trackBarQuality.Value;
                r3.ImageFileFormat = ImageFileFormat;
                r3.ResizeCompleted += new Resizer.ImageResized(r_ResizeCompleted);
                r3.AllImagesResized += new Resizer.AllImagesResizeCompleted(r2_AllImagesResized);
                r3.NewWidth = width;
                r3.NewHeight = height;
                r3.DestionationFolder = textBoxDestinationImagePath.Text;
                r3.FilesToResize = thirdHalf;

                Resizer r4 = new Resizer();
                r4.RootSourceFolder = textBoxSourceImagesPath.Text;
                r4.ImageQuality = trackBarQuality.Value;
                r4.ImageFileFormat = ImageFileFormat;
                r4.ResizeCompleted += new Resizer.ImageResized(r_ResizeCompleted);
                r4.AllImagesResized += new Resizer.AllImagesResizeCompleted(r2_AllImagesResized);
                r4.NewWidth = width;
                r4.NewHeight = height;
                r4.DestionationFolder = textBoxDestinationImagePath.Text;
                r4.FilesToResize = fourthHalf;


                try
                {
                    _threadFirstHalf = new System.Threading.Thread(new System.Threading.ThreadStart(r1.StartResize));
                    _threadSecondHalf = new System.Threading.Thread(new System.Threading.ThreadStart(r2.StartResize));
                    _threadThirdHalf = new System.Threading.Thread(new System.Threading.ThreadStart(r3.StartResize));
                    _threadFourthHalf = new System.Threading.Thread(new System.Threading.ThreadStart(r4.StartResize));

                    _threadFirstHalf.Start();
                    _threadSecondHalf.Start();
                    _threadThirdHalf.Start();
                    _threadFourthHalf.Start();
                }
                catch (Exception ex)
                {
                    helper.EnableFormControls(this.Controls);
                    StatusLabel.Text = ex.Message;
                }
            }
            else
            {

                Resizer r2 = new Resizer();
                r2.RootSourceFolder = textBoxSourceImagesPath.Text;
                r2.ImageQuality = trackBarQuality.Value;
                r2.ImageFileFormat = ImageFileFormat;
                r2.ResizeCompleted += new Resizer.ImageResized(r_ResizeCompleted);
                r2.AllImagesResized += new Resizer.AllImagesResizeCompleted(r_2_AllImagesResized);
                r2.NewWidth = width;
                r2.NewHeight = height;
                r2.DestionationFolder = textBoxDestinationImagePath.Text;
                r2.FilesToResize = imagesToResize;

                try
                {

                    _threadSecondHalf = new System.Threading.Thread(new System.Threading.ThreadStart(r2.StartResize));
                    _threadSecondHalf.Start();
                }
                catch (Exception ex)
                {
                    helper.EnableFormControls(this.Controls);
                    StatusLabel.Text = ex.Message;
                }
            }

            helper.DisableFormControls(this.Controls);
        }


        #endregion



        // Events - IMAGE RESIZE COMPLETED
        #region [ r_ResizeCompleted ]

        void r_ResizeCompleted(object sender, TimeSpan resizeTime)
        {

            CheckForIllegalCrossThreadCalls = false;

            try
            {
                resizedImagesCount++;

                progressBar1.PerformStep();

                if (counter < intervals.Length)
                {
                    intervals[counter] = resizeTime.TotalMilliseconds;
                    counter++;

                    if (_totalTime > 0)
                    {
                        remainingTimeTimer.Start();
                    }
                }
                else
                {
                    double avg = 0;

                    for (int i = 0; i < intervals.Length; i++)
                    {
                        avg = avg + intervals[i];
                    }

                    _totalTime = avg / intervals.Length;

                    counter = 0;

                    _totalTime = _totalTime * (imagesToResize.Length - resizedImagesCount);

                }



                if (resizedImagesCount > imagesToResize.Length / 10 && resizedImagesCount < (imagesToResize.Length / 10) + 15)
                {
                    _projectedOutputDirSize = string.Format("~{0:0,0.00} МБ", helper.GetProjectedFileSize((sender as Resizer).DestionationFolder) * imagesToResize.Length);
                }

                _totalEstimatedTimeRemaining = new TimeSpan((long)(_totalTime * TimeSpan.TicksPerMillisecond));


                if (_totalEstimatedTimeRemaining.Hours == 0 && _totalEstimatedTimeRemaining.Minutes == 0 && _totalEstimatedTimeRemaining.Seconds == 0)
                {
                    StatusLabel.Text = string.Format("Преобразование {0} изображений продолжается. Оставшееся время: Вычисление... Прогнозируемый размер выходного каталога: {1}", resizedImagesCount, _projectedOutputDirSize);
                }
                else
                {
                    StatusLabel.Text = string.Format("Преобразование {0} изображений продолжается. Оставшееся время: ~{1:00}:{2:00}:{3:00}. Прогнозируемый размер выходного каталога: {4}", resizedImagesCount, _totalEstimatedTimeRemaining.Hours, _totalEstimatedTimeRemaining.Minutes, _totalEstimatedTimeRemaining.Seconds, _projectedOutputDirSize);
                }

            }
            catch
            { }


        }

        #endregion

        // All images resized EVENT
        #region [ r2_AllImagesResized ]

        void r_2_AllImagesResized()
        {
            progressBar1.Value = 0;
            resizedImagesCount = 0;

            StatusLabel.Text = string.Format("Готово: {0}", DateTime.Now - startingTime);

            MessageBox.Show(string.Format("{0} изображений было успешно преобразовано!", imagesToResize.Length), "Преобразование завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);

            System.Diagnostics.Process.Start(textBoxDestinationImagePath.Text);

            helper.EnableFormControls(this.Controls);
        }

        void r2_AllImagesResized()
        {
            finishedThreadsCounter++;

            if (finishedThreadsCounter == 4)
            {
                finishedThreadsCounter = 0;
                progressBar1.Value = 0;
                resizedImagesCount = 0;

                StatusLabel.Text = string.Format("Готово: {0}", DateTime.Now - startingTime);

                MessageBox.Show(string.Format("{0} изображений было успешно преобразовано!", imagesToResize.Length), "Преобразование завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);

                helper.EnableFormControls(this.Controls);

                System.Diagnostics.Process.Start(textBoxDestinationImagePath.Text);
            }

        }
        #endregion






        // CROP & Mouse EventHandlers
        #region [ CROP ]

        // Start Drawing Cropping Rectangle
        #region [ pictureBoxPreview_MouseDown ]




        private void pictureBoxPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cropX = e.X;
                cropY = e.Y;

                pictureBoxPreview.Refresh();
            }
        }

        #endregion

        // Draw Selection Rectangle
        #region [ pictureBoxPreview_MouseMove ]

        private void pictureBoxPreview_MouseMove(object sender, MouseEventArgs e)
        {
            //new System.Threading.Thread(MouseMoves).Start(e);
            MouseMoves(e);
        }



        private void MouseMoves(object me)
        {
            

            MouseEventArgs e = me as MouseEventArgs;

            switch (editMode)
            {

                case Enums.EditMode.Scroll:

                    pictureBoxPreview.Cursor = Cursors.Hand;

                    #region [ Scroll ]
                    if (e.Button == MouseButtons.Left)
                    {
                        pictureBoxPreview.Top += (e.Y - (int)cropY);

                        pictureBoxPreview.Left += (e.X - (int)cropX);

                    }
                    #endregion


                    break;
                case Enums.EditMode.Crop:

                    pictureBoxPreview.Cursor = Cursors.Cross;

                    #region [ Crop ]
                    if (e.Button == System.Windows.Forms.MouseButtons.Left && this._originalImage != null)
                    {

                        cropWidth = e.X - cropX;
                        cropHeight = e.Y - cropY;


                        if (cropWidth < 1 || pictureBoxPreview.Image == null) return;

                        if (_presetCropWidth > 0 && cropWidth / previewRatio > _presetCropWidth)
                        {
                            cropWidth = _presetCropWidth * previewRatio;
                        }

                        if (_presetCropHeight > 0 && cropHeight / previewRatio > _presetCropHeight)
                        {
                            cropHeight = _presetCropHeight * previewRatio;
                        }

                       
                        // Make a copy of the bitmap and draw crop rectangle onto that copy
                        Bitmap copy = new Bitmap(this._originalThumbnailImage);

                        if (cropWidth > 0 && cropHeight > 0)
                        {

                            using (Graphics g = Graphics.FromImage(copy))
                            {

                                #region [ Draw crop rectangle and display information ]
                                //using (Pen cropPen = new Pen(Color.LawnGreen, 1))   
                                Color oldColor = copy.GetPixel((int)((cropX + cropWidth) / 2), (int)((cropY + cropHeight) / 2));
                                Color newColor = System.Drawing.Color.FromArgb(oldColor.A, 255 - oldColor.R, 255 - oldColor.G, 255 - oldColor.B);


                                using (Pen cropPen = new Pen(newColor))
                                {
                                    using (Pen diagPen = new Pen(Color.YellowGreen, 1))
                                    {

                                        cropPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                                        diagPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                                        if (Control.ModifierKeys == Keys.Shift)
                                            cropWidth = Convert.ToInt32(cropHeight * 1.33F);


                                        g.DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);

                                        PointF px = new PointF(cropX, cropY);
                                        PointF py = new Point(0, 0);

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX + cropWidth, cropY);
                                        py = new PointF(pictureBoxPreview.Width, 0);

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX + cropWidth, cropY + cropHeight);
                                        py = new PointF(pictureBoxPreview.Width, pictureBoxPreview.Height);

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX, cropY + cropHeight);
                                        py = new PointF(0, pictureBoxPreview.Height);

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX + (cropWidth / 2), cropY);
                                        py = new PointF(cropX + (cropWidth / 2), 0);

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX + (cropWidth / 2), cropY);
                                        py = new PointF(cropX + (cropWidth / 2), 0);

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX, cropY + (cropHeight / 2));
                                        py = new PointF(0, cropY + (cropHeight / 2));

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX + (cropWidth / 2), cropY + cropHeight);
                                        py = new PointF(cropX + (cropWidth / 2), pictureBoxPreview.Height);

                                        g.DrawLine(diagPen, px, py);


                                        px = new PointF(cropX + cropWidth, cropY + (cropHeight / 2));
                                        py = new PointF(pictureBoxPreview.Width, cropY + (cropHeight / 2));

                                        g.DrawLine(diagPen, px, py);
                                    }
                                }

                                string d1 = Math.Round(Math.Sqrt(Math.Pow(cropX, 2) + Math.Pow(cropY, 2))).ToString();
                                string d2 = Math.Round(Math.Sqrt(Math.Pow(pictureBoxPreview.Width - (cropX + cropWidth), 2) + Math.Pow(cropY, 2))).ToString();
                                string d3 = Math.Round(Math.Sqrt(Math.Pow(pictureBoxPreview.Width - (cropX + cropWidth), 2) + Math.Pow(pictureBoxPreview.Height - (cropY + cropHeight), 2))).ToString();
                                string d4 = Math.Round(Math.Sqrt(Math.Pow(cropX, 2) + Math.Pow(pictureBoxPreview.Height - (cropY + cropHeight), 2))).ToString();

                                string p1 = cropY.ToString();
                                string p2 = (pictureBoxPreview.Width - (cropX + cropWidth)).ToString();
                                string p3 = (pictureBoxPreview.Height - (cropY + cropHeight)).ToString();
                                string p4 = cropX.ToString();

                                g.DrawString(string.Format("{0}px", p1), new Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.Cyan), cropX + (cropWidth / 2), cropY / 2);
                                g.DrawString(string.Format("{0}px", p2), new Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.Cyan), pictureBoxPreview.Width - ((pictureBoxPreview.Width - (cropX + cropWidth)) / 2), cropY + (cropHeight / 2));
                                g.DrawString(string.Format("{0}px", p3), new Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.Cyan), cropX + (cropWidth / 2), pictureBoxPreview.Height - ((pictureBoxPreview.Height - (cropY + cropHeight)) / 2));
                                g.DrawString(string.Format("{0}px", p4), new Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.Cyan), cropX / 2, cropY + (cropHeight / 2));



                                g.DrawString(d1, new Font("Verdana", 9, FontStyle.Bold), new SolidBrush(Color.Cyan), cropX / 2, cropY / 2);
                                g.DrawString(d2, new Font("Verdana", 9, FontStyle.Bold), new SolidBrush(Color.Cyan), pictureBoxPreview.Width - ((pictureBoxPreview.Width - (cropX + cropWidth)) / 2), cropY / 2);
                                g.DrawString(d3, new Font("Verdana", 9, FontStyle.Bold), new SolidBrush(Color.Cyan), pictureBoxPreview.Width - ((pictureBoxPreview.Width - (cropX + cropWidth)) / 2), cropY + cropHeight + (pictureBoxPreview.Height - (cropY + cropHeight)) / 2);
                                g.DrawString(d4, new Font("Verdana", 9, FontStyle.Bold), new SolidBrush(Color.Cyan), cropX / 2, cropY + cropHeight + (pictureBoxPreview.Height - (cropY + cropHeight)) / 2);


                                float selectionX1 = cropX + (cropWidth / 2);
                                float selectionY1 = cropY;

                                float selectionX2 = cropX;
                                float selectionY2 = cropY + (cropHeight / 2);



                                g.DrawString(string.Format("X = {0}px", cropWidth / previewRatio), new Font("Verdana", 8, FontStyle.Regular), new SolidBrush(newColor), selectionX1, selectionY1);
                                g.DrawString(string.Format("Y = {0}px", cropHeight / previewRatio), new Font("Verdana", 8, FontStyle.Regular), new SolidBrush(newColor), selectionX2, selectionY2);
                                StatusLabel.Text = string.Format("X={0};Y={1} Ratio:{2}", cropWidth / previewRatio, cropHeight / previewRatio, previewRatio);

                                #endregion 

                                pictureBoxPreview.Image = copy;
                                // pictureBoxPreview.Refresh();
                            }
                        }
                    }
                    #endregion

                    break;
            }
        }

        #endregion

        // Save Selection Cropped Image
        #region [ pictureBoxPreview_MouseUp ]

        private void pictureBoxPreview_MouseUp(object sender, MouseEventArgs e)
        {


            if (editMode == Enums.EditMode.Crop)
            {



                RectangleF rect = new RectangleF();


                //rect = new RectangleF(cropX / previewRatio, cropY / previewRatio, cropWidth / previewRatio, cropHeight / previewRatio);

                cropX = cropX / previewRatio;
                cropY = cropY / previewRatio;

                cropWidth = cropWidth / previewRatio;
                cropHeight = cropHeight / previewRatio;

                rect = new RectangleF(cropX, cropY, cropWidth, cropHeight);





                Bitmap bit = new Bitmap(this._originalImage, this._originalImage.Width, this._originalImage.Height);

                _cropBitmap = new Bitmap((int)cropWidth, (int)cropHeight);

            
                Graphics g = Graphics.FromImage(_cropBitmap);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(bit, 0, 0, rect, GraphicsUnit.Pixel);

                pictureBoxPreview.Image = _cropBitmap;
                //pictureBoxPreview.Width = _cropBitmap.Width;
                //pictureBoxPreview.Height = _cropBitmap.Height;              

                CenterImageInPanel();

                DialogResult result = MessageBox.Show("Сохранить выбор?", "Обрезка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);



                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckAndSetDestinationPath())
                    {

                        this._originalImage.Dispose();

                        string oldFilePath = Path.Combine(textBoxDestinationImagePath.Text, Path.GetFileName(pictureBoxPreview.ImageLocation));

                        string path = Path.Combine(textBoxDestinationImagePath.Text, string.Format("{0}.bmp", helper.GetTempFileName()));

                        pictureBoxPreview.ImageLocation = path;

                        _cropBitmap.Save(path);
                        _cropBitmap.Dispose();

                        imagesToResize = new string[] { path };
                        RebindGrid(imagesToResize);
                        dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(0, 0));

                        File.Delete(oldFilePath);


                    }
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    //pictureBoxPreview.Width = _originalThumbnailImage.Width;
                    //pictureBoxPreview.Height = _originalThumbnailImage.Height;
                    pictureBoxPreview.Image = _originalThumbnailImage;
                    buttonZoomFit_Click(sender, e);
                }
            }

            CenterImageInPanel();

        }
        #endregion

        // Preset Crop Dimensions Width
        #region [ textBoxPresetCropWidth_TextChanged ]
        private void textBoxPresetCropWidth_TextChanged(object sender, EventArgs e)
        {
            SetPresetCropDimensions();
        }
        #endregion

        // Preset Crop Dimensions Height
        #region [ textBoxPresetCropHeight_TextChanged ]
        private void textBoxPresetCropHeight_TextChanged(object sender, EventArgs e)
        {
            SetPresetCropDimensions();
        }
        #endregion

        #endregion

        // PICTUREBOX CONTEXT MENU
        #region [ PictureBox Context Menu ]

        // ENABLE/DISABLE PASTE OPTION
        #region [ pictureBoxPreview_MouseClick ]
        private void pictureBoxPreview_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStripCopyPaste.Show(pictureBoxPreview, e.X, e.Y);

                Image clipBoardImage = Clipboard.GetImage();

                if (clipBoardImage == null)
                    contextMenuStripCopyPaste.Items[0].Enabled = false;
                else
                    contextMenuStripCopyPaste.Items[0].Enabled = true;
            }
        }
        #endregion

        // PASTE
        #region [ pasteToolStripMenuItem_Click ]
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image clipBoardImage = Clipboard.GetImage();

            if (clipBoardImage != null)
            {
                pictureBoxPreview.Image = clipBoardImage;

                string tempDir = @"C:\Temp\";

                string tempFileName = string.Format("{0}{1}.bmp", tempDir, helper.GetTempFileName());

                currentTempFile = tempFileName;

                if (!Directory.Exists(tempDir))
                    Directory.CreateDirectory(tempDir);

                clipBoardImage.Save(tempFileName);

                List<string> currentImages = imagesToResize.ToList();

                currentImages.Add(tempFileName);

                imagesToResize = currentImages.ToArray();

                RebindGrid(imagesToResize);

                dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(0, dataGridView1.Rows.Count - 1));
            }
        }
        #endregion

        // REMOVE
        #region [ removeToolStripMenuItem_Click ]
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveFileFromQueueAndGrid(pictureBoxPreview.ImageLocation);
        }
        #endregion

        // DELETE
        #region [ deleteToolStripMenuItem_Click ]
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(currentTempFile))
            {
                DialogResult result = MessageBox.Show(string.Format("Удалить {0} ?", currentTempFile), "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    File.Delete(currentTempFile);
                else
                    return;
            }

            RemoveFileFromQueueAndGrid(currentTempFile);
        }
        #endregion



        #endregion

        // DATAGRID CONTEXT MENU
        #region [ DataGrid CONTEXT MENU ]

        // SHOW CONTEXT MENU
        #region [ dataGridView1_CellMouseDown ]
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                this.contextMenuStrip1.Show(this.dataGridView1, this.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

            }
        }
        #endregion

        // OPEN
        #region [ toolStripMenuItemOpen_Click ]
        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            string imagePath = dataGridView1.SelectedCells[0].Value.ToString();

            System.Diagnostics.Process.Start(imagePath);
            StatusLabel.Text = String.Format("Открыть: {0}", imagePath);
        }
        #endregion

        // DELETE
        #region [ toolStripMenuItemDelete_Click ]
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            string imagePath = dataGridView1.SelectedCells[0].Value.ToString();

            DialogResult result = MessageBox.Show(string.Format("Вы уверены, что хотите удалить: {0}? \n\n Примечание: текущее действие удалит фактический файл из системы.", imagePath), "Подтверждение удаления", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case System.Windows.Forms.DialogResult.Yes:

                    File.Delete(imagePath);

                    string path = System.IO.Path.GetDirectoryName(imagesToResize[0]);

                    imagesToResize = Directory.GetFiles(path);

                    RebindGrid(imagesToResize);

                    MessageBox.Show(string.Format("{0} был удален!", imagePath), "Удалено", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    StatusLabel.Text = String.Format("{0} файлы были выбраны.", imagesToResize.Length);

                    break;

                case System.Windows.Forms.DialogResult.No: return;
                case System.Windows.Forms.DialogResult.Cancel: return;

            }

        }
        #endregion

        // RESIZE
        #region [ toolStripMenuItemResizeSelected_Click ]
        private void toolStripMenuItemResizeSelected_Click(object sender, EventArgs e)
        {
            buttonStartResize_Click(sender, e);
        }

      

        #endregion

        // OPEN CONTAINING FOLDER
        #region [ openContainingFolderToolStripMenuItem_Click ]
        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string imagePath = dataGridView1.SelectedCells[0].Value.ToString();

            System.Diagnostics.Process.Start(Path.GetDirectoryName(imagePath));
        }
        #endregion

        // REMOVE FROM LIST
        #region [ removeFromListToolStripMenuItem_Click ]
        private void removeFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewSelectedCellCollection selectedCells = dataGridView1.SelectedCells;

            IEnumerable<string> selectedFileNames = from DataGridViewCell cell in selectedCells select cell.Value.ToString();

            List<string> currentDataSource = imagesToResize.ToList<string>();

            IEnumerable<string> filteredDataSource = (from string file in currentDataSource
                                                      select file).Except(selectedFileNames);


            imagesToResize = filteredDataSource.ToArray<string>();

            RebindGrid(imagesToResize);
        }

        #endregion

        #endregion

        // DRAG & DROP
        #region [ Drag & Drop Events ]

        #region [ ImageResizer_DragEnter ]
        private void ImageResizer_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }

        }
        #endregion

        #region [ ImageResizer_DragDrop ]
        private void ImageResizer_DragDrop(object sender, DragEventArgs e)
        {

            List<string> addedImages = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList<string>();

            List<string> folders = new List<string>();
            List<string> folderImages = new List<string>();

            foreach (string imgPath in addedImages)
            {
                if (Path.GetExtension(imgPath) == string.Empty)
                {
                    string[] filesInFolder = Directory.GetFiles(imgPath);
                    folders.Add(imgPath);
                    folderImages.AddRange(filesInFolder.ToList<string>());
                }
            }

            foreach (string folderToRemove in folders)
            {
                addedImages.Remove(folderToRemove);
            }

            foreach (string folderImgsToAdd in folderImages)
            {
                addedImages.Add(folderImgsToAdd);
            }



            List<string> currentImages = imagesToResize.ToList<string>();

            currentImages.AddRange(addedImages);

            imagesToResize = currentImages.ToArray<string>();


            RebindGrid(imagesToResize);


            textBoxDestinationImagePath.Text = folderBrowserDialog1.SelectedPath;

        }
        #endregion

        #endregion

        // ARROW KEYS
        #region [ ARROW KEYS ]

        // Previous Picture
        #region [ linkLabelPrevious_Click ]
        private void linkLabelPrevious_Click(object sender, EventArgs e)
        {
            MoveToPreviousImage(sender);
        }
        #endregion

        // Next Picture
        #region [ linkLabelNext_LinkClicked ]
        private void linkLabelNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoveToNextImage(sender);
        }
        #endregion


        #region [ MoveToNextImage ]
        private void MoveToNextImage(object sender)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int colIndex = dataGridView1.SelectedCells[0].ColumnIndex;

                if (rowIndex + 1 >= 0 && rowIndex < dataGridView1.Rows.Count - 1)
                {
                    dataGridView1.Rows[rowIndex + 1].Selected = true;
                    dataGridView1.Rows[rowIndex].Selected = false;
                    dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(colIndex, rowIndex + 1));
                }
            }
        }
        #endregion


        #region [ MoveToPreviousImage ]
        private void MoveToPreviousImage(object sender)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int colIndex = dataGridView1.SelectedCells[0].ColumnIndex;

                if (rowIndex - 1 >= 0)
                {
                    dataGridView1.Rows[rowIndex - 1].Selected = true;
                    dataGridView1.Rows[rowIndex].Selected = false;
                    dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(colIndex, rowIndex - 1));
                }
            }
        }
        #endregion

        // DATAGRID KEYDOWN EVENTHANDLER
        #region [ dataGridView1_KeyDown ]
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    MoveToNextImage(sender);
                    break;
                case Keys.Left:
                    MoveToPreviousImage(sender);
                    break;
                case Keys.Enter:
                    if (dataGridView1.SelectedCells.Count > 0)
                    {
                        int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                        string imagePath = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();

                        System.Diagnostics.Process.Start(imagePath);
                        StatusLabel.Text = String.Format("Открыть: {0}", imagePath);
                    }
                    break;
                case Keys.Delete:
                    if (dataGridView1.SelectedCells.Count > 0)
                    {
                        int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                        string path = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();

                        DialogResult result = MessageBox.Show(string.Format("Действительно удалить: {0}", path), "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (File.Exists(path))
                            {
                                File.Delete(path);

                                List<string> images = imagesToResize.ToList();

                                images.Remove(path);

                                imagesToResize = images.ToArray();

                                RebindGrid(imagesToResize);
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        #endregion

        // CLIPBOARD
        #region [ ImageResizer_KeyDown ]
        private void ImageResizer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                SetPresetCropDimensions();
                pasteToolStripMenuItem_Click(sender, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Clipboard.SetImage(pictureBoxPreview.Image);
            }
        }
        #endregion

        // CLEAR
        #region [ buttonClear_Click ]
        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {


                if (_threadFirstHalf != null)
                    _threadFirstHalf.Abort();

                if (_threadSecondHalf != null)
                    _threadSecondHalf.Abort();

                if (_threadThirdHalf != null)
                    _threadThirdHalf.Abort();

                if (_threadFourthHalf != null)
                    _threadFourthHalf.Abort();

                string finishedTime = "00:00:00";

                if (startingTime != DateTime.MinValue)
                    finishedTime = (DateTime.Now - startingTime).ToString();

                StatusLabel.Text = String.Format("Преобразование отменено! {0} изображений изменено в {1}.", resizedImagesCount, finishedTime);

                helper.EnableFormControls(this.Controls);

                textBoxDestinationImagePath.Text = string.Empty;
                textBoxSourceImagesPath.Text = string.Empty;

                dataGridView1.DataSource = null;
                StatusLabel.Text = string.Empty;
                imagesToResize = new string[0];
                progressBar1.Value = 0;
                pictureBoxPreview.Image = null;
                _projectedOutputDirSize = "Вычисление...";

                RestoreFormSize();

                _totalEstimatedTimeRemaining = new TimeSpan();
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                StatusLabel.Text = ex.Message;
            }


        }
        #endregion

        // BROWSE
        #region [ Browse Source & Destination Images ]

        #region [ textBoxDestinationImagePath_MouseDoubleClick ]
        private void textBoxDestinationImagePath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = (sender as TextBox).Text;

            if (Directory.Exists(path))
                System.Diagnostics.Process.Start(path);
        }
        #endregion

        #region [ textBoxSourceImagesPath_MouseDoubleClick ]
        private void textBoxSourceImagesPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = (sender as TextBox).Text;

            if (Directory.Exists(path))
                System.Diagnostics.Process.Start(path);
        }
        #endregion

        #region [ CheckIfPathsAreSame ]
        private bool CheckIfPathsAreSame()
        {
            if (!string.IsNullOrEmpty(textBoxSourceImagesPath.Text) && !string.IsNullOrEmpty(textBoxDestinationImagePath.Text))
            {
                if (Path.GetFullPath(textBoxSourceImagesPath.Text) == Path.GetFullPath(textBoxDestinationImagePath.Text))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region [ CheckAndSetDestinationPath ]
        private bool CheckAndSetDestinationPath()
        {
            if (!Directory.Exists(textBoxDestinationImagePath.Text))
            {
                folderBrowserDialog1.ShowDialog();

                if (Directory.Exists(folderBrowserDialog1.SelectedPath))
                    textBoxDestinationImagePath.Text = folderBrowserDialog1.SelectedPath;
                else
                    return false;

                return true;
            }

            return true;
        }
        #endregion

        #region [ CheckAndSetSourcePath ]
        private bool CheckAndSetSourcePath()
        {
            if (!Directory.Exists(textBoxSourceImagesPath.Text) && (imagesToResize == null || imagesToResize.Length == 0))
            {
                folderBrowserDialogInputFiles.ShowDialog();

                if (Directory.Exists(folderBrowserDialogInputFiles.SelectedPath))
                {
                    imagesToResize = helper.LoadFiles(folderBrowserDialogInputFiles.SelectedPath);

                    RebindGrid(imagesToResize);

                    textBoxSourceImagesPath.Text = folderBrowserDialogInputFiles.SelectedPath;
                }
                else
                    return false;

                return true;
            }

            return true;
        }
        #endregion

        // Browse images to resize
        #region [ buttonBrowseImages_Click ]
        private void buttonBrowseImages_Click(object sender, EventArgs e)
        {
            CheckAndSetSourcePath();
        }
        #endregion

        #region [ buttonBrowseDestination_Click ]
        private void buttonBrowseDestination_Click(object sender, EventArgs e)
        {
            CheckAndSetDestinationPath();
        }
        #endregion

        #region [ buttonOpenAllInSource_Click ]
        private void buttonOpenAllInSource_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxSourceImagesPath.Text))
            {
                dataGridView1.DataSource = null;

                imagesToResize = helper.LoadFiles(textBoxSourceImagesPath.Text);

                RebindGrid(imagesToResize);
            }
        }
        #endregion

        #region [ buttonLoadDestinationImages_Click ]
        private void buttonLoadDestinationImages_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxDestinationImagePath.Text))
            {
                dataGridView1.DataSource = null;

                imagesToResize = helper.LoadFiles(textBoxDestinationImagePath.Text);

                RebindGrid(imagesToResize);
            }
            else
            {
                MessageBox.Show("Неверный каталог. Нажмите кнопку обзор и выберите папку, содержащую допустимые файлы изображений, или введите допустимый путь.", "Неверный каталог", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #endregion

        // FORM LAYOUT
        #region [ FORM LAYOUT ]

        // Move Quality TrackBar
        #region [ trackBarQuality_Scroll ]
        private void trackBarQuality_Scroll(object sender, EventArgs e)
        {
            int value = trackBarQuality.Value;

            if (value < 30)
                labelImageQuality.ForeColor = System.Drawing.Color.Red;
            else if (value >= 30 && value <= 60)
                labelImageQuality.ForeColor = System.Drawing.Color.Orange;
            else if (value > 60)
                labelImageQuality.ForeColor = System.Drawing.Color.Green;

            labelImageQuality.Text = string.Format("{0}%", value.ToString());
        }
        #endregion

        #region [ RestoreFormSize ]
        private void RestoreFormSize()
        {
            this.Width = 1024;
            this.Height = 710;

            StatusLabel.Text = string.Empty;
        }
        #endregion

        #endregion

        // LOG EXCEPTIONS
        #region [ Application_ThreadException ]
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            _logger.LogError(e.Exception);
        }
        #endregion

        // BUTTON CROP-SCROLL
        #region [ buttonCrop_Click ]
        private void buttonCrop_Click(object sender, EventArgs e)
        {
            if (editMode == Enums.EditMode.Scroll)
            {
                editMode = Enums.EditMode.Crop;
                buttonCrop.FlatStyle = FlatStyle.Flat;
                buttonCrop.BackColor = Color.Honeydew;
            }
            else
            {
                editMode = Enums.EditMode.Scroll;
                buttonCrop.FlatStyle = FlatStyle.Standard;
                buttonCrop.BackColor = buttonStartResize.BackColor;
            }
        }
        #endregion

        // Cell DOUBLE CLICK
        #region [ dataGridView1_CellDoubleClick ]
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string imagePath = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            System.Diagnostics.Process.Start(imagePath);
            StatusLabel.Text = String.Format("Открыть: {0}", imagePath);
        }
        #endregion

        // Cell SINGLE CLICK
        #region [ dataGridView1_CellClick ]
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                previewRatio = 1.0F;

                //this.WindowState = FormWindowState.Maximized;
                RestoreFormSize();



                string imagePath = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                Image previewedImage = Image.FromFile(imagePath);

                #region [ Get File Size ]
                double fileSizeBytes = new FileInfo(imagePath).Length;

                double fileSizeKBytes = fileSizeBytes / 1024;

                double fileSizeMBytes = fileSizeKBytes / 1024;

                labelFileSize.Text = string.Format("{0:0,0.00} Байт / {1:0,0.00} КБ / {2:0,0.00} МБ", fileSizeBytes, fileSizeKBytes, fileSizeMBytes);
                #endregion

                //pictureBoxPreview.Width = previewedImage.Width;
                //pictureBoxPreview.Height = previewedImage.Height;



                this._originalImage = previewedImage;
                this._originalThumbnailImage = previewedImage;


                pictureBoxPreview.ImageLocation = imagePath;
                pictureBoxPreview.Image = this._originalThumbnailImage;


                #region [ Display Image Resolution ]
                labelOriginalResolution.Text = string.Format("{0}x{1}", this._originalImage.Width, this._originalImage.Height);
                labelCurrentResolution.Text = string.Format("{0}x{1}", pictureBoxPreview.Width, pictureBoxPreview.Height);
                #endregion



            }
            catch (Exception ex)
            {
                StatusLabel.Text = ex.Message;
            }
        }
        #endregion

        // Pick Image Format
        #region [ comboBoxImageFormat_SelectedIndexChanged ]
        private void comboBoxImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxImageFormat.SelectedItem.ToString().ToLower())
            {
                case "bmp":
                    ImageFileFormat = ImageFormat.Bmp;
                    break;
                case "emf":
                    ImageFileFormat = ImageFormat.Emf;
                    break;
                case "exif":
                    ImageFileFormat = ImageFormat.Exif;
                    break;
                case "gif":
                    ImageFileFormat = ImageFormat.Gif;
                    break;
                case "icon":
                    ImageFileFormat = ImageFormat.Icon;
                    break;
                case "png":
                    ImageFileFormat = ImageFormat.Png;
                    break;
                case "tiff":
                    ImageFileFormat = ImageFormat.Tiff;
                    break;
                case "wmf":
                    ImageFileFormat = ImageFormat.Wmf;
                    break;
                default:
                    break;
            }
        }
        #endregion


        // ZOOM FIT (| |)
        #region [ buttonZoomFit_Click ]
        private void buttonZoomFit_Click(object sender, EventArgs e)
        {
            int originalWidth = this._originalImage.Width;
            int originalHeight = this._originalImage.Height;


            Resizer r = new Resizer();

            r.ImageToResize = this._originalImage;
            r.NewWidth = panelImage.Width - 50;

            while (r.NewHeight > panelImage.Height - 50)
            {
                r.NewWidth--;
            }


            Image zoomedInImage = r.ResizeSingleImage();


            previewRatio = (float)zoomedInImage.Width / (float)originalWidth;

            pictureBoxPreview.Image = zoomedInImage;


            this._originalThumbnailImage = zoomedInImage;

            CenterImageInPanel();


            labelCurrentResolution.Text = string.Format("{0}x{1}", pictureBoxPreview.Width, pictureBoxPreview.Height);
        }
        #endregion

        // ZOOM IN (+)
        #region [ buttonZoomIn_Click ]
        private void buttonZoomIn_Click(object sender, EventArgs e)
        {

            int originalWidth = this._originalImage.Width;
            int originalHeight = this._originalImage.Height;

            previewRatio += 0.076F;

            int zoomInWidth = (int)(this._originalImage.Width * previewRatio);
            int zoomInHeight = (int)(this._originalImage.Height * previewRatio);



            previewRatio = (float)zoomInWidth / (float)originalWidth;

            Resizer r = new Resizer();
            r.ImageToResize = this._originalImage;
            r.NewWidth = zoomInWidth;

            Image zoomedInImage = r.ResizeSingleImage();


            pictureBoxPreview.Image = zoomedInImage;


            this._originalThumbnailImage = zoomedInImage;

            CenterImageInPanel();

            labelCurrentResolution.Text = string.Format("{0}x{1}", pictureBoxPreview.Width, pictureBoxPreview.Height);
        }
        #endregion

        // ZOOM OUT (-)
        #region [ buttonZoomOut_Click ]
        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            int originalWidth = this._originalImage.Width;
            int originalHeight = this._originalImage.Height;

            previewRatio -= 0.076F;

            int zoomInWidth = (int)(this._originalImage.Width * previewRatio);
            int zoomInHeight = (int)(this._originalImage.Height * previewRatio);


            previewRatio = (float)zoomInWidth / (float)originalWidth;

            Resizer r = new Resizer();
            r.ImageToResize = this._originalImage;
            r.NewWidth = zoomInWidth;

            Image zoomedInImage = r.ResizeSingleImage();

            pictureBoxPreview.Image = zoomedInImage;


            this._originalThumbnailImage = zoomedInImage;

            CenterImageInPanel();


            labelCurrentResolution.Text = string.Format("{0}x{1}", pictureBoxPreview.Width, pictureBoxPreview.Height);

        }
        #endregion

        // CENTER IMAGE IN PANEL
        #region [ CenterImageInPanel ]
        private void CenterImageInPanel()
        {
            if (pictureBoxPreview.Width < panelImage.Width)
                pictureBoxPreview.Left = (panelImage.Width - pictureBoxPreview.Width) / 2;


            if (pictureBoxPreview.Height < panelImage.Height)
                pictureBoxPreview.Top = (panelImage.Height - pictureBoxPreview.Height) / 2;
        }
        #endregion



        #region [ ImageResizer_SizeChanged ]
        private void ImageResizer_SizeChanged(object sender, EventArgs e)
        {
            buttonZoomFit_Click(sender, e);
        }
        #endregion

        #region [ RemoveFileFromQueueAndGrid ]
        private void RemoveFileFromQueueAndGrid(string fileName)
        {
            List<string> currentImages = imagesToResize.ToList();

            currentImages.Remove(fileName);

            imagesToResize = currentImages.ToArray();

            RebindGrid(imagesToResize);

            pictureBoxPreview.Image = null;
        }
        #endregion

        #region [ ImageResizer_FormClosing ]
        private void ImageResizer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    foreach (Process p in Process.GetProcesses())
            //    {
            //        string procName = p.ProcessName.ToLower();
            //        if (procName.Contains("simpleimageresizer"))
            //            p.Kill();
            //    }
            //}
            //catch
            //{ }
        }
        #endregion

        #region [ SetPresetCropDimensions ]
        private void SetPresetCropDimensions()
        {
            if (!int.TryParse(textBoxPresetCropWidth.Text, out this._presetCropWidth))
                this._presetCropWidth = 0;

            if (!int.TryParse(textBoxPresetCropHeight.Text, out this._presetCropHeight))
                this._presetCropHeight = 0;
        }
        #endregion

        #region [ FillImageFormatsDropDown ]
        private void FillImageFormatsDropDown()
        {
            foreach (PropertyInfo propertyInfo in ImageFileFormat.GetType().GetProperties())
            {
                comboBoxImageFormat.Items.Add(propertyInfo.Name);
            }

            comboBoxImageFormat.Items.RemoveAt(0);
            comboBoxImageFormat.Items.RemoveAt(0);
            comboBoxImageFormat.SelectedIndex = 4;
        }
        #endregion




















    }
}
