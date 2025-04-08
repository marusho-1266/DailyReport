using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Shapes;
using DailyReportApp.ViewModels;
using DailyReportApp.Services;
using DailyReportApp.Models;

namespace DailyReportApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new Views.SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new Views.HistoryWindow();
            historyWindow.ShowDialog();
        }

        // TemplateButton_Click メソッドを削除
        // private void TemplateButton_Click(object sender, RoutedEventArgs e)
        // {
        //     var templateWindow = new Views.TemplateWindow();
        //     templateWindow.ShowDialog();
        // }

        // LoadPastReportButton_Click メソッドを削除
        // private async void LoadPastReportButton_Click(object sender, RoutedEventArgs e)
        // {
        //     var historyWindow = new Views.HistoryWindow();
        //     historyWindow.ShowDialog();
        // }
    }
}
