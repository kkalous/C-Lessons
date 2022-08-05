using System;
using System.Collections.Generic;
using System.Text;

namespace GSASolution.Outputer
{
    public interface IDataOutputer
    {
        IEnumerable<string> GetCapitalsInfo(string strategies);
        IEnumerable<string> GetPnlsInfo(string strategies);
    }
}
