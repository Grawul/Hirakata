using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Hirakata.BCL;
using Hirakata.Objects;
using Hirakata.Services;
using Hirakata.Windows;

namespace Hirakata.ViewModels
{
    public class HiraKataViewModel : ObservableObject
    {

        #region Fields

        private readonly Window _ownerWindow;
        private readonly List<Kana> _kanaList;
        private Kana _activeKana;
        private DispatcherTimer _timer;
        private int _rankedTimer;

        private string _signPath;
        private string _dakutenPath;
        private string _signPathIcon;
        private string _iconPath;
        private string _userInput;
        private bool _isKatakana;
        private bool _isHiragana;
        private bool _isMixed;
        private bool _isOrder;
        private bool _isRandom;
        private bool _strokeOrder;
        private bool _isDakuten;
        private bool _isRanked;
        private bool _isInvisible;
        private int _points;
        private int _time;
        private int _rightCounter;
        private int _wrongCounter;

        private string _notificationText;

        private const int GameWrongAnswerPointMalus = 50;
        private const int GameTotalTicks = 300;
        private const int GameTicksToAnswer = 10;
        private const double GameTicksPerSecond = 2;

        #endregion


        #region Constructors

        public HiraKataViewModel(Window ownerWindow)
        {
            _ownerWindow = ownerWindow;
            LoadOptions();
            _kanaList = KanaBuilder.BuildCompleteKanaList();
            ActiveKana = _kanaList[0];
            TimerStart();
        }

        #endregion




        #region Properties

        public string SignPath
        {
            get { return _signPath; }
            set
            {
                _signPath = value; 
                PropertyChange("SignPath");
            }
        }

        public string DakutenPath
        {
            get { return _dakutenPath; }
            set
            {
                _dakutenPath = value;
                PropertyChange("DakutenPath");
            }
        }

        public string SignPathIcon
        {
            get { return _signPathIcon; }
            set
            {
                _signPathIcon = value;
                PropertyChange("SignPathIcon");
            }
        }

        public string UserInput
        {
            get { return _userInput; }
            set
            {
                _userInput = value;
                PropertyChange("UserInput");
            }
        }

        public bool IsKatakana
        {
            get { return _isKatakana; }
            set
            {
                _isKatakana = value;
                PropertyChange("IsKatakana");
                ShowKana(ActiveKana);
            }
        }

        public bool IsHiragana
        {
            get { return _isHiragana; }
            set
            {
                _isHiragana = value;
                PropertyChange("IsHiragana");
                ShowKana(ActiveKana);
            }
        }

