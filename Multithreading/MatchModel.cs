using System;
using System.Collections.Generic;
using System.Text;

namespace Multhitreeading
{
    public class MatchModel
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public double Ratio { get; set; }
        public override string ToString()
        {
            return $"{Ratio}: {Name1} -> {Name2}";
        }
    }
}
