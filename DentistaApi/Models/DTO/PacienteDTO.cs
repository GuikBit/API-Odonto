using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DentistaApi.Models
{
    public class PacienteDTO : User
    {
        public string? NumPasta { get; set; }

        public EnderecoDTO? Endereco { get; set; }

        public AnamneseDTO? Anamnese { get; set; }

        public ResponsavelDTO? Responsavel { get; set; }

    }
}