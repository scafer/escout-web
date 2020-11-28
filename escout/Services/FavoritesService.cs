using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace escout.Services
{
    public class FavoritesService
    {
        private string token;

        public FavoritesService() => this.token = string.Empty;

        public FavoritesService(string token) => this.token = token;

        public async Task<SvcResult> ToogleFavorite(Favorite favorite)
        {
            var result = new SvcResult();
            var request = RestConnector.FAVORITE;
            var response = await new RestConnector(token).PostObjectAsync(request, favorite);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<SvcResult>(response);

            return result;
        }
    }
}
