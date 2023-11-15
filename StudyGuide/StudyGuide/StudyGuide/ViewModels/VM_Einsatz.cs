﻿using Proj_AzubiGuide.Models;
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
        }
        ObservableCollection<C_Einsatz> _dataSource;
        void LoadData()
        {
            try
            {
                var source = new List<C_Einsatz>()
                {
                    // Fake data
                    new C_Einsatz()
                    {
                       
                    },
                };//Srv_Data.GetAll<C_Einsatz>();
                ObservableCollection<C_Einsatz> Data = new ObservableCollection<C_Einsatz>();

                // Add the last 20 entries for editing by user
                int c = 0;
                for (var i = source.Count - 1; i >= 0; i--)
                {
                    if (c == 20)
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
                //Srv_Data.Remove<C_Einsatz>(Srv_Data.GetByID(Convert.ToString(param)));
                App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
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
            App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");

            /*var edit = new CP_EditE(Srv_Data.GetByID(Convert.ToString(param)));
            
            edit.Finished += (s, e) => { Srv_Data.InsertOrReplace<C_Einsatz>(s); LoadData(); };
            Navigation.PushAsync(edit);*/
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
                App.Current.MainPage.DisplayAlert("Achtung", "Hier fehlt ein Feature :)", "Abbrechen");
                // Build File
                /*EinsatzExport exp = new EinsatzExport();
                exp.Entries = Srv_Data.GetAll<C_Einsatz>().ToList();
                exp.DBOptions = Resources.Tree;
                exp.Save(App.FolderPath + @"/tempexp.xml");
                //Share file
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Wählen Sie ein Programm zum Teilen der Datenbank",
                    File = new ShareFile(App.FolderPath + @"/tempexp.xml")
                });*/
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