using Microsoft.AspNetCore.Mvc;
using ModernRecrut.Documents.API.Interfaces;
using ModernRecrut.Documents.API.Models;
using System.IO.Pipelines;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ModernRecrut.Documents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionDocumentsController : ControllerBase
    {
        private readonly IGestionFichiers _gestionFichiers;
        public GestionDocumentsController(IGestionFichiers gestionFichiers)
        {
            _gestionFichiers = gestionFichiers;
        }

        // GET: api/<GestionDocumentsController>
        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id)
        {

            return _gestionFichiers.ObtenirNomFichiersSelonId(id);
        }

        // POST api/<GestionDocumentsController>
        [HttpPost]
        public async Task<IActionResult> EnregistrementDocument(Fichier fichierRecu)
        {
            string nomFichier = await _gestionFichiers.EnregistrerFichier(fichierRecu);

            return CreatedAtAction(nameof(EnregistrementDocument), nomFichier);
        }
    }
}
