﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.XPath;

namespace StudyGuide.Models
{
    public class Singleton
    {
        private static readonly object padlock = new object();
        static Singleton _instance;
        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                        _instance.OnNavBack += (s, e) => { };
                        _instance.OnAddEntry += (s, e) => { };
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
        public string TemplateFilePath
        {
            get
            {
                return Path.Combine(FolderPath, "template.xlsx");
            }
        }
        public void TriggerNavBackEvent()
        {
            OnNavBackEvent(null, new EventArgs());
        }
        protected virtual void OnNavBackEvent(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = OnNavBack;
            handler(sender, e);
        }
        public event EventHandler<EventArgs> OnNavBack;
        public void TriggerAddEntryEvent(C_Beobachtung result)
        {
            OnAddEntryEvent(result, new EventArgs());
        }
        protected virtual void OnAddEntryEvent(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = OnAddEntry;
            handler(sender, e);
        }
        public event EventHandler<EventArgs> OnAddEntry;
        #endregion Parameter
    }
}
