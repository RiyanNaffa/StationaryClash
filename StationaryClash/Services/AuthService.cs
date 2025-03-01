using System.Text.Json;
using StationaryClash.Interfaces.Services;
using StationaryClash.Models;

namespace StationaryClash.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        // Constructor
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<User?> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new LoginRequest
            {
                Username = username,
                Password = password
            });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }

            return null;
        }
        public async Task<RegisterResponse> RegisterAsync(string username, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", new RegisterRequest
                {
                    Username= username,
                    Password = password
                });

                if (!response.IsSuccessStatusCode)
                {
                    // Tangani kasus jika respons gagal
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Failed to register. Status: {response.StatusCode}, Error: {errorMessage}");
                }
                var responseContent = await response.Content.ReadFromJsonAsync<RegisterResponse>()
                                      ?? throw new Exception("Empty response content");
                return responseContent;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse JSON response.", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("An error occurred while sending the request.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred.", ex);
            }
        }
    }
}
