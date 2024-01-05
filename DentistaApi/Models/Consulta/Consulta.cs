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
        public DateOnly DataConsulta { get; set; }
        public TimeOnly HoraConsulta2 { get; set; }
        public string HoraConsulta { get; set; } = "";
        public string? TempoPrevisto { get; set; }
        public DateTime? DataHoraAtendimento { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

        [ForeignKey("DentistaId")]
        public Dentista Dentista { get; set; }

        [ForeignKey("PagamentoId")]
        public Pagamento? Pagamento { get; set; }

        public void setIniciarConsulta()
        {
            if(DataHoraAtendimento != null) 
            {
                DataHoraAtendimento = DateTime.Now;
            }
        }


    }
}