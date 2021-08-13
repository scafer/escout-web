using escout.Models.Rest.GameObjects;
using escout.Models.Rest.GameStatistics;
using escout.Models.Rest.GenericObjects;
using escout.Services.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace escout.Services.Generic
{
    public class AthleteService
    {
        private readonly string token;

        public AthleteService()
        {
            token = string.Empty;
        }

        public AthleteService(string token)
        {
            this.token = token;
        }

        public async Task<List<Athlete>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
            {
                return await GetFavoriteAthletes();
            }
            else if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
            {
                return await GetAthletes(null);
            }
            else
            {
                return await GetAthletes(new SearchQuery(Key, "iLIKE", Value + "%"));
            }
        }

        public async Task<Athlete> GetAthleteById(int id)
        {
            var athlete = new Athlete();
            var response = await new RestConnector(token).GetObjectAsync(RestConstValues.ATHLETE + "?id=" + id);

            if (response.IsSuccessStatusCode)
            {
                athlete = JsonConvert.DeserializeObject<Athlete>(await response.Content.ReadAsStringAsync());
            }

            return athlete;
        }

        public async Task<HttpResponseMessage> CreateAthlete(Athlete athlete)
        {
            return await new RestConnector(token).PostObjectAsync(RestConstValues.ATHLETE, new List<Athlete> { athlete });
        }

        public async Task<HttpResponseMessage> UpdateAthlete(Athlete athlete)
        {
            return await new RestConnector(token).PutObjectAsync(RestConstValues.ATHLETE, athlete);
        }

        public async Task<HttpResponseMessage> DeleteAthlete(int athleteId)
        {
            return await new RestConnector(token).DeleteObjectAsync(RestConstValues.ATHLETE + "?id=" + athleteId);
        }

        public async Task<Statistics> GetAthleteStatistics(int athleteId, int? gameId)
        {
            var statistics = new Statistics();
            var request = RestConstValues.ATHLETE_STATISTICS;

            if (gameId == null)
            {
                request += "?athleteId=" + athleteId;
            }
            else
            {
                request += "?athleteId=" + athleteId + "&gameId=" + gameId;
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                statistics = JsonConvert.DeserializeObject<Statistics>(await response.Content.ReadAsStringAsync());
            }

            return statistics;
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            var athletes = new List<Athlete>();
            var request = RestConstValues.ATHLETES;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                athletes = JsonConvert.DeserializeObject<List<Athlete>>(await response.Content.ReadAsStringAsync());
            }

            return athletes;
        }

        private async Task<List<Athlete>> GetFavoriteAthletes()
        {
            var athletes = new List<Athlete>();
            var request = RestConstValues.FAVORITES + "?query=athleteId";
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());

                foreach (var f in favorites)
                {
                    athletes.Add(await GetAthleteById(int.Parse(f.AthleteId.ToString())));
                }
            }

            return athletes;
        }
    }
}
