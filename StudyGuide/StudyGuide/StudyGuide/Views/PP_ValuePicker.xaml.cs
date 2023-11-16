using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGuide.Models;
using StudyGuide.Services;
using StudyGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PP_ValuePicker : ContentPage
    {
        public PP_ValuePicker(Button param)
        {
            InitializeComponent();
            //Button
            Button = param;
        }
        //var
        Button Button;
        //Keypad
        private void aC(string c)
        {
            if (EDI_number.Text == "-1") EDI_number.Text = "";
            EDI_number.Text = EDI_number.Text + c;
        }
        private void B1_Clicked(object sender, EventArgs e) { aC("1"); }
        private void B2_Clicked(object sender, EventArgs e) { aC("2"); }
        private void B3_Clicked(object sender, EventArgs e) { aC("3"); }
        private void B4_Clicked(object sender, EventArgs e) { aC("4"); }
        private void B5_Clicked(object sender, EventArgs e) { aC("5"); }
        private void B6_Clicked(object sender, EventArgs e) { aC("6"); }
        private void B7_Clicked(object sender, EventArgs e) { aC("7"); }
        private void B8_Clicked(object sender, EventArgs e) { aC("8"); }
        private void B9_Clicked(object sender, EventArgs e) { aC("9"); }
        private void B0_Clicked(object sender, EventArgs e) { aC("0"); }

        private void BAbbr_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void BDelete_Clicked(object sender, EventArgs e)
        {
            EDI_number.Text = "";
        }
        private void BOK_Clicked(object sender, EventArgs e)
        {
            string TS = EDI_number.Text;
            Button.Text = $"{TS}";
            Navigation.PopAsync();
        }

        private void Bkomma_Clicked(object sender, EventArgs e) { aC(","); }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}