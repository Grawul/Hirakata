using Hirakata.Services;

namespace Hirakata.Objects
{
    public class Kana
    {


        #region Fields

        private string _romaji;
        private string _katakanaPath;
        private string _hiraganaPath;
        private string _dakutenPath;
        private string _katakanaOnlyPath;
        private string _hiraganaOnlyPath;
        private int _numeration;

        #endregion


        #region Constructors

        public Kana(string romaji, int numeration, string dakutenPath = "", string dakutenRomaji = "")
        {
            Romaji = romaji;
            Numeration = numeration;

            KatakanaPath = KanaUris.KatakanaUri(romaji, false);
            HiraganaPath = KanaUris.HiraganaUri(romaji, false);
            KatakanaOnlyPath = KanaUris.KatakanaUri(romaji, true);
            HiraganaOnlyPath = KanaUris.HiraganaUri(romaji, true);

            DakutenPath = dakutenPath;
            if (!string.IsNullOrWhiteSpace(dakutenRomaji)) Romaji = dakutenRomaji;
        }

        #endregion



        #region Properties

        public string Romaji
        {
            get { return _romaji; }
            set { _romaji = value; }
        }

        public string KatakanaPath
        {
            get { return _katakanaPath; }
            set { _katakanaPath = value; }
        }

        public string HiraganaPath
        {
            get { return _hiraganaPath; }
            set { _hiraganaPath = value; }
        }

        public string DakutenPath
        {
            get { return _dakutenPath; }
            set { _dakutenPath = value; }
        }

        public string KatakanaOnlyPath
        {
            get { return _katakanaOnlyPath; }
            set { _katakanaOnlyPath = value; }
        }

        public string HiraganaOnlyPath
        {
            get { return _hiraganaOnlyPath; }
            set { _hiraganaOnlyPath = value; }
        }

        public int Numeration
        {
            get { return _numeration; }
            set { _numeration = value; }
        }


        #endregion
    }
}
