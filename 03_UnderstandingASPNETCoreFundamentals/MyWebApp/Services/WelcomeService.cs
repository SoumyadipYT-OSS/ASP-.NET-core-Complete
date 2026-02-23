
using MyWebApp.Interfaces;

namespace MyWebApp.Services; 


public class WelcomeService : IWelcomeService
{
    readonly DateTime _serviceCreated; 
    readonly Guid _serviceId; 

    public WelcomeService()
    {
        _serviceCreated = DateTime.Now; 
        _serviceId = Guid.NewGuid();
    }

    public string GetWelcomeMessage()
    {
        return $"Welcome to Chennai! The current time is {_serviceCreated}. This service instance has an ID of {_serviceId}"; 
    }
}