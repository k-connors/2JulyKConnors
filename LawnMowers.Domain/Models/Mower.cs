using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnMowers.Domain.Models
{
    public class Mower
    {
        public int StartingX { get; set; }
        public int StartingY { get; set; }
        public string StartingCardinal { get; set; }
        public int FinishingX { get; set; }
        public int FinishingY { get; set; }
        public string FinishingCardinal { get; set; }
        public char[] Instructions { get; set; }
    }
}
