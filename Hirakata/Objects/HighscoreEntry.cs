using System;
using Hirakata.BCL;

namespace Hirakata.Objects
{
    [Serializable]
    public class HighscoreEntry : ObservableObject
    {

        #region Fields

        private string _name;
        private int _points;
        private DateTime _date;

        #endregion


        #region Constructors

        public HighscoreEntry(string name, int points, DateTime date)
        {
            Name = name;
            Points = points;
            Date = date;
        }

        #endregion



        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChange("Name");
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

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChange("Date");
            }
        }

        #endregion

        
    }
}
