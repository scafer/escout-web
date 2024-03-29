﻿using escout.Models.Rest.GameObjects;
using escout.Services.Rest;

namespace escout.Services.Generic;

public class FavoritesService
{
    private readonly string token;

    public FavoritesService()
    {
        token = string.Empty;
    }

    public FavoritesService(string token)
    {
        this.token = token;
    }

    public async Task<HttpResponseMessage> ToogleFavorite(Favorite favorite)
    {
        return await new RestConnector(token).PostObjectAsync(RestConstValues.FAVORITE, favorite);
    }
}