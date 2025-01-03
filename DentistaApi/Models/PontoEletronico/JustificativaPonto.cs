using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class JustificativaPonto
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    public int OrganizacaoId { get; set; }
    public Organizacao? Organizacao { get; set; }
    public int FuncionarioId { get; set; }
    public RegistroPonto? RegistroPonto { get; set; }
    public int RegistroPontoId { get; set; }
    public string Motivo { get; set; }
    public Status Status { get; set; }
    public int AvaliadoPor {  get; set; }
    public DateTime DataAvaliacao { get; set; }
}
