using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Netflixroulette.Models
{
    public class MovieService
    {
        private const string Url = "https://netflixroulette.net/api/api.php?";
        private const string SuffixActor = "actor=";
        private const string SuffixTitle = "title=";
        private HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Movie>> FindMoviesByActor(string actor)
        {
            var response = await _client.GetAsync(Url + SuffixActor + actor);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<Movie>();
            }

            var content = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<Movie>>(content);
        }

        public async Task<Movie> GetMovie(string title)
        {
            var response = await _client.GetAsync(Url + SuffixTitle + title);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            var content = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Movie>(content);
        }
    }
}
