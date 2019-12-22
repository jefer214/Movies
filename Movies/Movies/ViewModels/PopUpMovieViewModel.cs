namespace Movies.ViewModels
{
    using Movies.Configuration;
    using Movies.Models;
    using System.ComponentModel;

    public class PopUpMovieViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public PopUpMovieViewModel(Movie selectedMovie)
        {
            SelectedMovie = AppSettings.Instance.Searches.Find(x => x.Title == selectedMovie.Title);
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private properties
        private Movie _selectedMovie { get; set; }
        #endregion

        #region Public properties
        public Movie SelectedMovie
        {
            get
            {
                return _selectedMovie;
            }
            set
            {
                _selectedMovie = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(SelectedMovie)));
            }
        }
        #endregion

    }
}
