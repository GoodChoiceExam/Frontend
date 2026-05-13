using System.Net.Http.Headers;
using System.Net.Http.Json;
using FitLife.Frontend.Models;

namespace FitLife.Frontend.Services;

public class LivestreamService(HttpClient http, AuthService authService)
{
    private async Task SetAuthHeader()
    {
        var token = await authService.GetTokenAsync();
        if (token is not null)
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<LivestreamSession>> GetAllAsync()
    {
        await SetAuthHeader();
        return await http.GetFromJsonAsync<List<LivestreamSession>>("api/livestreams") ?? [];
    }

    public async Task<LivestreamSession?> GetLiveNowAsync()
    {
        await SetAuthHeader();
        var response = await http.GetAsync("api/livestreams/live");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<LivestreamSession>();
    }
    
    public async Task<bool> CreateLivestreamAsync(object request)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync("api/livestreams", request);
        return response.IsSuccessStatusCode;
    }
}