using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace crud_super_heroes.API.Models
{
    public class Heroi
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeHeroi { get; set; }
        public DateTime? DataNascimento { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        
        [JsonIgnore]
        public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; }  // Relacionamento com a tabela intermediária

    }
}
