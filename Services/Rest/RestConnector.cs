using System.Text;
using Newtonsoft.Json;

namespace escout.Services.Rest;

public class RestConnector
{
    private const string AUTHENTICATION_MODE = "Authorization";
    private const string CONTENT_TYPE = "application/json";
    private readonly string token;

    public RestConnector()
    {
        token = string.Empty;
    }

    public RestConnector(string token)
    {
        this.token = token;
    }

    public async Task<HttpResponseMessage> GetObjectAsync(string conn)
    {
        var response = new HttpResponseMessage();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());
            response = await client.GetAsync(GetApiUrl() + conn);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex);
        }

        return response;
    }

    public async Task<HttpResponseMessage> PostObjectAsync(string conn, object data)
    {
        var response = new HttpResponseMessage();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());
            response = await client.PostAsync(GetApiUrl() + conn,
                new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, CONTENT_TYPE));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex);
        }

        return response;
    }

    public async Task<HttpResponseMessage> PutObjectAsync(string conn, object data)
    {
        var response = new HttpResponseMessage();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());
            response = await client.PutAsync(GetApiUrl() + conn,
                new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, CONTENT_TYPE));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex);
        }

        return response;
    }

    public async Task<HttpResponseMessage> DeleteObjectAsync(string conn)
    {
        var response = new HttpResponseMessage();

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add(AUTHENTICATION_MODE, GetAuthenticationHeader());
            response = await client.DeleteAsync(GetApiUrl() + conn);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex);
        }

        return response;
    }

    private string GetAuthenticationHeader()
    {
        return "bearer" + " " + token;
    }

    private static string GetApiUrl()
    {
        return Environment.GetEnvironmentVariable("ESCOUT_SERVER_URL") ?? "https://escout-server.onrender.com";
    }
}