using System.ComponentModel.DataAnnotations.Schema;

namespace DentistaApi.Models
{
    public class Dentista : User
    {

        [InverseProperty("Dentista")]
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

        [ForeignKey("EspecialidadeId")]
        public Especialidade? Especialidade { get; set; }

        public string CorDentista { get; set; } = "";

        public void SetRole()
        {
            if (Role == null || Role == "")
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
