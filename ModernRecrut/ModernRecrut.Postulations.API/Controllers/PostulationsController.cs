using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernRecrut.Postulations.API.Data;
using ModernRecrut.Postulations.API.Interfaces;
using ModernRecrut.Postulations.API.Models;
using System.Diagnostics;

namespace ModernRecrut.Postulations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulationsController : ControllerBase
    {
        private readonly ModernRecrutPostulationsContext _context;
        private readonly IGenererEvaluationService _evaluationService;

        public PostulationsController(ModernRecrutPostulationsContext context, IGenererEvaluationService evaluationService)
        {
            _context = context;
            _evaluationService = evaluationService;
        }

        // GET: api/Postulations/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Postulation>>> ObtenirPostulations()
        {
            if (_context.Postulation == null)
            {
                return NotFound();
            }
            return await _context.Postulation.ToListAsync();

        }

        // GET: api/Postulations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Postulation>> ObtenirPostulationById(int id)
        {
            if (_context.Postulation == null)
            {
                return NotFound();
            }
            var postulation = await _context.Postulation.FindAsync(id);

            if (postulation == null)
            {
                return NotFound();
            }

            return postulation;
        }

        // PUT: api/Postulations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificationPostulation(int id, Postulation postulation)
        {
            if (id != postulation.Id)
            {
                return BadRequest();
            }

            _context.Entry(postulation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostulationExists(id))
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

        // POST: api/Postulations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Postulation>> CreerPostulation (Postulation postulation)
        {
            if (cand)


            _context.Postulation.Add(postulation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostulationExists(postulation.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(ObtenirPostulationById), new { id = postulation.Id }, postulation);
        }

        // DELETE: api/Postulations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> SupprimerPostulation(int id)
        {
            if (_context.Postulation == null)
            {
                return NotFound();
            }
            var postulation = await _context.Postulation.FindAsync(id);
            if (postulation == null)
            {
                return NotFound();
            }

            _context.Postulation.Remove(postulation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostulationExists(int id)
        {
            return (_context.Postulation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
