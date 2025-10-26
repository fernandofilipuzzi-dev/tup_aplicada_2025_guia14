using GeometriaRestAPIWeb.DTOs;
using HotChocolate;

namespace GeometriaRestAPIWeb.GraphQL;

public class Query
{
    private static List<FiguraDTO> _figuras = new List<FiguraDTO>()
    {
        new FiguraDTO{ Id=1, Tipo=1, Largo=1, Ancho=1 }
    };

    [GraphQLDescription("Obtiene todas las figuras")]
    public IEnumerable<FiguraDTO> GetFiguras() => _figuras;

    [GraphQLDescription("Obtiene una figura por su ID")]
    public FiguraDTO? GetFigura(int id) => _figuras.FirstOrDefault(f => f.Id == id);

    [GraphQLDescription("Agrega una nueva figura")]
    public FiguraDTO AddFigura(FiguraDTO figura)
    {
        _figuras.Add(figura);
        return figura;
    }

    [GraphQLDescription("Actualiza una figura existente")]
    public FiguraDTO? UpdateFigura(int id, FiguraDTO value)
    {
        var figura = _figuras.FirstOrDefault(f => f.Id == id);
        if (figura != null)
        {
            figura.Ancho = value.Ancho;
            figura.Largo = value.Largo;
            figura.Radio = value.Radio;
            figura.Tipo = value.Tipo;
        }
        return figura;
    }

    [GraphQLDescription("Elimina una figura por su ID")]
    public bool DeleteFigura(int id)
    {
        var figura = _figuras.FirstOrDefault(f => f.Id == id);
        if (figura != null)
        {
            return _figuras.Remove(figura);
        }
        return false;
    }
}