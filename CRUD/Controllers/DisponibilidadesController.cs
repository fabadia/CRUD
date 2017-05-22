using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD.Data;
using CRUD.Models;

namespace CRUD.Controllers
{
    [Produces("application/json")]
    [Route("api/Disponibilidades")]
    public class DisponibilidadesController : Controller
    {
        private readonly AppDbContext _context;

        public DisponibilidadesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Vagas
        [HttpGet]
        public IEnumerable<Disponibilidade> GetDisponibilidades()
        {
            return _context.Disponibilidades;
        }
    }
}