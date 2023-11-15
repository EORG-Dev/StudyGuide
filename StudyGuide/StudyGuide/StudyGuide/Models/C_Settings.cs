using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Essentials;

namespace StudyGuide.Models
{
    [DataContract]
    public class C_Settings
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Vorname { get; set; }
        [DataMember]
        public string Zugehorigkeit { get; set; }

        public static C_Settings Load(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                return new C_Settings();
            }
            using (FileStream reader = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(C_Settings));
                return (C_Settings)ser.ReadObject(reader);
            }
        }
        public bool SaveFile(string FilePath)
        {
            try
            {
                using (FileStream writer = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(C_Settings));
                    ser.WriteObject(writer, this);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Die Einstellungen konnten nicht in den Dateien gespeichert werden.\n\n {ex.Message}");
            }

        }
    }
}
