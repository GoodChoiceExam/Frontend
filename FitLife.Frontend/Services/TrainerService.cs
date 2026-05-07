using System.Net.Http.Json;
using FitLife.Frontend.Models;

namespace FitLife.Frontend.Services;

public class TrainerService(HttpClient http)
{
    public async Task<List<PersonalTrainer>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<PersonalTrainer>>("api/trainers") ?? [];
    }
}