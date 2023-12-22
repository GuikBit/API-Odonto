using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models
{
    public class ResponsavelDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Nome { get; set; } = "";
        public string Cpf { get; set; } = "";
        public string Telefone { get; set; } = "";

    }

}

