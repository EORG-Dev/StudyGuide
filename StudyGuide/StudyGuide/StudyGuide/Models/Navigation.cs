using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StudyGuide.Models
{
    public class Navigation
    {
        private static readonly object padlock = new object();
        private static INavigation _instance;
        public static INavigation Instance
        {
            get
            {
                lock (padlock) 
                {
                    if (_instance == null)
                    {
                        throw new Exception("Fehler - Singleton Navigation nicht initialisiert.");
                    }
                    return _instance;
                }
            }
            set
            {
                _instance = value;
            }
        }
    }
}
