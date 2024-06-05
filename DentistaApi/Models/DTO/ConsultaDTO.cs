using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models
{
    public class ConsultaDTO
    {

        public string Observacao { get; set; } = "";
        public string? Procedimentos { get; set; }
        public DateTime DataConsulta { get; set; }
        public string HoraConsulta { get; set; }
        public int TempoPrevisto { get; set; }
        public bool Ausente { get; set; } = false;
        public string? NomePaciente { get; set; }
        public string? Telefone { get; set; }
        public int? pacienteId { get; set; }
      // public Paciente Paciente { get; set; }

       public int dentistaId { get; set; }
       // public Dentista Dentista { get; set; }

       public int PagamentoId { get; set; }
       // public Pagamento Pagamento { get; set; }
        public int ConsultaEspecialidadeId { get; set; }
        //public ConsultaEspecialidade ConsultaEspecialidade { get; set; }


       

    }
}