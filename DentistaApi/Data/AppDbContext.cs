using DentistaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



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
        public DbSet<Parcela> Parcela { get; set; }
        public DbSet<Anamnese> Anamnese { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<ConsultaEspecialidade> ConsultaEspecialidades { get; set; }
        public DbSet<Organizacao> Organizacao { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        //public DbSet<Produto> Produtos { get; set; }
        //public DbSet<Categoria> Categorias { get; set; }
        //public DbSet<Fornecedor> Fornecedores { get; set; }
        //public DbSet<Pedido> Pedidos { get; set; }
        //public DbSet<DetalhePedido> DetalhePedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Organizacao>()
            //    .HasMany(o => o.Funcionarios)
            //    .WithOne(f => f.IdOrganizacao)
            //    .HasForeignKey(f => f.OrganizacaoId);

            //modelBuilder.Entity<Organizacao>()
            //    .HasMany(o => o.Dentistas)
            //    .WithOne(d => d.IdOrganizacao)
            //    .HasForeignKey(d => d.OrganizacaoId);

            //modelBuilder.Entity<Organizacao>()
            //    .HasMany(o => o.Cargos)
            //    .WithOne(c => c.OrganizacaoId)
            //    .HasForeignKey(c => c.OrganizacaoId);

            //modelBuilder.Entity<Organizacao>()
            //    .HasMany(o => o.Pacientes)
            //    .WithOne(p => p.IdOrganizacao)
            //    .HasForeignKey(p => p.OrganizacaoId);


            modelBuilder.Entity<Organizacao>()
                 .HasOne(o => o.Endereco)
                 .WithMany()
                 .HasForeignKey(o => o.EnderecoId);

            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.cargo)
                .WithMany()
                .HasForeignKey(f => f.IdCargo);

            //modelBuilder.Entity<Dentista>()
            //    .HasOne(d => d.Cargo)
            //    .WithMany()
            //    .HasForeignKey(d => d.CargoId);

            modelBuilder.Entity<Dentista>()
                .HasOne(d => d.Especialidade)
                .WithMany()
                .HasForeignKey(d => d.EspecialidadeId);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Dentista)
                .WithMany(d => d.Consultas)
                .HasForeignKey(c => c.DentistaId);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Pagamento)
                .WithMany()
                .HasForeignKey(c => c.PagamentoId);

            //modelBuilder.Entity<Administrador>()
            //   .HasOne(c => c.IdOrganizacao)
            //   .WithMany()
            //   .HasForeignKey(c => c.OrganizacaoId);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.ConsultaEspecialidade)
                .WithMany()
                .HasForeignKey(c => c.ConsultaEspecialidadeId);

            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Endereco)
                .WithMany()
                .HasForeignKey(p => p.EnderecoId);

            
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Anamnese)
                .WithMany()
                .HasForeignKey(p => p.AnamneseId);
            
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Responsavel)
                .WithMany()
                .HasForeignKey(p => p.ResponsavelId);

            modelBuilder.Entity<DetalhePedido>()
                .HasKey(dp => new { dp.PedidoId, dp.ProdutoId });

            modelBuilder.Entity<DetalhePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.DetalhesPedido)
                .HasForeignKey(dp => dp.PedidoId);



            //modelBuilder.Entity<DetalhePedido>()
            //    .HasOne(dp => dp.Produto)
            //    .WithMany(p => p.DetalhesPedido)
            //    .HasForeignKey(dp => dp.ProdutoId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost:3306;uid=root;pwd=root;database=dbDentista");
            // optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
            optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=dentistabanco");


        }
    }
}