using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Clases
{
    internal class Pokemon
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BaseExperience { get; set; }
        public Sprites Sprites { get; set; }
    }

    public class Sprites
    {
        public string FrontDefault { get; set; }
    }
}
