using StudyGuide.Models;
using StudyGuide.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyGuide
{
    public partial class App : Application
    {
        public App(string folderpath)
        {
            // Initialize
            InitializeComponent();
            Sharpnado.Tabs.Initializer.Initialize(false, true);

            // Setup Singleton
            Singleton.Instance.FolderPath = folderpath;

            // Navigation
            MainPage = new CP_MainPage();

            // Add Navigation
            Navigation.Instance = MainPage.Navigation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
