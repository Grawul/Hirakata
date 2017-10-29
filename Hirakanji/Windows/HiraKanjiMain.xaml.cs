using System;
using System.Windows;
using Hirakanji.ViewModels;

namespace Hirakanji.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _hkvm = new HiraKanjiViewModel(this);
            DataContext = _hkvm;
            InputBox.Focus();
        }

        private readonly HiraKanjiViewModel _hkvm;


        private void Check_OnClick(object sender, RoutedEventArgs e)
        {
            _hkvm.CheckUserInput();
        }

        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
            _hkvm.NextKanjiButton();
        }

        private void OpenHighScore_OnClick(object sender, RoutedEventArgs e)
        {
            //_hkvm.ShowHighscore();
        }

        private void SaveOption_OnClick(object sender, RoutedEventArgs e)
        {
            _hkvm.SaveOptions();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
