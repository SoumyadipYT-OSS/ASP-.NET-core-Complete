using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyWebApp.Interfaces;
using System.Net;

namespace MyWebApp.Tests;

public class GreetingApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory; 

    public GreetingApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory; 
    }


    [Fact] 
    public async Task GetGreeting_ReturnsExpectedGreeting()
    {
        // Arrange 
        var mockPersonDetailsService = new Mock<IPersonDetailsService>(); 
        mockPersonDetailsService.Setup(detailsService => detailsService.GetFirstName()).Returns("Nabonita"); 
        mockPersonDetailsService.Setup(detailsService => detailsService.GetLastName()).Returns("Roy");

        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(detailService =>
            {
                detailService.AddSingleton(mockPersonDetailsService.Object);
            });
        }).CreateClient();


        // Act 
        var response = await client.GetAsync("/personDetails"); 
        var responseString = await response.Content.ReadAsStringAsync(); 

        // Assert 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
        Assert.Equal($"Namaskar Nabonita \n Swagatam Ms. Roy!", responseString);
    }
}