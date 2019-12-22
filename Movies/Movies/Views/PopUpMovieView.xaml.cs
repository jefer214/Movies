using Movies.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpMovieView
    {
        public PopUpMovieView(Movie search)
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.PopUpMovieViewModel(search);
        }
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}