using escout.Models.Rest.GameObjects;
using escout.Models.Rest.GenericObjects;
using escout.Services.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace escout.Services.Generic
{
    public class BoardsService
    {
        private readonly string token;

        public BoardsService()
        {
            token = string.Empty;
        }


        public BoardsService(string token)
        {
            this.token = token;
        }

        public async Task<List<Competition>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
            {
                return await GetFavoriteBoards();
            }
            else if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
            {
                return await GetCompetitions(null);
            }
            else
            {
                return await GetCompetitions(new SearchQuery(Key, "iLIKE", Value + "%"));
            }
        }

        private async Task<List<Competition>> GetCompetitions(SearchQuery query)
        {
            var competition = new List<Competition>();
            var request = RestConstValues.COMPETITIONS;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                competition = JsonConvert.DeserializeObject<List<Competition>>(await response.Content.ReadAsStringAsync());
            }

            return competition;
        }

        public async Task<Competition> GetCompetitionById(int id)
        {
            var competition = new Competition();
            var request = RestConstValues.COMPETITION + "?id=" + id;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                competition = JsonConvert.DeserializeObject<Competition>(await response.Content.ReadAsStringAsync());
            }

            return competition;
        }

        public async Task<List<CompetitionBoard>> GetCompetitionBoardById(int id)
        {
            var boards = new List<CompetitionBoard>();
            var request = RestConstValues.COMPETITION_BOARD + "?id=" + id;
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                boards = JsonConvert.DeserializeObject<List<CompetitionBoard>>(await response.Content.ReadAsStringAsync());
            }

            return boards;
        }

        public async Task<HttpResponseMessage> CreateCompetition(Competition competitionId)
        {
            var competitions = new List<Competition>() { competitionId };
            return await new RestConnector(token).PostObjectAsync(RestConstValues.COMPETITION, competitions);
        }

        public async Task<HttpResponseMessage> UpdateCompetition(Competition competitionId)
        {
            return await new RestConnector(token).PutObjectAsync(RestConstValues.COMPETITION, competitionId);
        }

        public async Task<HttpResponseMessage> DeleteCompetition(int competitionId)
        {
            var request = RestConstValues.COMPETITION + "?id=" + competitionId;
            return await new RestConnector(token).DeleteObjectAsync(request);
        }

        public async Task<HttpResponseMessage> CreateCompetitionBoard(CompetitionBoard board)
        {
            var obj = new List<CompetitionBoard>() { board };
            return await new RestConnector(token).PostObjectAsync(RestConstValues.COMPETITION_BOARD, obj);
        }

        public async Task<CompetitionBoard> UpdateCompetitionBoard(CompetitionBoard board)
        {
            var result = new CompetitionBoard();
            var response = await new RestConnector(token).PutObjectAsync(RestConstValues.COMPETITION_BOARD, board);

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<CompetitionBoard>(await response.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<HttpResponseMessage> DeleteCompetitionBoard(int competitionBoardId)
        {
            var request = RestConstValues.COMPETITION_BOARD + "?id=" + competitionBoardId;
            return await new RestConnector(token).DeleteObjectAsync(request);
        }

        private async Task<List<Competition>> GetFavoriteBoards()
        {
            var competitions = new List<Competition>();
            var request = RestConstValues.FAVORITES + "?query=competitionId";
            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(await response.Content.ReadAsStringAsync());

                foreach (var f in _favorites)
                {
                    competitions.Add(await GetCompetitionById(int.Parse(f.CompetitionId.ToString())));
                }
            }

            return competitions;
        }
    }
}
