using System.ComponentModel.DataAnnotations.Schema;


namespace DentistaApi.Models
{
    public class Paciente : User
    {
        public string? NumPasta { get; set; }

        [ForeignKey("PacienteId")]
        public Endereco? Endereco { get; set; }

        [ForeignKey("PacienteId")]
        public Anamnese? Anamnese { get; set; }

        [ForeignKey("PacienteId")]
        public Responsavel? Responsavel { get; set; }

        [InverseProperty("Paciente")]
        public ICollection<Consulta> Consultas { get; } = new List<Consulta>();


        public void SetRole()
        {
            if (Role == null || Role == "")
            {
                Role = "Paciente";
            }

        }

    }
}