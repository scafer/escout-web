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
            var response = await new RestConnector(token).GetObjectAsync(RestConnector.ATHLETE + "?id=" + id);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                athlete = JsonConvert.DeserializeObject<Athlete>(await response.Content.ReadAsStringAsync());

            return athlete;
        }

        public async Task<List<Athlete>> CreateAthlete(Athlete athlete)
        {
            var result = new List<Athlete>();
            var response = await new RestConnector(token).PostObjectAsync(RestConnector.ATHLETE, new List<Athlete> { athlete });

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<List<Athlete>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> UpdateAthlete(Athlete athlete)
        {
            var result = new HttpResponse();
            var response = await new RestConnector(token).PutObjectAsync(RestConnector.ATHLETE, athlete);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> DeleteAthlete(int athleteId)
        {
            var result = new HttpResponse();
            var response = await new RestConnector(token).DeleteObjectAsync(RestConnector.ATHLETE + "?id=" + athleteId);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

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

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                statistics = JsonConvert.DeserializeObject<Statistics>(await response.Content.ReadAsStringAsync());

            return statistics;
        }

        private async Task<List<Athlete>> GetAthletes(SearchQuery query)
        {
            var athletes = new List<Athlete>();
            var request = RestConnector.ATHLETES;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                athletes = JsonConvert.DeserializeObject<List<Athlete>>(await response.Content.ReadAsStringAsync());

            return athletes;
        }

        private async Task<List<Athlete>> GetFavoriteAthletes()
        {
            var athletes = new List<Athlete>();
            var request = RestConnector.FAVORITES + "?query=athleteId";
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
            {
                var favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());
                foreach (var f in favorites)
                    athletes.Add(await GetAthleteById(int.Parse(f.AthleteId.ToString())));
            }

            return athletes;
        }
    }
}
