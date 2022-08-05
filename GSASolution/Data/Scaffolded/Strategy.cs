using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Scaffolded
{
    public partial class Strategy
    {
        public Strategy()
        {
            Capitals = new HashSet<Capital>();
            Pnls = new HashSet<Pnl>();
        }

        public int StrategyId { get; set; }
        public string StrategyName { get; set; }
        public string Region { get; set; }

        public virtual ICollection<Capital> Capitals { get; set; }
        public virtual ICollection<Pnl> Pnls { get; set; }
    }
}
