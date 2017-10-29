using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using BCL.WPF;
using Hirakanji.Objects;
using Hirakanji.Services;
using Hirakata.Services;

namespace Hirakanji.ViewModels
{
    public class HiraKanjiViewModel : ViewModel
    {
        #region Fields

        // ReSharper disable once NotAccessedField.Local
        private readonly Window _ownerWindow;
        private readonly List<Kanji> _kanjiList;
        private Kanji _activeKanji;
        private DispatcherTimer _timer;
        private int _rankedTimer;

        private string _iconPath;
        private string _userInput;
        private bool _isOrder;
        private bool _isRandom;
        private bool _isReadOnly;
        private bool _isRanked;
        private bool _isInvisible;
        private bool _isKana;
        private bool _isRomaji;
        private bool _isKanaShown;
        private bool _isRomajiShown;
        private int _points;
        private int _time;
        private int _rightCounter;
        private int _wrongCounter;

        private const int GameWrongAnswerPointMalus = 50;
        private const int GameTotalTicks = 300;
        private const int GameTicksToAnswer = 10;
        private const double GameTicksPerSecond = 2;

        #endregion


        #region Constructors

        public HiraKanjiViewModel(Window ownerWindow)
        {
            _ownerWindow = ownerWindow;
            LoadOptions();
            _kanjiList = KanjiBuilder.BuildCompleteKanjiList();
            ActiveKanji = _kanjiList[0];
            TimerStart();
        }

        #endregion




        #region Properties

        public string UserInput
        {
            get { return _userInput; }
            set
            {
                _userInput = value;
                PropertyChange("UserInput");
            }
        }

        public bool IsOrder
        {
            get { return _isOrder; }
            set
            {
                _isOrder = value;
                PropertyChange("IsOrder");
            }
        }

        public bool IsRandom
        {
            get { return _isRandom; }
            set
            {
                _isRandom = value;
                PropertyChange("IsRandom");
            }
        }

        public bool IsRanked
        {
            get { return _isRanked; }
            set
            {
                _isRanked = value;
                PropertyChange("IsRanked");
                if (value)
                {
                    _rankedTimer = 0;
                    Points = 0;
                    Time = 0;
                    RightCounter = 0;
                    WrongCounter = 0;
                    Time = GameTicksToAnswer + 1;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                PropertyChange("IsReadOnly");
                if (IsKana) IsKanaShown = value;
                if (IsRomaji) IsRomajiShown = value;
            }
        }

        public bool IsInvisible
        {
            get { return _isInvisible; }
            set
            {
                _isInvisible = value;
                PropertyChange("IsInvisible");
            }
        }

        public Kanji ActiveKanji
        {
            get { return _activeKanji; }
            set
            {
                _activeKanji = value;
                PropertyChange("ActiveKanji");
                ActiveKanjiChange();
            }
        }

        public string IconPath
        {
            get { return _iconPath; }
            set
            {
                _iconPath = value;
                PropertyChange("IconPath");
            }
        }

        public int Points
        {
            get { return _points; }
            set
            {
                _points = value; 
                PropertyChange("Points");
            }
        }

        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                PropertyChange("Time");
            }
        }

        public int RightCounter
        {
            get { return _rightCounter; }
            set
            {
                if (!IsRanked || Time != GameTicksToAnswer + 1) _rightCounter = value;
                PropertyChange("RightCounter");
            }
        }

        public int WrongCounter
        {
            get { return _wrongCounter; }
            set
            {
                if (!IsRanked || Time != GameTicksToAnswer + 1) _wrongCounter = value;
                PropertyChange("WrongCounter");
            }
        }

        public bool IsKana
        {
            get { return _isKana; }
            set
            {
                _isKana = value;
                if (value && IsRomajiShown)
                {
                    IsRomajiShown = false;
                    IsKanaShown = true;
                }
                PropertyChange("IsKana");
            }
        }

        public bool IsRomaji
        {
            get { return _isRomaji; }
            set
            {
                _isRomaji = value;
                if (value && IsKanaShown)
                {
                    IsRomajiShown = true;
                    IsKanaShown = false;
                }
                PropertyChange("IsRomaji");
            }
        }

