using Microsoft.AspNetCore.Authentication;
using NSwag.Generation.Processors.Security;
using NSwag;
using WebAppi.Authentication.ApiKey;
using WebAppi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, UserService>();

// Added BasicAuthenticationHandler class 
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

//must be added!
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
builder.Services
    .AddAuthentication(sharedOptions =>
    {
        sharedOptions.DefaultScheme = ApiKeyAuthenticationOptions.AuthenticationScheme;
    })
    .AddApiKey<ApiKeyAuthenticationService>(options => configuration.Bind("ApiKeyAuth", options));

//here using nswag.aspnecore
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = doc =>
    {
        doc.Info.Version = "v1";
        doc.Info.Title = "Tracking API";
        doc.Info.Description = "Awesome API";
        doc.Info.TermsOfService = "None";
    };
    options.AddSecurity(
        "ApiKey",
        new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "ApiKey",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Type Api Key below"
        });

    options.OperationProcessors.Add(new OperationSecurityScopeProcessor("ApiKey"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //without apikey in swagger
    //app.UseSwagger();
    //app.UseSwaggerUI();
    //app.UseResponseCaching();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthentication();//configure basic authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
