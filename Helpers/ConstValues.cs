using System.Collections.ObjectModel;

namespace escout.Helpers;

public static class ConstValues
{
    public const string TOKEN = "eScoutToken";
    public const string USER_ID = "eScoutUserId";

    public const string MSG_GENERIC_ERROR = "Error";
    public const string MSG_GENERIC_SUCCESS = "Success";
    public const string MSG_GENERIC_UPDATED = "Updated";

    public const string MSG_GENERIC_OBJECT_CREATED = "{0} created.";
    public const string MSG_GENERIC_OBJECT_UPDATED = "{0} updated.";
    public const string MSG_GENERIC_OBJECT_DELETED = "{0} deleted.";
    public const string MSG_GENERIC_OBJECT_SAVED = "{0} saved.";
    public const string MSG_GENERIC_OBJECT_NEW = "New {0}";

    public const string MSG_WELCOME = "Welcome to eScout.";
    public const string MSG_FILL_USER_PASSWORD = "Fill username and password!";
    public const string MSG_USER_CREATED = "Your account was successfully created.";
    public const string MSG_RESET_PASSWORD_OK = "The new password was been send to your e-mail.";
    public const string MSG_ATHLETE_WITHOUT_CLUB = "Athlete without club affiliation.";

    public const string MSG_ERROR_CREATING_OBJECT = "Couldn't create new {0}.";

    public const string OPTION_GENERIC_SELECT = "Select...";
    public const string IMAGE_PATH_SVG = "/media/images/{0}.svg";

    public static readonly IReadOnlyDictionary<int, string> PositionKeys = new ReadOnlyDictionary<int, string>(
        new Dictionary<int, string>
        {
            { 0, "Player" },
            { 1, "Goalkeeper" }
        });
}