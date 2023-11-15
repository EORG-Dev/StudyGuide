using StudyGuide.Models;
using StudyGuide.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudyGuide.ViewModels
{
    public class VM_MainPage : VM_Base
    {
        public VM_MainPage()
        {

        }
        int _selectedTabIndex = 0;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged();
            }
        }
        Command _OpenAddCommand;
        public Command OpenAddCommand
        {
            get
            {
                return _OpenAddCommand ?? (_OpenAddCommand = new Command(ExecuteOpenAddCommand));
            }
        }
        async void ExecuteOpenAddCommand(object param)
        {
            try
            {
                App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
                //var edit = new CP_EditE(Srv_Data.GetPreset());
                //edit.Finished += (s, e) => { Srv_Data.Insert<Einsatz>(s); };
                //Navigation.PushAsync(edit);
                //SelectedTabIndex = 2;
            } catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "Abbrechen");
            }
        }
    }
}
