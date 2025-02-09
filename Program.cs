using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


// Register DbConnection
builder.Services.AddSingleton<IDbConnection, DbConnection>();

// Register services/repo
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Initialize DB
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    DbInitializer.Initialize(connectionString!);


    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();


// serve index.html
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "Views", "layout.html"));
        return;
    }
    await next();
});

app.MapControllers();
app.Run();
