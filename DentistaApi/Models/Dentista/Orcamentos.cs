using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Orcamentos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime DataOrcamento { get; set; }
    public required int IdDentista { get; set; }
    public int? IdPaciente { get; set; }
    public string? NomePaciente { get; set; }
    public string? Telefone { get; set; }
    public string? Cpf {  get; set; }
    public string? Observacao { get; set; }
    public required ICollection<ConsultaEspecialidade> ListProcedimentos  { get; set; }
    public required ICollection<Consulta> ListConsultas { get; set; }
    public required Pagamento Pagamento { get; set; }

}
