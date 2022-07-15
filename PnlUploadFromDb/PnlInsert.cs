using System;
using System.Collections.Generic;

#nullable disable

namespace PnlUploadFromDb
{
    public partial class PnlInsert
    {
        public DateTime Date { get; set; }
        public string StrategyName { get; set; }
        public decimal? Amount { get; set; }
    }
}
