using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Fornecedor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string NomeContato { get; set; } = "";
    public string Email { get; set; } = "";
    public string Telefone { get; set; } = "";
    public string Endereco { get; set; } = "";

    public DateTime DtCriacao { get; set; } = DateTime.Now;
    public DateTime DtUpdate { get; set; } = DateTime.Now;

    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
