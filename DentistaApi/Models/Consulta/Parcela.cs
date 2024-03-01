using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentistaApi.Models
{
    public class Parcela
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Pago { get; set; }
        public bool EhEntrada { get; set; }
        public double ValorParcela { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int? FormaDePagamento { get; set; }

    }
}
