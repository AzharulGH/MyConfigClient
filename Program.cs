using System.Collections;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("ASPNETCORE_ENVIRONMENT = " + builder.Environment.EnvironmentName);
Console.WriteLine("SPRING__PROFILES__ACTIVE = " + builder.Configuration["SPRING:PROFILES:ACTIVE"]);

// Config is automatically enriched with environment variables from launchSettings.json
var app = builder.Build();

app.MapGet("/", (IConfiguration config) =>
{
    // Example: read greeting from Spring Config Server
    var greeting = config["greeting"] ?? "No config found!";
    return new { Greeting = greeting };
});

// Optional: Dump all keys to console for debugging
foreach (var kvp in builder.Configuration.AsEnumerable())
{
    Console.WriteLine($"{kvp.Key} = {kvp.Value}");
}

Console.WriteLine("=== Raw Environment Variables ===");
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
{
    if (de.Key.ToString()!.StartsWith("SPRING"))
        Console.WriteLine($"{de.Key} = {de.Value}");
}

app.Run();

//INII

//using Steeltoe.Extensions.Configuration.ConfigServer;
//using System.Collections;

//var builder = WebApplication.CreateBuilder(args);
//Console.WriteLine("ASPNETCORE_ENVIRONMENT = " + builder.Environment.EnvironmentName);
//Console.WriteLine("SPRING__PROFILES__ACTIVE = " + builder.Configuration["SPRING:PROFILES:ACTIVE"]);

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

//// Enable Spring Cloud Config Server
//builder.Configuration.AddConfigServer(builder.Environment);

//var app = builder.Build();

//app.MapGet("/", (IConfiguration config) =>
//{
//    // Read value from Spring Config
//    var greeting = config["greeting"] ?? "No config found!";
//    return new { Greeting = greeting };
//});

//// Optional: dump all keys for debugging
//Console.WriteLine("=== Loaded Configuration ===");
//foreach (var kvp in builder.Configuration.AsEnumerable())
//{
//    Console.WriteLine($"{kvp.Key} = {kvp.Value}");
//}

//Console.WriteLine("=== Raw Environment Variables ===");
//foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
//{
//    if (de.Key.ToString()!.StartsWith("SPRING"))
//        Console.WriteLine($"{de.Key} = {de.Value}");
//}

//app.Run();