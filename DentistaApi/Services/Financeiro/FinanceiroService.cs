using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DentistaApi.Services.Financeiro
{
    public class FinanceiroService
    {
        private readonly AppDbContext db = new();


        public List<ContasReceber> GetContasReceber() 
        {
            var consultas = db.Consultas.Include(x=> x.Pagamento.Parcelas).ToList();

            List<ContasReceber> lista = new List<ContasReceber>(); ;

            foreach (var item in consultas)
            {
                if (!item.Pagamento.Pago) { 
                    foreach (var parcela in item.Pagamento.Parcelas)
                    {
                        if (parcela.DataVencimento > DateTime.Now)
                        {
                            ContasReceber i = new ContasReceber();

                            i.Parcela = parcela;
                            i.IdConsulta = item.Id;
                            i.DataConsulta = item.DataConsulta;
                            i.NomeDentista = item.Dentista.Nome;
                            i.NomePaciente = item.Paciente.Nome;
                            
                            lista.Add(i);
                        }
                    }
                        
                }
            }

            return lista;
        }
    }
}
