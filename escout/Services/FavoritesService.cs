using escout.Helpers;
using escout.Models;
using System.Threading.Tasks;

namespace escout.Services
{
    public class FavoritesService
    {
        private string token;

        public FavoritesService() => this.token = string.Empty;

        public FavoritesService(string token) => this.token = token;

        public async Task<HttpResponse> ToogleFavorite(Favorite favorite)
        {
            var response = await new RestConnector(token).PostObjectAsync(RestConnector.FAVORITE, favorite);
            return new HttpResponse(response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
