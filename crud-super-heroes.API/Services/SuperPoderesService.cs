using crud_super_heroes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_super_heroes.API.Services
{
    public class SuperPoderesService
    {
        private readonly ApplicationDbContext _context;

        public SuperPoderesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SuperPoderes>> GetAllSuperPoderesAsync()
        {
            return await _context.SuperPoderes.ToListAsync();
        }

        public async Task<SuperPoderes> AddSuperPoderAsync(SuperPoderes superPoder)
        {
            if (await SuperPoderExisteAsync(superPoder.SuperPoder))
                throw new Exception("Já existe um superpoder com este nome!");

            _context.SuperPoderes.Add(superPoder);
            await _context.SaveChangesAsync();
            return superPoder;
        }

        public async Task<SuperPoderes> UpdateSuperPoderAsync(int id, SuperPoderes superPoder)
        {
            var existingSuperPoder = await _context.SuperPoderes.FindAsync(id);
            if (existingSuperPoder == null)
                throw new Exception("Superpoder não encontrado.");

            existingSuperPoder.SuperPoder = superPoder.SuperPoder;
            existingSuperPoder.Descricao = superPoder.Descricao;

            await _context.SaveChangesAsync();
            return existingSuperPoder;
        }

        public async Task DeleteSuperPoderAsync(int id)
        {
            var superPoder = await _context.SuperPoderes.FindAsync(id);
            if (superPoder == null)
                throw new Exception("Superpoder não encontrado.");

            _context.SuperPoderes.Remove(superPoder);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SuperPoderExisteAsync(string nome)
        {
            return await _context.SuperPoderes.AnyAsync(sp => sp.SuperPoder == nome);
        }
    }
}
