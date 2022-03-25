using System.Text.Json;

namespace SmartStream.Client.Services
{
    public class StreamingHttpClient : HttpClient
    {
        protected JsonSerializerOptions JsonOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public async Task<T?> GetJsonAsync<T>(string uri, string querystring = null, T? defaultValue = default)
        {
            try
            {
                var httpResponse = await GetAsync($"{uri}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseAsString = await httpResponse.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseAsString, JsonOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops: {ex}");
            }

            return defaultValue;
        }
    }
}
