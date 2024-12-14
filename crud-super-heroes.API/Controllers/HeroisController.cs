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
        public async Task<IActionResult> AddHeroi([FromBody] HeroiDto heroiDto)
        {
            try
            {
                var novoHeroi = await _heroiService.AddHeroiAsync(heroiDto.Heroi, heroiDto.SuperPoderIds);
                return CreatedAtAction(nameof(GetHerois), new { id = novoHeroi.Id }, novoHeroi);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("superpoderes")]
        public async Task<IActionResult> GetSuperPoderes()
        {
            var superPoderes = await _heroiService.GetAllSuperPoderesAsync();
            return Ok(superPoderes);
        }
    }
}
