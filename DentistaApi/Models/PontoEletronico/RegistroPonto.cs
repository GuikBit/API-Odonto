using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class RegistroPonto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    public int OrganizacaoId { get; set; }
    public Organizacao? Organizacao { get; set; }
    public int FuncionarioId { get; set; }
    public Funcionario? Funcionario { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateOnly DataRegistro { get; set; }
    public TimeOnly HoraRegistro { get; set; }  
    public Status Status { get; set; }
    public TipoRegistro Registro { get; set; }
    public char Controle { get; set; } = 'A';
    public string Observacao { get; set; }


}
