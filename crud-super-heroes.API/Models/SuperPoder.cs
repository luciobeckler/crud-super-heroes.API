namespace crud_super_heroes.API.Models
{
        public class SuperPoder
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }

            public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; }
        }
}
