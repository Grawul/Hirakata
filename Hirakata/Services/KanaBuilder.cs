using System.Collections.Generic;
using Hirakata.Objects;

namespace Hirakata.Services
{
    public class KanaBuilder
    {
        public static List<Kana> BuildCompleteKanaList()
        {
            List<Kana> kanas = new List<Kana>();

            kanas.Add(new Kana("a", 1));
            kanas.Add(new Kana("i", 2));
            kanas.Add(new Kana("u", 3));
            kanas.Add(new Kana("e", 4));
            kanas.Add(new Kana("o", 5));

            kanas.Add(new Kana("ka", 6));
            kanas.Add(new Kana("ki", 7));
            kanas.Add(new Kana("ku", 8));
            kanas.Add(new Kana("ke", 9));
            kanas.Add(new Kana("ko", 10));

            kanas.Add(new Kana("sa", 11));
            kanas.Add(new Kana("shi", 12));
            kanas.Add(new Kana("su", 13));
            kanas.Add(new Kana("se", 14));
            kanas.Add(new Kana("so", 15));

            kanas.Add(new Kana("ta", 16));
            kanas.Add(new Kana("chi", 17));
            kanas.Add(new Kana("tsu", 18));
            kanas.Add(new Kana("te", 19));
            kanas.Add(new Kana("to", 20));

            kanas.Add(new Kana("na", 21));
            kanas.Add(new Kana("ni", 22));
            kanas.Add(new Kana("nu", 23));
            kanas.Add(new Kana("ne", 24));
            kanas.Add(new Kana("no", 25));

            kanas.Add(new Kana("ha", 26));
            kanas.Add(new Kana("hi", 27));
            kanas.Add(new Kana("fu", 28));
            kanas.Add(new Kana("he", 29));
            kanas.Add(new Kana("ho", 30));

            kanas.Add(new Kana("ma", 31));
            kanas.Add(new Kana("mi", 32));
            kanas.Add(new Kana("mu", 33));
            kanas.Add(new Kana("me", 34));
            kanas.Add(new Kana("mo", 35));

            kanas.Add(new Kana("ya", 36));
            kanas.Add(new Kana("yu", 37));
            kanas.Add(new Kana("yo", 38));

            kanas.Add(new Kana("ra", 39));
            kanas.Add(new Kana("ri", 40));
            kanas.Add(new Kana("ru", 41));
            kanas.Add(new Kana("re", 42));
            kanas.Add(new Kana("ro", 43));

            kanas.Add(new Kana("wa", 44));
            kanas.Add(new Kana("wo", 45));

            kanas.Add(new Kana("n", 46));


            // Dakuten
            kanas.Add(new Kana("ka", 47, KanaUris.Dakuten, "ga"));
            kanas.Add(new Kana("ki", 48, KanaUris.Dakuten, "gi"));
            kanas.Add(new Kana("ku", 49, KanaUris.Dakuten, "gu"));
            kanas.Add(new Kana("ke", 50, KanaUris.Dakuten, "ge"));
            kanas.Add(new Kana("ko", 51, KanaUris.Dakuten, "go"));

            kanas.Add(new Kana("sa", 52, KanaUris.Dakuten, "za"));
            kanas.Add(new Kana("shi", 53, KanaUris.Dakuten, "ji"));
            kanas.Add(new Kana("su", 54, KanaUris.Dakuten, "zu"));
            kanas.Add(new Kana("se", 55, KanaUris.Dakuten, "ze"));
            kanas.Add(new Kana("so", 56, KanaUris.Dakuten, "zo"));

            kanas.Add(new Kana("ta", 58, KanaUris.Dakuten, "da"));
            kanas.Add(new Kana("chi", 58, KanaUris.Dakuten, "ji"));
            kanas.Add(new Kana("tsu", 59, KanaUris.Dakuten, "dsu"));
            kanas.Add(new Kana("te", 60, KanaUris.Dakuten, "de"));
            kanas.Add(new Kana("to", 61, KanaUris.Dakuten, "do"));

            kanas.Add(new Kana("ha", 62, KanaUris.Dakuten, "ba"));
            kanas.Add(new Kana("hi", 63, KanaUris.Dakuten, "bi"));
            kanas.Add(new Kana("fu", 64, KanaUris.Dakuten, "bu"));
            kanas.Add(new Kana("he", 65, KanaUris.Dakuten, "be"));
            kanas.Add(new Kana("ho", 66, KanaUris.Dakuten, "bo"));

            kanas.Add(new Kana("ha", 67, KanaUris.Handakuten, "pa"));
            kanas.Add(new Kana("hi", 68, KanaUris.Handakuten, "pi"));
            kanas.Add(new Kana("fu", 69, KanaUris.Handakuten, "pu"));
            kanas.Add(new Kana("he", 70, KanaUris.Handakuten, "pe"));
            kanas.Add(new Kana("ho", 71, KanaUris.Handakuten, "po"));


            return kanas;
        }
    }
}
