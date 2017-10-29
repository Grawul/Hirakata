using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Hirakata.BCL;
using Hirakata.Objects;

namespace Hirakata.ViewModels
{
    public class HighscoreViewModel : ObservableObject
    {
        #region Fields

        private BindingList<HighscoreEntry> _highscoreList;

        private readonly string _path;

        #endregion


        #region Constructors

        public HighscoreViewModel()
        {
            _path = string.Format(@"{0}\Highscore.hs", Environment.CurrentDirectory);
            DeserializeHighscore();
        }

        #endregion


        #region Properties

        public BindingList<HighscoreEntry> HighscoreList
        {
            get { return _highscoreList; }
            set
            {
                _highscoreList = value; 
                PropertyChange("HighscoreList");
            }
        }

        #endregion

        public void SortHighscore()
        {
            List<HighscoreEntry> sortedList = HighscoreList.OrderByDescending(o => o.Points).ToList();

            HighscoreList = new BindingList<HighscoreEntry>();
            foreach (HighscoreEntry highscoreEntry in sortedList)
            {
                HighscoreList.Add(highscoreEntry);
            }
        }


        public void SerializeHighscore()
        {
            FileStream writeStream = null;
            try
            {

                if (HighscoreList.Count == 0) return;
                List<KeyValuePair<string, KeyValuePair<int, DateTime>>> serializeList = new List<KeyValuePair<string, KeyValuePair<int, DateTime>>>();

                foreach (HighscoreEntry highscoreEntry in HighscoreList)
                {
                    serializeList.Add(new KeyValuePair<string, KeyValuePair<int, DateTime>>(highscoreEntry.Name, new KeyValuePair<int, DateTime>(highscoreEntry.Points, highscoreEntry.Date)));
                }

                BinaryFormatter bf = new BinaryFormatter();
                writeStream = new FileStream(_path, FileMode.Create);
                bf.Serialize(writeStream, serializeList);

            }
            finally
            {
                if (writeStream != null) writeStream.Dispose();
            }
        }


        private void DeserializeHighscore()
        {
            HighscoreList = new BindingList<HighscoreEntry>();
            FileStream readStream = null;

            try
            {
                if (File.Exists(_path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    readStream = new FileStream(_path, FileMode.Open);
                    if (readStream.Length == 0) return;

                    List<KeyValuePair<string, KeyValuePair<int, DateTime>>> deserializedList = (List<KeyValuePair<string, KeyValuePair<int, DateTime>>>)bf.Deserialize(readStream);

                    List<HighscoreEntry> unsortedList = deserializedList.Select(keyValuePair => new HighscoreEntry(keyValuePair.Key, keyValuePair.Value.Key, keyValuePair.Value.Value)).ToList();
                    List<HighscoreEntry> sortedList = unsortedList.OrderByDescending(o => o.Points).ToList();

                    foreach (HighscoreEntry highscoreEntry in sortedList)
                    {
                        HighscoreList.Add(highscoreEntry);
                    }
                }
            }
            finally
            {
                if (readStream != null) readStream.Dispose();
            }
        }
    }
}
