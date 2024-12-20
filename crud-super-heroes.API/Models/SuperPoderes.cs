﻿using System.Text.Json.Serialization;

namespace crud_super_heroes.API.Models
{
        public class SuperPoderes
        {
            public int Id { get; set; }
            public string SuperPoder { get; set; }
            public string Descricao { get; set; }

            [JsonIgnore]
            public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; }  // Relacionamento com a tabela intermediária

    }
}
