using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Candidato> Candidatos { get; set; }
        public DbSet<Models.Disponibilidade> Disponibilidades { get; set; }
        public DbSet<Models.CandidatoDisponibilidade> CandidatosDisponibilidades { get; set; }
        public DbSet<Models.MelhorHorario> MelhoresHorarios { get; set; }
        public DbSet<Models.CandidatoMelhorHorario> CandidatosMelhoresHorarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Candidato>()
                .ToTable(nameof(Candidatos));

            modelBuilder.Entity<Models.CandidatoDisponibilidade>()
                .ToTable(nameof(CandidatosDisponibilidades))
                .HasKey($"{nameof(Models.Candidato)}Id", $"{nameof(Models.Disponibilidade)}Id");

            modelBuilder.Entity<Models.CandidatoDisponibilidade>()
                .HasOne(candidatoDisponibilidade => candidatoDisponibilidade.Candidato)
                .WithMany(candidato => candidato.CandidatoDisponibilidades)
                .HasConstraintName($"FK_{nameof(CandidatosDisponibilidades)}_{nameof(Candidatos)}")
                .HasForeignKey($"{nameof(Models.CandidatoDisponibilidade.Candidato)}Id");

            modelBuilder.Entity<Models.CandidatoDisponibilidade>()
                .HasOne(candidatoDisponibilidade => candidatoDisponibilidade.Disponibilidade)
                .WithMany(disponibilidades => disponibilidades.CandidatoDisponibilidades)
                .HasConstraintName($"FK_{nameof(CandidatosDisponibilidades)}_{nameof(Disponibilidades)}")
                .HasForeignKey($"{nameof(Models.CandidatoDisponibilidade.Disponibilidade)}Id");

            modelBuilder.Entity<Models.CandidatoMelhorHorario>()
                .ToTable(nameof(CandidatosMelhoresHorarios));

            modelBuilder.Entity<Models.CandidatoMelhorHorario>()
                .HasOne(candidaoMelhorHorario => candidaoMelhorHorario.Candidato)
                .WithMany(candidato => candidato.CandidatoMelhoresHorarios)
                .HasConstraintName($"FK_{nameof(CandidatosMelhoresHorarios)}_{nameof(Candidatos)}")
                .HasForeignKey($"{nameof(Models.CandidatoMelhorHorario.Candidato)}Id");

            modelBuilder.Entity<Models.CandidatoMelhorHorario>()
                .HasOne(candidaoMelhorHorario => candidaoMelhorHorario.MelhorHorario)
                .WithMany(melhorHorario => melhorHorario.CandidatoMelhoresHorarios)
                .HasConstraintName($"FK_{nameof(CandidatosMelhoresHorarios)}_{nameof(MelhoresHorarios)}")
                .HasForeignKey($"{nameof(Models.CandidatoMelhorHorario.MelhorHorario)}Id");

            modelBuilder.Entity<Models.CandidatoMelhorHorario>()
                .HasKey($"{nameof(Models.CandidatoMelhorHorario.Candidato)}Id", $"{nameof(Models.CandidatoMelhorHorario.MelhorHorario)}Id");

            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Nome)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Cpf)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.EMail)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Skype)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.LinkedIn)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Telefone)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Cidade)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Estado)).IsUnicode(false).HasColumnType("char(2)");
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Portifolio)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.InformacaoBancaria)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Titular)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Banco)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Agencia)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.Conta)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.InformacaoBancaria)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.OutrosConhecimentos)).IsUnicode(false);
            modelBuilder.Entity<Models.Candidato>()
                .Property(nameof(Models.Candidato.LinkCrud)).IsUnicode(false);

            modelBuilder.Entity<Models.Disponibilidade>()
                .ToTable(nameof(Disponibilidades));

            modelBuilder.Entity<Models.Disponibilidade>()
                .Property(nameof(Models.Disponibilidade.Descricao)).IsUnicode(false);

            modelBuilder.Entity<Models.MelhorHorario>()
                .ToTable(nameof(MelhoresHorarios));

            modelBuilder.Entity<Models.MelhorHorario>()
                .Property(nameof(Models.MelhorHorario.Descricao)).IsUnicode(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
