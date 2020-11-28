using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Services
{
    public class AthleteService
    {
        private string token;

        public AthleteService() => this.token = string.Empty;

        public AthleteService(string token) => this.token = token;

        public async Task<List<Athlete>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
                return await GetFavoriteAthletes();
            else if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
                return await GetAthletes(null);
            else
                return await GetAthletes(new SearchQuery(Key, "iLIKE", Value + "%"));
        }

        public async Task<Athlete> GetAthleteById(int id)
        {
            var athlete = new Athlete();
            var request = RestConnector.ATHLETE + "?id=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                athlete = JsonConvert.DeserializeObject<Athlete>(response);

            return athlete;
        }

        public async Task<List<Athlete>> CreateAthlete(Athlete athlete)
        {
            var result = new List<Athlete>();
            var request = RestConnector.ATHLETE;
            var athletes = new List<Athlete>();
            athletes.Add(athlete);
            var response = await new RestConnector(token).PostObjectAsync(request, athletes);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<List<Athlete>>(response);

            return result;
        }

        public async Task<SvcResult> UpdateAthlete(Athlete athlete)
        {
            SvcResult result = new SvcResult();
            var request = RestConnector.ATHLETE;

            var response = await new RestConnector(token).PutObjectAsync(request, athlete);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<SvcResult>(response);

            return result;
        }

        public async Task<SvcResult> DeleteAthlete(int athleteId)
        {
            var result = new SvcResult();
            var request = RestConnector.ATHLETE + "?id=" + athleteId;
            var response = await new RestConnector(token).DeleteObjectAsync(request);

            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<SvcResult>(response);

            return result;
        }

        public async Task<Statistics> GetAthleteStatistics(int athleteId, int? gameId)
        {
            var statistics = new Statistics();
            var request = RestConnector.ATHLETE_STATISTICS;

            if (gameId == null)
                request += "?athleteId=" + athleteId;
            else
                request += "?athleteId=" + athleteId + "&gameId=" + gameId;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                statistics = JsonConvert.DeserializeObject<Statistics>(response);

            return statistics;
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            var _athletes = new List<Athlete>();
            var request = RestConnector.ATHLETES;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                _athletes = JsonConvert.DeserializeObject<List<Athlete>>(response);

            return _athletes;
        }

        private async Task<List<Athlete>> GetFavoriteAthletes()
        {
            var athletes = new List<Athlete>();
            var request = RestConnector.FAVORITES + "?query=athleteId";

            var favoriteResponse = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(favoriteResponse))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(favoriteResponse);
                foreach (var f in _favorites)
                    athletes.Add(await GetAthleteById(int.Parse(f.AthleteId.ToString())));
            }

            return athletes;
        }
    }
}
