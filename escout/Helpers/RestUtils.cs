﻿using escout.Models;
using escout.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public class RestUtils
    {
        private readonly string token;

        public RestUtils() => token = string.Empty;

        public RestUtils(string token) => this.token = token;

        public async Task<Image> GetImage(int? imageId)
        {
            var img = new Image();
            var response = await new RestConnector(token).GetObjectAsync(RestConnector.IMAGE + "?id=" + imageId);
            if (!string.IsNullOrEmpty(response))
                img = JsonConvert.DeserializeObject<Image>(response);

            return img;
        }

        public async Task<List<Event>> GetEvents(SearchQuery query)
        {
            List<Event> events = new List<Event>();
            var request = RestConnector.EVENTS;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                events = JsonConvert.DeserializeObject<List<Event>>(response);

            return events;
        }

        public async Task<List<Club>> GetClubs()
        {
            List<Club> clubs = new List<Club>();
            var request = RestConnector.CLUBS;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                clubs = JsonConvert.DeserializeObject<List<Club>>(response);

            return clubs;
        }

        public async Task<List<Competition>> GetCompetitions()
        {
            List<Competition> competitions = new List<Competition>();
            var request = RestConnector.COMPETITIONS;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                competitions = JsonConvert.DeserializeObject<List<Competition>>(response);

            return competitions;
        }

        public static async Task<Club> GetClubName(string token, int? clubId)
        {
            return await new ClubService(token).GetClubById(clubId);
        }

        public static string GenerateSha256String(string inputString)
        {
            var sb = new StringBuilder();
            using var hash = SHA256.Create();

            var enc = Encoding.UTF8;
            var result = hash.ComputeHash(enc.GetBytes(inputString));

            foreach (var b in result)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}
