using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace PnlUploadFromDb.Scaffolded
{
    public partial class PnlCapital
    {
        public int StrategyId { get; set; }
        public DateTime Date { get; set; }
        public decimal? Amount { get; set; }
        public string AmountType { get; set; }
        [NotMapped]
        public virtual Strategy Strategy { get; set; }
    }
}
