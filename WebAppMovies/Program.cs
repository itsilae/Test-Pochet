using Microsoft.EntityFrameworkCore;
using WebAppMovies.Data;
using WebAppMovies.Mapper;
using WebAppMovies.MiddleWare;
using WebAppMovies.Repository;
using WebAppMovies.Services;
using WebAppMovies.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// =====================
// DB CONTEXT MYSQL
// =====================
builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// =====================
// INJECTION DES DEPENDANCES
// =====================
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieValidator, MovieValidator>();
builder.Services.AddScoped<IMovieMapper, MovieMapper>();

// =====================
// SWAGGER 
// =====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =====================
// MIDDLEWARE
// =====================
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();