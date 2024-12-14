namespace crud_super_heroes.API.Models
{
    public class Heroi
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }

        public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; }
    }
}
