namespace DentistaApi.Models;

public class DetalhePedido
{
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }

    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
}
