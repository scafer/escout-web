﻿namespace escout.Services.Rest
{
    public class RestConstValues
    {
        //Authentication API Endpoints
        public const string V1_AUTHENTICATION = "/api/v1/authentication/";
        public const string SIGN_IN = V1_AUTHENTICATION + "sign-in";
        public const string SIGN_UP = V1_AUTHENTICATION + "sign-up";
        public const string RESET_PASSWORD = V1_AUTHENTICATION + "reset-password";
        public const string AUTHENTICATED = V1_AUTHENTICATION + "authenticated";

        //Generic-Objects API Endpoints
        public const string V1_GENERIC_OBJECTS = "/api/v1/generic-object/";
        public const string USER = V1_GENERIC_OBJECTS + "user";
        public const string USERS = V1_GENERIC_OBJECTS + "users";
        public const string IMAGE = V1_GENERIC_OBJECTS + "image";
        public const string CHANGE_PASSWORD = V1_GENERIC_OBJECTS + "​change-password";

        //Game-Objects API Endpoints
        public const string V1_GAME_OBJECTS = "/api/v1/game-object/";
        public const string ATHLETE = V1_GAME_OBJECTS + "athlete";
        public const string ATHLETES = V1_GAME_OBJECTS + "athletes";
        public const string CLUB = V1_GAME_OBJECTS + "club";
        public const string CLUBS = V1_GAME_OBJECTS + "clubs";
        public const string COMPETITION = V1_GAME_OBJECTS + "competition";
        public const string COMPETITIONS = V1_GAME_OBJECTS + "competitions";
        public const string COMPETITION_BOARD = V1_GAME_OBJECTS + "competition-board";
        public const string EVENT = V1_GAME_OBJECTS + "event";
        public const string EVENTS = V1_GAME_OBJECTS + "events";
        public const string GAME = V1_GAME_OBJECTS + "game";
        public const string GAMES = V1_GAME_OBJECTS + "games";
        public const string GAME_EVENT = V1_GAME_OBJECTS + "game-event";
        public const string GAME_EVENTS = V1_GAME_OBJECTS + "game-events";
        public const string GAME_ATHLETE = V1_GAME_OBJECTS + "game-athlete";
        public const string GAME_ATHLETES = V1_GAME_OBJECTS + "game-athletes";
        public const string GAME_DATA = V1_GAME_OBJECTS + "game-data";
        public const string SPORT = V1_GAME_OBJECTS + "sport";
        public const string SPORTS = V1_GAME_OBJECTS + "sports";
        public const string FAVORITE = V1_GAME_OBJECTS + "favorite";
        public const string FAVORITES = V1_GAME_OBJECTS + "favorites";

        //Game-Statistics API Endpoints
        public const string V1_GAME_STATISTICS = "/api/v1/game-statistics/";
        public const string CLUB_STATISTICS = V1_GAME_STATISTICS + "club";
        public const string GAME_STATISTICS = V1_GAME_STATISTICS + "game";
        public const string ATHLETE_STATISTICS = V1_GAME_STATISTICS + "athlete";
    }
}
