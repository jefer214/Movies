namespace Movies.Configuration
{
    using Movies.Models;
    using System.Collections.Generic;

    internal class AppSettings
    {
        private static readonly AppSettings _instance = new AppSettings();

        #region Singleton
        public static AppSettings Instance
        {
            get { return _instance; }
        }
        #endregion

        #region Content data 
        public List<Movie> Searches { get; set; }

        public Movie SelectedMovie { get; set; }
        #endregion
    }
}
