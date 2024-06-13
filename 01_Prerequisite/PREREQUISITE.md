# Prerequisites
_.NET 8.0 SDK_ [https://dotnet.microsoft.com/download/dotnet/8.0]


**Create a web app project**
.NET CLI
=> `dotnet new webapp --output aspnetcoreapp --no-https`
The preceding command creates a new web app project in a directory named 'aspnetcoreapp'. The project doesn't use HTTPS.
• You can give any name of your project. ex:
`dotnet new webapp --output myaspcoreApp --no-https`


**Run the app**
Run the following commands:
.NET CLI
`cd aspnetcoreapp`
`dotnet run`

The run command produces output like the following example:
_Output_
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5109
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\aspnetcoreapp


Open a browser and go to the URL shown in the output. In this example, the URL is http://localhost:5109.

<img width="400" height="240" src="https://learn.microsoft.com/en-us/aspnet/core/getting-started/_static/home-page.png?view=aspnetcore-8.0">


**Edit a Razor page**
• In the command shell, press Ctrl+C (Cmd+C in macOS) to exit the program.

• Open Pages/Index.cshtml in a text editor.

• Replace the line that begins with "Learn about" with the following highlighted markup and code:
_CSHTML_
@page
@model IndexModel
@{
    ViewData["Title"] = "Home page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Hello, world! The time on the server is @DateTime.Now</p>
</div>

• Save your changes.

• In the command shell run the dotnet run command again.

• In the browser, refresh the page and verify the changes are displayed.

<img width="400" height="240" src="https://learn.microsoft.com/en-us/aspnet/core/getting-started/_static/home-page-changed.png?view=aspnetcore-8.0">
