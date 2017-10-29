namespace Hirakata.Services
{
    public class KanaUris
    {
        public static string HiraganaUri(string romanji, bool only)
        {
            if (only) return string.Format("pack://application:,,,/Hirakata;Component/Resources/Trans Only Hiragana/trans only hira {0}.png", romanji);
            return string.Format("pack://application:,,,/Hirakata;Component/Resources/Trans Hiragana/trans hira {0}.png", romanji);
        }

        public static string KatakanaUri(string romanji, bool only)
        {
            if (only) return string.Format("pack://application:,,,/Hirakata;Component/Resources/Trans Only Katakana/trans only kata {0}.png", romanji);
            return string.Format("pack://application:,,,/Hirakata;Component/Resources/Trans Katakana/trans kata {0}.png", romanji);
        }

        public const string Dakuten = "pack://application:,,,/Hirakata;Component/Resources/trans dakuten.png";
        public const string Handakuten = "pack://application:,,,/Hirakata;Component/Resources/trans handakuten.png";

        public const string CorrectIconUri = "pack://application:,,,/Hirakata;Component/Resources/correct.ico";
        public const string WrongIconUri = "pack://application:,,,/Hirakata;Component/Resources/wrong.ico";
    }
}
