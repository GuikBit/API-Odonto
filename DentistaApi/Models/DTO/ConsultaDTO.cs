using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models
{
    public class ConsultaDTO
    {
        public int? Id { get; set; }
        public string Observacao { get; set; } = "";
        public string? Procedimentos { get; set; } = "";
        public DateTime DataConsulta { get; set; }
        public string HoraConsulta { get; set; }
        public DateTime? DataConsultaReserva { get; set; }
        public int TempoPrevisto { get; set; }
        public DateTime? DataHoraInicioAtendimento { get; set; }
        public DateTime? DataHoraFimAtendimento { get; set; }
        public Boolean Ausente { get; set; } = false;       
        public Paciente Paciente { get; set; }        
        public Dentista Dentista { get; set; }        
        public Pagamento? Pagamento { get; set; }
        public ConsultaEspecialidade ConsultaEspecialidade { get; set; }


    }
}