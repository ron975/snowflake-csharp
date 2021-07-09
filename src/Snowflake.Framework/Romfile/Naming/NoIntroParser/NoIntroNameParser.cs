using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Snowflake.Romfile.Naming.CommonParsers;
using static Pidgin.Parser;
using static Pidgin.Parser<char, string>;
using StringParser = Pidgin.Parser<char, string>;
using InfoFlagParser = Pidgin.Parser<char, Snowflake.Romfile.Naming.InfoFlags>;
using VersionParser = Pidgin.Parser<char, Snowflake.Romfile.Naming.RomVersion>;
using RegionParser = Pidgin.Parser<char, System.Collections.Generic.IEnumerable<Snowflake.Romfile.Naming.Region>>;

namespace Snowflake.Romfile.Naming.NoIntroParser
{
    public sealed class NoIntroNameParser
        : IFilenameParser
    {
        InfoFlagParser ParseUnlicensed = InParens(String("Unl")).ThenReturn(InfoFlags.Unlicensed);
        InfoFlagParser ParseProto = InParens(String("Proto")).ThenReturn(InfoFlags.Prototype);
        InfoFlagParser ParseKiosk = InParens(String("Kiosk")).ThenReturn(InfoFlags.Kiosk);
        InfoFlagParser ParseDemo = InParens(String("Demo")).ThenReturn(InfoFlags.Demo);
        InfoFlagParser ParseBonus = InParens(String("Bonus Disc")).ThenReturn(InfoFlags.Bonus);
        InfoFlagParser ParseTaikenban = InParens(String("Taikenban")).ThenReturn(InfoFlags.Demo);
        InfoFlagParser ParseTentouTaikenban = InParens(String("Tentou Taikenban")).ThenReturn(InfoFlags.Kiosk);
        VersionParser ParseRevision = InParens(String("Rev ")
            .Then(DecimalNum).Map(v => new RomVersion("Rev", v.ToString())));
        //VersionParser ParseVersion = InParens(String("v")
        //                                .Or(String("Version "))
        //                                .Then(DecimalNum).
        //    .Then(DecimalNum).Map(v => new Version("Rev", v.ToString())));

        private static readonly StringParser RegionKey = OneOf(NoIntroRegionParser.NOINTRO_MAP.Keys.Select(s => Try(String(s)))
                                                           .Concat(new[] { Try(String("World")),
                                                                Try(String("Latin America")),
                                                                Try(String("Scandinavia")) }));

        internal static readonly RegionParser ParseRegion = InParens(RegionKey.Map(x => NoIntroRegionParser.ParseRegion(x)).Separated(String(", "))).Map(x => x.SelectMany(r => r).Distinct());


        public bool TryParse(string filename, out NameInfo? nameInfo)
        {
            throw new NotImplementedException();
        }
    }
}
