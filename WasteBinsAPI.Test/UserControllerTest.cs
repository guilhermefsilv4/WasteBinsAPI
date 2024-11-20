using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WasteBinsAPI.Test;

public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public UserControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
        });
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_Returns200StatusCode()
    {
        string request = "/api/v1/User";
        
        var response = await _client.GetAsync(request);
        
        response.EnsureSuccessStatusCode();
    }
}