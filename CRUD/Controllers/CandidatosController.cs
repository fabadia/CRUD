using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD.Data;
using CRUD.Models;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Controllers
{
    [Produces("application/json")]
    [Route("api/Candidatos")]
    public class CandidatosController : Controller
    {
        private readonly AppDbContext _context;

        public CandidatosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("metadata")]
        public IEnumerable GetCandidatoMetadata()
        {
            var properties = typeof(Candidato).GetProperties();
            var metadata = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<string, object> info;
            foreach (var property in properties)
            {
                string nome = property.Name;
                string titulo = property.GetCustomAttribute<DisplayAttribute>()?.Name ?? (property.Name.StartsWith("Nivel") ? property.Name.Substring(5) : property.Name);

                metadata.Add(nome, info = new Dictionary<string, object>());
                info.Add("Titulo", titulo);
            }
            return metadata;
        }

        // GET: api/Candidatos
        [HttpGet]
        public IEnumerable GetCandidatos()
        {
            return _context.Candidatos.Select(c => new { c.Id, c.Nome, c.EMail, c.Skype, c.LinkedIn });
        }

        // GET: api/Candidatos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidato([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidato = await _context.Candidatos
                .Include(c => c.CandidatoDisponibilidades)
                .Include(c => c.CandidatoMelhoresHorarios)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (candidato == null)
            {
                return NotFound();
            }

            return Ok(candidato);
        }

        // PUT: api/Candidatos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato([FromRoute] int id, [FromBody] Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != candidato.Id)
            {
                return BadRequest();
            }

            var original = _context.Candidatos
                .Include(p => p.CandidatoDisponibilidades)
                .Include(p => p.CandidatoMelhoresHorarios)
                .SingleOrDefault(p => p.Id == candidato.Id);

            for (int i = original.CandidatoDisponibilidades.Count - 1; i >= 0; i--)
                if (candidato.CandidatoDisponibilidades.Find(p => p.DisponibilidadeId == original.CandidatoDisponibilidades[i].DisponibilidadeId) == null)
                    original.CandidatoDisponibilidades.RemoveAt(i);

            for (int i = candidato.CandidatoDisponibilidades.Count - 1; i >= 0; i--)
                if (original.CandidatoDisponibilidades.Find(p => p.DisponibilidadeId == candidato.CandidatoDisponibilidades[i].DisponibilidadeId) == null)
                    original.CandidatoDisponibilidades.Add(candidato.CandidatoDisponibilidades[i]);

            for (int i = original.CandidatoMelhoresHorarios.Count - 1; i >= 0; i--)
                if (candidato.CandidatoMelhoresHorarios.Find(p => p.MelhorHorarioId == original.CandidatoMelhoresHorarios[i].MelhorHorarioId) == null)
                    original.CandidatoMelhoresHorarios.RemoveAt(i);

            for (int i = candidato.CandidatoMelhoresHorarios.Count - 1; i >= 0; i--)
                if (original.CandidatoMelhoresHorarios.Find(p => p.MelhorHorarioId == candidato.CandidatoMelhoresHorarios[i].MelhorHorarioId) == null)
                    original.CandidatoMelhoresHorarios.Add(candidato.CandidatoMelhoresHorarios[i]);

            _context.Entry(original).CurrentValues.SetValues(candidato);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Candidatos
        [HttpPost]
        public async Task<IActionResult> PostCandidato([FromBody] Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidato", new { id = candidato.Id }, candidato);
        }

        // DELETE: api/Candidatos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidato([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidato = await _context.Candidatos.SingleOrDefaultAsync(m => m.Id == id);
            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidatos.Remove(candidato);
            await _context.SaveChangesAsync();

            return Ok(candidato);
        }

        private bool CandidatoExists(int id)
        {
            return _context.Candidatos.Any(e => e.Id == id);
        }
    }
}