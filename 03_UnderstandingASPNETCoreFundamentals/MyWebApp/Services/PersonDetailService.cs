using MyWebApp.Interfaces;

namespace MyWebApp.Services;

internal sealed class PersonDetailService : IPersonDetailsService
{
    public string GetFirstName()
    {
        return "Nabonita ";
    }

    public string GetLastName()
    {
        return "Roy";
    }
}