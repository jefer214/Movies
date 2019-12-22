namespace Movies.Services
{
    using Movies.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ApiService
    {
        #region Constants
        public static class Cloud
        {
            public static string ServerUrl = "http://www.omdbapi.com";
            public static string BaseUrl = "?apikey={0}&s={1}&r=json&page={2}";
            public static string FindUrlByID = "?apikey={0}&i={1}&plot=full";
            public static string ApiKey = "5eec5adc";
        }
        #endregion

        public async Task<List<Movie>> SearchContentsAsync(string query, int page)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var _httpClient = new HttpClient();

                var url = string.Format(Cloud.ServerUrl + Cloud.BaseUrl, Cloud.ApiKey, query, page);
                var response = await _httpClient.GetAsync(url); if (response.IsSuccessStatusCode)
                {
                    var resultContent = await response.Content.ReadAsStringAsync();
                    var resultData = JsonConvert.DeserializeObject<ContentResponse>(resultContent);
                    if (resultData.Search == null)
                        return null;
                    else
                        return resultData.Search.ToList();
                }
                return new List<Movie>();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
               "Error",
               "¡Asegúrese de que su dispositivo esté conectado a Internet!",
               "Accept");
               
                return null;
            }
        }
    }
}
