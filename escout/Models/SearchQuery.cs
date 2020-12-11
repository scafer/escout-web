﻿using Newtonsoft.Json;

namespace escout.Models
{
    public class SearchQuery
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }
        [JsonProperty("condition")]
        public string Condition { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }

        public SearchQuery() { }

        public SearchQuery(string fieldName)
        {
            FieldName = fieldName;
        }

        public SearchQuery(string fieldName, string condition, string value)
        {
            FieldName = fieldName;
            Condition = condition;
            Value = value;
        }
    }
}