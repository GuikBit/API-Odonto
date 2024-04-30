namespace DentistaApi.Models;

public class ContasReceber
{
    public Parcela? Parcela { get; set; }
    public int? IdConsulta { get; set; }
    public string? NomePaciente { get; set; }
    public string? contatoPaciente { get; set; }
    public string? NomeDentista { get; set; }
    public DateTime? DataConsulta { get; set; }

}
