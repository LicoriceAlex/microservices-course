using Dal;
using Dal.Repositories.Interfaces;
using Dal.Repositories.Implementations;
using Logic.Managers;
using Api.UseCases.Interfaces;
using Api.UseCases;
using Dal.Repositories;
using Logic.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core (PostgreSQL)
builder.Services.AddDbContext<GiftCatalogDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("GiftCatalogDb")));

// DAL: repositories
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IDenominationRepository, DenominationRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();

// Logic: managers
builder.Services.AddScoped<IVendorManager, VendorManager>();
builder.Services.AddScoped<IDenominationManager, DenominationManager>();
builder.Services.AddScoped<IBatchesManager, BatchesManager>();
builder.Services.AddScoped<ICardsManager, CardsManager>();

// API UseCase managers
builder.Services.AddScoped<IVendorUseCaseManager, VendorUseCaseManager>();
builder.Services.AddScoped<IDenominationUseCaseManager, DenominationUseCaseManager>();
builder.Services.AddScoped<IBatchUseCaseManager, BatchUseCaseManager>();
builder.Services.AddScoped<ICardUseCaseManager, CardUseCaseManager>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Если нужен HTTPS — оставь, иначе можно убрать при локалке Docker
app.UseHttpsRedirection();

// Маршрутизация контроллеров
app.MapControllers();

// Health
app.MapGet("/health", () => Results.Ok(new { status = "ok", time = DateTime.UtcNow }));

app.Run();