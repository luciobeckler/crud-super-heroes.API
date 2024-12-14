using crud_super_heroes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_super_heroes.API.Services
{
    public class HeroiService
    {
        private readonly ApplicationDbContext _context;

        public HeroiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Heroi>> GetAllHeroisAsync()
        {
            return await _context.Herois
                .Include(h => h.HeroisSuperPoderes)
                .ThenInclude(hsp => hsp.SuperPoder)
                .ToListAsync();
        }

        public async Task<Heroi> AddHeroiAsync(Heroi heroi, List<int> superPoderIds)
        {
            if (await _context.Herois.AnyAsync(h => h.Nome == heroi.Nome))
                throw new Exception("Já existe um herói com este nome!");

            _context.Herois.Add(heroi);
            await _context.SaveChangesAsync();

            foreach (var superPoderId in superPoderIds)
            {
                _context.HeroisSuperPoderes.Add(new HeroiSuperPoder
                {
                    HeroiId = heroi.Id,
                    SuperPoderId = superPoderId
                });
            }

            await _context.SaveChangesAsync();
            return heroi;
        }

        public async Task<List<SuperPoder>> GetAllSuperPoderesAsync()
        {
            return await _context.SuperPoderes.ToListAsync();
        }
    }
}
