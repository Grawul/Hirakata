namespace Hirakanji.Objects
{
    public class Kanji
    {
        #region Fields

        private string _kanjiSign;
        private string _onKata;
        private string _onRomaji;
        private string _kunHira;
        private string _kunRomaji;
        private string _chinese;
        private string _german;

        #endregion


        #region Constructors

        public Kanji()
        {

        }

        public Kanji(string kanjiSign, string onKata, string onRomaji, string kunHira, string kunRomaji, string chinese, string german)
        {
            KanjiSign = kanjiSign;
            OnKata = onKata;
            OnRomaji = onRomaji;
            KunHira = kunHira;
            KunRomaji = kunRomaji;
            Chinese = chinese;
            German = german;
        }

        #endregion



        #region Properties

        public string KanjiSign
        {
            get { return _kanjiSign; }
            set { _kanjiSign = value; }
        }

        public string OnKata
        {
            get { return _onKata; }
            set { _onKata = value; }
        }

        public string OnRomaji
        {
            get { return _onRomaji; }
            set { _onRomaji = value; }
        }

        public string KunHira
        {
            get { return _kunHira; }
            set { _kunHira = value; }
        }

        public string KunRomaji
        {
            get { return _kunRomaji; }
            set { _kunRomaji = value; }
        }

        public string Chinese
        {
            get { return _chinese; }
            set { _chinese = value; }
        }

        public string German
        {
            get { return _german; }
            set { _german = value; }
        }

        #endregion

    }
}
