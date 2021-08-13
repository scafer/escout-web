using System.Collections.Generic;

namespace escout.Helpers
{
    public class ConstValues
    {
        public const string TOKEN = "eScoutToken";
        public const string USER_ID = "eScoutUserId";

        public const string MSG_GENERIC_ERROR = "Error";
        public const string MSG_GENERIC_SUCCESS = "Success";
        public const string MSG_GENERIC_UPDATED = "Updated";

        public const string MSG_WELCOME = "Welcome to eScout";
        public const string MSG_FILL_USER_PASSWORD = "Fill username and password!";
        public const string MSG_USER_CREATED = "Your account was successfully created";
        public const string MSG_RESET_PASSWORD_OK = "The new password was been send to your e-mail.";

        public static readonly Dictionary<int, string> PositionKeys = new Dictionary<int, string>
        {
            {0, "Player" },
            {1, "Goalkeeper" }
        };
    }
}
