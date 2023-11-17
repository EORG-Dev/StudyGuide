using StudyGuide.Models;
using StudyGuide.Services;
using StudyGuide.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudyGuide.ViewModels
{
    public class VM_Einsatz : VM_Base
    {
        public VM_Einsatz()
        {
            LoadData();
            Singleton.Instance.OnNavBack += (s, e) =>
            {
                LoadData();
            };
        }
        void LoadData()
        {
            try
            {
                var source = Srv_Data.GetAll<C_Einsatz>();
                ObservableCollection<C_Einsatz> Data = new ObservableCollection<C_Einsatz>();

                // Add the last 10 entries for editing by user
                int c = 0;
                for (var i = source.Count - 1; i >= 0; i--)
                {
                    if (c == 10)
                        break;
                    Data.Add(source[i]);
                    c++;
                }

                // Display data
                DataSource = Data;
            } catch (Exception ex)
            {
                App.Current.MainPage = new ContentPage();
                App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "Abbrechen");
            }
        }
        ObservableCollection<C_Einsatz> _dataSource;
        public ObservableCollection<C_Einsatz> DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                OnPropertyChanged();
            }
        }
        Command _NewEntryCommand;
        public Command NewEntryCommand
        {
            get
            {
                return _NewEntryCommand ?? (_NewEntryCommand = new Command(ExecuteNewEntryCommand));
            }
        }
        async void ExecuteNewEntryCommand(object param)
        {
            LoadData();
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
                Srv_Data.Remove<C_Einsatz>(Srv_Data.GetByID(Convert.ToString(param)));
                LoadData();
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
            var edit = new CP_EditE(Srv_Data.GetByID(Convert.ToString(param)), true);
            Navigation.Instance.PushAsync(edit);
        }
        Command _ExportCommand;
        public Command ExportCommand
        {
            get
            {
                return _ExportCommand ?? (_ExportCommand = new Command(ExecuteExportCommand));
            }
        }
        async void ExecuteExportCommand(object param)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Exportieren", "Mit dieser Funktion kannst du deine Beobachtungsergebnisse exportieren und mit deiner Mail-App teilen, um sie deinem Ansprechpartner zuzusenden. Möchtest du fortfahren?", "Ja", "Nein");
                if (res)
                {
                    Navigation.Instance.PopAsync();
                    Singleton.Instance.TriggerNavBackEvent();
                }
                //App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
                // Build File
                var list = Srv_Data.GetAll<C_Einsatz>().ToList();
                await SRV_Export.ExportEntries(list);
            } catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Fehler!", ex.Message, "Ok");
            }
        }
        Command _ImportCommand;
        public Command ImportCommand
        {
            get
            {
                return _ImportCommand ?? (_ImportCommand = new Command(ExecuteImportCommand));
            }
        }
        async void ExecuteImportCommand(object param)
        {
            try
            {
                App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
                /*var options = new PickOptions
                {
                    PickerTitle = "Bitte eine Datenbank-Datei (.xml) auswählen.",
                };
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    var load = EinsatzExport.Load(result.FullPath);
                    foreach (var entry in load.Entries)
                    {
                        Srv_Data.Insert<C_Einsatz>(entry);
                        LoadData();
                    }
                }*/
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Fehler!", ex.Message, "Ok");
            }
        }
    }
}
