
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Cargo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required int OrganizacaoId { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public required string NivelHierarquico { get; set; }
    public required string Requisitos { get; set; }
    public int? IdUserUpdade { get; set; }
    public required int IdUsercriacao { get; set; }
    public required double SalarioBase { get; set; } 
    public required string CargaHoraria { get; set; }
    public required Boolean Status {  get; set; }
    public Boolean? ValeTrans { get; set; }
    public Boolean? ValeAR { get; set; }
    public Boolean? PlanoSaude { get; set; }
    public Boolean? Premiacao { get; set; }
    public Boolean? GymPass { get; set; }
    public Boolean? PLR { get; set; }
    public DateTime? DataUpdate { get; set; }
    public required DateTime DataCadastro { get; set; }

}
