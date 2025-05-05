using ADUSAPI.Context;
using ADUSAPI.Middlewares;
using ADUSAPI.Services;
using ADUSAPI.Validators.Assinatura;
using ADUSAPI.Validators.Banco;
using ADUSAPI.Validators.Localidade;
using ADUSAPI.Validators.Moeda;
using ADUSAPI.Validators.Parceiro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Logging.AddEventLog();

var app = builder.Build();
app.UseCors("AllowAllOrigins");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ADUSContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ValidationExceptionHandlerMiddleware>();

app.Run();