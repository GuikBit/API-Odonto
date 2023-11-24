using System.ComponentModel.DataAnnotations.Schema;

namespace DentistaApi.Models
{
    public class Dentista : User
    {
        public string CRO {  get; set; }

        [InverseProperty("Dentista")]
        public ICollection<Consulta> Consultas { get; } = new List<Consulta>();

        [ForeignKey("EspecialidadeId")]
        public Especialidade? Especialidade { get; set; }

        public void SetRole()
        {
            if (this.Role == null || this.Role == "")
            {
                this.Role = "Dentista";
            }

        }
    }
}