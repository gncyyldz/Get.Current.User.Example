using Get.Current.User.Example.Configurations;
using Get.Current.User.Example.Configurations.Abstractions;
using Get.Current.User.Example.Services;
using Get.Current.User.Example.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ITokenHandler, Get.Current.User.Example.Services.TokenHandler>();
builder.Services.AddSingleton<IUserContext, UserContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/get-token", (ITokenHandler tokenHandler) => tokenHandler.CreateAccessToken());

app.MapGet("/get-userid", (IUserContext userContext) => userContext.UserId)
    .RequireAuthorization();

app.MapGet("/get-username", (IUserContext userContext) => userContext.Username)
    .RequireAuthorization();

app.Run();
