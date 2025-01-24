var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agregar servicios para los controladores
builder.Services.AddControllers();

// Otros servicios que puedas necesitar (Swagger en este caso)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS para que se aplique globalmente
app.UseCors("AllowBlazorApp");

app.UseAuthorization();

// Mapeo de los controladores
app.MapControllers();

app.Run();
