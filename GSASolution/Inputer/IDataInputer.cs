using Data.Scaffolded;
using System;
using System.Collections.Generic;


namespace GSASolution
{
    public interface IDataInputer
    {
        IEnumerable<Strategy> ReadStrategies(string fileLoc);
        Dictionary<string, List<Capital>> ReadCapital(string fileLoc);
        Dictionary<string, List<Pnl>> ReadPnl(string fileLoc);
    }
}
