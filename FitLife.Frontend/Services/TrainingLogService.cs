using System.Net.Http.Headers;
using System.Net.Http.Json;
using FitLife.Frontend.Models;

namespace FitLife.Frontend.Services;

public class TrainingLogService(HttpClient http, AuthService authService)
{
    private async Task SetAuthHeader()
    {
        var token = await authService.GetTokenAsync();
        if (token is not null)
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<WorkoutProgram>> GetProgramsAsync()
    {
        await SetAuthHeader();
        return await http.GetFromJsonAsync<List<WorkoutProgram>>("workout/programs") ?? [];
    }

    public async Task<WorkoutProgram?> CreateProgramAsync(string name)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync("workout/programs", new { name });
        if (!response.IsSuccessStatusCode)
            return null;
        return await response.Content.ReadFromJsonAsync<WorkoutProgram>();
    }

    public async Task<WorkoutProgram?> AddExerciseAsync(Guid programId, string name, int sets, int reps, decimal? weightKg)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync($"workout/programs/{programId}/exercises", new { name, sets, reps, weightKg });
        if (!response.IsSuccessStatusCode)
            return null;
        return await response.Content.ReadFromJsonAsync<WorkoutProgram>();
    }

    public async Task DeleteExerciseAsync(Guid programId, Guid exerciseId)
    {
        await SetAuthHeader();
        await http.DeleteAsync($"workout/programs/{programId}/exercises/{exerciseId}");
    }
    
    public async Task<WorkoutProgram?> UpdateProgramNameAsync(Guid programId, string newName)
    {
        await SetAuthHeader();
        var response = await http.PutAsJsonAsync($"workout/programs/{programId}", newName);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<WorkoutProgram>();
    }
    
    public async Task<WorkoutProgram?> UpdateExerciseAsync(Guid programId, Guid exerciseId, string name, int sets, int reps, decimal? weightKg)
    {
        await SetAuthHeader();
        var response = await http.PutAsJsonAsync($"workout/programs/{programId}/exercises/{exerciseId}", new { name, sets, reps, weightKg });
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<WorkoutProgram>();
    }
}