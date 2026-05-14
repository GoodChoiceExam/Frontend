using System.Net.Http.Headers;
using System.Net.Http.Json;
using FitLife.Frontend.Models;

namespace FitLife.Frontend.Services;

public class ClassesService(HttpClient http, AuthService authService)
{
    private async Task SetAuthHeader()
    {
        var token = await authService.GetTokenAsync();
        if (token is not null)
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<TrainingClass>> GetAllAsync()
    {
        await SetAuthHeader();
        try
        {
            return await http.GetFromJsonAsync<List<TrainingClass>>("api/classes") ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> CreateClassAsync(object request)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync("api/classes", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<ClassBooking?> BookAsync(Guid classId, Guid memberId)
    {
        await SetAuthHeader();
        var response = await http.PostAsJsonAsync($"api/classes/{classId}/bookings", new { memberId });
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<ClassBooking>();
    }

    public async Task<bool> CancelBookingAsync(Guid classId, Guid bookingId)
    {
        await SetAuthHeader();
        var response = await http.PutAsync($"api/classes/{classId}/bookings/{bookingId}/cancel", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<ClassBooking>> GetMyBookingsAsync(Guid memberId)
    {
        await SetAuthHeader();
        try
        {
            return await http.GetFromJsonAsync<List<ClassBooking>>($"api/classes/bookings/mine?memberId={memberId}") ?? [];
        }
        catch
        {
            return [];
        }
    }
}