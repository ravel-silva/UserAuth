using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserAuth.Data;
using UserAuth.Model;
using UserAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// configura��o do banco de dados MySql
var ConectionString = builder.Configuration.GetConnectionString("UserConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(ConectionString,
        ServerVersion.AutoDetect(ConectionString));
});

//configura��o do Identity
builder.Services.AddIdentity<User,IdentityRole>()   //Adiciona a identidade do usu�rio
    .AddEntityFrameworkStores<AppDbContext>()   //Adiciona o contexto do banco de dados
    .AddDefaultTokenProviders(); //Adiciona o token padr�o

//configura��o do automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// inje��o do servi�o
builder.Services.AddScoped<UserServices>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
