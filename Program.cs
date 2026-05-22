var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        builder => builder.WithOrigins("http://localhost:5173", "http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");

var services = new[]
{
    new Service("Web Development", "Stunning websites for your brand.", "Globe"),
    new Service("Mobile Apps", "Powerful iOS and Android apps.", "Smartphone"),
    new Service("SEO Optimization", "Get found on Google faster.", "Search")
};

app.MapGet("/api/services", () => services)
   .WithName("GetServices");

app.MapPost("/api/contact", (ContactRequest request) => 
{
    // Mock saving to DB
    return Results.Ok(new { message = "Contact form received successfully!" });
})
.WithName("SubmitContact");

app.Run();

record Service(string Title, string Description, string Icon);
record ContactRequest(string Name, string Email, string Message);
