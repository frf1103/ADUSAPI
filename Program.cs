using ADUSAPI.Context;
using ADUSAPI.Middlewares;
using ADUSAPI.Services;
using ADUSAPI.Shared;
using ADUSAPI.Validators.Assinatura;
using ADUSAPI.Validators.Banco;
using ADUSAPI.Validators.Localidade;
using ADUSAPI.Validators.Moeda;
using ADUSAPI.Validators.Parceiro;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ASAASSettings>(builder.Configuration.GetSection("ASAASSettings"));

builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API ADUS", Version = "v1" });

    // Define o esquema de segurança JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT no campo abaixo usando o formato: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ADUSContext>();
builder.Services.AddTransient<ExcluirRegiaoValidator>();
builder.Services.AddTransient<RegiaoValidator>();

builder.Services.AddTransient<ExcluirParceiroValidator>();
builder.Services.AddTransient<ParceiroValidator>();
builder.Services.AddTransient<ParceiroService>();

builder.Services.AddTransient<MoedaValidator>();
builder.Services.AddTransient<ExcluirMoedaValidator>();
builder.Services.AddTransient<MoedaService>();

builder.Services.AddTransient<CotacaoMoedaValidator>();
builder.Services.AddTransient<ExcluirCotacaoMoedaValidator>();
builder.Services.AddTransient<CotacaoMoedaService>();

builder.Services.AddTransient<AssinaturaValidator>();
//builder.Services.AddTransient<ExcluirCotacaoMoedaValidator>();
builder.Services.AddTransient<AssinaturaService>();

builder.Services.AddTransient<ParametrosGuruService>();

builder.Services.AddTransient<ParcelaService>();
builder.Services.AddTransient<LocalidadeService>();

builder.Services.AddTransient<BancoValidator>();
builder.Services.AddTransient<ExcluirBancoValidator>();
builder.Services.AddTransient<BancoService>();
builder.Services.AddTransient<PlanoContaService>();
builder.Services.AddTransient<TransacaoService>();
builder.Services.AddTransient<CentroCustoService>();
builder.Services.AddTransient<MovimentoCaixaService>();
builder.Services.AddTransient<TransacBancoService>();

builder.Services.AddTransient<ContaCorrenteValidator>();
//builder.Services.AddTransient<ExcluirContaCorrenteValidator>();
builder.Services.AddTransient<ContaCorrenteService>();
builder.Services.AddTransient<ConviteService>();
builder.Services.AddTransient<CartaoAssinaturaService>();
builder.Services.AddTransient<LogCheckoutService>();
builder.Services.AddTransient<WebhookAsaasService>();
builder.Services.AddTransient<BuscarParceiroPorCustomerAsaasService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<RefreshTokenStore>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Logging.AddEventLog();

/* TRANTANDO TOKEN */

var key = builder.Configuration["JwtSettings:Secret"];
if (string.IsNullOrEmpty(key) || key.Length < 32)
    throw new Exception("A chave JWT é muito curta. Use no mínimo 32 caracteres.");

// Configura autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors("AllowAllOrigins");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ADUSContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.

/*
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
//app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ValidationExceptionHandlerMiddleware>();

app.Run();