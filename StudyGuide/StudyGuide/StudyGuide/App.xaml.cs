using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyGuide
{
    public partial class App : Application
    {
        public App(string folderpath)
        {
            InitializeComponent();
            Sharpnado.Tabs.Initializer.Initialize(false, true);

            MainPage = null;
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
