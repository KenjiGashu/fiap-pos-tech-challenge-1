namespace Tests.Integration.Helpers;

using System.Net.Http.Headers;
using System.Net.Http.Json;

public static class AuthHelper
{
    public static async Task<string> GetToken(HttpClient client)
    {
        var login = new
        {
            email = "Admin@gmail.com",
            password = "1234"
        };

        var response = await client.PostAsJsonAsync("/api/identidade/login", login);
        var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();

        return json["token"].ToString();
    }

    public static void SetToken(HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
}
