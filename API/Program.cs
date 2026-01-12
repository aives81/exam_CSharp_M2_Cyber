using exam_CSharp_M2_Cyber.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services nécessaires
builder.Services.AddControllers();

// Enregistrement du service singleton pour les produits
builder.Services.AddSingleton<IProductService, ProductService>();

//Swagger
builder.Services.AddOpenApi();

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Activation des routes des controllers
app.MapControllers();

app.Run();