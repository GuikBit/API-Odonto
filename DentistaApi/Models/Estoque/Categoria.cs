using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = "";
        [Required]
        public string Descricao { get; set; } = "";

        public DateTime DtCriacao { get; set; } = DateTime.Now;
        public DateTime DtUpdate { get; set; } = DateTime.Now;

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }

