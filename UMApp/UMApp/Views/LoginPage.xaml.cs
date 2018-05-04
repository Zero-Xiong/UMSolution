using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UMApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Test", "I am testing", "OK");
        }
    }
}