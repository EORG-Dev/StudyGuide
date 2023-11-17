using DocumentFormat.OpenXml.Math;
using StudyGuide.Models;
using StudyGuide.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StudyGuide.ViewModels
{
    public class VM_Settings : VM_Base
    {
        public VM_Settings() { }
        Command _DeleteAllCommand;
        public Command DeleteAllCommand
        {
            get
            {
                return _DeleteAllCommand ?? (_DeleteAllCommand = new Command(ExecuteDeleteAllCommand));
            }
        }
        async void ExecuteDeleteAllCommand(object param)
        {
            var res = await App.Current.MainPage.DisplayAlert("Löschen?", "Möchtest du wirklich ALLE erhobenen DATEN LÖSCHEN? Dieser Schritt ist irreversibel! Möchtest du wirklich alle Daten löschen? Ganz sicher?", "Löschen", "Nein, abbrechen");
            if (res)
            {
                var res1 = await App.Current.MainPage.DisplayAlert("Sicher?", "Bist du dir ganz sicher? Möchtest du wirklich ALLE erhobenen DATEN LÖSCHEN?", "Löschen", "Nein, abbrechen");
                if (res1)
                {
                    // add Beobachtungen to base
                    Srv_Data.RemoveAll<C_Einsatz>();
                    Singleton.Instance.TriggerNavBackEvent();
                }
            }
         
        }
    }
}
