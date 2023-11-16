using StudyGuide.Models;
using StudyGuide.ViewModels;
using System;
using Xamarin.Forms;

namespace StudyGuide.Views
{
    public partial class CP_EditE : ContentPage
    {
        public CP_EditE(C_Einsatz init, bool isEdit)
        {
            InitializeComponent();

            var bc = new VM_EditE(init, isEdit);
            BindingContext = bc;

            // BTN_geschlecht.Text = init.Geschlecht;
            // BTN_Trsp.Text = init.RDVersorgung;
        }
    }
}
