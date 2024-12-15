using crud_super_heroes.API.Models;

namespace crud_super_heroes.API.Dto
{
    public class HeroiDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string NomeHeroi { get; set; }
        public DateTime? DataNascimento { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public List<int> SuperPoderIds { get; set; }

    }

}
