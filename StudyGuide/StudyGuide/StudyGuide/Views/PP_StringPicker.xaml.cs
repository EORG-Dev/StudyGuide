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
    public partial class PP_StringPicker : ContentPage
    {
        // Rework to make it search the options db and so on
        public PP_StringPicker(Button param, List<string> options)
        {
            InitializeComponent();
            Button = param;
            Load(options);
        }
        Button Button;
        List<int> SelectedValues = new List<int>();
        void Load(List<string> options)
        {
            // Load from List
            SLO.Children.Clear();
            int i = 0;
            foreach (string s in options)
            {
                var b = new Button()
                {
                    Text = s,
                    WidthRequest = 380,
                    HeightRequest = 48,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                b.Clicked += B_Selected;
                SLO.Children.Add(b);
                i++;
            }
        }
        async void B_Selected(object sender, EventArgs args)
        {
            var s = sender as Button;
            if (s != null)
            {
                await Navigation.PopAsync();
                Button.Text = s.Text;
            }
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}