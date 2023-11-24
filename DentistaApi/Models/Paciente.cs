using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DentistaApi.Models
{
    public class Paciente : User
    {

        public Endereco? Endereco { get; set; }

        public Anamnese? Anamnese { get; set; }

        public Responsavel? Responsavel { get; set; }

        [InverseProperty("Paciente")]
        public ICollection<Consulta> Consultas { get; } = new List<Consulta>();


        public void SetRole()
        {
            if (this.Role == null || this.Role == "")
            {
                this.Role = "Paciente";
            }

        }

    }
}