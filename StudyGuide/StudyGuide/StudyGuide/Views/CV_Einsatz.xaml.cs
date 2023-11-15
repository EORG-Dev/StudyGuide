using StudyGuide.ViewModels;
using Xamarin.Forms;

namespace StudyGuide.Views
{
    public partial class CV_Einsatz : ContentView
    {
        public CV_Einsatz()
        {
            InitializeComponent();

            BindingContext = new VM_Einsatz();
        }
    }
}

