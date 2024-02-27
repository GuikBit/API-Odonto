using DentistaApi.Models.Dashboard;

namespace DentistaApi.Models
{
    public class Dashbords
    {    
            public List<string> Meses { get; set; } //ok
            public List<int> QtdMes { get; set; } //ok
            //paciente
            public List<int> QtdPacienteMes { get; set; } //ok
            
            //atraso
            public List<double> QtdAtrasoMes { get; set; } //ok

            //nao sei
            public List<double> NaoSei { get; set; }

            //faturamento
            public List<double> QtdFaturamentoMes { get; set; }

            //Consultas
            public List<int> QtdConsultasMes { get; set; }

            //Consultas por dentista
            public List<List<int>> QtdConsultasDentistaMes { get; set; } //ok

            //procedimentos
            public List<EspecialidadeConsultaDTO> QtdProcedimentos {  get; set; }

            //especialidade
            public List<int> QtdEspecialidade {  get; set; } //ok


            public DateTime? Inicio { get; set; }
            public DateTime? Fim { get; set; }
            public List<Dentista> DentistasList { get; set; } 
            public List<ConsultaEspecialidade> Procedimentos { get; set; }
            public List<Especialidade> Especialidades { get; set; }
        

    }
}
