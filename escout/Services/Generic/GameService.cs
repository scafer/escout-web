using escout.Models.Rest.GameObjects;
using escout.Models.Rest.GameStatistics;
using escout.Models.Rest.GenericObjects;
using escout.Services.Rest;
using Newtonsoft.Json;

namespace escout.Services.Generic
{
    public class GameService
    {
        private string token;

        public GameService()
        {
            token = string.Empty;
        }

        public GameService(string token)
        {
            this.token = token;
        }

        public async Task<List<Game>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
            {
                return await GetFavoriteGames();
            }
            else if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
            {
                return await GetGames(null);
            }
            else
            {
                return await GetGames(new SearchQuery(Key, "iLIKE", Value + "%"));
            }
        }

        private async Task<List<Game>> GetGames(SearchQuery query)
        {
            var _games = new List<Game>();
            var request = RestConstValues.GAMES;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _games = JsonConvert.DeserializeObject<List<Game>>(await response.Content.ReadAsStringAsync());
            }

            return _games;
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = new Game();
            var request = RestConstValues.GAME + "?id=" + id;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                game = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
            }

            return game;
        }

        public async Task<GameData> GetGameDataById(int id)
        {
            var gameData = new GameData();
            var request = RestConstValues.GAME_DATA + "?gameId=" + id;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                gameData = JsonConvert.DeserializeObject<GameData>(await response.Content.ReadAsStringAsync());
            }

            return gameData;
        }

        public async Task<List<ClubStat>> GetGameStatistics(int id)
        {
            var result = new List<ClubStat>();
            var request = RestConstValues.GAME_STATISTICS + "?gameId=" + id;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<ClubStat>>(await response.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<HttpResponseMessage> CreateGame(Game game)
        {
            var games = new List<Game>() { game };
            return await new RestConnector(token).PostObjectAsync(RestConstValues.GAME, games);
        }

        public async Task<HttpResponseMessage> UpdateGame(Game game)
        {
            return await new RestConnector(token).PutObjectAsync(RestConstValues.GAME, game);
        }

        public async Task<HttpResponseMessage> DeleteGame(int gameId)
        {
            var request = RestConstValues.GAME + "?gameId=" + gameId;
            return await new RestConnector(token).DeleteObjectAsync(request);
        }

        private async Task<List<Game>> GetFavoriteGames()
        {
            var games = new List<Game>();
            var request = RestConstValues.FAVORITES + "?query=gameId";
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());

                foreach (var f in favorites)
                {
                    games.Add(await GetGameById(int.Parse(f.GameId.ToString())));
                }
            }

            return games;
        }
    }
}
