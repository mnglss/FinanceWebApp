using API.Extensions;
using Application.Extensions;
using FluentValidation;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAPIService(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "FinanceWebApp",
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.WithExposedHeaders("content-disposition");
                      });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.MapOpenApi(); // https://localhost:7134/openapi/v1.json https://localhost:7134/api-docs/index.html https://localhost:7134/scalar/v1
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(options =>
    {
        options.DocumentTitle = "FinanceWebApp API";
        options.SpecUrl = "/openapi/v1.json";
    });
    app.MapScalarApiReference();
}
app.UseCors("FinanceWebApp");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public class LowerCaseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths.ToDictionary(
            path => path.Key.ToLowerInvariant(), // Convert the key to lowercase
            path => swaggerDoc.Paths[path.Key]
        );
        swaggerDoc.Paths.Clear(); // Clear the original paths
        foreach (var path in paths)
        {
            swaggerDoc.Paths.Add(path.Key, path.Value);
        }
    }
}

public class FinanceAppRoles()
{
    public const string PowerAdmin = "PowerAdmin";
    public const string Admin = "Admin";
    public const string User = "User";
}
