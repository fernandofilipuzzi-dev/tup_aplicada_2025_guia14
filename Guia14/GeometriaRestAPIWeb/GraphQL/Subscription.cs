using GeometriaRestAPIWeb.DTOs;
using HotChocolate;
using HotChocolate.Types;

namespace GeometriaRestAPIWeb.GraphQL;

public class Subscription
{
  [Subscribe]
    [Topic]
    public FiguraDTO OnFiguraAdded([EventMessage] FiguraDTO figura) => figura;

    [Subscribe]
    [Topic]
    public FiguraDTO OnFiguraUpdated([EventMessage] FiguraDTO figura) => figura;

    [Subscribe]
    [Topic]
    public int OnFiguraDeleted([EventMessage] int figuraId) => figuraId;
}