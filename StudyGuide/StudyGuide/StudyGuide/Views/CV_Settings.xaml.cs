using StudyGuide.Models;
using StudyGuide.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyGuide.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CV_Settings : ContentView
	{
		public CV_Settings ()
		{
			InitializeComponent ();

            BindingContext = new VM_Settings();

            // Load

            var set = C_Settings.Load(Singleton.Instance.SettingsFilePath);
            ENT_Name.Text = set.Name;
            ENT_Vorname.Text = set.Vorname;
            ENT_Zugehorigkeit.Text = set.Zugehorigkeit;
		}
        private void BTN_Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                var set = C_Settings.Load(Singleton.Instance.SettingsFilePath);
                set.Name = ENT_Name.Text;
                set.Vorname = ENT_Vorname.Text;
                set.Zugehorigkeit = ENT_Zugehorigkeit.Text;
                set.SaveFile(Singleton.Instance.SettingsFilePath);
                Application.Current.MainPage.DisplayAlert("Erfolg", "Die geänderten Einstellungen wurden erfolgreich gespeichert!", "Ok");
            } catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Fehler", "Die geänderten Einstellungen konnten nicht gespeichert werden. Versuche es nochmal!", "Ok");
            }
        }
    }
}