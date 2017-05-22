using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRUD.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Agencia = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
                    Banco = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Cidade = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Conta = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Cpf = table.Column<string>(unicode: false, maxLength: 14, nullable: true),
                    EMail = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "char(2)", unicode: false, maxLength: 2, nullable: true),
                    InformacaoBancaria = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LinkCrud = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LinkedIn = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    NivelAndroid = table.Column<int>(nullable: false),
                    NivelAngularJs = table.Column<int>(nullable: false),
                    NivelAspNetMvc = table.Column<int>(nullable: false),
                    NivelBootstrap = table.Column<int>(nullable: false),
                    NivelC = table.Column<int>(nullable: true),
                    NivelCPlusPlus = table.Column<int>(nullable: true),
                    NivelCake = table.Column<int>(nullable: true),
                    NivelCss = table.Column<int>(nullable: true),
                    NivelDjango = table.Column<int>(nullable: true),
                    NivelHtml = table.Column<int>(nullable: true),
                    NivelIOs = table.Column<int>(nullable: false),
                    NivelIllustrator = table.Column<int>(nullable: true),
                    NivelIonic = table.Column<int>(nullable: false),
                    NivelJQuery = table.Column<int>(nullable: false),
                    NivelJava = table.Column<int>(nullable: true),
                    NivelMajento = table.Column<int>(nullable: true),
                    NivelMySql = table.Column<int>(nullable: true),
                    NivelPhotoshop = table.Column<int>(nullable: true),
                    NivelPhp = table.Column<int>(nullable: false),
                    NivelPhyton = table.Column<int>(nullable: true),
                    NivelRuby = table.Column<int>(nullable: true),
                    NivelSalesforce = table.Column<int>(nullable: true),
                    NivelSeo = table.Column<int>(nullable: true),
                    NivelSqlServer = table.Column<int>(nullable: true),
                    NivelWordpress = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    OutrosConhecimentos = table.Column<string>(unicode: false, nullable: true),
                    Portifolio = table.Column<string>(unicode: false, nullable: true),
                    PretencaoSalarialHora = table.Column<decimal>(nullable: false),
                    Skype = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telefone = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    TipoConta = table.Column<byte>(nullable: true),
                    Titular = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disponibilidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibilidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MelhoresHorarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MelhoresHorarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidatosDisponibilidades",
                columns: table => new
                {
                    CandidatoId = table.Column<int>(nullable: false),
                    DisponibilidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatosDisponibilidades", x => new { x.CandidatoId, x.DisponibilidadeId });
                    table.ForeignKey(
                        name: "FK_CandidatosDisponibilidades_Candidatos",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidatosDisponibilidades_Disponibilidades",
                        column: x => x.DisponibilidadeId,
                        principalTable: "Disponibilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidatosMelhoresHorarios",
                columns: table => new
                {
                    CandidatoId = table.Column<int>(nullable: false),
                    MelhorHorarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatosMelhoresHorarios", x => new { x.CandidatoId, x.MelhorHorarioId });
                    table.ForeignKey(
                        name: "FK_CandidatosMelhoresHorarios_Candidatos",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidatosMelhoresHorarios_MelhoresHorarios",
                        column: x => x.MelhorHorarioId,
                        principalTable: "MelhoresHorarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatosDisponibilidades_DisponibilidadeId",
                table: "CandidatosDisponibilidades",
                column: "DisponibilidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatosMelhoresHorarios_MelhorHorarioId",
                table: "CandidatosMelhoresHorarios",
                column: "MelhorHorarioId");

            migrationBuilder.Sql(@"
insert into Disponibilidades values
    ('Up to 4 hours per day / Até 4 horas por dia'),
    ('4 to 6 hours per day / De 4 á 6 horas por dia'),
    ('6 to 8 hours per day /De 6 á 8 horas por dia'),
    ('Up to 8 hours a day (are you sure?) / Acima de 8 horas por dia (tem certeza?)'),
    ('Only weekends / Apenas finais de semana')

insert into MelhoresHorarios values
    ('Morning(from 08:00 to 12:00) / Manhã(de 08:00 ás 12:00)'),
    ('Afternoon (from 1:00 p.m. to 6:00 p.m.) / Tarde (de 13:00 ás 18:00)'),
    ('Night (7:00 p.m. to 10:00 p.m.) /Noite (de 19:00 as 22:00)'),
    ('Dawn (from 10 p.m. onwards) / Madrugada (de 22:00 em diante)'),
    ('Business (from 08:00 a.m. to 06:00 p.m.) / Comercial (de 08:00 as 18:00)')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatosDisponibilidades");

            migrationBuilder.DropTable(
                name: "CandidatosMelhoresHorarios");

            migrationBuilder.DropTable(
                name: "Disponibilidades");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "MelhoresHorarios");
        }
    }
}
