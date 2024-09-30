using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentistaApi.Models;

public class Consulta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    public string Observacao { get; set; } = "";
    public string? Procedimentos { get; set; }
    public DateTime DataConsulta { get; set; }
    public DateTime? DataConsultaReserva { get; set; }
    public int TempoPrevisto { get; set; }
    public DateTime? DataHoraInicioAtendimento { get; set; }
    public DateTime? DataHoraFimAtendimento { get; set; }
    public bool Ausente { get; set; } = false;
    public int? PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
    public string? NomePaciente { get; set; }
    public string? Telefone { get; set; }
    public int? DentistaId { get; set; }
    public Dentista Dentista { get; set; }
    public int? PagamentoId { get; set; }
    public Pagamento Pagamento { get; set; }
    public string CorDentista { get; set; }
    public int? ConsultaEspecialidadeId { get; set; }
    public ConsultaEspecialidade ConsultaEspecialidade { get; set; }


    public void setIniciarConsulta()
    {
        if (DataHoraInicioAtendimento == null)
        {
            DataHoraInicioAtendimento = DateTime.Now;
        }
    }

    public void setFinalizarConsulta()
    {
        if (DataHoraFimAtendimento == null)
        {
            DataHoraFimAtendimento = DateTime.Now;
        }
    }

    public void setTempoPrevisto(int obj)
    {
        var data = DataConsulta;

        switch (obj)
        {
            case 1: { DataConsultaReserva = data.AddMinutes(15); break; }
            case 2: { DataConsultaReserva = data.AddMinutes(30); break; }
            case 3: { DataConsultaReserva = data.AddMinutes(45); break; }
            case 4: { DataConsultaReserva = data.AddMinutes(60); break; }
        }

    }

    public void setAusentarPaciente()
    {
        Ausente = true;
    }

    public void setPresencaPaciente()
    {
        Ausente = false;
    }
}

//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DentistaApi.Models
//{
//    public class Consulta
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int? Id { get; set; }

//        public string Observacao { get; set; } = "";
//        public string? Procedimentos { get; set; }
//        public DateTime DataConsulta { get; set; }
//        public DateTime? DataConsultaReserva { get; set; }
//        public int TempoPrevisto { get; set; }
//        public DateTime? DataHoraInicioAtendimento { get; set; }
//        public DateTime? DataHoraFimAtendimento { get; set; }
//        public bool Ausente { get; set; } = false;
//        public int? PacienteId { get; set; }
//        public Paciente? Paciente { get; set; }
//        public string? NomePaciente { get; set; }
//        public string? Telefone { get; set; }
//        public int? DentistaId { get; set; }
//        public Dentista Dentista { get; set; }
//        public int? PagamentoId { get; set; }
//        public Pagamento Pagamento { get; set; }
//        public string CorDentista { get; set; }
//        public int? ConsultaEspecialidadeId { get; set; }
//        public ConsultaEspecialidade ConsultaEspecialidade { get; set; }

//        // Método para iniciar a consulta
//        public void IniciarConsulta()
//        {
//            if (DataHoraInicioAtendimento == null)
//            {
//                DataHoraInicioAtendimento = DateTime.Now;
//            }
//        }

//        // Método para finalizar a consulta
//        public void FinalizarConsulta()
//        {
//            if (DataHoraFimAtendimento == null)
//            {
//                DataHoraFimAtendimento = DateTime.Now;
//            }
//        }

//        // Método para definir o tempo previsto com base em um valor
//        public void DefinirTempoPrevisto(int tipo)
//        {
//            switch (tipo)
//            {
//                case 1:
//                    TempoPrevisto = 15;
//                    break;
//                case 2:
//                    TempoPrevisto = 30;
//                    break;
//                case 3:
//                    TempoPrevisto = 45;
//                    break;
//                case 4:
//                    TempoPrevisto = 60;
//                    break;
//                default:
//                    TempoPrevisto = 30; // Valor padrão
//                    break;
//            }

//            DataConsultaReserva = DataConsulta.AddMinutes(TempoPrevisto);
//        }

//        // Método para marcar o paciente como ausente
//        public void MarcarPacienteAusente()
//        {
//            Ausente = true;
//        }

//        // Método para marcar a presença do paciente
//        public void MarcarPresencaPaciente()
//        {
//            Ausente = false;
//        }

//        // Método para calcular a duração da consulta
//        public int CalcularDuracaoConsulta()
//        {
//            if (DataHoraInicioAtendimento.HasValue && DataHoraFimAtendimento.HasValue)
//            {
//                return (int)(DataHoraFimAtendimento.Value - DataHoraInicioAtendimento.Value).TotalMinutes;
//            }
//            return 0;
//        }

//        // Método para validar a consulta
//        public bool ValidarConsulta()
//        {
//            return DentistaId.HasValue && PacienteId.HasValue && DataConsulta > DateTime.Now;
//        }

//        // Método para atualizar a observação
//        public void AtualizarObservacao(string novaObservacao)
//        {
//            if (!string.IsNullOrEmpty(novaObservacao))
//            {
//                Observacao = novaObservacao;
//            }
//        }

//        // Método para definir o dentista e sua cor
//        public void DefinirDentista(int dentistaId, string corDentista)
//        {
//            DentistaId = dentistaId;
//            CorDentista = corDentista;
//        }
//    }
//}
