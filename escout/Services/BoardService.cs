using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Services
{
    public class BoardsService
    {
        private string token;

        public BoardsService() => this.token = string.Empty;

        public BoardsService(string token) => this.token = token;

        public async Task<List<Competition>> SearchExecuted(string Key, string Value)
        {
            if (Key == "favorites")
                return await GetFavoriteBoards();
            else if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
                return await GetCompetitions(null);
            else
                return await GetCompetitions(new SearchQuery(Key, "iLIKE", Value + "%"));
        }

        private async Task<List<Competition>> GetCompetitions(SearchQuery query)
        {
            var competition = new List<Competition>();
            var request = RestConnector.COMPETITIONS;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                competition = JsonConvert.DeserializeObject<List<Competition>>(await response.Content.ReadAsStringAsync());

            return competition;
        }

        public async Task<Competition> GetCompetitionById(int id)
        {
            var competition = new Competition();
            var request = RestConnector.COMPETITION + "?id=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                competition = JsonConvert.DeserializeObject<Competition>(await response.Content.ReadAsStringAsync());

            return competition;
        }

        public async Task<List<CompetitionBoard>> GetCompetitionBoardById(int id)
        {
            var boards = new List<CompetitionBoard>();
            var request = RestConnector.COMPETITION_BOARD + "?id=" + id;

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                boards = JsonConvert.DeserializeObject<List<CompetitionBoard>>(await response.Content.ReadAsStringAsync());

            return boards;
        }

        public async Task<List<Competition>> CreateCompetition(Competition competitionId)
        {
            var result = new List<Competition>();
            var request = RestConnector.COMPETITION;
            var competitions = new List<Competition>();
            competitions.Add(competitionId);
            var response = await new RestConnector(token).PostObjectAsync(request, competitions);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<List<Competition>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> UpdateCompetition(Competition competitionId)
        {
            HttpResponse result = new HttpResponse();
            var request = RestConnector.COMPETITION;

            var response = await new RestConnector(token).PutObjectAsync(request, competitionId);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> DeleteCompetition(int competitionId)
        {
            var result = new HttpResponse();
            var request = RestConnector.COMPETITION + "?id=" + competitionId;
            var response = await new RestConnector(token).DeleteObjectAsync(request);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<List<CompetitionBoard>> CreateCompetitionBoard(CompetitionBoard board)
        {
            var result = new List<CompetitionBoard>();
            var request = RestConnector.COMPETITION_BOARD;

            var obj = new List<CompetitionBoard>();
            obj.Add(board);

            var response = await new RestConnector(token).PostObjectAsync(request, obj);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<List<CompetitionBoard>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<CompetitionBoard> UpdateCompetitionBoard(CompetitionBoard board)
        {
            var result = new CompetitionBoard();
            var request = RestConnector.COMPETITION_BOARD;

            var response = await new RestConnector(token).PutObjectAsync(request, board);
            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<CompetitionBoard>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<HttpResponse> DeleteCompetitionBoard(int competitionBoardId)
        {
            var result = new HttpResponse();
            var request = RestConnector.COMPETITION_BOARD + "?id=" + competitionBoardId;
            var response = await new RestConnector(token).DeleteObjectAsync(request);

            if (!string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                result = JsonConvert.DeserializeObject<HttpResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        private async Task<List<Competition>> GetFavoriteBoards()
        {
            var competitions = new List<Competition>();
            var request = RestConnector.FAVORITES + "?query=competitionId";

            var favoriteResponse = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await favoriteResponse.Content.ReadAsStringAsync()))
            {
                var _favorites = JsonConvert.DeserializeObject<List<Favorite>>(await favoriteResponse.Content.ReadAsStringAsync());
                foreach (var f in _favorites)
                    competitions.Add(await GetCompetitionById(int.Parse(f.CompetitionId.ToString())));
            }

            return competitions;
        }
    }
}
