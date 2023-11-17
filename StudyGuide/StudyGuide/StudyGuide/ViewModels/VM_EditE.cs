using StudyGuide.Models;
using StudyGuide.Services;
using StudyGuide.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudyGuide.ViewModels
{
    public class VM_EditE : VM_Base
    {
        public VM_EditE(C_Einsatz init, bool isEdit)
        {
            Base = init;
            IsEdit = isEdit;
            List = new ObservableCollection<C_Beobachtung>(Base.Beobachtungen ?? new List<C_Beobachtung>());

            Singleton.Instance.OnAddEntry += (s, e) =>
            {
                var entry = s as C_Beobachtung;
                List?.Add(entry);
            };
        }
        bool IsEdit;
        C_Einsatz _base;
        public C_Einsatz Base
        {
            get { return _base; }
            set 
            { 
                _base = value; 
                OnPropertyChanged();
            }
        }
        ObservableCollection<C_Beobachtung> _list;
        public ObservableCollection<C_Beobachtung> List
        {
            get { return _list; }
            set
            {
                _list = value;
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

        Command _SelectionEinweisungCommand;
        public Command SelectionEinweisungCommand
        {
            get
            {
                return _SelectionEinweisungCommand ?? (_SelectionEinweisungCommand = new Command(ExecuteSelectionEinweisungCommand));
            }
        }
        async void ExecuteSelectionEinweisungCommand(object param)
        {
            var p = new PP_StringPicker(param as Button, new List<string>()
            {
                "Arztpraxis",
                "KV-Dienst",
                "Polizei",
                "BF/FFW",
                "Rettungsdienst",
                "NA/NÄ",
                ""
            });
            Navigation.Instance.PushAsync(p);
        }
        Command _AddCommand;
        public Command AddCommand
        {
            get
            {
                return _AddCommand ?? (_AddCommand = new Command(ExecuteAddCommand));
            }
        }
        async void ExecuteAddCommand(object param)
        {
            var cp = new CP_EditBeob(new C_Beobachtung(), false);
            Navigation.Instance.PushAsync(cp);
        }
        Command _DeleteCommand;
        public Command DeleteCommand
        {
            get
            {
                return _DeleteCommand ?? (_DeleteCommand = new Command(ExecuteDeleteCommand));
            }
        }
        async void ExecuteDeleteCommand(object param)
        {
            bool answer = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Löschen?", "Möchtest du den Eintrag wirklich löschen?", "Ja", "Nein");
            if (answer)
            {
                string id = param as string;
                var item = List.Where(X => X.ID == id).FirstOrDefault();
                List.Remove(item);
            }
        }
        Command _EditCommand;
        public Command EditCommand
        {
            get
            {
                return _EditCommand ?? (_EditCommand = new Command(ExecuteEditCommand));
            }
        }
        async void ExecuteEditCommand(object param)
        {
            string id = param as string;
            var item = List.Where(X => X.ID == id).FirstOrDefault();
            var edit = new CP_EditBeob(item, true);
            Navigation.Instance.PushAsync(edit);
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
                var res = await App.Current.MainPage.DisplayAlert("Abbrechen?", "Möchtest du das Erstellen einer Einsatzdokumentation wirklich abbrechen?", "Ja", "Nein");
                if (!res)
                    return;
            }
            Navigation.Instance.PopAsync();
            Singleton.Instance.TriggerNavBackEvent();
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
            // add Beobachtungen to base
            Base.Beobachtungen = _list.ToList();

            // Save whole
            if (IsEdit)
            {
                Srv_Data.InsertOrReplace<C_Einsatz>(Base);
            } else
            {
                Srv_Data.Insert<C_Einsatz>(Base);
            }
            Navigation.Instance.PopAsync();
            Singleton.Instance.TriggerNavBackEvent();
        }
        #endregion Commands
    }
}
