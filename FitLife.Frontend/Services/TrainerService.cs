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
        try
        {
            return await http.GetFromJsonAsync<List<PersonalTrainer>>("api/trainers") ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> CreateTrainerAsync(object request)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync("api/trainers", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<TrainerBooking?> BookAsync(Guid trainerId, Guid memberId, DateTime sessionTime)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync($"api/trainers/{trainerId}/bookings", new { memberId, sessionTime });
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<TrainerBooking>();
    }

    public async Task<bool> CancelBookingAsync(Guid trainerId, Guid bookingId)
    {
        await SetAuthHeader();
        var response = await http.PutAsync($"api/trainers/{trainerId}/bookings/{bookingId}/cancel", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<TrainerBooking>> GetMyBookingsAsync(Guid memberId)
    {
        await SetAuthHeader();
        try
        {
            return await http.GetFromJsonAsync<List<TrainerBooking>>($"api/trainers/bookings/mine?memberId={memberId}") ?? [];
        }
        catch
        {
            return [];
        }
    }
    
    public async Task<List<int>> GetBookedHoursAsync(Guid trainerId, DateOnly date)
    {
        await SetAuthHeader();
        try
        {
            return await http.GetFromJsonAsync<List<int>>($"api/trainers/{trainerId}/booked-hours?date={date:yyyy-MM-dd}") ?? [];
        }
        catch
        {
            return [];
        }
    }
}