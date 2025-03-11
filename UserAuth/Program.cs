using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserAuth.Authorization;
using UserAuth.Data;
using UserAuth.Model;
using UserAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// configuração do banco de dados MySql
var ConectionString = builder.Configuration.GetConnectionString("UserConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(ConectionString,
        ServerVersion.AutoDetect(ConectionString));
});

//configuração do Identity
builder.Services.AddIdentity<User,IdentityRole>()   //Adiciona a identidade do usuário
    .AddEntityFrameworkStores<AppDbContext>()   //Adiciona o contexto do banco de dados
    .AddDefaultTokenProviders(); //Adiciona o token padrão

//configuração do automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// injeção do serviço
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ljkbbHJKL345632RSDFDFBFGDDfsdfdjy")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
        options.AddPolicy("Idade", options => options.AddRequirements(new Age(18))));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
