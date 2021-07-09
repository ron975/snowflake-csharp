using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowflake.Romfile.Naming
{
    /// <summary>
    /// The information from a parsed file name.
    /// </summary>
    public sealed record NameInfo
    {
        /// <summary>
        /// The title of the game as it was parsed from the filename.
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// The title normalized according to the <a href="https://github.com/SnowflakePowered/shiratsu/blob/master/spec.md">shiratsu specification</a>.
        /// </summary>
        public string NormalizedTitle => NameInfo.NormalizeTitle(this.Title);
        /// <summary>
        /// An array of regions this game is associated with.
        /// </summary>
        public ImmutableArray<Region> Region { get; }
        /// <summary>
        /// Flags this game is associated with.
        /// </summary>
        public InfoFlags Flags { get; }
        /// <summary>
        /// The naming convention used to parse this name.
        /// </summary>
        public NamingConvention NamingConvention { get; }
        /// <summary>
        /// The version of the game if any.
        /// </summary>
        public RomVersion? Version { get; }
        /// <summary>
        /// The year of the game if any.
        /// </summary>
        public Year? Year { get; }

        /// <summary>
        /// Initialize a new NameInfo
        /// </summary>
        /// <param name="convention">The naming convention.</param>
        /// <param name="entryTitle">The title as it was parsed from the filename.</param>
        /// <param name="region">The regions as were parsed from the filename.</param>
        /// <param name="flags">The info flags as were parsed from the filename.</param>
        /// <param name="version">The version if available from the filename.</param>
        /// <param name="year">The year if available from the filename.</param>
        public NameInfo(NamingConvention convention, string entryTitle, ImmutableArray<Region> region, InfoFlags flags, RomVersion? version = null, Year? year = null)
            => (NamingConvention, Title, Region, Flags, Version, Year) = (convention, entryTitle, region, flags, version, year);

        /// <summary>
        /// Normalize a title according to the <a href="https://github.com/SnowflakePowered/shiratsu/blob/master/spec.md">shiratsu specification</a>.
        /// </summary>
        /// <param name="title">The title to normalize.</param>
        /// <returns>The normalized title.</returns>
        public static string NormalizeTitle(string title) => title;
    }
}
