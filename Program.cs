using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Application;
using SistemaEstoque.Data;
using SistemaEstoque.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// EF Core + SQLite, usando a connection string do appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Registro das dependências (isto faltava por completo no projeto original).
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IEstoqueRepository, EstoqueRepository>();
builder.Services.AddScoped<IServiceEstoque, ServiceEstoque>();

var app = builder.Build();

// Cria o banco/tabelas se ainda não existirem (equivalente ao CriarConexao() de antes,
// mas rodando uma única vez na inicialização, não a cada requisição).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();