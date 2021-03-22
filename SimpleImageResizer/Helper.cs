using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SimpleImageResizer
{
    public class Helper
    {

        // Load Files
        #region [ LoadFiles ]
        List<string> files = new List<string>();

        public string[] LoadFiles(string path)
        {
            string[] subDirs = Directory.GetDirectories(path);

            if (subDirs.Length > 0)
            {
                foreach (string subDir in subDirs)
                {
                    LoadFiles(subDir);
                }
            }

            foreach (string file in Directory.GetFiles(path))
            {
                if (!string.IsNullOrEmpty(file) && Resizer.IsValidImageFile(file))
                    files.Add(file);
            }

            return files.ToArray();
        }
        #endregion


        #region [ DisableFormControls ]
        public void DisableFormControls(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control ctl in controls)
            {
                Type ctlType = ctl.GetType();

                if (ctlType != typeof(ProgressBar) && ctlType != typeof(StatusStrip) && ctlType != typeof(Label) && !ctl.Name.Equals("buttonClear"))
                {
                    ctl.Enabled = false;
                }
            }
        }
        #endregion

        #region [ EnableFormControls ]
        public void EnableFormControls(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control ctl in controls)
            {
                ctl.Enabled = true;
            }
        }
        #endregion

        public double GetProjectedFileSize(string path)
        {
            this.files = new List<string>();
            string[] files = LoadFiles(path);

            long[] sizes = new long[files.Length];

            int i = 0;
            foreach (string fileName in files)
            {
                FileInfo fi = new FileInfo(fileName);

                sizes[i] = fi.Length;

                i++;
            }

            long totalAvgSize = 0;

            for (int j = 0; j < sizes.Length; j++)
            {
                totalAvgSize = totalAvgSize + sizes[j];
            }

            totalAvgSize = totalAvgSize / sizes.Length;

            return ConvertBytesToMegabytes(totalAvgSize);
        }

        public double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public string GetTempFileName()
        {
            DateTime dateTimeNow = DateTime.Now;

            string year = dateTimeNow.Year.ToString();
            string month = dateTimeNow.Month.ToString();
            string days = dateTimeNow.Day.ToString();

            month = month.Length == 1 ? string.Format("0{0}", month) : month;
            days = days.Length == 1 ? string.Format("0{0}", days) : days;

            string hour = dateTimeNow.Hour.ToString();
            string minute = dateTimeNow.Minute.ToString();
            string second = dateTimeNow.Second.ToString();

            hour = hour.Length == 1 ? string.Format("0{0}", hour) : hour;
            minute = minute.Length == 1 ? string.Format("0{0}", minute) : minute;
            second = second.Length == 1 ? string.Format("0{0}", second) : second;

            string fileName = string.Format("SIMP_Clipboard_{0}{1}{2}_{3}{4}{5}", year, month, days, hour, minute, second);

            return fileName;

        }

    }
}
