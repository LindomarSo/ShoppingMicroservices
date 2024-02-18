using AutoMapper;
using GeekShopping.Product.Config;
using GeekShopping.Product.Model.Context;
using GeekShopping.Product.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "GeekShoppingApi" });
});

builder.Services.AddDbContext<MySQLContext>(options =>
{
    var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];
    options.UseMySql(connection, new MySqlServerVersion(new Version()));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
