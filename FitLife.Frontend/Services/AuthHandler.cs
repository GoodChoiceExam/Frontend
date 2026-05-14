using Microsoft.AspNetCore.Components;
using System.Net;

namespace FitLife.Frontend.Services;

public class AuthHandler(AuthService authService, NavigationManager navigation) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var response = await base.SendAsync(request, ct);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await authService.LogoutAsync();
            navigation.NavigateTo("/login", replace: true);
        }

        return response;
    }
}
