using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models
{
    public class ConsultaEspecialidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public double ValorBase {  get; set; }
        public string DataCadastro { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        
    }
}
