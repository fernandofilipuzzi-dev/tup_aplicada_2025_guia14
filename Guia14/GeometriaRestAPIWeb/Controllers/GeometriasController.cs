using GeometriaRestAPIWeb.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeometriaRestAPIWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeometriasController : Controller
{

    private static List<FiguraDTO> figuras = new List<FiguraDTO>()
    {
        new FiguraDTO{ Id=1, Tipo=1, Largo=1, Ancho=1 }
    };

    // GET: api/<GeometriaController>
    [HttpGet]
    public ActionResult<List<FiguraDTO>> Get()
    {
        if (figuras.Any() == false) return NotFound("No se encontraron figuras");
        return Ok(figuras);
    }

    // GET api/<GeometriaController>/5
    [HttpGet("{id}")]
    public ActionResult<FiguraDTO> Get(int id)
    {
        var figura = (from f in figuras where f.Id == id select f).FirstOrDefault();

        if (figuras.Any() == false) return NotFound("No se encontro la figura");

        return Ok(figura);
    }

    // POST api/<GeometriaController>
    [HttpPost]
    public void Post([FromBody] FiguraDTO figuraDTO)
    {
        figuras.Add(figuraDTO);
    }

    // PUT api/<GeometriaController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] FiguraDTO value)
    {
        var figura = (from f in figuras where f.Id == id select f).FirstOrDefault();
        figura.Ancho = value.Ancho;
        figura.Largo = value.Largo;
        figura.Radio = value.Radio;
        figura.Tipo = value.Tipo;
    }

    // DELETE api/<GeometriaController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var figura = (from f in figuras where f.Id == id select f).FirstOrDefault();
        figuras.Remove(figura);
    }
}