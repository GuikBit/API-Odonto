using DentistaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DentistaApi.Models;


namespace DentistaApi.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Anamnese> Anamneses { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<ConsultaEspecialidade> ConsultaEspecialidades { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost:3306;uid=root;pwd=root;database=dbDentista");
             optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}