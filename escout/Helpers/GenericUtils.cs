using System.Collections.Generic;

namespace escout.Helpers
{
    public static class GenericUtils
    {
        public static string GetImageUrl(string eventKey)
        {
            return string.Format("/media/images/timeline/timeline_{0}.svg", eventKey);
        }

        public static string GetDisplayOptionFromDictionary(Dictionary<string, string> displayOptions, string key, string defaultValue)
        {
            if (displayOptions != null && displayOptions.ContainsKey(key))
            {
                return displayOptions[key];
            }

            return defaultValue;
        }
    }
}
