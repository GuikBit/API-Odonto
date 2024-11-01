using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace DentistaApi.Controllers;
[Authorize]
[ApiController]
[Route("v1/[controller]")]
public class ConsultaController : ControllerBase
{
    public class ProcedimentoRequest
    {
        public int Id { get; set; }
        public string Procedimentos { get; set; }
    }

    [HttpGet]
    public ActionResult<IList<Consulta>> Get()
    {
        var consultas = db.Consultas.Include(p => p.Dentista)
                                    .Include(p => p.Paciente)
                                    .Include(p => p.Pagamento)
                                    .Include(p => p.Dentista.Especialidade)
                                    .Include(p=> p.ConsultaEspecialidade)
                                    .OrderBy(p => p.DataConsulta)
                                    .ToList();


        return Ok(ajustaConsultas(consultas));
    }
    private IList<Consulta> ajustaConsultas(List<Consulta> consultas)
    {
        foreach (var consulta in consultas)
        {
            if (consulta.NomePaciente == null)
            {
                consulta.Paciente.Consultas.Clear();
            }

            consulta.Dentista.Consultas.Clear();
        }

        return consultas;
    }

    //private IList<ConsultaDTO> ajustaConsultas(List<Consulta> consultas)
    //{
    //    consultas.ToList().ForEach(p => p.Paciente.Consultas.Clear());
    //    consultas.ToList().ForEach(p => p.Dentista.Consultas.Clear());

    //    IList<ConsultaDTO> lista = new List<ConsultaDTO>();
    //    foreach (var consulta in consultas)
    //    {
    //        ConsultaDTO c = new ConsultaDTO();
    //        c.Id = consulta.Id;

    //        c.ProcedimentoConsulta = consulta.ProcedimentoConsulta;
    //        c.Dentista = consulta.Dentista;
    //        c.DataConsulta = consulta.DataConsulta;
    //        c.HoraConsulta = consulta.HoraConsulta;
    //        c.Paciente = consulta.Paciente;
    //        c.Pagamento = consulta.Pagamento;

    //        lista.Add(c);
    //    }

    //    return lista;
    //}


    //[HttpGet]
    //[Route("/v1/consultas")]
    //public ActionResult<IList<Consulta>> GetConsultasWeb([FromQuery] int page, [FromQuery] int size)
    //{

    //    int indiceInicial = (page - 1) * size;


    //    var consultasDaPagina = db.Consultas
    //        .Include(p => p.Dentista)
    //        .Include(p => p.Paciente)
    //        .Include(p => p.Pagamento)
    //        .Include(p => p.Dentista.Especialidade)
    //        .Skip(indiceInicial)
    //        .Take(size)
    //        .OrderBy(x => x.DataConsulta)
    //        .ToList();


    //    return consultasDaPagina == null ? NotFound() : Ok(ajustaConsultas(consultasDaPagina));

    //}
    //[HttpGet]
    //[Route("/v1/consultas/total")]
    //public ActionResult<int> getTotalConsultas()
    //{

    //    int total = db.Consultas.Count();

    //    return Ok(total);
    //}

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Consulta> GetById(int id)
    {

        var consulta = db.Consultas
            .Include(p => p.Dentista)
            .Include(p => p.Pagamento)
            .Include(p => p.Pagamento.Parcelas)
            .Include(p => p.ConsultaEspecialidade)
            .FirstOrDefault(p => p.Id == id);

        consulta.Dentista.Consultas.Clear();

        if (consulta != null && consulta.NomePaciente == null)
        {
            var consultaP = db.Consultas
            .Include(p => p.Dentista)
            .Include(p => p.Pagamento)
            .Include(p => p.Paciente)
            .Include(p => p.Pagamento.Parcelas)
            .Include(p => p.ConsultaEspecialidade)
            .FirstOrDefault(p => p.Id == id);
            
            consulta.Paciente.Consultas.Clear();
            
            consulta = consultaP;
        }

        return consulta == null ? NotFound() : Ok(consulta);
    }

