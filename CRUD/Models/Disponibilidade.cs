using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Disponibilidade
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Descricao { get; set; }
        public List<CandidatoDisponibilidade> CandidatoDisponibilidades { get; set; }
    }
}
