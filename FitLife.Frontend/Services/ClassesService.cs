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
        return await http.GetFromJsonAsync<List<TrainingClass>>("api/classes") ?? [];
    }

    public async Task<List<TrainingClass>> GetBookedByMemberAsync(Guid memberId)
    {
        var classes = await GetAllAsync();

        return classes
            .Where(trainingClass => GetActiveBooking(trainingClass, memberId) is not null)
            .OrderBy(trainingClass => trainingClass.StartTime)
            .ToList();
    }

    public async Task<bool> CancelBookingAsync(Guid trainingClassId, Guid bookingId)
    {
        await SetAuthHeader();
        var response = await http.PutAsync($"api/classes/{trainingClassId}/bookings/{bookingId}/cancel", null);

        return response.IsSuccessStatusCode;
    }

    public static ClassBooking? GetActiveBooking(TrainingClass trainingClass, Guid memberId) =>
        trainingClass.Bookings.FirstOrDefault(booking =>
            booking.MemberId == memberId &&
            string.Equals(booking.Status, "Booked", StringComparison.OrdinalIgnoreCase));
}
