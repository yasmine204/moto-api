using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoRentAPI.Common.Swagger;
using MotoRentAPI.Data;
using MotoRentAPI.Dtos;
using MotoRentAPI.Messaging;
using MotoRentAPI.Repositories;
using MotoRentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// PostgresSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Connection string 'DefaultConnection not found'")
    )
);

// RabbitMQ
builder.Services.AddSingleton<IMessagePublisher>(sp => new RabbitMqPublisher(
    builder.Configuration["RabbitMQ:HostName"] ?? "localhost",
    builder.Configuration["RabbitMQ:UserName"] ?? "rabbit_user",
    builder.Configuration["RabbitMQ:Password"] ?? "Rabbit@123"
));

builder.Services.AddSingleton<IMessageSubscriber>(sp => new RabbitMqSubscriber(
    builder.Configuration["RabbitMQ:HostName"] ?? "localhost",
    builder.Configuration["RabbitMQ:UserName"] ?? "rabbit_user",
    builder.Configuration["RabbitMQ:Password"] ?? "Rabbit@123"
));

builder.Services.AddHostedService<Motorcycle2024Consumer>();

// Services & Repositories
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<MotorcycleService>();

builder.Services.AddScoped<IDeliveryDriverRepository, DeliveryDriverRepository>();
builder.Services.AddScoped<DeliveryDriverService>();

builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<RentalService>();

builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped(sp => new ImageService(
    Path.Combine(Directory.GetCurrentDirectory(), "Storage")
));

// Validation
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        return new BadRequestObjectResult(new MessageResponseDto { Message = "Invalid data" });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Sistema de Manutenção de Motos",
            Version = "v1",
            Description = "API para gerenciamento de aluguel de motos e entregadores",
        }
    );

    c.EnableAnnotations();
    c.SchemaFilter<ExamplesSchemaFilter>();
});

var app = builder.Build();

// Auto-Migrate + Seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();
    SeedData.SeedDatabase(db);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
