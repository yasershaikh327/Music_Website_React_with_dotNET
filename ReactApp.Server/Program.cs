using AutoMapper;
using DataAccess.DbContext;
using DataAccess.Mapper;
using DataAccess.Models.Ui;
using DataAccess.Repository;
using DataAccess.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReactApp.Server.ExceptionFilter;

var builder = WebApplication.CreateBuilder(args);

// Access the Configuration object
var configuration = builder.Configuration;

// Add services to the container
builder.Services.AddControllersWithViews(); // This will add services for MVC views and TempData
builder.Services.AddControllers(c => c.Filters.Add(new MyExceptionFilter()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MongoDB.Admin and Email settings
builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<EmailCredentials>(configuration.GetSection("EmailCredentials"));
builder.Services.Configure<AdminCredits>(configuration.GetSection("AdminCredits"));

// Add services

builder.Services.AddScoped<IMemberMappper, MemberMapper>();
builder.Services.AddScoped<IDtoMemberMapper, DtoMemberMapper>();
builder.Services.AddScoped<IPlaylistDToMapper, PlaylistDToMapper>();
builder.Services.AddScoped<IPlaylistMapper, PlaylistMapper>();
builder.Services.AddScoped<IMusicMapper, MusicMapper>();
builder.Services.AddScoped<IDtoMusicMapper, MusicDtoMapper>();
builder.Services.AddScoped<IRegistrationMapper, RegistrationMapper>();
builder.Services.AddScoped<IRegistrationDtoMapper, RegistrationDtoMapper>();
builder.Services.AddScoped<ILoginMapper, LoginMapper>();
builder.Services.AddScoped<ILoginDtoMapper, LoginDtoMapper>();
builder.Services.AddScoped<IDtoMusictoPlaylistMapper, MusictoPlaylistDtoMapper>();
builder.Services.AddScoped<IMusictoPlaylistMapper, MusictoPlaylistMapper>();
builder.Services.AddScoped<ILoginDtoMapper, LoginDtoMapper>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddSingleton<EmailCredentials>(serviceProvider =>
{
    var emailCredentials = serviceProvider.GetRequiredService<IOptions<EmailCredentials>>().Value;
    return emailCredentials;
});

//Configure Admin
builder.Services.AddSingleton<AdminCredits>(serviceProvider =>
{
    var adminCredits = serviceProvider.GetRequiredService<IOptions<AdminCredits>>().Value;
    return adminCredits;
});



// Configure MongoDB client as a singleton
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var mongoDbSettings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(mongoDbSettings.ConnectionString);
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseCors("AllowAll"); // Enable CORS policy

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");


app.Run();
