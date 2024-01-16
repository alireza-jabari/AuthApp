using System.Text;
using AuthApp;
using AuthApp.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// method 3 middleware :A
builder.Services.AddTransient<MiddlewareMethodThree>();


//jwt 
builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>{
    options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "alireza-jabari",

        RequireExpirationTime=true,
        ClockSkew=TimeSpan.Zero,
        ValidAudience = "alireza-jabari",
        // todo : get secret key from appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hello secret key alireza klahsdjf ljkahsdjfklj alksdjfkla jsdklfjlashdflasdf"))
    };
});

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();





//use middleware method two
app.UseMiddleware<ExceptionHandlingMiddleware>();

#region customMiddlewareFirstMethod
app.Use(async(context,next)=>{

    try
    {
            Console.WriteLine("hhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
        
        await next(context);
    }
    catch (Exception e)
    {
        Console.WriteLine("++++++++++++++++11111111111111111+++++++++++++++");
        context.Response.StatusCode=500;
        await next(context);
    }
});
#endregion




// method 3 middleware :B
app.UseMiddleware<MiddlewareMethodThree>();







app.MapControllers();
app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
