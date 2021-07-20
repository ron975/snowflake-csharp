using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Snowflake.Romfile.Naming.CommonParsers;
using static Pidgin.Parser;
using static Pidgin.Parser<char, string>;
using static Pidgin.Parser<char>;

using StringParser = Pidgin.Parser<char, string>;
using InfoFlagParser = Pidgin.Parser<char, Snowflake.Romfile.Naming.Tags.RomInfo>;
using VersionParser = Pidgin.Parser<char, Snowflake.Romfile.Naming.Tags.VersionTag>;
using TagParser = Pidgin.Parser<char, Snowflake.Romfile.Naming.Tags.RomTag>;
using RegionParser = Pidgin.Parser<char, System.Collections.Generic.IEnumerable<Snowflake.Romfile.Naming.Region>>;
using System.Collections.Immutable;
using Pidgin;
using Snowflake.Romfile.Naming.Tags;

namespace Snowflake.Romfile.Naming.NoIntroParser
{
    public sealed class NoIntroNameParser
        : IFilenameParser
    {

        private static TagParser ParseBiosTag = InBrackets(String("BIOS"))
            .ThenReturn<RomTag>(new TextTag("BIOS", TagCategory.Bracketed));

        private static TagParser ParseSceneTag = OneOf(
            Digit.RepeatString(4).Map<RomTag>(s => new SceneTag(s, null)),
            String("xB").Then(Digit.RepeatString(2)).Map<RomTag>(s => new SceneTag(s, "xB")),
            Map((prefix, number) => (RomTag) new SceneTag(number, prefix.ToString()), CIOneOf('x', 'z'), Digit.RepeatString(3)))
        .Before(String(" - "));

        private static TagParser ParseRedumpMultitapTag = InParens(Sequence(String("Multi Tap ("), TakeUntil(String(")")), String(")"), TakeUntil(String(")"))).Map(s => string.Concat(s)))
                    .Map<RomTag>(s => new TextTag(s, TagCategory.Parenthesized));

        VersionParser ParseRevision = InParens(String("Rev ")
            .Then(DecimalNum).Map(v => new VersionTag("Rev", v.ToString())));


        //VersionParser ParseVersion = InParens(String("v")
        //                                .Or(String("Version "))
        //                                .Then(DecimalNum).
        //    .Then(DecimalNum).Map(v => new Version("Rev", v.ToString())));

        private static readonly StringParser RegionKey = OneOf(NoIntroRegionParser.NOINTRO_MAP.Keys.Select(s => Try(String(s)))
                                                           .Concat(new[] { Try(String("World")),
                                                                Try(String("Latin America")),
                                                                Try(String("Scandinavia")) }));
        private static readonly TagParser ParseAdditionalFlag = InParens(Any.AtLeastOnceUntil(Lookahead(Char(')')))
            .Select(cs => string.Concat(cs))).Map<RomTag>(s => new TextTag(s, TagCategory.Parenthesized));

        private static TagParser ParseBadDumpTag = InBrackets(String("b"))
                                                .ThenReturn<RomTag>(new TextTag("b", TagCategory.Bracketed));

        internal static readonly RegionParser ParseRegion = InParens(RegionKey.Map(x => NoIntroRegionParser.ParseRegion(x)).Separated(String(", "))).Map(x => x.SelectMany(r => r).Distinct());
        internal static readonly RegionParser ParseRegionAndEnsureEnd = ParseRegion.Before(OneOf(
                Try(End),
                Try(Lookahead(Char(' ').Then(
                    ParseBadDumpTag.ThenReturn(Unit.Value).Or(
                    ParseAdditionalFlag.ThenReturn(Unit.Value))
                )))
            ));


        private static TagParser ParseKnownTags = Char(' ').Optional().Then(OneOf(
            Try(ParseRedumpMultitapTag),
            Try(ParseAdditionalFlag)
            ));
        private static readonly Parser<char, NameInfo> NameParser = from scene in ParseSceneTag.Optional()
                                                                    from bios in ParseBiosTag.Optional()
                                                                    from title in Any.AtLeastOnceUntil(Lookahead(Try(ParseRegionAndEnsureEnd))).Select(s => string.Concat(s))
                                                                    from region in ParseRegionAndEnsureEnd
                                                                    from restTags in Try(ParseKnownTags).Many()
                                                                    from badDump in Char(' ').Optional().Then(ParseBadDumpTag).Optional()
                                                                    let tags = MergeTags(restTags, bios, scene, badDump)
                                                                    from _eof in End
                                                                    select new NameInfo(NamingConvention.NoIntro, title.Trim(), region.ToImmutableArray(), tags,
                                                                      MergeInfoFlags(tags));
                                                                    
        public bool TryParse(string filename, out NameInfo? nameInfo)
        {
            var res = NameParser.Parse(filename);
            nameInfo = res.Success ? res.Value : null;
            return res.Success;
        }

        private static ImmutableArray<RomTag> MergeTags(IEnumerable<RomTag> flags2, params Maybe<RomTag>[] flags)
        {
            return flags.Where(h => h.HasValue).Select(h => h.Value).Concat(flags2).ToImmutableList().ToImmutableArray();
        }

        private static RomInfo MergeInfoFlags(ImmutableArray<RomTag> flags)
        {
            RomInfo flag = RomInfo.None;
            
            foreach (var maybeFlag in flags)
            {
                flag |= maybeFlag switch
                {
                    TextTag { Text: "Unl", Category: TagCategory.Parenthesized } => RomInfo.Unlicensed,
                    TextTag { Text: "b", Category: TagCategory.Bracketed } => RomInfo.BadDump,
                    TextTag { Text: "BIOS", Category: TagCategory.Bracketed } => RomInfo.BIOS,
                    _ => RomInfo.None,
                };
            }
            return flag;
        }
    }
}

//static InfoFlagParser ParseUnlicensed = InParens(String("Unl")).ThenReturn(RomInfo.Unlicensed);
//static InfoFlagParser ParseProto = InParens(String("Proto")).ThenReturn(RomInfo.Prototype);
//static InfoFlagParser ParseKiosk = InParens(String("Kiosk")).ThenReturn(RomInfo.Kiosk);
//static InfoFlagParser ParseDemo = InParens(String("Demo")).ThenReturn(RomInfo.Demo);
//static InfoFlagParser ParseBonus = InParens(String("Bonus Disc")).ThenReturn(RomInfo.Bonus);
//static InfoFlagParser ParseTaikenban = InParens(String("Taikenban")).ThenReturn(RomInfo.Demo);
//static InfoFlagParser ParseTentouTaikenban = InParens(String("Tentou Taikenban")).ThenReturn(RomInfo.Kiosk);
