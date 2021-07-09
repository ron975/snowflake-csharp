using Snowflake.Romfile.Naming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
using static Pidgin.Parser<char, string>;
using static Snowflake.Romfile.Naming.CommonParsers;

using StringParser = Pidgin.Parser<char, string>;
using InfoFlagParser = Pidgin.Parser<char, Snowflake.Romfile.Naming.InfoFlags>;
using VersionParser = Pidgin.Parser<char, Snowflake.Romfile.Naming.RomVersion>;
using RegionParser = Pidgin.Parser<char, System.Collections.Generic.IEnumerable<Snowflake.Romfile.Naming.Region>>;
using Version = Snowflake.Romfile.Naming.RomVersion;
using Xunit;
using Pidgin;
using Snowflake.Romfile.Naming.NoIntroParser;
using System.Collections.Immutable;

namespace Snowflake.Romfile.Tests
{
    public class NoIntroParserTest
    {
        InfoFlagParser ParseUnlicensed = InParens(String("Unl")).ThenReturn(InfoFlags.Unlicensed);
        InfoFlagParser ParseProto = InParens(String("Proto")).ThenReturn(InfoFlags.Prototype);
        InfoFlagParser ParseKiosk = InParens(String("Kiosk")).ThenReturn(InfoFlags.Kiosk);
        InfoFlagParser ParseDemo = InParens(String("Demo")).ThenReturn(InfoFlags.Demo);
        InfoFlagParser ParseBonus = InParens(String("Bonus Disc")).ThenReturn(InfoFlags.Bonus);
        InfoFlagParser ParseTaikenban = InParens(String("Taikenban")).ThenReturn(InfoFlags.Demo);
        InfoFlagParser ParseTentouTaikenban = InParens(String("Tentou Taikenban")).ThenReturn(InfoFlags.Kiosk);
        InfoFlagParser ParseBadDump = InBrackets(String("b")).ThenReturn(InfoFlags.BadDump);
        InfoFlagParser ParseBIOS = InBrackets(String("BIOS")).ThenReturn(InfoFlags.BIOS);

       

        VersionParser ParseRevision = InParens(String("Rev ")
            .Then(DecimalNum).Map(v => new Version("Rev", v.ToString())));

        VersionParser ParseVersion = InParens(
            OneOf(String("v"), String("Version "))
                .Then(Map((major, minor) => new Version("Version", major.ToString(), minor.GetValueOrDefault()), 
                        DecimalNum, Char('.').Then(OneOf(LetterOrDigit, Char('.')).AtLeastOnceString()).Optional()
                        )
                    )
            );
        
        [Fact]
        public void Parse_Test()
        {
            var ver = ParseRevision.Parse("(Rev 4)");
            var verX = ParseVersion.Parse("(Version 4.52)");
            var verY = ParseVersion.Parse("(Version 4)");
            var verD = ParseVersion.Parse("(v4.XX)");
        }

        [Fact]
        public void Parse_Region()
        {
            // need to convert to list to trigger xunit overload
            var r = NoIntroRegionParser.ParseRegion("Japan, Europe, Australia, New Zealand").ToList();
            Assert.Equal(r, NoIntroNameParser.ParseRegion.Parse("(Japan, Europe, Australia, New Zealand)").Value);
        }

        [Fact]
        public void Parse_Region_End()
        {
            // need to convert to list to trigger xunit overload
            var r = NoIntroRegionParser.ParseRegion("Japan, Europe, Australia, New Zealand").ToList();
            var res = NoIntroNameParser.ParseRegionAndEnsureEnd.Parse("(Japan, Europe) (b12)");
            Assert.Equal(r, NoIntroNameParser.ParseRegionAndEnsureEnd.Parse("(Japan, Europe, Australia, New Zealand) (b12)").Value);
        }

        [Fact]
        public void TryParse_Test()
        {
            var parser = new NoIntroNameParser();
            parser.TryParse("FIFA 20 - Portuguese (Brazil) In-Game Commentary (World)", out var fifa20);
            parser.TryParse("Odekake Lester - Lelele no Le (^^; (Japan)", out var odekake);
            parser.TryParse("void tRrLM(); Void Terrarium (Japan)", out var voidTer);

        }
    }
}
