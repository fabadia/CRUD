using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class MelhorHorario
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Descricao { get; set; }
        public List<CandidatoMelhorHorario> CandidatoMelhoresHorarios { get; set; }
    }
}
