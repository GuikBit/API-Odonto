using System.ComponentModel.DataAnnotations.Schema;


namespace DentistaApi.Models
{
    public class Paciente : User
    {
        public string? NumPasta { get; set; }

        public int EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        public int AnamneseId { get; set; }
        public Anamnese? Anamnese { get; set; }

        public int ResponsavelId { get; set; }
        public Responsavel? Responsavel { get; set; }

        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

        public string? FotoPerfil { get; set; }


        public void SetRole()
        {
            if (string.IsNullOrEmpty(Role))
            {
                Role = "PACIENTE";
            }

        }

    }
}