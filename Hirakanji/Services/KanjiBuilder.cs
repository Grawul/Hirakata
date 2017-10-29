using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Hirakanji.Objects;

namespace Hirakanji.Services
{
    public class KanjiBuilder
    {
        public static List<Kanji> BuildCompleteKanjiList()
        {
            List<Kanji> kanjis = new List<Kanji>();

            try
            {
                string[] allLines = File.ReadAllLines("Kanji.txt");

                foreach (string line in allLines)
                {
                    string[] split = line.Split(new []{"\t"}, StringSplitOptions.RemoveEmptyEntries);

                    Kanji newKanji = new Kanji();
                    newKanji.KanjiSign = split[0];
                    newKanji.OnKata = split[1].Replace(" ","");
                    newKanji.OnRomaji = split[2].Replace(" ", "");
                    newKanji.KunHira = split[3].Replace(" ", "");
                    newKanji.KunRomaji = split[4].Replace(" ", "");
                    newKanji.Chinese = split[5].Replace(" ", "");
                    newKanji.German = split[6];

                    kanjis.Add(newKanji);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kanjis konnten nicht geladen werden.", "Laden hat nicht geklappt", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return kanjis;
        }
    }
}
