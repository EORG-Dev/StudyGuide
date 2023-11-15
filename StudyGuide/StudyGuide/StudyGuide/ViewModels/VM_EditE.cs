using StudyGuide.Models;
using StudyGuide.Services;
using System;
using System.Collections.Generic;
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
        }
        C_Einsatz _base;
        bool IsEdit;
        public C_Einsatz Base
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
        Command _SelectionCommand;
        public Command SelectionCommand
        {
            get
            {
                return _SelectionCommand ?? (_SelectionCommand = new Command(ExecuteSelectionCommand));
            }
        }
        async void ExecuteSelectionCommand(object param)
        {
            App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
            //var p = new PP_StringPicker(param);
            //Navigation.PushAsync(p);
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
                App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
                //PP_ValuePicker vp = new PP_ValuePicker(param as CButton);
                //Navigation.PushAsync(vp);
            } catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "Abbrechen");
            }
            
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
            Navigation.Instance.PopToRootAsync();
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
            if (IsEdit)
            {
                Srv_Data.InsertOrReplace<C_Einsatz>(Base);
            } else
            {
                Srv_Data.Insert<C_Einsatz>(Base);
            }
            Navigation.Instance.PopToRootAsync();
            Singleton.Instance.TriggerNavBackEvent();
        }
        #endregion Commands
    }
}
