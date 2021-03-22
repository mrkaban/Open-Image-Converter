using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SimpleImageResizer
{
    class Resizer
    {

        #region [ Constructors ]
        public Resizer()
        { }
        #endregion

        #region [ Properties ]
        private int _newWidth;
        public int NewWidth
        {

            get
            {
                return _newWidth;
            }
            set
            {
                _newWidth = value;
            }
        }

        private int _newHeight;
        public int NewHeight
        {
            get
            {
                double resizedImageWidtheightRatio = (double)this._currentImage.Width / (double)this._currentImage.Height;

                _newHeight = (int)Math.Round((double)_newWidth / resizedImageWidtheightRatio);

                return _newHeight;
            }
            set
            {
                _newHeight = value;
            }
        }

        private int _quality;
        public int ImageQuality
        {
            get
            {
                return _quality;
            }
            set
            {
                _quality = value;
            }
        }

        private string[] _filesToResize;
        public string[] FilesToResize
        {
            get
            {
                return _filesToResize;
            }

            set
            {
                _filesToResize = value;
            }
        }

        private string _destinationFolder;
        public string DestionationFolder
        {
            get
            {
                return _destinationFolder;
            }

            set
            {
                _destinationFolder = value;
            }
        }

        private string _rootSourceFolder;
        public string RootSourceFolder
        {
            get
            {
                return _rootSourceFolder;
            }

            set
            {
                _rootSourceFolder = value;
            }
        }

        private ImageFormat _imageFormat = ImageFormat.Jpeg;
        public ImageFormat ImageFileFormat
        {
            get
            {
                return _imageFormat;
            }
            set
            {
                _imageFormat = value;
            }
        }

        private Image _currentImage;
        public Image ImageToResize
        { 
            get
            {
                return _currentImage;
            }
            set
            {
                _currentImage = value;
            }
            }



        #endregion

        #region [ Delegates ]

        public delegate void ImageResized(object sender, TimeSpan resizeTime);

        public delegate void AllImagesResizeCompleted();

        #endregion

        #region [ Events ]

        public event ImageResized ResizeCompleted;

        public event AllImagesResizeCompleted AllImagesResized;

        #endregion

        #region [ Public methods ]

        public void StartResize()
        {
            foreach (string filePath in _filesToResize)
            {
                if (File.Exists(filePath))
                {
                    TimeSpan resizeTime = ResizeImage(filePath);

                    if (ResizeCompleted != null)
                        ResizeCompleted(this, resizeTime);
                }
            }

            AllImagesResized();
        }

        #region [ ResizeSingleImage ]
        public Image ResizeSingleImage()
        {
            Image resizedImage = null;

            try
            {
                Rectangle newSize;

              
             

                newSize = new Rectangle(0, 0, _newWidth, NewHeight);

                resizedImage = new Bitmap(newSize.Width, newSize.Height);

                using (Graphics g = Graphics.FromImage(resizedImage))
                {                    
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //g.SmoothingMode = SmoothingMode.HighQuality;                   


                    g.InterpolationMode = InterpolationMode.Low;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.SmoothingMode = SmoothingMode.None;
                    g.DrawImage(_currentImage, newSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Resized.ResizeSingleImage:{0}", ex.Message), ex);
            }

            return resizedImage;
        }

        #endregion

        #endregion

        #region [ Private methods ]

        #region [ ResizeImage ]
        private TimeSpan ResizeImage(string filePath)
        {

            DateTime startTime = DateTime.Now;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (Image original = Image.FromStream(fs))
                    {

                        _currentImage = original;

                        Rectangle newSize;

                        double originalWidtheightRatio = (double)original.Width / (double)original.Height;

                        int height = (int)Math.Round((double)_newWidth / originalWidtheightRatio);


                        newSize = new Rectangle(0, 0, _newWidth, height);

                        using (Image resizedImage = new Bitmap(newSize.Width, newSize.Height))
                        {
                            using (Graphics g = Graphics.FromImage(resizedImage))
                            {
                                g.DrawImage(original, newSize);

                                string fileName = Path.GetFileNameWithoutExtension(filePath);

                                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                                ImageCodecInfo ici = null;

                                foreach (ImageCodecInfo codec in codecs)
                                {
                                    if (codec.FormatID.Equals(_imageFormat.Guid))
                                        ici = codec;
                                }

                                EncoderParameters ep = new EncoderParameters();
                                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)_quality);


                                string destinationFolder = string.Empty;

                                string rootFolder = _rootSourceFolder.Split('\\').Last();

                                int rootFolderIndex = filePath.LastIndexOf(rootFolder);

                                if (rootFolderIndex != -1)
                                {
                                    destinationFolder = string.Format(@"{0}\{1}", _destinationFolder, filePath.Substring(rootFolderIndex, filePath.Length - rootFolderIndex));
                                    destinationFolder = Path.GetDirectoryName(destinationFolder);
                                }
                                else
                                {
                                    destinationFolder = Path.Combine(_destinationFolder, Path.GetDirectoryName(filePath).Split('\\').Last());
                                }

                                if (!Directory.Exists(destinationFolder))
                                    Directory.CreateDirectory(destinationFolder);

                                resizedImage.Save(string.Format(@"{0}\{1}.{2}", destinationFolder, fileName, _imageFormat.ToString().ToLower()), ici, ep);

                            }
                        }
                    }
                }
            }
            catch
            {

            }

            TimeSpan endTime = DateTime.Now - startTime;

            return endTime;

        }

        #endregion      

        #region [ Crop ]

        public Image Crop(string img, int width, int height, int x, int y)
        {
            throw new NotImplementedException("Still to come...");
        }

        #endregion

        #region [ ValidImageFile ]
        public static Boolean IsValidImageFile(string filePath)
        {
            try
            {
                List<string> allowedExtensions = new List<string>();

                allowedExtensions.Add(".jpg");
                allowedExtensions.Add(".bmp");
                allowedExtensions.Add(".jpeg");
                allowedExtensions.Add(".gif");
                allowedExtensions.Add(".png");

                string extension = System.IO.Path.GetExtension(filePath).ToLower();

                IEnumerable<string> validExtension = from string ext in allowedExtensions where ext == extension select ext;

                if (validExtension.Count() > 0)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region [ GetValidImages ]

        public static string[] GetValidImages(string[] files)
        {
            IEnumerable<string> validFiles = from string file in files where IsValidImageFile(file) select file;

            return validFiles.ToArray<string>();
        }

        #endregion

        #endregion

    }
}
