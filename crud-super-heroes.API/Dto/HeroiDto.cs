using crud_super_heroes.API.Models;

namespace crud_super_heroes.API.Dto
{
    public class HeroiDto
    {
        public Heroi Heroi { get; set; }
        public List<int> SuperPoderIds { get; set; }
    }

}
