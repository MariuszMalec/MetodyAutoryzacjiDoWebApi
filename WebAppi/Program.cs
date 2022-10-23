using Microsoft.AspNetCore.Authentication;
using WebAppi.Authentication.ApiKey;
using WebAppi.Service;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, UserService>();

#region Configure Swagger  
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasicAuth", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });
});
#endregion

builder.Services.AddAuthentication("BasicAuthentication")
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


////TODO here using nswag.aspnecore nugets
//builder.Services.AddOpenApiDocument(options =>
//{
//    options.PostProcess = doc =>
//    {
//        doc.Info.Version = "v1";
//        doc.Info.Title = "Tracking API";
//        doc.Info.Description = "Awesome API";
//        doc.Info.TermsOfService = "None";
//    };
//    options.AddSecurity(
//        "Basic",
//        new OpenApiSecurityScheme
//        {
//            Type = OpenApiSecuritySchemeType.Basic,
//            Name = "Authorization",
//            In = OpenApiSecurityApiKeyLocation.Header,
//            Description = "Type username and password"
//        });

//    options.OperationProcessors.Add(new OperationSecurityScopeProcessor("BasicAuth"));
//});

//must be added!
//ConfigurationManager configuration = builder.Configuration;
//IWebHostEnvironment environment = builder.Environment;
//builder.Services
//    .AddAuthentication(sharedOptions =>
//    {
//        sharedOptions.DefaultScheme = ApiKeyAuthenticationOptions.AuthenticationScheme;
//    })
//    .AddApiKey<ApiKeyAuthenticationService>(options => configuration.Bind("ApiKeyAuth", options));

////TODO here using nswag.aspnecore nugets
//builder.Services.AddOpenApiDocument(options =>
//{
//    options.PostProcess = doc =>
//    {
//        doc.Info.Version = "v1";
//        doc.Info.Title = "Tracking API";
//        doc.Info.Description = "Awesome API";
//        doc.Info.TermsOfService = "None";
//    };
//    options.AddSecurity(
//        "ApiKey",
//        new OpenApiSecurityScheme
//        {
//            Type = OpenApiSecuritySchemeType.ApiKey,
//            Name = "ApiKey",
//            In = OpenApiSecurityApiKeyLocation.Header,
//            Description = "Type Api Key below"
//        });

//    options.OperationProcessors.Add(new OperationSecurityScopeProcessor("ApiKey"));
//});

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
    //app.UseOpenApi();
    //app.UseSwaggerUi3();

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicAuth v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();//configure basic authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
