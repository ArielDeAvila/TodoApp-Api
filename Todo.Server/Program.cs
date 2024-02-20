using Microsoft.EntityFrameworkCore;
using Todo.Server.Data;
using Todo.Server.Extensions;
using Todo.Server.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string Cors = "Cors";

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
      policy =>
      {
          policy.WithOrigins("*");
          policy.AllowAnyHeader();
          policy.AllowAnyMethod();
      }
    );
});

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoApp"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddValidationExtension();
builder.Services.AddServicesExtension();
builder.Services.AddAuthenticationExtension(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    TodoContext context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
