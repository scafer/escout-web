using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Services
{
    public class EventService
    {
        private string token;

        public EventService() => this.token = string.Empty;

        public EventService(string token) => this.token = token;

        public async Task<List<Event>> SearchExecuted(string Key, string Value)
        {
            if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value))
                return await GetEvents(null);
            else
                return await GetEvents(new SearchQuery(Key, "iLIKE", Value + "%"));
        }

        private async Task<List<Event>> GetEvents(SearchQuery query)
        {
            var _events = new List<Event>();
            var request = RestConnector.EVENTS;

            if (query != null)
                request += "?query=" + JsonConvert.SerializeObject(query);

            var response = await new RestConnector(token).GetObjectAsync(request);
            if (!string.IsNullOrEmpty(response))
                _events = JsonConvert.DeserializeObject<List<Event>>(response);

            return _events;
        }

        public async Task<List<Event>> CreateEvent(Event e)
        {
            var result = new List<Event>();
            var request = RestConnector.EVENT;
            var events = new List<Event>();
            events.Add(e);
            var response = await new RestConnector(token).PostObjectAsync(request, events);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<List<Event>>(response);

            return result;
        }

        public async Task<SvcResult> UpdateEvent(Event e)
        {
            SvcResult result = new SvcResult();
            var request = RestConnector.EVENT;

            var response = await new RestConnector(token).PutObjectAsync(request, e);
            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<SvcResult>(response);

            return result;
        }

        public async Task<SvcResult> DeleteEvent(int eventId)
        {
            var result = new SvcResult();
            var request = RestConnector.EVENT + "?eventId=" + eventId;
            var response = await new RestConnector(token).DeleteObjectAsync(request);

            if (!string.IsNullOrEmpty(response))
                result = JsonConvert.DeserializeObject<SvcResult>(response);

            return result;
        }
    }
}
