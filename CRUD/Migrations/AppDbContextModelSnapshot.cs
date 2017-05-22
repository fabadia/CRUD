using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CRUD.Data;

namespace CRUD.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRUD.Models.Candidato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Agencia")
                        .HasMaxLength(6)
                        .IsUnicode(false);

                    b.Property<string>("Banco")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Cidade")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Conta")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Cpf")
                        .HasMaxLength(14)
                        .IsUnicode(false);

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Estado")
                        .HasColumnType("char(2)")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("InformacaoBancaria")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("LinkCrud")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("LinkedIn")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("NivelAndroid");

                    b.Property<int>("NivelAngularJs");

                    b.Property<int>("NivelAspNetMvc");

                    b.Property<int>("NivelBootstrap");

                    b.Property<int?>("NivelC");

                    b.Property<int?>("NivelCPlusPlus");

                    b.Property<int?>("NivelCake");

                    b.Property<int?>("NivelCss");

                    b.Property<int?>("NivelDjango");

                    b.Property<int?>("NivelHtml");

                    b.Property<int>("NivelIOs");

                    b.Property<int?>("NivelIllustrator");

                    b.Property<int>("NivelIonic");

                    b.Property<int>("NivelJQuery");

                    b.Property<int?>("NivelJava");

                    b.Property<int?>("NivelMajento");

                    b.Property<int?>("NivelMySql");

                    b.Property<int?>("NivelPhotoshop");

                    b.Property<int>("NivelPhp");

                    b.Property<int?>("NivelPhyton");

                    b.Property<int?>("NivelRuby");

                    b.Property<int?>("NivelSalesforce");

                    b.Property<int?>("NivelSeo");

                    b.Property<int?>("NivelSqlServer");

                    b.Property<int>("NivelWordpress");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("OutrosConhecimentos")
                        .IsUnicode(false);

                    b.Property<string>("Portifolio")
                        .IsUnicode(false);

                    b.Property<decimal>("PretencaoSalarialHora");

                    b.Property<string>("Skype")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<byte?>("TipoConta");

                    b.Property<string>("Titular")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Candidatos");
                });

            modelBuilder.Entity("CRUD.Models.CandidatoDisponibilidade", b =>
                {
                    b.Property<int>("CandidatoId");

                    b.Property<int>("DisponibilidadeId");

                    b.HasKey("CandidatoId", "DisponibilidadeId");

                    b.HasIndex("DisponibilidadeId");

                    b.ToTable("CandidatosDisponibilidades");
                });

            modelBuilder.Entity("CRUD.Models.CandidatoMelhorHorario", b =>
                {
                    b.Property<int>("CandidatoId");

                    b.Property<int>("MelhorHorarioId");

                    b.HasKey("CandidatoId", "MelhorHorarioId");

                    b.HasIndex("MelhorHorarioId");

                    b.ToTable("CandidatosMelhoresHorarios");
                });

            modelBuilder.Entity("CRUD.Models.Disponibilidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Disponibilidades");
                });

            modelBuilder.Entity("CRUD.Models.MelhorHorario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("MelhoresHorarios");
                });

            modelBuilder.Entity("CRUD.Models.CandidatoDisponibilidade", b =>
                {
                    b.HasOne("CRUD.Models.Candidato", "Candidato")
                        .WithMany("CandidatoDisponibilidades")
                        .HasForeignKey("CandidatoId")
                        .HasConstraintName("FK_CandidatosDisponibilidades_Candidatos")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRUD.Models.Disponibilidade", "Disponibilidade")
                        .WithMany("CandidatoDisponibilidades")
                        .HasForeignKey("DisponibilidadeId")
                        .HasConstraintName("FK_CandidatosDisponibilidades_Disponibilidades")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRUD.Models.CandidatoMelhorHorario", b =>
                {
                    b.HasOne("CRUD.Models.Candidato", "Candidato")
                        .WithMany("CandidatoMelhoresHorarios")
                        .HasForeignKey("CandidatoId")
                        .HasConstraintName("FK_CandidatosMelhoresHorarios_Candidatos")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRUD.Models.MelhorHorario", "MelhorHorario")
                        .WithMany("CandidatoMelhoresHorarios")
                        .HasForeignKey("MelhorHorarioId")
                        .HasConstraintName("FK_CandidatosMelhoresHorarios_MelhoresHorarios")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
