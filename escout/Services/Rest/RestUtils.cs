using System.Security.Cryptography;
using System.Text;
using escout.Models.Rest.GameObjects;
using escout.Models.Rest.GenericObjects;
using escout.Services.Generic;
using Newtonsoft.Json;

namespace escout.Services.Rest
{
    public class RestUtils
    {
        private readonly string token;

        public RestUtils()
        {
            token = string.Empty;
        }

        public RestUtils(string token)
        {
            this.token = token;
        }

        public async Task<Image> GetImage(int? imageId)
        {
            var img = new Image();
            var response = await new RestConnector(token).GetObjectAsync(RestConstValues.IMAGE + "?id=" + imageId);

            if (response.IsSuccessStatusCode)
            {
                img = JsonConvert.DeserializeObject<Image>(await response.Content.ReadAsStringAsync());
            }

            return img;
        }

        public async Task<List<Event>> GetEvents(SearchQuery query)
        {
            var events = new List<Event>();
            var request = RestConstValues.EVENTS;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                events = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
            }

            return events;
        }

        public async Task<List<Club>> GetClubs()
        {
            var clubs = new List<Club>();
            var request = RestConstValues.CLUBS;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                clubs = JsonConvert.DeserializeObject<List<Club>>(await response.Content.ReadAsStringAsync());
            }

            return clubs;
        }

        public async Task<List<Competition>> GetCompetitions()
        {
            var competitions = new List<Competition>();
            var request = RestConstValues.COMPETITIONS;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                competitions = JsonConvert.DeserializeObject<List<Competition>>(await response.Content.ReadAsStringAsync());
            }

            return competitions;
        }

        public static async Task<Club> GetClubName(string token, int? clubId)
        {
            return await new ClubService(token).GetClubById(clubId);
        }

        public static string GenerateSha256String(string inputString)
        {
            using var hash = SHA256.Create();

            var sb = new StringBuilder();
            var result = hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            foreach (var b in result)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
