using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RedPoc.Repository.Context;
using RedPoc.Repository.Interfaces;
using RedPoc.Repository.Repository;
using RedPoc.Service.Interfaces;
using RedPoc.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//In Memory Db Context configuration            
builder.Services.AddDbContext<RedContext>(opt => opt.UseInMemoryDatabase("RedPocDB"));
builder.Services.AddMvcCore();
 
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddMemoryCache();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Swagger Initialization 
builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RedPoc API",
        Version = "v1",
        Description = "RedPoc API poc.",
        Contact = new OpenApiContact
        {
            Name = "Ignacio Mariscal",
            Email = "mariscal.ignacio@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/ignacio-mariscal-martinez-31752246/"),
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RedPoc API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
