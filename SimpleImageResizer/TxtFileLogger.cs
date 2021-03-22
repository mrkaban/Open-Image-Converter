using System;
using System.Configuration;
using System.IO;
using System.Text;


namespace SimpleImageResizer
{
    public class TxtFileLogger 
    {
        private readonly string _folderPath;
        private readonly bool _throwExceptions;
        
        public TxtFileLogger(string folder, bool throwExceptions)
        {
            this._folderPath = folder;
            this._throwExceptions = throwExceptions;
        }

        public string FileName
        {
            get
            {
                return string.Format("{0}_{1}_{2}.txt", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year);
            }
        }

        public string CreateExceptionText(Exception ex)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine();
            result.AppendFormat("{0}:{1}", DateTime.Now, ex.Message);
            result.AppendLine();

            return result.ToString();
        }

        public void LogError(string error)
        {
            try
            {
                string fileName = Path.Combine(this._folderPath, this.FileName);

                if (File.Exists(fileName))
                {
                    File.AppendAllText(fileName, error);
                }
                else
                {
                    if (!Directory.Exists(this._folderPath))
                    {
                        Directory.CreateDirectory(this._folderPath);
                    }

                    File.WriteAllText(fileName, error);
                }
            }
            catch (Exception ex1)
            {
                if (this._throwExceptions) throw ex1;
            }
        }

        public void LogError(Exception ex)
        {
            this.LogError(this.CreateExceptionText(ex));
        }
    }
}