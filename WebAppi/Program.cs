using Microsoft.AspNetCore.Authentication;
using NSwag;
using NSwag.Generation.Processors.Security;
using WebAppi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddAuthentication("BasicAuthentication")
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

//TODO here using nswag.aspnecore nugets
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
        "Basic",
        new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.Basic,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Type username and password"
        });

    options.OperationProcessors.Add(new OperationSecurityScopeProcessor("BasicAuth"));
});

//must be added!
//ConfigurationManager configuration = builder.Configuration;
//IWebHostEnvironment environment = builder.Environment;
//builder.Services
//    .AddAuthentication(sharedOptions =>
//    {
//        sharedOptions.DefaultScheme = ApiKeyAuthenticationOptions.AuthenticationScheme;
//    })
//    .AddApiKey<ApiKeyAuthenticationService>(options => configuration.Bind("ApiKeyAuth", options));


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
