using MongoDB.Driver;
using PRUEBA_GSE;
using PRUEBA_GSE.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Diego: Se registra la conexión con su repectivo contexto
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<MongoContextDB>();
//Serivio del Token creado
builder.Services.AddSingleton<TokenService>();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    });
builder.Services.AddAuthorization();
    

//Servicio de los DTO
builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<VehiculosService>();
builder.Services.AddScoped<ServicioVehiculoService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<PagoSevice>();
builder.Services.AddScoped<TipoVehiculoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();//Se agrega estaa parte ya que hace parte de los tokens al validar el login

app.UseStatusCodePages(async xon => {
    var respuesta = xon.HttpContext.Response;
    if (respuesta.StatusCode == 401)
    {
        await respuesta.WriteAsJsonAsync(new { mensaje = "No autorizado, debe iniciar sesión en Login" });
    }
    else if (respuesta.StatusCode == 403)
    {
        await respuesta.WriteAsJsonAsync(new { mensaje = "No tiene permisos" });
    }
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
