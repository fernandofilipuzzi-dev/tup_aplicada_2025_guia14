using GeometriaRestAPIWeb.DTOs;
using HotChocolate.Types;

namespace GeometriaRestAPIWeb.GraphQL.Types;

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