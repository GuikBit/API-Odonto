using System.ComponentModel.DataAnnotations.Schema;

namespace DentistaApi.Models
{
    public class Dentista : User
    {

        public ICollection<Consulta>? Consultas { get; set; } = new List<Consulta>();

        public int? EspecialidadeId { get; set; }
        public Especialidade? Especialidade { get; set; }
        public int? CargoId { get; set; }
        public Cargo? Cargo { get; set; }

        public string CorDentista { get; set; } = "";

        public void SetRole()
        {
            if (string.IsNullOrEmpty(Role))
            {
                Role = "Dentista";
            }

        }
        //public void SetConsulta(List<Consulta> lista)
        //{
        //    con
        //}
    }
}
