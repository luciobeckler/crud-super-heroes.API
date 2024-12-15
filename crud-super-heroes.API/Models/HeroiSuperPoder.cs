namespace crud_super_heroes.API.Models
{
    public class HeroiSuperPoder
    {
        public int HeroiId { get; set; }  // Chave estrangeira para Heroi
        public Heroi Heroi { get; set; }  // Navegação para a entidade Heroi

        public int SuperPoderId { get; set; }  // Chave estrangeira para SuperPoder
        public SuperPoderes SuperPoder { get; set; }  // Navegação para a entidade SuperPoder
    }
}
