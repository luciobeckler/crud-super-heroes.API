using crud_super_heroes.API.Dto;
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
                .ToListAsync();
        }
        public async Task<HeroiDto> AddHeroiAsync(HeroiESuperPoderesIdDto heroiDto)
        {
            if (await _context.Herois.AnyAsync(h => h.Nome == heroiDto.Nome))
                throw new Exception("Já existe um herói com este nome!");

            var heroi = new Heroi
            {
                Nome = heroiDto.Nome,
                NomeHeroi = heroiDto.NomeHeroi,
                Altura = heroiDto.Altura,
                DataNascimento = heroiDto.DataNascimento,
                Peso = heroiDto.Peso,
            };

            _context.Herois.Add(heroi);
            await _context.SaveChangesAsync();

            foreach (var superPoderId in heroiDto.SuperPoderIds)
            {
                var superPoder = await _context.SuperPoderes.FindAsync(superPoderId);

                if (superPoder == null)
                {
                    throw new Exception($"Superpoder com ID {superPoderId} não encontrado.");
                }

                _context.HeroisSuperPoderes.Add(new HeroiSuperPoder
                {
                    HeroiId = heroi.Id, 
                    SuperPoderId = superPoderId
                });
            }

            await _context.SaveChangesAsync();

            return heroiDto;
        }

        public async Task DeleteHeroiAsync(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);

            if (heroi == null)
                throw new Exception("Super-Herói não encontrado.");

            _context.Herois.Remove(heroi);
            await _context.SaveChangesAsync();
        }

        public async Task<Heroi> UpdateHeroiAsync(int id, HeroiESuperPoderesIdDto heroiESuperPoderesDto)
        {
            if (await _context.Herois.AnyAsync(h => h.NomeHeroi == heroiESuperPoderesDto.NomeHeroi))
                throw new Exception("Já existe um herói com este nome!");

            var heroiExistente = await _context.Herois.FindAsync(id);

            if (heroiExistente == null)
                throw new Exception("Super-Herói não encontrado.");

            heroiExistente.Nome = heroiESuperPoderesDto.Nome;
            heroiExistente.NomeHeroi = heroiESuperPoderesDto.NomeHeroi;
            heroiExistente.DataNascimento = heroiESuperPoderesDto.DataNascimento;
            heroiExistente.Altura = heroiESuperPoderesDto.Altura;
            heroiExistente.Peso = heroiESuperPoderesDto.Peso;

            // Verifica e adiciona os superpoderes
            foreach (var superPoderId in heroiESuperPoderesDto.SuperPoderIds)
            {
                var superPoder = await _context.SuperPoderes.FindAsync(superPoderId);

                if (superPoder == null)
                {
                    throw new Exception($"Superpoder com ID {superPoderId} não encontrado.");
                }

                // Verifica se a relação já existe
                var existeRelacao = await _context.HeroisSuperPoderes
                    .AnyAsync(hsp => hsp.HeroiId == id && hsp.SuperPoderId == superPoderId);

                if (!existeRelacao)
                {
                    _context.HeroisSuperPoderes.Add(new HeroiSuperPoder
                    {
                        HeroiId = id,
                        SuperPoderId = superPoderId
                    });
                }
            }

            await _context.SaveChangesAsync();

            return heroiExistente;
        }

    }
}
