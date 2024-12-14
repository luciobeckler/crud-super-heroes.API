using crud_super_heroes.API.Models;
using crud_super_heroes.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace crud_super_heroes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperPoderesController : ControllerBase
    {
        private readonly SuperPoderesService _superPoderesService;

        public SuperPoderesController(SuperPoderesService superPoderesService)
        {
            _superPoderesService = superPoderesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperPoderes>>> GetSuperPoderes()
        {
            var superPoderes = await _superPoderesService.GetAllSuperPoderesAsync();
            return Ok(superPoderes);
        }

        [HttpPost]
        public async Task<ActionResult<SuperPoderes>> PostSuperPoder(SuperPoderes superPoder)
        {
            try
            {
                var createdSuperPoder = await _superPoderesService.AddSuperPoderAsync(superPoder);
                return CreatedAtAction(nameof(GetSuperPoder), new { id = createdSuperPoder.Id }, createdSuperPoder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperPoderes>> GetSuperPoder(int id)
        {
            var superPoderes = await _superPoderesService.GetAllSuperPoderesAsync();
                
            var superPoder = superPoderes.FirstOrDefault(sp => sp.Id == id);

            if (superPoder == null)
            {
                return NotFound();
            }

            return Ok(superPoder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperPoder(int id, SuperPoderes superPoder)
        {
            if (id != superPoder.Id)
            {
                return BadRequest();
            }

            try
            {
                await _superPoderesService.UpdateSuperPoderAsync(id, superPoder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperPoder(int id)
        {
            try
            {
                await _superPoderesService.DeleteSuperPoderAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
