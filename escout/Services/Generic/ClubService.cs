using escout.Models.Rest.GameObjects;
using escout.Models.Rest.GameStatistics;
using escout.Models.Rest.GenericObjects;
using escout.Services.Rest;
using Newtonsoft.Json;

namespace escout.Services.Generic
{
    public class ClubService
    {
        private string token;

        public ClubService()
        {
            token = string.Empty;
        }

        public ClubService(string token)
        {
            this.token = token;
        }

        public async Task<List<Club>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
            {
                return await GetFavoriteClubs();
            }
            else if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
            {
                return await GetClubs(null);
            }
            else
            {
                return await GetClubs(new SearchQuery(Key, "iLIKE", Value + "%"));
            }
        }

        private async Task<List<Club>> GetClubs(SearchQuery query)
        {
            var clubs = new List<Club>();
            var request = RestConstValues.CLUBS;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                clubs = JsonConvert.DeserializeObject<List<Club>>(await response.Content.ReadAsStringAsync());
            }

            return clubs;
        }

        public async Task<Club> GetClubById(int? id)
        {
            var club = new Club();
            var request = RestConstValues.CLUB + "?id=" + id;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                club = JsonConvert.DeserializeObject<Club>(await response.Content.ReadAsStringAsync());
            }

            return club;
        }

        public async Task<Statistics> GetClubStatistics(int clubId, int? gameId)
        {
            var statistics = new Statistics();
            var request = RestConstValues.CLUB_STATISTICS;

            if (gameId == null)
            {
                request += "?clubId=" + clubId;
            }
            else
            {
                request += "?clubId=" + clubId + "&gameId=" + gameId;
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                statistics = JsonConvert.DeserializeObject<Statistics>(await response.Content.ReadAsStringAsync());
            }

            return statistics;
        }

        public async Task<HttpResponseMessage> CreateClub(Club club)
        {
            var clubs = new List<Club>() { club };
            return await new RestConnector(token).PostObjectAsync(RestConstValues.CLUB, clubs);
        }

        public async Task<HttpResponseMessage> UpdateClub(Club club)
        {
            return await new RestConnector(token).PutObjectAsync(RestConstValues.CLUB, club);
        }

        public async Task<HttpResponseMessage> DeleteClub(int clubId)
        {
            var request = RestConstValues.CLUB + "?id=" + clubId;
            return await new RestConnector(token).DeleteObjectAsync(request);
        }

        private async Task<List<Club>> GetFavoriteClubs()
        {
            var clubs = new List<Club>();
            var request = RestConstValues.FAVORITES + "?query=clubId";
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());
                foreach (var f in favorites)
                {
                    clubs.Add(await GetClubById(int.Parse(f.ClubId.ToString())));
                }
            }

            return clubs;
        }
    }
}
