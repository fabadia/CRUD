using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class CandidatoDisponibilidade
    {
        public int CandidatoId { get; set; }
        public int DisponibilidadeId { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public Candidato Candidato { get; set; }
        [System.Runtime.Serialization.IgnoreDataMember]
        public Disponibilidade Disponibilidade { get; set; }
    }
}
