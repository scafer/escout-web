using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public class RestConnector
    {
        //API endpoints
        public const string SIGN_IN = "/api/v1/signIn";
        public const string SIGN_UP = "/api/v1/signUp";
        public const string RESET_PASSWORD = "/api/v1/resetPassword";
        public const string CHANGE_PASSWORD = "​/api/v1/changePassword";
        public const string AUTHENTICATED = "/api/v1/authenticated";
        public const string USER = "/api/v1/user";
        public const string USERS = "/api/v1/users";
        public const string ATHLETE = "/api/v1/athlete";
        public const string ATHLETES = "/api/v1/athletes";
        public const string ATHLETE_STATISTICS = "/api/v1/athleteStatistics";
        public const string CLUB = "/api/v1/club";
        public const string CLUBS = "/api/v1/clubs";
        public const string CLUB_STATISTICS = "/api/v1/clubStatistics";
        public const string COMPETITION = "/api/v1/competition";
        public const string COMPETITIONS = "/api/v1/competitions";
        public const string COMPETITION_BOARD = "/api/v1/competitionBoard";
        public const string EVENT = "/api/v1/event";
        public const string EVENTS = "/api/v1/events";
        public const string GAME = "/api/v1/game";
        public const string GAMES = "/api/v1/games";
        public const string GAME_EVENT = "/api/v1/gameEvent";
        public const string GAME_EVENTS = "/api/v1/gameEvents";
        public const string GAME_ATHLETE = "/api/v1/gameAthlete";
        public const string GAME_ATHLETES = "/api/v1/gameAthletes";
        public const string GAME_DATA = "/api/v1/gameData";
        public const string GAME_STATISTICS = "​/api/v1/gameStatistics";
        public const string IMAGE = "/api/v1/image";
        public const string SPORT = "/api/v1/sport";
        public const string SPORTS = "/api/v1/sports";
        public const string FAVORITE = "/api/v1/favorite";
        public const string FAVORITES = "/api/v1/favorites";

        private readonly string token;
        private const string AUTHENTICATION_MODE = "Authorization";
        private const string CONTENT_TYPE = "application/json";

        public RestConnector(string token) => this.token = token;

        private string GetAuthenticationHeader() => "bearer" + " " + token;

        private static string GetApiUrl() => Environment.GetEnvironmentVariable("ESCOUT_SERVER_URL") ?? "https://escout-server.herokuapp.com";

        public async Task<string> GetObjectAsync(string conn)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());
                var httpResponse = await client.GetAsync(GetApiUrl() + conn);
                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
            }
            return response;
        }

        public async Task<string> PostObjectAsync(string conn, object data)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());

                var httpResponse = await client.PostAsync(GetApiUrl() + conn,
                    new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, CONTENT_TYPE));

                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
            }
            return response;
        }

        public async Task<string> PutObjectAsync(string conn, object data)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());

                var httpResponse = await client.PutAsync(GetApiUrl() + conn,
                    new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, CONTENT_TYPE));

                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
            }
            return response;
        }

        public async Task<string> DeleteObjectAsync(string conn)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());

                var httpResponse = await client.DeleteAsync(GetApiUrl() + conn);
                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
            }
            return response;
        }
    }
}
