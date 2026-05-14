using System.Net.Http.Json;
using FitLife.Frontend.Models;
using Microsoft.JSInterop;

namespace FitLife.Frontend.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { email, password });
        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (token is null)
            return false;

        await _js.InvokeVoidAsync("localStorage.setItem", "jwt", token.AccessToken);
        return true;
    }

    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "jwt");
    }
    
    public async Task<bool> RegisterTrainerAsync(string fullName, string email, string password)
    {
        var token = await GetTokenAsync();
        if (token is not null)
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        
        var response = await _http.PostAsJsonAsync("auth/register-trainer", new { fullName, email, password });
        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (token is null)
            return false;

        await _js.InvokeVoidAsync("localStorage.setItem", "jwt", result.AccessToken);
        return true;
    }
    
    public async Task<bool> RegisterAsync(string fullName, string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/register", new
        {
            fullName,
            email,
            password
        });

        if (!response.IsSuccessStatusCode)
            return false;

        var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (token is null)
            return false;

        await _js.InvokeVoidAsync("localStorage.setItem", "jwt", token.AccessToken);
        return true;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _js.InvokeAsync<string?>("localStorage.getItem", "jwt");
    }
    
    public async Task<bool> IsLoggedInAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }
    
    public async Task<string?> GetRoleAsync()
    {
        var token = await GetTokenAsync();
        if (token is null) return null;

        var payload = token.Split('.')[1];
        var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(padded));
        var doc = System.Text.Json.JsonDocument.Parse(json);

        if (doc.RootElement.TryGetProperty("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", out var role))
            return role.GetString();

        return null;
    }
    
    public async Task<string?> GetUserNameAsync()
    {
        var token = await GetTokenAsync();
        if (token is null) return null;

        var parts = token.Split('.');
        if (parts.Length != 3) return null;

        var payload = parts[1];
        var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(padded));
        var doc = System.Text.Json.JsonDocument.Parse(json);

        return doc.RootElement.TryGetProperty("name", out var name) ? name.GetString() : null;
    }
    
    public async Task<Guid?> GetMemberIdAsync()
    {
        var token = await GetTokenAsync();
        if (token is null) return null;

        var parts = token.Split('.');
        if (parts.Length != 3) return null;

        var payload = parts[1];
        var padded = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(padded));
        var doc = System.Text.Json.JsonDocument.Parse(json);

        return doc.RootElement.TryGetProperty("sub", out var sub) && Guid.TryParse(sub.GetString(), out var id)
            ? id
            : null;
    }
}