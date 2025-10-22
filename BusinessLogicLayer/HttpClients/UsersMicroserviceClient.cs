using eCommerce.OrdersMicroservice.BusinessLogicLayer.DTO;
using System.Net.Http.Json;

namespace eCommerce.OrdersMicroservice.BusinessLogicLayer.HttpClients;

public class UsersMicroserviceClient
{
    private readonly HttpClient _httpClient;

    public UsersMicroserviceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDTO?> GetUserByUserId(Guid userId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/api/users/{userId}");

        if (!response.IsSuccessStatusCode)
        { 
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
                
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException("Error retrieving user data from Users Microservice.", null, System.Net.HttpStatusCode.BadRequest);
            }
            else
            {   
                throw new HttpRequestException($"Http request failed with status code {response.StatusCode}"); 
            }
        }

       UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>();

        if (user == null)
        {
            throw new InvalidOperationException("Received null user data from Users Microservice.");
        }
        return user;
    }
}
