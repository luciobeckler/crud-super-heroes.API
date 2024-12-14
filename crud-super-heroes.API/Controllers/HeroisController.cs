using crud_super_heroes.API.Dto;
using crud_super_heroes.API.Models;
using crud_super_heroes.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace crud_super_heroes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroisController : ControllerBase
    {
        private readonly HeroiService _heroiService;

        public HeroisController(HeroiService heroiService)
        {
            _heroiService = heroiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHerois()
        {
            var herois = await _heroiService.GetAllHeroisAsync();
            return Ok(herois);
        }

        [HttpPost]
        public async Task<IActionResult> AddHeroi([FromBody] HeroiESuperPoderesIdDto heroiDto)
        {
            try
            {
                var novoHeroi = await _heroiService.AddHeroiAsync(heroiDto);
                return CreatedAtAction(nameof(GetHerois), novoHeroi);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperPoderes>> GetHeroeById(int id)
        {
            var herois = await _heroiService.GetAllHeroisAsync();

            var heroiAlvo = herois.FirstOrDefault(sp => sp.Id == id);

            if (heroiAlvo == null)
            {
                return NotFound("Super-Herói não encontrado.");
            }

            return Ok(heroiAlvo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeroi(int id, HeroiESuperPoderesIdDto heroiESuperPoderesIdDto)
        {
            Heroi heroiAtualizado;

            try
            {
                heroiAtualizado = await _heroiService.UpdateHeroiAsync(id, heroiESuperPoderesIdDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(heroiAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroi(int id)
        {
            try
            {
                await _heroiService.DeleteHeroiAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
