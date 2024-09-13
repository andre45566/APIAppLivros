using LivrosAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao container.
builder.Services.AddControllers();

// Configura��o da conex�o com o banco de dados
builder.Services.AddSingleton<LivroRepository>(
    provider => new LivroRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configura��o do Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Livros V1");
        c.RoutePrefix = string.Empty;
    });
}

// Pipeline de requisi��o HTTP
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();