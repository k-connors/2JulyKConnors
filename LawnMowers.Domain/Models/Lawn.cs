using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnMowers.Domain.Models
{
    public class Lawn
    {
        public int LawnWidth { get; set; }
        public int LawnHeight { get; set; }
        public int NumberOfMowers { get; set; }
        public List<Mower> Mowers { get; set; }
    }
}
