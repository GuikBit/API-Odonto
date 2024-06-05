using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Cargo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required DateTime DataUpdate { get; set; }
    public required DateTime DataCadastro { get; set;  }
    public required int IdUserUpdade { get; set; }
    public required double Salario { get; set; }
    public double Bonificacao { get; set; }

    public int OrganizacaoId { get; set; }
    public Organizacao IdOrganizacao { get; set; }

}
