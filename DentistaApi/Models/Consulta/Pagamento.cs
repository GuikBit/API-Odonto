using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DentistaApi.Models { 

    public class Pagamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double ValorTotal { get; set; }
        public double Desconto { get; set; }
        public double Acrecimo { get; set; }
        public int qtdParcela { get; set; }
        public bool Pago { get; set; }
        public bool FatFechado { get; set; }
        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();

    }
}