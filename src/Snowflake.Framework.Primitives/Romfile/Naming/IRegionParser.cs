using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowflake.Romfile.Naming
{
    /// <summary>
    /// Implements a parser that returns a list of regions from a region string
    /// </summary>
    public interface IRegionParser
    {
        /// <summary>
        /// Returns an array of 
        /// </summary>
        /// <param name="regionString"></param>
        /// <returns></returns>
        ImmutableArray<Region> ParseRegion(string regionString);
    }
}
