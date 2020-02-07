using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinTutorial
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnLoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(entryEmail.Text) || string.IsNullOrEmpty(entryPassword.Text))
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }

        }
    }
}
