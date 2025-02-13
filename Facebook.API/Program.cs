using Facebook.API.Handlers;
using Facebook.API.Middlewares;
using Facebook.Application.IServices.IAuth;
using Facebook.Application.IServices.IConservation;
using Facebook.Application.IServices.IRelationship;
using Facebook.Application.IServices.IUsers;
using Facebook.Application.Services.Auth;
using Facebook.Application.Services.Conservation;
using Facebook.Application.Services.Jwt;
using Facebook.Application.Services.Message;
using Facebook.Application.Services.MessageReadBy;
using Facebook.Application.Services.Relationship;
using Facebook.Application.Services.Users;
using Facebook.Domain.IRepositories;
using Facebook.Domain.IRepositories.IAuth;
using Facebook.Domain.IRepositories.IConservation;
using Facebook.Domain.IRepositories.IRelationship;
using Facebook.Domain.IRepositories.Users;
using Facebook.Infrastructure.Migrations.Contexts;
using Facebook.Infrastructure.Repositories;
using Facebook.Infrastructure.Repositories.Conservation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); // Json converter to accept type Date from Frontend to DateOnly in Backend
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Customize

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// @Todo: use assembly scanning to import service and repository into DI container of .NET
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRelationshipService, RelationshipService>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IJwtRepository>(sp =>
    new JwtRepository(sp.GetRequiredService<IConfiguration>(),
                      () => sp.GetRequiredService<IAuthRepository>()));
builder.Services.AddScoped<IConservationService, ConservationService>();
builder.Services.AddScoped<IConservationRepository, ConservationRepository>();
builder.Services.AddScoped<IConservationMemberService, ConservationMemberService>();
builder.Services.AddScoped<IConservationMemberRepository, ConservationMemberRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageReadByService, MessageReadByService>();
builder.Services.AddScoped<IMessageReadByRepository, MessageReadByRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                      });
});

// logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
});

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Custome
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<FormatDateMiddleware>();

app.MapControllers();

app.Run();
