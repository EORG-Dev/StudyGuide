using StudyGuide.Models;
using StudyGuide.Services;
using StudyGuide.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudyGuide.ViewModels
{
    public class VM_EditBeob : VM_Base
    {
        public VM_EditBeob(C_Beobachtung init, bool isEdit, CP_EditBeob view)
        {
            Base = init;
            IsEdit = isEdit;
            View = view;
        }
        CP_EditBeob View { get; set; }
        C_Beobachtung _base;
        bool IsEdit;
        public C_Beobachtung Base
        {
            get { return _base; }
            set 
            { 
                _base = value; 
                OnPropertyChanged();
            }
        }
        int _TestInts = 0;
        public int TestInts
        {
            get { return _TestInts; }
            set
            {
                _TestInts = value;
                OnPropertyChanged();
            }
        }
        #region Commands
        Command _SelectionSexCommand;
        public Command SelectionSexCommand
        {
            get
            {
                return _SelectionSexCommand ?? (_SelectionSexCommand = new Command(ExecuteSelectionSexCommand));
            }
        }
        async void ExecuteSelectionSexCommand(object param)
        {
            //App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
            var p = new PP_StringPicker(param as Button, new List<string> { "m", "w", "d", "" });
            Navigation.Instance.PushAsync(p);
        }
        Command _ZahlCommand;
        public Command ZahlCommand
        {
            get
            {
                return _ZahlCommand ?? (_ZahlCommand = new Command(ExecuteZahlCommand));
            }
        }
        async void ExecuteZahlCommand(object param)
        {
            try
            {
                PP_ValuePicker vp = new PP_ValuePicker(param as Button);
                Navigation.Instance.PushAsync(vp);
            } catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "Abbrechen");
            }
            
        }
        Command _SelectionPhaseCommand;
        public Command SelectionPhaseCommand
        {
            get
            {
                return _SelectionPhaseCommand ?? (_SelectionPhaseCommand = new Command(ExecuteSelectionPhaseCommand));
            }
        }
        async void ExecuteSelectionPhaseCommand(object param)
        {
            var p = new PP_StringPicker(param as Button, new List<string>()
            {
                "1. ab Alarmierung",
                "2. ab Ankunft EO",
                "3. ab Transport",
                "4. nach dem Einsatz",
            });
            Navigation.Instance.PushAsync(p);
        }
        Command _SelectionAkteurCommand;
        public Command SelectionAkteurCommand
        {
            get
            {
                return _SelectionAkteurCommand ?? (_SelectionAkteurCommand = new Command(ExecuteSelectionAkteurCommand));
            }
        }
        async void ExecuteSelectionAkteurCommand(object param)
        {
            var p = new PP_StringPicker(param as Button, new List<string>()
            {
                "RD",
                "Arzt",
                "Patient",
                "Angehörige",
                "Dritte"
            });
            Navigation.Instance.PushAsync(p);
        }
        Command _SelectionSymbolCommand;
        public Command SelectionSymbolCommand
        {
            get
            {
                return _SelectionSymbolCommand ?? (_SelectionSymbolCommand = new Command(ExecuteSelectionSymbolCommand));
            }
        }
        async void ExecuteSelectionSymbolCommand(object param)
        {
            var p = new PP_StringPicker(param as Button, new List<string>()
            {
                "Konflikt",
                "Nachfrage",
                "Kritizitätswechel",
                "Improvisation"
            });
            Navigation.Instance.PushAsync(p);
        }
        Command _CloseCommand;
        public Command CloseCommand
        {
            get
            {
                return _CloseCommand ?? (_CloseCommand = new Command(ExecuteCloseCommand));
            }
        }
        async void ExecuteCloseCommand(object param)
        {
            if (!IsEdit)
            {
                var res = await App.Current.MainPage.DisplayAlert("Abbrechen?", "Möchtest du das Erstellen einer Beobachtung wirklich abbrechen?", "Ja", "Nein");
                if (!res)
                    return;
            }
            View.Navigation.PopAsync();
            //Singleton.Instance.TriggerAddEntryEvent();
        }
        Command _SaveCommand;
        public Command SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new Command(ExecuteSaveCommand));
            }
        }
        async void ExecuteSaveCommand(object param)
        {
            View.Navigation.PopAsync();
            Singleton.Instance.TriggerAddEntryEvent(Base);
        }
        #endregion Commands
    }
}
