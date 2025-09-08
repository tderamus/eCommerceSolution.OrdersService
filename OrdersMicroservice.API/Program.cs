using eCommerce.OrdersMicroservice.BusinessLogicLayer;
using eCommerce.OrdersMicroservice.DataAccessLayer;
using eCommerce.OrdersMicroservice.API.Middleware;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add Dataaccess layer services and Business logic layer services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddDBusinessLogicLayer(builder.Configuration);

//Add controllers and FluentValidation
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

//Add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

//Configure the HTTP request pipeline.
app.UseRouting();

//Use CORS
app.UseCors();

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

//Map controllers
app.MapControllers();

app.Run();
