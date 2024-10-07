using System.ComponentModel.DataAnnotations.Schema;

namespace DentistaApi.Models
{
    public class Dentista : User
    {

        public ICollection<Consulta>? Consultas { get; set; } = new List<Consulta>();
        public string CRO { get; set; }
        public int? EspecialidadeId { get; set; }
        public Especialidade? Especialidade { get; set; }
        public string CorDentista { get; set; } = "";

        public void SetRole()
        {
            if (string.IsNullOrEmpty(Role))
            {
                Role = "Dentista";
            }

        }

    }
}
