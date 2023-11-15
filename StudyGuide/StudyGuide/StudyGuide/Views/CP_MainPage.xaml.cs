using StudyGuide.ViewModels;
using Xamarin.Forms;

namespace StudyGuide.Views
{
    public partial class CP_MainPage : ContentPage
    {
        public CP_MainPage()
        {
            InitializeComponent();

            BindingContext = new VM_MainPage();
        }
    }
}
