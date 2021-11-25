using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using escout.Models.Rest.GameObjects;
using escout.Models.Rest.GenericObjects;
using escout.Services.Rest;
using Newtonsoft.Json;

namespace escout.Services.Generic
{
    public class EventService
    {
        private string token;

        public EventService()
        {
            token = string.Empty;
        }

        public EventService(string token)
        {
            this.token = token;
        }

        public async Task<List<Event>> SearchExecuted(string Key, string Value)
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
            {
                return await GetEvents(null);
            }
            else
            {
                return await GetEvents(new SearchQuery(Key, "iLIKE", Value + "%"));
            }
        }

        private async Task<List<Event>> GetEvents(SearchQuery query)
        {
            var _events = new List<Event>();
            var request = RestConstValues.EVENTS;

            if (query != null)
            {
                request += "?query=" + JsonConvert.SerializeObject(query);
            }

            var response = await new RestConnector(token).GetObjectAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _events = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
            }

            return _events;
        }

        public async Task<List<Event>> CreateEvent(Event e)
        {
            var result = new List<Event>();
            var events = new List<Event>() { e };
            var response = await new RestConnector(token).PostObjectAsync(RestConstValues.EVENT, events);

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Event>>(await response.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<HttpResponseMessage> UpdateEvent(Event e)
        {
            return await new RestConnector(token).PutObjectAsync(RestConstValues.EVENT, e);
        }

        public async Task<HttpResponseMessage> DeleteEvent(int eventId)
        {
            var request = RestConstValues.EVENT + "?eventId=" + eventId;
            return await new RestConnector(token).DeleteObjectAsync(request);
        }
    }
}
