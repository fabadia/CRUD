using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class CandidatoMelhorHorario
    {
        public int CandidatoId { get; set; }
        public int MelhorHorarioId { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public Candidato Candidato { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public MelhorHorario MelhorHorario { get; set; }
    }
}
