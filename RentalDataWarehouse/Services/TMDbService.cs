using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace RentalDataWarehouse.Services
{
    public class TMDbService
    {
        private readonly TMDbClient _client;

        public TMDbService()
        {
            _client = new TMDbClient(ConfigurationData.AppSettings["TMDbv3Key"]);
        }

        public async Task<Movie> GetMovieAsync(string imdbId, MovieMethods extraMethods = MovieMethods.Undefined)
        {
            return await _client.GetMovieAsync(imdbId, extraMethods);
        }

        public async Task<Movie> GetMovieAsync(int movieId, MovieMethods extraMethods = MovieMethods.Undefined)
        {
            return await _client.GetMovieAsync(movieId, extraMethods);
        }

        public async Task<Movie> GetMovieAsync(string imdbId, string language, MovieMethods extraMethods = MovieMethods.Undefined)
        {
            return await _client.GetMovieAsync(imdbId, language, extraMethods);
        }

        public async Task<Movie> GetMovieAsync(int movieId, string language, MovieMethods extraMethods = MovieMethods.Undefined)
        {
            return await _client.GetMovieAsync(movieId, language, extraMethods);
        }
    }
}
