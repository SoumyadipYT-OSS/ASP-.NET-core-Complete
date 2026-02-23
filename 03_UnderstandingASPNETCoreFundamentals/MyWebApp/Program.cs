using Microsoft.AspNetCore.Rewrite;
using MyWebApp.Interfaces;
using MyWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PersonService>();     // Singleton using service 
builder.Services.AddSingleton<IPersonDetailsService, PersonDetailService>();       // Singleton using interface and service 
builder.Services.AddTransient<IWelcomeService, WelcomeService>();    // Transient using interface and service 
var app = builder.Build();


app.Use(async (context, next) =>
{
    await next(); 
    Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
});
app.UseRewriter(new RewriteOptions().AddRedirect("history", "about"));
app.UseRewriter(new RewriteOptions().AddRedirect("tshirts", "GokalDasExports"));

app.MapGet("/", () => "Welcome to Chennai!");
app.MapGet("/about", () => "Chennai was founded in 1639");

app.MapGet("/Shirts", () => "Bombay Shirts");
app.MapGet("/GokalDasExports", () => "Style Fashions you get here!");

// Singleton service Mapping 
app.MapGet("/person", (PersonService personService) =>
{
    return $"Namaste, {personService.GetPersonName()}!";
});

// Singleton interface service Mapping 
app.MapGet("/personDetails", (IPersonDetailsService personDetailsService) => {
    return $"Namaskar {personDetailsService.GetFirstName()} \n Swagatam Ms. {personDetailsService.GetLastName()}!";
}); 

// Transient WelcomeService Mapping 
app.MapGet("/Welcome", async (IWelcomeService welcomeService1, IWelcomeService welcomeService2) => 
    {
        string message1 = $"Message1: {welcomeService1.GetWelcomeMessage()}";
        string message2 = $"Message2: {welcomeService2.GetWelcomeMessage()}";
        return $"{message1}\n{message2}";
    });
app.Run();
