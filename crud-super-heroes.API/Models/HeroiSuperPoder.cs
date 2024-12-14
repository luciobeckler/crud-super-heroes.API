namespace crud_super_heroes.API.Models
{
    public class HeroiSuperPoder
    {
        public int HeroiId { get; set; }
        public Heroi Heroi { get; set; }

        public int SuperPoderId { get; set; }
        public SuperPoder SuperPoder { get; set; }
    }
}
