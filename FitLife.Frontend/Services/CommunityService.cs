using System.Net.Http.Headers;
using System.Net.Http.Json;
using FitLife.Frontend.Models;
using System.Net;

namespace FitLife.Frontend.Services;

public class CommunityService(HttpClient http, AuthService authService)
{
    private async Task SetAuthHeader()
    {
        var token = await authService.GetTokenAsync();
        if (token is not null)
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<CenterCommunity>> GetCommunitiesAsync()
    {
        return await http.GetFromJsonAsync<List<CenterCommunity>>("api/communities") ?? [];
    }
    
    public async Task<CenterCommunity?> GetCommunityByCenterAsync(string center)
    {
        var response = await http.GetAsync($"api/communities/by-center/{Uri.EscapeDataString(center)}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CenterCommunity>();
    }

    public async Task<List<CommunityPost>> GetPostsAsync(Guid communityId)
    {
        return await http.GetFromJsonAsync<List<CommunityPost>>($"api/communities/{communityId}/posts") ?? [];
    }

    public async Task CreatePostAsync(Guid communityId, string content)
    {
        await SetAuthHeader();
        await http.PostAsJsonAsync($"api/communities/{communityId}/posts", new { content });
    }
}