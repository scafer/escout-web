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
            if (!string.IsNullOrEmpty(response))
                _games = JsonConvert.DeserializeObject<List<Game>>(response);

            return _games;
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = new Game();
            var request = RestConnector.GAME + "?id=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                game = JsonConvert.DeserializeObject<Game>(response);

            return game;
        }

        public async Task<GameData> GetGameDataById(int id)
        {
            var gameData = new GameData();
            var request = RestConnector.GAME_DATA + "?gameId=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                gameData = JsonConvert.DeserializeObject<GameData>(response);

            return gameData;
        }

        public async Task<List<ClubStat>> GetGameStatistics(int id)
        {
            var result = new List<ClubStat>();
            var request = RestConnector.GAME_STATISTICS + "?gameId=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<List<ClubStat>>(response);

            return result;
        }

        public async Task<List<Game>> CreateGame(Game game)
        {
            var result = new List<Game>();
            var request = RestConnector.GAME;
            var games = new List<Game>();
            games.Add(game);
            var response = await new RestConnector(token).PostObjectAsync(request, games);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<List<Game>>(response);

            return result;
        }

        public async Task<SvcResult> UpdateGame(Game game)
        {
            SvcResult result = new SvcResult();
            var request = RestConnector.GAME;

            var response = await new RestConnector(token).PutObjectAsync(request, game);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<SvcResult>(response);

            return result;
        }

        public async Task<SvcResult> DeleteGame(int gameId)
        {
            var result = new SvcResult();
            var request = RestConnector.GAME + "?gameId=" + gameId;
            var response = await new RestConnector(token).DeleteObjectAsync(request);

            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<SvcResult>(response);

            return result;
        }

        private async Task<List<Game>> GetFavoriteGames()
        {
            var games = new List<Game>();
            var request = RestConnector.FAVORITES + "?query=gameId";

            var favoriteResponse = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(favoriteResponse))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(favoriteResponse);
                foreach (var f in _favorites)
                    games.Add(await GetGameById(int.Parse(f.GameId.ToString())));
            }

            return games;
        }
    }
}
