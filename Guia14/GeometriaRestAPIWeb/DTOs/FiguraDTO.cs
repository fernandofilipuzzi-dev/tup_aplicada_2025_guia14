namespace GeometriaRestAPIWeb.DTOs;

public class FiguraDTO
{
    public int? Id { get; set; }
    public int Tipo { get; set; }
    public double? Area { get; set; }
    public double? Ancho { get; set; }
    public double? Largo { get; set; }
    public double? Radio { get; set; }

    public string ToString()
    {
        return $"Id: {Id}, Area: {Area}, Ancho: {Ancho}, Largo: {Largo}, Radio: {Radio}";
    }
}
