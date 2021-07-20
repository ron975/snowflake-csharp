using Pidgin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pidgin;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;
using static Pidgin.Parser<char, string>;
using StringParser = Pidgin.Parser<char, string>;
using Snowflake.Romfile.Naming.NoIntroParser;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace Snowflake.Romfile.Naming
{
    internal static class CommonParsers
    {
      
        public static StringParser OpenParens = String("(");
        public static StringParser CloseParens = String(")");
        public static Parser<char, T> InParens<T>(Parser<char, T> inner) =>
            inner.Between(OpenParens, CloseParens);

        public static StringParser OpenBracket = String("[");
        public static StringParser CloseBracket = String("]");

        public static StringParser TakeUntil<T>(Parser<char, T> inner) => Any.AtLeastOnceUntil(Lookahead(inner)).Select(s => string.Concat(s));
        public static StringParser InBrackets(StringParser inner) =>
            inner.Between(OpenBracket, CloseBracket);


        private static IFilenameParser noIntroNameParser = new NoIntroNameParser();
        public static bool TryParseFileName(string filename, bool stripFileext, [NotNullWhen(true)] out NameInfo? nameInfo)
        {
            if (stripFileext)
                filename = Path.GetFileNameWithoutExtension(filename).Trim();

            if (noIntroNameParser.TryParse(filename, out nameInfo))
            {
                return true;
            }
            return false;
        }
    }
}
