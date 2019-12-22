namespace Movies
{
    using dotMorten.Xamarin.Forms;
    using Movies.Configuration;
    using Movies.ViewModels;
    using Movies.Views;
    using Rg.Plugins.Popup.Services;
    using System.ComponentModel;
    using System.Linq;
    using Xamarin.Forms;

    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainViewModel ViewModel => (MainViewModel)BindingContext;
        public MainPage()
        {
            InitializeComponent();
        }

        //Autocomplete event
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            AutoSuggestBox box = (AutoSuggestBox)sender;

            if (e.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                    box.ItemsSource = null;
                else
                {
                    var suggestions = ViewModel.ReceiveSuggestions(box.Text);
                    box.ItemsSource = suggestions.ToList();
                }
            }
        }

        //Suggestion selected
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs e)
        {
            var selecteditem = e.SelectedItem.ToString();
            var SelectedMovie = AppSettings.Instance.Searches.Find(x => x.Title == selecteditem);
            PopupNavigation.PushAsync(new PopUpMovieView(SelectedMovie));
        }
    }
}
