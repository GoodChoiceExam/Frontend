using System.Net.Http.Headers;
using System.Net.Http.Json;
using FitLife.Frontend.Models;

namespace FitLife.Frontend.Services;

public class TrainerService(HttpClient http, AuthService authService)
{
    private async Task SetAuthHeader()
    {
        var token = await authService.GetTokenAsync();
        if (token is not null)
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<PersonalTrainer>> GetAllAsync()
    {
        await SetAuthHeader();
        return await http.GetFromJsonAsync<List<PersonalTrainer>>("api/trainers") ?? [];
    }
    
    public async Task<bool> CreateTrainerAsync(object request)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync("api/trainers", request);
        return response.IsSuccessStatusCode;
    }
}