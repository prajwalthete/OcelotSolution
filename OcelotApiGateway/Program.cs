using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure that HTTPS redirection is applied before other middleware
app.UseHttpsRedirection();

// Use Ocelot middleware
await app.UseOcelot();

// Map controllers after Ocelot middleware
app.MapControllers();

app.Run();



/*
 * To request every article from our database, let’s make a request to:

https://localhost:5003/gateway/articles
To get a specific writer from the database, let’s make a request to:

https://localhost:5003/gateway/writers/1
 * */