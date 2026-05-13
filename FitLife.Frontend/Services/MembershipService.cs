using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FitLife.Frontend.Models;

namespace FitLife.Frontend.Services;

public class MembershipService(HttpClient http, AuthService authService)
{
    private async Task SetAuthHeader()
    {
        var token = await authService.GetTokenAsync();
        http.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(token)
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<Member?> GetCurrentMemberAsync()
    {
        await SetAuthHeader();

        var response = await http.GetAsync("api/members");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<Member>();
    }

    public async Task<Member?> UpdateMemberAsync(UpdateMemberRequest request)
    {
        await SetAuthHeader();

        var response = await http.PutAsJsonAsync("api/members", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<Member>();
    }

    public async Task<Member?> UpdatePreferencesAsync(UpdateUserPreferenceRequest request)
    {
        await SetAuthHeader();

        var response = await http.PutAsJsonAsync("api/members/preferences", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<Member>();
    }
    
    public async Task<Member?> CreateMemberAsync(CreateMemberRequest request)
    {
        await SetAuthHeader();

        var response = await http.PostAsJsonAsync("api/members", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<Member>();
    }
}