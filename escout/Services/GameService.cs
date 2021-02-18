using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Services
{
    public class GameService
    {
        private string token;

        public GameService() => this.token = string.Empty;

        public GameService(string token) => this.token = token;

        public async Task<List<Game>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
                return await GetFavoriteGames();
            if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
                return await GetGames(null);
            else
                return await GetGames(new SearchQuery(Key, "iLIKE", Value + "%"));
        }

        private async Task<List<Game>> GetGames(SearchQuery query)
        {
            var _games = new List<Game>();
            var request = RestConnector.GAMES;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                _games = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());

            return _games;
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = new Game();
            var request = RestConnector.GAME + "?id=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                game = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());

            return game;
        }

        public async Task<GameData> GetGameDataById(int id)
        {
            var gameData = new GameData();
            var request = RestConnector.GAME_DATA + "?gameId=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                gameData = JsonConvert.DeserializeObject<GameData>(await response.Content.ReadAsStringAsync());

            return gameData;
        }

        public async Task<List<ClubStat>> GetGameStatistics(int id)
        {
            var result = new List<ClubStat>();
            var request = RestConnector.GAME_STATISTICS + "?gameId=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<List<ClubStat>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<List<Game>> CreateGame(Game game)
        {
            var result = new List<Game>();
            var request = RestConnector.GAME;
            var games = new List<Game>();
            games.Add(game);
            var response = await new RestConnector(token).PostObjectAsync(request, games);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> UpdateGame(Game game)
        {
            HttpResponse result = new HttpResponse();
            var request = RestConnector.GAME;

            var response = await new RestConnector(token).PutObjectAsync(request, game);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> DeleteGame(int gameId)
        {
            var result = new HttpResponse();
            var request = RestConnector.GAME + "?gameId=" + gameId;
            var response = await new RestConnector(token).DeleteObjectAsync(request);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        private async Task<List<Game>> GetFavoriteGames()
        {
            var games = new List<Game>();
            var request = RestConnector.FAVORITES + "?query=gameId";

            var favoriteResponse = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await favoriteResponse.Content.ReadAsStringAsync()))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(await favoriteResponse.Content.ReadAsStringAsync());
                foreach (var f in _favorites)
                    games.Add(await GetGameById(int.Parse(f.GameId.ToString())));
            }

            return games;
        }
    }
}
