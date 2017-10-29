using System.ComponentModel;
using System.Windows;
using Hirakata.ViewModels;

namespace Hirakata.Windows
{
    /// <summary>
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore
    {
        public Highscore()
        {
            InitializeComponent();
        }

        private void Highscore_OnClosing(object sender, CancelEventArgs e)
        {
            HighscoreViewModel hvm = DataContext as HighscoreViewModel;
            if (hvm == null) return;
            hvm.SerializeHighscore();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
