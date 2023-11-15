using StudyGuide.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudyGuide.Models
{
    public class CButton : Button
    {
        //um event erweitern
        public CButton()
        {
            //this.BorderColor = Color.FromHex("#d10f32");
            TextChanged+= (sender, e) => { };
        }
        public static readonly BindableProperty CTextProperty =
            BindableProperty.Create(nameof(CText),
            typeof(string),
            typeof(CButton),
            default(string),
            BindingMode.TwoWay,
            propertyChanged: OnTextChanged);
        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CButton)bindable;
            var value = (string)newValue;
            control.CText = value;
        }
        public string CText
        {
            get => (string)GetValue(TextProperty);
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == "-1")
                {
                    SetValue(TextProperty, "Auswahl");
                } else
                {
                    SetValue(TextProperty, value);
                }
                TextChangedEvent(this, new EventArgs());
            }
        }
        protected virtual void TextChangedEvent(object sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = TextChanged;
            handler(sender, e);
        }
        public event EventHandler<EventArgs> TextChanged;

        // Request
        string _request = "";
        public string CRequest
        {
            get => _request;
            set
            {
                _request = value;
                //RequestChangedEvent(this, new EventArgs());
            }
        }
        bool _multi = false;
        public bool CMulti
        {
            get => _multi;
            set
            {
                _multi = value;
                //RequestChangedEvent(this, new EventArgs());
            }
        }
    }
    public class CImageButton : ImageButton
    {
        public CImageButton()
        {

        }
        string _request = "";
        public string CRequest
        {
            get => _request;
            set
            {
                _request = value;
                //RequestChangedEvent(this, new EventArgs());
            }
        }
    }
}
