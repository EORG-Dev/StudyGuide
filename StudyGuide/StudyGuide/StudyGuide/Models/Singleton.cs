using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StudyGuide.Models
{
    public class Singleton
    {
        private static readonly object padlock = new object();
        public static Singleton _instance;
        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                    return _instance;
                }
            }
            set
            {
                _instance = value;
            }
        }
        #region Parameter
        // FileSystem
        public string FolderPath { get; set; }
        public string DataFilePath
        {
            get
            {
                return Path.Combine(FolderPath, "data.xml"); 
            }
        }
        public string SettingsFilePath
        {
            get
            {
                return Path.Combine(FolderPath, "settings.xml");
            }
        }
        #endregion Parameter
    }
}
