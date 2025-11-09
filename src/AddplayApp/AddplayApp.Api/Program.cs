using AddplayApp.Api.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5175") // your React dev server
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapPost("/api/users/create-user", async (User u, AppDbContext db) =>
{
    u.TimeStamp = DateTime.UtcNow;
    db.Users.Add(u);
    await db.SaveChangesAsync();
    return Results.Created($"/api/users/{u.Id}", u);
});

app.MapPost("/api/users/create-bulk-users", async (AppDbContext db) =>
{
    var faker = new Faker<User>()
        .RuleFor(x => x.Name, f => f.Name.FullName())
        .RuleFor(x => x.Age, f => f.Random.Int(18, 60))
        .RuleFor(x => x.Email, f => f.Internet.Email())
        .RuleFor(x => x.TimeStamp, f => DateTime.UtcNow);

    db.Users.AddRange(faker.Generate(10_000));
    await db.SaveChangesAsync();
    return Results.Ok(new { message = "10,000 users inserted" });
});

app.MapGet("/api/users/fetch-users",
async (AppDbContext db, IMemoryCache cache) =>
{
    if (!cache.TryGetValue("AllUsers", out List<User>? users))
    {
        users = await db.Users.AsNoTracking().ToListAsync();
        cache.Set("AllUsers", users, TimeSpan.FromMinutes(5));
    }
    return Results.Ok(users);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
