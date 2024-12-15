using crud_super_heroes.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud_super_heroes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperPoderesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SuperPoderesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<SuperPoderes>> CriarSuperPoder(SuperPoderes superPoder)
        {
            _context.SuperPoderes.Add(superPoder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ListarSuperPoderes), new { id = superPoder.Id }, superPoder);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperPoderes>>> ListarSuperPoderes()
        {
            return await _context.SuperPoderes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperPoderes>> ListarSuperPoderesById(int id)
        {
            var superPoder = await _context.SuperPoderes.FindAsync(id);

            if (superPoder == null)
            {
                return NotFound(new { mensagem = $"Super poder com ID {id} não encontrado." });
            }

            return Ok(superPoder);
        }

    }
}
