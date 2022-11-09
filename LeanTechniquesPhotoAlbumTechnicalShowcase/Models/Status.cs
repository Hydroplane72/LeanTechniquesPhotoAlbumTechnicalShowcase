using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanTechniquesPhotoAlbumTechnicalShowcase.Models
{
    /// <summary>
    /// Used throughout to detirmine the state of happenings in mostly services. Provides ability to allow the program to error and continue still.
    /// </summary>
    internal class Status
    {
        public Status(bool pDefaultStatus)
        {
            IsSuccessfull = pDefaultStatus;

            if (pDefaultStatus == false)
            {
                mLastError = "Defaulted to False status";
            }
        }
        public bool IsSuccessfull { get; set; }

        private string mLastError;

        public string LastError { get
            {
                return mLastError;
            } 
        }

        public List<string> PastErrorMessages { get; set; }

        public void AddNewErrorMessage(string pSource, string pErrorMessage)
        {
            PastErrorMessages.Add(pSource + " | " +pErrorMessage);

            mLastError = pErrorMessage;
        }

        internal string GetErrors()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string errorMessage in PastErrorMessages)
            {
                sb.AppendLine(errorMessage);
            }
            return sb.ToString();
        }
    }
}
