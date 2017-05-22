using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        [MaxLength(100), Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }
        [MaxLength(14), Display(Name ="CPF"), RegularExpression(@"\d{3}\.\d{3}\.\d{3}-\d{2}")]
        public string Cpf { get; set; }
        [MaxLength(100), Required, EmailAddress, Display(Name = "e-Mail")]
        public string EMail { get; set; }
        [MaxLength(50)]
        public string Skype { get; set; }
        [MaxLength(50)]
        public string LinkedIn { get; set; }
        [MaxLength(20)]
        public string Telefone { get; set; }
        [MaxLength(50)]
        public string Cidade { get; set; }
        [MaxLength(2)]
        public string Estado { get; set; }
        [Display(Name = "Pretenção Salarial")]
        public decimal PretencaoSalarialHora { get; set; }
        public int NivelIonic { get; set; }
        public int NivelAndroid { get; set; }
        [Display(Name = "IOS")]
        public int NivelIOs { get; set; }
        [Display(Name = "HTML")]
        public int? NivelHtml { get; set; }
        [Display(Name = "CSS")]
        public int? NivelCss { get; set; }
        public int NivelBootstrap { get; set; }
        [Display(Name = "jQuery")]
        public int NivelJQuery { get; set; }
        [Display(Name = "AngularJS")]
        public int NivelAngularJs { get; set; }
        public int? NivelJava { get; set; }
        [Display(Name = "Asp.Net MVC")]
        public int NivelAspNetMvc { get; set; }
        public int? NivelC { get; set; }
        [Display(Name = "C++")]
        public int? NivelCPlusPlus { get; set; }
        public int? NivelCake { get; set; }
        public int? NivelDjango { get; set; }
        public int? NivelMajento { get; set; }
        [Display(Name = "PHP")]
        public int NivelPhp { get; set; }
        public int NivelWordpress { get; set; }
        public int? NivelPhyton { get; set; }
        public int? NivelRuby { get; set; }
        [Display(Name = "SQL Server")]
        public int? NivelSqlServer { get; set; }
        [Display(Name = "MySQL")]
        public int? NivelMySql { get; set; }
        public int? NivelSalesforce { get; set; }
        public int? NivelPhotoshop { get; set; }
        public int? NivelIllustrator { get; set; }
        [Display(Name = "SEO")]
        public int? NivelSeo { get; set; }
        public string Portifolio { get; set; }
        [MinLength(1), Display(Name = "Disponibilidade")]
        public List<CandidatoDisponibilidade> CandidatoDisponibilidades { get; set; }
        [MinLength(1), Display(Name = "Melhor horário")]
        public List<CandidatoMelhorHorario> CandidatoMelhoresHorarios { get; set; }
        [MaxLength(50), Display(Name = "Informação bancária")]
        public string InformacaoBancaria { get; set; }
        [MaxLength(100)]
        public string Titular { get; set; }
        [MaxLength(50)]
        public string Banco { get; set; }
        [MaxLength(6), Display(Name = "Agência")]
        public string Agencia { get; set; }
        [Display(Name = "Tipo de conta")]
        public byte? TipoConta { get; set; }
        [MaxLength(20)]
        public string Conta { get; set; }
        public string OutrosConhecimentos { get; set; }
        [MaxLength(100)]
        public string LinkCrud { get; set; }
    }
}
