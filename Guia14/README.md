# API de Geometrías con GraphQL

## Instalación y Configuración

### Servidor (GeometriaRestAPIWeb)

1. Instalar paquetes NuGet:
```bash
dotnet add package HotChocolate.AspNetCore --version 13.7.0
```

2. Componentes principales:

#### Schema Definition (FiguraType.cs)
```csharp
public class FiguraType : ObjectType<FiguraDTO>
{
    protected override void Configure(IObjectTypeDescriptor<FiguraDTO> descriptor)
    {
        descriptor.Field(f => f.Id).Type<IntType>();
        descriptor.Field(f => f.Tipo).Type<IntType>();
        descriptor.Field(f => f.Area).Type<FloatType>();
  descriptor.Field(f => f.Ancho).Type<FloatType>();
      descriptor.Field(f => f.Largo).Type<FloatType>();
        descriptor.Field(f => f.Radio).Type<FloatType>();
    }
}
```

#### Query Definition (Query.cs)
```csharp
public class Query
{
    [GraphQLDescription("Obtiene todas las figuras")]
    public IEnumerable<FiguraDTO> GetFiguras() => _figuras;

    [GraphQLDescription("Obtiene una figura por su ID")]
    public FiguraDTO? GetFigura(int id) => _figuras.FirstOrDefault(f => f.Id == id);
}
```

### Cliente (GeometriaClientRestAPIDesktop)

1. Instalar paquetes NuGet:
```bash
dotnet add package GraphQL.Client
dotnet add package GraphQL.Client.Serializer.Newtonsoft
```

## Operaciones GraphQL Disponibles

### Queries

1. **Obtener todas las figuras**

GraphQL:
```graphql
query {
  figuras {
    id
    tipo
    ancho
    largo
    radio
    area
  }
}
```

C# Cliente:
```csharp
var client = new GraphQLHttpClient("https://localhost:7257/graphql", new NewtonsoftJsonSerializer());

var request = new GraphQLRequest
{
    Query = @"
        query {
 figuras {
           id
                tipo
         ancho
              largo
    radio
  area
            }
     }"
};

var response = await client.SendQueryAsync<FigurasResponse>(request);
var figuras = response.Data.Figuras;
```

2. **Obtener figura por ID**

GraphQL:
```graphql
query {
  figura(id: 1) {
    id
    tipo
    ancho
    largo
  }
}
```

C# Cliente:
```csharp
var request = new GraphQLRequest
{
    Query = @"
     query($figuraId: Int!) {
figura(id: $figuraId) {
          id
                tipo
            ancho
     largo
      }
}",
    Variables = new { figuraId = 1 }
};

var response = await client.SendQueryAsync<SingleFiguraResponse>(request);
var figura = response.Data.Figura;
```

### Mutations

1. **Agregar nueva figura**

GraphQL:
```graphql
mutation {
  addFigura(figura: {
    id: 2
    tipo: 1
    ancho: 10
    largo: 20
  }) {
    id
    tipo
  }
}
```

C# Cliente:
```csharp
var mutation = new GraphQLRequest
{
    Query = @"
        mutation($figura: FiguraInput!) {
          addFigura(figura: $figura) {
    id
      tipo
      ancho
        largo
  }
    }",
    Variables = new { 
        figura = new { 
          id = 2, 
   tipo = 1, 
            ancho = 10.0, 
     largo = 20.0 
  } 
    }
};

var response = await client.SendMutationAsync<AddFiguraResponse>(mutation);
```

2. **Actualizar figura**

GraphQL:
```graphql
mutation {
  updateFigura(
    id: 1, 
    value: {
      tipo: 1
      ancho: 15
   largo: 25
 }
  ) {
    id
    tipo
  }
}
```

C# Cliente:
```csharp
var mutation = new GraphQLRequest
{
    Query = @"
   mutation($id: Int!, $figura: FiguraInput!) {
            updateFigura(id: $id, value: $figura) {
 id
        tipo
 ancho
      largo
       }
        }",
    Variables = new { 
id = 1, 
        figura = new { 
       tipo = 1, 
            ancho = 15.0, 
  largo = 25.0 
  } 
    }
};

var response = await client.SendMutationAsync<UpdateFiguraResponse>(mutation);
```

3. **Eliminar figura**

GraphQL:
```graphql
mutation {
  deleteFigura(id: 1)
}
```

C# Cliente:
```csharp
var mutation = new GraphQLRequest
{
    Query = @"
  mutation($id: Int!) {
            deleteFigura(id: $id)
        }",
    Variables = new { id = 1 }
};

var response = await client.SendMutationAsync<DeleteFiguraResponse>(mutation);
```

### Subscriptions (WebSocket)

Para habilitar las subscripciones, primero agregamos el soporte en el servidor:

```csharp
// Program.cs
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
 .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();  // Agrega soporte para subscripciones

app.UseWebSockets();  // Habilita WebSockets
```

Definición de Subscription:
```csharp
// Subscription.cs
public class Subscription
{
    [Subscribe]
    [Topic]
    public FiguraDTO OnFiguraAdded([EventMessage] FiguraDTO figura) => figura;

    [Subscribe]
    [Topic]
    public FiguraDTO OnFiguraUpdated([EventMessage] FiguraDTO figura) => figura;
}
```

Cliente WebSocket:
```csharp
var client = new GraphQLHttpClient("wss://localhost:7257/graphql", new NewtonsoftJsonSerializer());

// Suscribirse a nuevas figuras
var subscription = new GraphQLRequest
{
    Query = @"
        subscription {
    onFiguraAdded {
        id
       tipo
  ancho
          largo
     }
      }"
};

var subscriptionStream = await client.CreateSubscriptionStream<FiguraSubscriptionResponse>(subscription);

await foreach (var response in subscriptionStream)
{
    var nuevaFigura = response.Data.OnFiguraAdded;
    // Manejar la nueva figura
}
```

## Clases de Respuesta

```csharp
public class FigurasResponse
{
    public List<FiguraDTO> Figuras { get; set; }
}

public class SingleFiguraResponse
{
    public FiguraDTO Figura { get; set; }
}

public class AddFiguraResponse
{
    public FiguraDTO AddFigura { get; set; }
}

public class UpdateFiguraResponse
{
    public FiguraDTO UpdateFigura { get; set; }
}

public class DeleteFiguraResponse
{
    public bool DeleteFigura { get; set; }
}

public class FiguraSubscriptionResponse
{
    public FiguraDTO OnFiguraAdded { get; set; }
}
```

## Estructura del Proyecto

```
GeometriaRestAPIWeb/
??? GraphQL/
?   ??? Types/
?   ?   ??? FiguraType.cs
?   ??? Query.cs
?   ??? Mutation.cs
?   ??? Subscription.cs
??? DTOs/
?   ??? FiguraDTO.cs
??? Program.cs

GeometriaClientRestAPIDesktop/
??? FormPrincipal.cs
??? DTOs/
    ??? FiguraDTO.cs
```

## Notas Importantes

1. **Configuración del Cliente**:
   - Usar `HttpClient` para queries y mutations
   - Usar `WebSocket` para subscripciones
   - Manejar errores y timeouts apropiadamente

2. **Seguridad**:
   - Las URLs mostradas son para desarrollo local
   - En producción, usar HTTPS y autenticación
   - Configurar CORS según necesidad

3. **Performance**:
   - Usar DataLoader para N+1 queries
   - Implementar caching cuando sea necesario
   - Limitar profundidad de queries