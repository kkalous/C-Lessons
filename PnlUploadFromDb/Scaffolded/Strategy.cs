using System;
using System.Collections.Generic;

#nullable disable

namespace PnlUploadFromDb.Scaffolded
{
    public partial class Strategy
    {
        public Strategy()
        {
            PnlCapitals = new HashSet<PnlCapital>();
        }

        public int StrategyId { get; set; }
        public string StrategyName { get; set; }
        public string Region { get; set; }

        public virtual ICollection<PnlCapital> PnlCapitals { get; set; }
    }
}
