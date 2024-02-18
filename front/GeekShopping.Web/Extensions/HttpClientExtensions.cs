using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Extensions;

public static class HttpClientExtensions
{
    private static MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("application/json");
    private static JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public static async Task<T?> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Something went wrong calling the API");

        var dataAsString = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(dataAsString, _options);
    }

    public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient http, string url, T data)
    {
        var dataAsString = JsonSerializer.Serialize(data, _options);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = _contentType;
        return await http.PostAsync(url, content);
    }
}
