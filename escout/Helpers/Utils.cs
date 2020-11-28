using System.Collections.Generic;

namespace escout.Helpers
{
    public static class Utils
    {
        public const string TOKEN = "eScoutToken";
        public const string USER_ID = "eScoutUserId";

        public const string TOAST_GENERIC_ERROR = "";
        public const string TOAST_SUCCESS = "";

        public const string TOAST_EMAIL_ERROR = "";
        public const string TOAST_EMAIL_SUCCESS = "";

        public static string GetImageUrl(string eventKey)
        {
            return string.Format("/images/timeline/timeline_" + eventKey + ".svg");
        }

        public static readonly Dictionary<int, string> PositionKeys = new Dictionary<int, string>
        {
            {0, "Player" },
            {1, "Goalkeeper" }
        };
    }
}
