using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Services
{
    public class ClubService
    {
        private string token;

        public ClubService() => this.token = string.Empty;

        public ClubService(string token) => this.token = token;

        public async Task<List<Club>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
                return await GetFavoriteClubs();
            else if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
                return await GetClubs(null);
            else
                return await GetClubs(new SearchQuery(Key, "iLIKE", Value + "%"));
        }

        private async Task<List<Club>> GetClubs(SearchQuery query)
        {
            var _clubs = new List<Club>();
            var request = RestConnector.CLUBS;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                _clubs = JsonConvert.DeserializeObject<List<Club>>(await response.Content.ReadAsStringAsync());

            return _clubs;
        }

        public async Task<Club> GetClubById(int? id)
        {
            var club = new Club();
            var request = RestConnector.CLUB + "?id=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                club = JsonConvert.DeserializeObject<Club>(await response.Content.ReadAsStringAsync());

            return club;
        }

        public async Task<Statistics> GetClubStatistics(int clubId, int? gameId)
        {
            var statistics = new Statistics();
            var request = RestConnector.CLUB_STATISTICS;

            if (gameId == null)
                request += "?clubId=" + clubId;
            else
                request += "?clubId=" + clubId + "&gameId=" + gameId;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                statistics = JsonConvert.DeserializeObject<Statistics>(await response.Content.ReadAsStringAsync());

            return statistics;
        }

        public async Task<List<Club>> CreateClub(Club club)
        {
            var result = new List<Club>();
            var request = RestConnector.CLUB;
            var clubs = new List<Club>();
            clubs.Add(club);
            var response = await new RestConnector(token).PostObjectAsync(request, clubs);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<List<Club>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> UpdateClub(Club club)
        {
            HttpResponse result = new HttpResponse();
            var request = RestConnector.CLUB;

            var response = await new RestConnector(token).PutObjectAsync(request, club);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> DeleteClub(int clubId)
        {
            var result = new HttpResponse();
            var request = RestConnector.CLUB + "?id=" + clubId;
            var response = await new RestConnector(token).DeleteObjectAsync(request);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        private async Task<List<Club>> GetFavoriteClubs()
        {
            var clubs = new List<Club>();
            var request = RestConnector.FAVORITES + "?query=clubId";

            var favoriteResponse = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await favoriteResponse.Content.ReadAsStringAsync()))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(await favoriteResponse.Content.ReadAsStringAsync());
                foreach (var f in _favorites)
                    clubs.Add(await GetClubById(int.Parse(f.ClubId.ToString())));
            }

            return clubs;
        }
    }
}
