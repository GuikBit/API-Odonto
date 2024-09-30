using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Produto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; } = "";
    [Required]
    public string Descricao { get; set; } = "";
    [Required]
    public decimal Preco { get; set; }
    [Required]
    public int Quantidade { get; set; }

    [Required]
    public int CategoriaId { get; set; }
   
    public int FornecedorId { get; set; }

    public DateTime DtCriacao { get; set; } = DateTime.Now;
    public DateTime DtUpdate { get; set; } = DateTime.Now;
}
