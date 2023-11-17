using StudyGuide.Models;
using StudyGuide.ViewModels;
using System;
using Xamarin.Forms;

namespace StudyGuide.Views
{
    public partial class CP_EditBeob : ContentPage
    {
        public CP_EditBeob(C_Beobachtung init, bool isEdit)
        {
            InitializeComponent();

            var bc = new VM_EditBeob(init, isEdit, this);
            BindingContext = bc;

            // BTN_geschlecht.Text = init.Geschlecht;
            // BTN_Trsp.Text = init.RDVersorgung;
        }
    }
}
