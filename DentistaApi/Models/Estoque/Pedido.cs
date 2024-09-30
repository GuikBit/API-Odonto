using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Pedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime DtPedido { get; set; } = DateTime.Now;
    public string Status { get; set; } = "";
    public decimal ValorTotal { get; set; }

    public DateTime DtCriacao { get; set; } = DateTime.Now;
    public DateTime DtUpdate { get; set; } = DateTime.Now;

    public ICollection<DetalhePedido> DetalhesPedido { get; set; } = new List<DetalhePedido>();
}