    [HttpPost]
    public ActionResult<Consulta> Post(ConsultaDTO obj)
    {

        try
        {
            //Random random = new Random();

            //int randDentista = random.Next(4, 9);

            //int randPaciente = random.Next(10, 26);

            //int randonDia = random.Next(1, 30);

            //int randHora = random.Next(8, 18);

            //int randTempo = random.Next(1, 4);

            Consulta nova = new Consulta();

            if (obj.pacienteId != null)
            {
                var paciente = db.Pacientes.First(x => x.Id == obj.pacienteId);
                nova.Paciente = paciente;
            }
            else
            {
                nova.NomePaciente = obj.NomePaciente;
                nova.Telefone = obj.Telefone;
            }

            var dentista = db.Dentistas.First(x => x.Id == obj.dentistaId);            
            var consEspec = db.ConsultaEspecialidades.First(x => x.Id == obj.ConsultaEspecialidadeId);

            //Consulta nova = new Consulta();
            
            nova.Dentista = dentista;
            nova.ConsultaEspecialidade = consEspec;

            nova.Observacao = obj.Observacao;
            nova.Procedimentos = obj.Procedimentos;
            //DateTime dateTime = DateTime.Parse("2024-01-"+ randonDia.ToString() +"T11:00:00.000Z"); randHora.ToString()+":00"

            nova.DataConsulta = ajustaDataConsulta(obj.DataConsulta, obj.HoraConsulta);

            nova.TempoPrevisto = obj.TempoPrevisto;
            nova.setTempoPrevisto(obj.TempoPrevisto);
            nova.CorDentista = dentista.CorDentista;
            nova.setStatus(3);

            nova.Pagamento = new Pagamento();
            Parcela novo = new Parcela();

            novo.ValorParcela = nova.ConsultaEspecialidade.ValorBase;
            DateTime date = nova.DataConsulta;
            novo.DataVencimento = date.AddDays(7);
            nova.Pagamento.Parcelas.Add(novo);

            db.Consultas.Add(nova);
            db.SaveChanges();

            return Ok();

        }
        catch (Exception)
        {
            return BadRequest("Erro ao salvar ao consulta.");
        }

        

    }

    private DateTime ajustaDataConsulta(DateTime dataConsulta, string horaConsulta)
    {
        try
        {
            if (dataConsulta != null && horaConsulta != null)
            {
                string[] partes = horaConsulta.Split(':');

                return new DateTime(dataConsulta.Year, dataConsulta.Month, dataConsulta.Day, int.Parse(partes[0]), int.Parse(partes[1]), 0); ;
            }
            else
            {
                return new DateTime(0, 0, 0, 0, 0, 0);
            }
        }
        catch (Exception ex)
        {
            return new DateTime(0,0,0,0,0,0);
        }
    }

    [HttpGet]
    [Route("/v1/consulta/especialidade")]
    public ActionResult<ConsultaEspecialidade> GetEspecConsulta()
    {
        try { 
            var lista = db.ConsultaEspecialidades.ToList();

            return lista == null ? NotFound() : Ok(lista);

        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost]
    [Route("/v1/consulta/especialidade")]
    public ActionResult<ConsultaEspecialidade> PostEspecConsulta(ConsultaEspecialidade obj)
    {
        try
        {
            if(obj != null )
            {
                ConsultaEspecialidade novo = new ConsultaEspecialidade();
                novo.Tipo = obj.Tipo;
                novo.Descricao = obj.Descricao;  
                novo.ValorBase = obj.ValorBase;               

                db.ConsultaEspecialidades.Add(novo);
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
        catch (Exception e)
        {
            return BadRequest();
        }
        
    }
    [HttpPut]
    [Route("/v1/consulta/especialidade")]
    public ActionResult PutEspecConsulta(ConsultaEspecialidade obj)
    {
        try
        {
            var let = db.ConsultaEspecialidades.FirstOrDefault(x => x.Id == obj.Id);
            if (let == null)
            {
                return NotFound();
            }
            let.DataUpdade = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            db.ConsultaEspecialidades.Update(let);
            db.SaveChanges();
            //db.Entry(let).CurrentValues.SetValues(obj);
            //db.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao atualizar a especialidade: {e.Message}");
        }
    }

    [HttpDelete]
    [Route("/v1/consulta/especialidade/{id}")]
    public ActionResult DeleteEspecConsulta(int id)
    {
        try
        {
            var especialidade = db.ConsultaEspecialidades.FirstOrDefault(x => x.Id == id);
            if (especialidade == null)
            {
                return NotFound();
            }

            db.Remove(especialidade);
            db.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao deletar a especialidade: {e.Message}");
        }
    }


    [HttpPatch]
    [Route("procedimento")]
    public ActionResult PostProcedimento([FromBody] ProcedimentoRequest request)
    {
        try
        {
            var consulta = db.Consultas.FirstOrDefault(c => c.Id == request.Id);
            if (consulta == null)
            {
                return BadRequest("Consulta não encontrada.");
            }
            consulta.Procedimentos = request.Procedimentos;
            consulta.setFinalizarConsulta();
            db.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id,  PutConsultaDTO obj)
    {
        try
        {
            var consulta = db.Consultas.FirstOrDefault(c => c.Id == id);
            if(consulta == null)
            {
                return BadRequest();
            }
            consulta.DataConsulta = ajustaDataConsulta(obj.DataConsulta, obj.HoraConsulta);
            consulta.setTempoPrevisto(consulta.TempoPrevisto);
            db.SaveChanges();

            return Ok();
        }
        catch (Exception e )
        {
            return BadRequest("Houve um erro ao editar a consulta");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (db.Consultas == null)
            return NotFound();

        var obj = db.Consultas.FirstOrDefault(x => x.Id == id);

        if (obj == null)
            return NotFound();

        db.Consultas.Remove(obj);
        db.SaveChanges();

        return NoContent();
    }

    [HttpPatch]
    [Route("/v1/consulta/iniciar/{id}")]
    public ActionResult<Consulta> IniciarConsulta( int id )
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }
        consulta.setStatus(4);
        consulta.setIniciarConsulta();        
        db.SaveChanges();

        consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        return Ok(consulta);
    }

    [HttpPatch]
    [Route("/v1/consulta/finalizar/{id}")]
    public ActionResult FinalizarConsulta(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }
        consulta.setStatus(5);
        consulta.setFinalizarConsulta();
        db.SaveChanges();

        return Ok();
    }
    [HttpPatch]
    [Route("/v1/consulta/aguardando/{id}")]
    public ActionResult AguardandoConsulta(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }
        consulta.setStatus(1);
        consulta.setFinalizarConsulta();
        db.SaveChanges();

        return Ok();
    }
    [HttpPatch]
    [Route("/v1/consulta/ausentar/{id}")]
    public ActionResult AusentarPaciente(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }
        consulta.setStatus(6);
        consulta.setAusentarPaciente();
        db.SaveChanges();

        return Ok();
    }

