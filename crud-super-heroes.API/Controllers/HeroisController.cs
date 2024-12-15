using crud_super_heroes.API.Models;
using crud_super_heroes.API.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud_super_heroes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HeroisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Endpoint para cadastrar um novo herói
        [HttpPost]
        public async Task<ActionResult<Heroi>> CriarHeroi(HeroiDto heroiDto)
        {
            // Verificar se o nome do herói já existe
            if (_context.Herois.Any(h => h.NomeHeroi == heroiDto.NomeHeroi))
            {
                return BadRequest("Já existe um herói com esse nome.");
            }

            // Verificar se todos os superpoderes existem
            var superPoderesExistem = await _context.SuperPoderes
                .Where(sp => heroiDto.SuperPoderIds.Contains(sp.Id))
                .CountAsync();

            if (superPoderesExistem != heroiDto.SuperPoderIds.Count)
            {
                return BadRequest("Um ou mais superpoderes não existem.");
            }

            // Criar o herói
            var heroi = new Heroi
            {
                Nome = heroiDto.Nome,
                NomeHeroi = heroiDto.NomeHeroi,
                DataNascimento = heroiDto.DataNascimento,
                Altura = heroiDto.Altura,
                Peso = heroiDto.Peso,
                HeroisSuperPoderes = heroiDto.SuperPoderIds.Select(id => new HeroiSuperPoder
                {
                    SuperPoderId = id
                }).ToList()
            };

            _context.Herois.Add(heroi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ConsultarHeroi), new { id = heroi.Id }, heroi);
        }

        // Endpoint para listar todos os heróis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroiDto>>> ListarHerois()
        {
            var heroisDto = await _context.Herois
                .Include(h => h.HeroisSuperPoderes)
                    .ThenInclude(hsp => hsp.SuperPoder)
                .Select(heroi => new HeroiDto
                {
                    Id = heroi.Id,
                    Nome = heroi.Nome,
                    NomeHeroi = heroi.NomeHeroi,
                    DataNascimento = heroi.DataNascimento,
                    Altura = heroi.Altura,
                    Peso = heroi.Peso,
                    SuperPoderIds = heroi.HeroisSuperPoderes.Select(hsp => hsp.SuperPoderId).ToList()
                })
                .ToListAsync();

            return heroisDto;
        }

        // Endpoint para consultar um herói pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Heroi>> ConsultarHeroi(int id)
        {
            var heroi = await _context.Herois.Include(h => h.HeroisSuperPoderes)
                                             .ThenInclude(hsp => hsp.SuperPoder)
                                             .FirstOrDefaultAsync(h => h.Id == id);

            if (heroi == null)
            {
                return NotFound();
            }

            return heroi;
        }

        // Endpoint para excluir um herói pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirHeroi(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);
            if (heroi == null)
            {
                return NotFound();
            }

            _context.Herois.Remove(heroi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Endpoint para atualizar um herói pelo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarHeroi(int id, HeroiDto heroiDto)
        {
            var heroi = await _context.Herois.Include(h => h.HeroisSuperPoderes)
                                             .FirstOrDefaultAsync(h => h.Id == id);

            if (heroi == null)
            {
                return NotFound();
            }

            // Verificar se o nome do herói já existe em outro herói
            if (_context.Herois.Any(h => h.NomeHeroi == heroiDto.NomeHeroi && h.Id != id))
            {
                return BadRequest("Já existe um herói com esse nome.");
            }

            // Verificar se todos os superpoderes existem
            var superPoderesExistem = await _context.SuperPoderes
                .Where(sp => heroiDto.SuperPoderIds.Contains(sp.Id))
                .CountAsync();

            if (superPoderesExistem != heroiDto.SuperPoderIds.Count)
            {
                return BadRequest("Um ou mais superpoderes não existem.");
            }

            heroi.Nome = heroiDto.Nome;
            heroi.NomeHeroi = heroiDto.NomeHeroi;
            heroi.DataNascimento = heroiDto.DataNascimento;
            heroi.Altura = heroiDto.Altura;
            heroi.Peso = heroiDto.Peso;

            // Atualizando os superpoderes
            heroi.HeroisSuperPoderes = heroiDto.SuperPoderIds.Select(id => new HeroiSuperPoder
            {
                SuperPoderId = id
            }).ToList();

            _context.Herois.Update(heroi);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