        public bool IsKanaShown
        {
            get { return _isKanaShown; }
            set
            {
                _isKanaShown = value;
                PropertyChange("IsKanaShown");
            }
        }

        public bool IsRomajiShown
        {
            get { return _isRomajiShown; }
            set
            {
                _isRomajiShown = value;
                PropertyChange("IsRomajiShown");
            }
        }

        #endregion



        private void NewKanji()
        {
            IconPath = "";
            UserInput = "";

            if (IsOrder)
            {
                ActiveKanji = _kanjiList.LastIndexOf(ActiveKanji) == _kanjiList.Count - 1 ? _kanjiList[0] : _kanjiList[_kanjiList.LastIndexOf(ActiveKanji) + 1];
            }
            else if (IsRandom)
            {
                Random rand = new Random();
                int number = rand.Next(0, _kanjiList.Count);
                if (number == _kanjiList.LastIndexOf(ActiveKanji)) number = rand.Next(0, _kanjiList.Count);
                ActiveKanji = _kanjiList[number];
            }

            if (IsRanked && _rankedTimer < GameTotalTicks)
            {
                TimerReset();
            }

            if (IsReadOnly)
            {
                if (IsKana) IsKanaShown = true;
                if (IsRomaji) IsRomajiShown = true;
            }
            else
            {
                IsKanaShown = false;
                IsRomajiShown = false;
            }
        }

        private void ActiveKanjiChange()
        {
            //SignPath = ActiveKanji.KanjiSign;
        }



        private void TimerStart()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan((long)(10000000 / GameTicksPerSecond));
            _timer.Tick += TimerOnTick;
            _timer.Start();
        }

        private void TimerReset()
        {
            Time = GameTicksToAnswer;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(IconPath) && Time != GameTicksToAnswer + 1 && IsRanked)
            {
                _rankedTimer++;

                if (_rankedTimer >= GameTotalTicks)
                {
                    IsRanked = false;
                    MessageBox.Show(string.Format("Congratulations! You have got {0} Points.", Points), "Ranked is over",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    //ShowHighscore(true);
                }



                if (Time > 0)
                {
                    Time--;
                }
                else if (Time == 0)
                {
                    Points -= GameWrongAnswerPointMalus;
                    WrongCounter++;
                    NewKanji();
                }
            }
        }




        public void CheckUserInput()
        {
            if (string.IsNullOrWhiteSpace(IconPath))
            { 
                if (UserInput == ActiveKanji.OnRomaji.ToUpper() || 
                    UserInput == ActiveKanji.OnKata             ||
                    UserInput == ActiveKanji.KunRomaji.ToUpper()||
                    UserInput == ActiveKanji.KunHira            ||
                    (ActiveKanji.German.ToUpper().Contains(UserInput) && UserInput.Length > 2))
                {
                    // Richtig
                    IconPath = KanaUris.CorrectIconUri;
                    RightCounter++;
                    if (!IsReadOnly)
                    {
                        IsKanaShown = IsKana;
                        IsRomajiShown = IsRomaji;
                    }

                    if (Time != GameTicksToAnswer + 1) Points += Time;
                }
                else
                {
                    // Falsch
                    IconPath = KanaUris.WrongIconUri;
                    WrongCounter++;
                    if (!IsReadOnly)
                    {
                        IsKanaShown = IsKana;
                        IsRomajiShown = IsRomaji;
                    }

                    if (Time != GameTicksToAnswer + 1) Points -= GameWrongAnswerPointMalus;
                }
            }
            else
            {
                NewKanji();
            }
        }

        public void NextKanjiButton()
        {

            if (string.IsNullOrWhiteSpace(IconPath)) WrongCounter++;
            if (Time != GameTicksToAnswer + 1) Points -= GameWrongAnswerPointMalus;
            NewKanji();
        }

        public void SaveOptions()
        {
            // Save Options
        }

        private void LoadOptions()
        {
            // Load Options
            IsKana = true;
            IsRandom = true;
        }
    }
}