    [HttpPatch]
    [Route("/v1/consulta/presenca/{id}")]
    public ActionResult PresencaPaciente(int id)
    {
        if (id == 0 || id == null)
        {
            return BadRequest();
        }
        var consulta = db.Consultas.FirstOrDefault(x => x.Id.Equals(id));

        if (consulta == null)
        {
            return BadRequest();
        }
        consulta.setStatus(1);
        consulta.setPresencaPaciente();
        db.SaveChanges();

        return Ok();
    }
    
    [HttpPost]
    [Route("/v1/consulta/salvarPagamento")]
    public ActionResult salvarPagamentoConsulta(Consulta obj)
    {
        if(obj == null)
        {
            return BadRequest();
        }
        var pagamento = db.Pagamentos.Include(x => x.Parcelas).FirstOrDefault(x => x.Id == obj.Pagamento.Id);

        if (pagamento == null)
        {
            return BadRequest();
        }

        if(obj.Pagamento.Parcelas.ToList().Count > pagamento.Parcelas.ToList().Count) {

            foreach (var item in obj.Pagamento.Parcelas)
            {
                if(item.Id != 0)
                {
                    var parcela = db.Parcela.FirstOrDefault(x => x.Id == item.Id);
                    if (parcela != null) { 
                        parcela.ValorParcela = item.ValorParcela;
                        parcela.DataPagamento = item.DataPagamento;
                        parcela.EhEntrada = item.EhEntrada;
                        parcela.Pago = item.Pago;
                        parcela.DataVencimento = item.DataVencimento;
                        parcela.FormaDePagamento = item.FormaDePagamento;

                        //pagamento.FatFechado = true;

                        db.SaveChanges();
                    }
                }
                else if(item.Id == 0)
                {
                    Parcela novo = new Parcela();
                    novo.FormaDePagamento = item.FormaDePagamento;
                    novo.DataPagamento = item.DataPagamento;
                    novo.Pago = item.Pago;
                    novo.EhEntrada = item.EhEntrada == true ? true : false;
                    novo.ValorParcela = item.ValorParcela;
                    novo.DataPagamento = item.DataPagamento;
                    novo.DataVencimento = item.DataVencimento;
                    novo.FormaDePagamento = item.FormaDePagamento;

                    pagamento.Parcelas.Add(novo);
                }
            }
            pagamento.FatFechado = true;
            pagamento.Acrecimo = obj.Pagamento.Acrecimo;
            pagamento.Desconto = obj.Pagamento.Desconto;
            pagamento.qtdParcela = pagamento.Parcelas.Count();
            pagamento.ValorTotal = pagamento.Parcelas.Sum(x => x.ValorParcela);
        }
        
        

        db.SaveChanges();
        
        

        return Ok();
    }
   

    
    private readonly AppDbContext db = new();


}
