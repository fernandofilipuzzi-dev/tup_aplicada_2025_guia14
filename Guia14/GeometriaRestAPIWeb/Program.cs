using GeometriaRestAPIWeb.GraphQL;
using GeometriaRestAPIWeb.GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<FiguraType>()
    .AddInMemorySubscriptions(); // Agregar soporte para subscripciones

var app = builder.Build();

app.UseHttpsRedirection();
app.UseWebSockets();  // Habilitar WebSockets

// Configurar la ruta por defecto para redirigir a Banana Cake Pop UI
app.MapGet("/", context =>
{
    context.Response.Redirect("/graphql");
    return Task.CompletedTask;
});

app.MapGraphQL();

app.Run();
