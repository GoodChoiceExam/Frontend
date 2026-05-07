using System.Net.Http.Json;
using FitLife.Frontend.Models;

namespace FitLife.Frontend.Services;

public class ClassesService(HttpClient http)
{
    public async Task<List<TrainingClass>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<TrainingClass>>("api/classes") ?? [];
    }
}