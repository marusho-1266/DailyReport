using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DailyReportApp.ViewModels;
using DailyReportApp.Models;

namespace DailyReportApp.Views
{
    /// <summary>
    /// HistoryWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            DataContext = new HistoryViewModel();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is DailyReport selectedReport)
            {
                // メインウィンドウのDataContextを取得
                if (Application.Current.MainWindow?.DataContext is MainViewModel mainViewModel)
                {
                    // 選択された日報の内容をメインウィンドウに反映
                    mainViewModel.Tasks = selectedReport.Tasks;
                    mainViewModel.Results = selectedReport.Results;
                    mainViewModel.TomorrowPlan = selectedReport.TomorrowPlan;
                    mainViewModel.MonthlyReview = selectedReport.MonthlyReview;
                }

                // 履歴ウィンドウを閉じる
                this.Close();
            }
        }
    }
}
