using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.Clases
{
    internal class PokemonInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }

        [JsonProperty("abilities")]
        public List<AbilityInfo> Abilities { get; set; }

        [JsonProperty("types")]
        public List<TypeInfo> Types { get; set; }

        [JsonProperty("stats")]
        public List<StatInfo> Stats { get; set; }

        [JsonProperty("base_experience")]
        public int BaseExperience { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("species")]
        public SpeciesInfo Species { get; set; }
    }

    internal class Sprites
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }

        [JsonProperty("back_default")]
        public string BackDefault { get; set; }

        [JsonProperty("front_shiny")]
        public string FrontShiny { get; set; }

        [JsonProperty("back_shiny")]
        public string BackShiny { get; set; }
    }

    internal class AbilityInfo
    {
        [JsonProperty("ability")]
        public NamedAPIResource Ability { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public int Slot { get; set; }
    }

    internal class TypeInfo
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public NamedAPIResource Type { get; set; }
    }

    internal class StatInfo
    {
        [JsonProperty("base_stat")]
        public int BaseStat { get; set; }

        [JsonProperty("effort")]
        public int Effort { get; set; }

        [JsonProperty("stat")]
        public NamedAPIResource Stat { get; set; }
    }

    internal class SpeciesInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    internal class NamedAPIResource
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
