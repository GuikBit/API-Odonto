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

//        // M�todo para iniciar a consulta
//        public void IniciarConsulta()
//        {
//            if (DataHoraInicioAtendimento == null)
//            {
//                DataHoraInicioAtendimento = DateTime.Now;
//            }
//        }

//        // M�todo para finalizar a consulta
//        public void FinalizarConsulta()
//        {
//            if (DataHoraFimAtendimento == null)
//            {
//                DataHoraFimAtendimento = DateTime.Now;
//            }
//        }

//        // M�todo para definir o tempo previsto com base em um valor
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
//                    TempoPrevisto = 30; // Valor padr�o
//                    break;
//            }

//            DataConsultaReserva = DataConsulta.AddMinutes(TempoPrevisto);
//        }

//        // M�todo para marcar o paciente como ausente
//        public void MarcarPacienteAusente()
//        {
//            Ausente = true;
//        }

//        // M�todo para marcar a presen�a do paciente
//        public void MarcarPresencaPaciente()
//        {
//            Ausente = false;
//        }

//        // M�todo para calcular a dura��o da consulta
//        public int CalcularDuracaoConsulta()
//        {
//            if (DataHoraInicioAtendimento.HasValue && DataHoraFimAtendimento.HasValue)
//            {
//                return (int)(DataHoraFimAtendimento.Value - DataHoraInicioAtendimento.Value).TotalMinutes;
//            }
//            return 0;
//        }

//        // M�todo para validar a consulta
//        public bool ValidarConsulta()
//        {
//            return DentistaId.HasValue && PacienteId.HasValue && DataConsulta > DateTime.Now;
//        }

//        // M�todo para atualizar a observa��o
//        public void AtualizarObservacao(string novaObservacao)
//        {
//            if (!string.IsNullOrEmpty(novaObservacao))
//            {
//                Observacao = novaObservacao;
//            }
//        }

//        // M�todo para definir o dentista e sua cor
//        public void DefinirDentista(int dentistaId, string corDentista)
//        {
//            DentistaId = dentistaId;
//            CorDentista = corDentista;
//        }
//    }
//}
