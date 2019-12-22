namespace Movies.ViewModels
{
    using Models;
    using Movies.Configuration;
    using Movies.Views;
    using Rg.Plugins.Popup.Services;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel: INotifyPropertyChanged
    {
        ApiService apiService = new ApiService();
        
        #region Constructor
        public MainViewModel()
        {
            GetMovies();
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private properties
        private List<string> _suggestions { get; set; }
        private List<Movie> _movieItems { get; set; }
        private int _count { get; set; }

        private Movie _selectedMovie;
        #endregion

        #region Public properties

        public List<string> Suggestions
        {
            get
            {
                return _suggestions;
            }
            set
            {
                _suggestions = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Suggestions)));
            }
        }

        public List<Movie> MovieItems
        {
            get
            {
                return _movieItems;
            }
            set
            {
                _movieItems = value;
                PropertyChanged?.Invoke(
                    this, 
                    new PropertyChangedEventArgs(nameof(MovieItems)));
            }
        }

        
        public Movie SelectedMovie
        {
            get
            {
                return _selectedMovie;
            }
            set
            {
                if (_selectedMovie != value)
                {
                    _selectedMovie = value;
                    NavigateToSelectedMovieAsync(SelectedMovie);
                }
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Count)));
            }
        }
        #endregion

        #region Commands
        public ICommand LaunchPopupMovieCommand => new Command<Movie>(NavigateToSelectedMovieAsync);
        #endregion

        #region Methods

        public async void GetMovies()
        {
            Suggestions = new List<string>();
            var TotalMovieItems = new List<Movie>();

            for (int i = 1; i < 6; i++)
            {
                var response = await apiService.SearchContentsAsync("scary", i);

                TotalMovieItems.AddRange(response);
            }
            
            foreach (var item in TotalMovieItems)
            {
                Suggestions.Add(item.Title);
            }

            AppSettings.Instance.Searches = TotalMovieItems;
            MovieItems = TotalMovieItems;
            Count = TotalMovieItems.Count;
        }

        public IEnumerable<String> ReceiveSuggestions(string text)
        {
            return Suggestions.Where(x => x.StartsWith(text, StringComparison.InvariantCultureIgnoreCase));
        }

        protected async void NavigateToSelectedMovieAsync(Movie movieData)
        {
            AppSettings.Instance.SelectedMovie = movieData;

            await PopupNavigation.PushAsync(new PopUpMovieView(movieData));
        }

        #endregion

        #region Screen text
        public string Text_Title
        {
            get
            {
                return "CARTELERA";
            }
        }
        public string Text_DescriptionSearch
        {
            get
            {
                return "Hola, ¿Qué película de terror buscas?";
            }
        }
        #endregion


    }
}
