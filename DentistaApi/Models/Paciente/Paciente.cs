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
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

        public string? FotoPerfil { get; set; }


        public void SetRole()
        {
            if (Role == null || Role == "")
            {
                Role = "Paciente";
            }

        }

    }
}