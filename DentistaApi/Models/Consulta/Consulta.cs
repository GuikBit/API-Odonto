using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DentistaApi.Models
{
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string ProcedimentoConsulta { get; set; } = "";
        public DateTime DataConsulta { get; set; }
        public DateTime? DataConsultaReserva { get; set; }
        public int TempoPrevisto { get; set; }
        public DateTime? DataHoraInicioAtendimento { get; set; }
        public DateTime? DataHoraFimAtendimento { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

        [ForeignKey("DentistaId")]
        public Dentista Dentista { get; set; }

        [ForeignKey("PagamentoId")]
        public Pagamento? Pagamento { get; set; }

        
        public ConsultaEspecialidade ConsultaEspecialidade { get; set; }


        public void setIniciarConsulta()
        {
            if (DataHoraInicioAtendimento != null)
            {
                DataHoraInicioAtendimento = DateTime.Now;
            }
        }

        public void setFinalizarConsulta()
        {
            if (DataHoraFimAtendimento != null)
            {
                DataHoraFimAtendimento = DateTime.Now;
            }
        }

        public void setTempoPrevisto(int obj)
        {
            if (obj == 1)
            {
                DataConsultaReserva = DataConsulta.AddMinutes(60);
            }
            else
            {
                DataConsultaReserva = DataConsulta.AddMinutes(30);
            }

        }


    }
}