        public bool IsMixed
        {
            get { return _isMixed; }
            set
            {
                _isMixed = value;
                PropertyChange("IsMixed");
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

        public bool StrokeOrder
        {
            get { return _strokeOrder; }
            set
            {
                _strokeOrder = value;
                PropertyChange("StrokeOrder");
                ShowKana(ActiveKana);
            }
        }

        public bool IsDakuten
        {
            get { return _isDakuten; }
            set
            {
                _isDakuten = value;
                PropertyChange("IsDakuten");
                if (value) IsInvisible = false;
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

        public bool IsInvisible
        {
            get { return _isInvisible; }
            set
            {
                _isInvisible = value;
                PropertyChange("IsInvisible");
                if (value) IsDakuten = false;
            }
        }
        public Kana ActiveKana
        {
            get { return _activeKana; }
            set
            {
                _activeKana = value;
                ShowKana(value);
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

        public string NotificationText
        {
            get { return _notificationText; }
            set
            {
                _notificationText = value;
                PropertyChange("NotificationText");
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

        

        

        #endregion


        #region Buttons

        public void CheckUserInput()
        {
            if (string.IsNullOrWhiteSpace(IconPath))
            {
                if (UserInput == ActiveKana.Romaji.ToUpper())
                {
                    // Richtig
                    IconPath = KanaUris.CorrectIconUri;
                    NotificationText = "Correct";
                    RightCounter++;

                    if (Time != GameTicksToAnswer + 1) Points += Time;
                }
                else
                {
                    // Falsch
                    IconPath = KanaUris.WrongIconUri;
                    NotificationText = string.Format("Wrong. {0}", ActiveKana.Romaji.ToUpper());
                    WrongCounter++;

                    if (Time != GameTicksToAnswer + 1) Points -= GameWrongAnswerPointMalus;
                }
            }
            else
            {
                NewKana();
            }
        }


        public void NextKanaButton()
        {
            if (string.IsNullOrWhiteSpace(IconPath)) WrongCounter++;
            if (Time != GameTicksToAnswer + 1) Points -= GameWrongAnswerPointMalus;
            NewKana();
        }

        private void NewKana()
        {
            IconPath = "";
            NotificationText = "";
            UserInput = "";

            int maximum = 46;
            if (IsDakuten) maximum = 71;


            if (IsOrder)
            {
                ActiveKana = ActiveKana.Numeration == maximum ? _kanaList[0] : _kanaList[ActiveKana.Numeration];
            }
            else if (IsRandom)
            {
                Random rand = new Random();
                int number = rand.Next(0, maximum);
                if (number == ActiveKana.Numeration - 1) number = rand.Next(0, maximum);
                ActiveKana = _kanaList[number];
            }

            if (IsRanked && _rankedTimer < GameTotalTicks)
            {
                TimerReset();
            }
        }


        private void ShowKana(Kana kana)
        {
            if (kana == null) return;

            if (StrokeOrder)
            {
                if (IsKatakana)
                {
                    SignPath = kana.KatakanaPath;
                    SignPathIcon = kana.KatakanaOnlyPath;
                }
                else if (IsHiragana)
                {
                    SignPath = kana.HiraganaPath;
                    SignPathIcon = kana.HiraganaOnlyPath;
                }
                else if (IsMixed)
                {
                    Random rand = new Random();
                    if (rand.Next(0, 2) == 1)
                    {
                        SignPath = kana.KatakanaPath;
                        SignPathIcon = kana.KatakanaOnlyPath;
                    }
                    else
                    {
                        SignPath = kana.HiraganaPath;
                        SignPathIcon = kana.HiraganaOnlyPath;
                    }
                }
            }
            else
            {
                if (IsKatakana)
                {
                    SignPath = kana.KatakanaOnlyPath;
                    SignPathIcon = kana.KatakanaOnlyPath;
                }
                else if (IsHiragana)
                {
                    SignPath = kana.HiraganaOnlyPath;
                    SignPathIcon = kana.HiraganaOnlyPath;
                }
                else if (IsMixed)
                {
                    Random rand = new Random();
                    if (rand.Next(0, 2) == 1)
                    {
                        SignPath = kana.KatakanaOnlyPath;
                        SignPathIcon = kana.KatakanaOnlyPath;
                    }
                    else
                    {
                        SignPath = kana.HiraganaOnlyPath;
                        SignPathIcon = kana.HiraganaOnlyPath;
                    }
                }
            }

            DakutenPath = kana.DakutenPath;

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
                    ShowHighscore(true);
                }



                if (Time > 0)
                {
                    Time--;
                }
                else if (Time == 0)
                {
                    Points -= GameWrongAnswerPointMalus;
                    WrongCounter++;
                    NewKana();
                }
            }
        }

        public void ShowHighscore(bool isNewEntry = false)
        {
            Highscore highscore = new Highscore();
            HighscoreViewModel hvm = new HighscoreViewModel();
            highscore.DataContext = hvm;
            highscore.Owner = _ownerWindow;

            if (isNewEntry)
            {
                hvm.HighscoreList.Add(new HighscoreEntry(Environment.UserName, Points, DateTime.Now));
                hvm.SortHighscore();
            }

            highscore.ShowDialog();
        }



        public void SaveOptions()
        {
            Properties.Settings.Default.IsKatakana = IsKatakana;
            Properties.Settings.Default.IsHiragana = IsHiragana;
            Properties.Settings.Default.IsMixed = IsMixed;
            Properties.Settings.Default.IsOrder = IsOrder;
            Properties.Settings.Default.IsRandom = IsRandom;
            Properties.Settings.Default.IsDakuten = IsDakuten;
            Properties.Settings.Default.IsStrokeOrder = StrokeOrder;
            Properties.Settings.Default.IsInvisible = IsInvisible;
            Properties.Settings.Default.IsRanked = IsRanked;

            Properties.Settings.Default.Save();
        }


        private void LoadOptions()
        {
            IsKatakana = Properties.Settings.Default.IsKatakana;
            IsHiragana = Properties.Settings.Default.IsHiragana;
            IsMixed = Properties.Settings.Default.IsMixed;
            IsOrder = Properties.Settings.Default.IsOrder;
            IsRandom = Properties.Settings.Default.IsRandom;
            IsDakuten = Properties.Settings.Default.IsDakuten;
            StrokeOrder = Properties.Settings.Default.IsStrokeOrder;
            IsInvisible = Properties.Settings.Default.IsInvisible;
            IsRanked = Properties.Settings.Default.IsRanked;
        }

        #endregion

    }
}
