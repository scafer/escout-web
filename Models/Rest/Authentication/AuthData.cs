﻿using Newtonsoft.Json;

namespace escout.Models.Rest.Authentication;

public class AuthData
{
    [JsonProperty("token")] public string Token { get; set; }

    [JsonProperty("tokenExpirationTime")] public long TokenExpirationTime { get; set; }

    [JsonProperty("id")] public string Id { get; set; }
}