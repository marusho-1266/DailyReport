using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using DailyReportApp.Services;
using DailyReportApp.Models;
using DailyReportApp;
using NLog;

namespace DailyReportApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private string _tasks;
        public string Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        private string _results;
        public string Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged("Results");
            }
        }

        private string _tomorrowPlan;
        public string TomorrowPlan
        {
            get { return _tomorrowPlan; }
            set
            {
                _tomorrowPlan = value;
                OnPropertyChanged("TomorrowPlan");
            }
        }

        private string _monthlyReview;
        public string MonthlyReview
        {
            get { return _monthlyReview; }
            set
            {
                _monthlyReview = value;
                OnPropertyChanged("MonthlyReview");
            }
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
                LoadDailyReport();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SendToChatworkCommand { get; private set; }
        public ICommand LoadDailyReportCommand { get; private set; }
        public ICommand SaveDailyReportCommand { get; private set; }

        public MainViewModel()
        {
            SendToChatworkCommand = new RelayCommand(SendToChatwork);
            LoadDailyReportCommand = new RelayCommand(LoadDailyReport);
            SaveDailyReportCommand = new RelayCommand(SaveDailyReport);
        }

        private async void SaveDailyReport()
        {
            var databaseService = new DatabaseService();
            var report = new DailyReport
            {
                Date = SelectedDate,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Tasks = Tasks ?? "",
                Results = Results ?? "",
                TomorrowPlan = TomorrowPlan ?? "",
                MonthlyReview = MonthlyReview ?? "",
                IsSentToChatwork = false,
                SentToChatworkAt = "",
            };

            try
            {
                bool isSuccess = await databaseService.SaveDailyReportAsync(report);

                if (isSuccess)
                {
                    MessageBox.Show("日報を保存しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("日報の保存に失敗しました。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "日報の保存中にエラーが発生しました");
                MessageBox.Show($"日報の保存中にエラーが発生しました。詳細はログファイルを確認してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SendToChatwork()
        {
            // 設定からAPIキーとルームIDを取得
            string apiKey = Properties.Settings.Default.ChatworkApiKey;
            string roomId = Properties.Settings.Default.ChatworkRoomId;

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("APIキーとルームIDを設定してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ChatworkServiceを初期化
            var chatworkService = new ChatworkService(apiKey, roomId);

            // メッセージを作成
            string message = $"1. 今日行ったタスク・かかった時間	◆日報必須◆\n{Tasks}\n\n2. 今日の結果		◆日報必須◆\n{Results}\n\n3. 明日の予定と目標		◆日報必須◆\n{TomorrowPlan}";

            // メッセージを送信
            bool isSuccess = await chatworkService.SendMessage(message);

            if (isSuccess)
            {
                MessageBox.Show("Chatworkに送信しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Chatworkへの送信に失敗しました。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadDailyReport()
        {
            var databaseService = new DatabaseService();
            var reports = await databaseService.GetDailyReportsAsync(SelectedDate);

            if (reports != null && reports.Any())
            {
                var report = reports.First();
                Tasks = report.Tasks;
                Results = report.Results;
                TomorrowPlan = report.TomorrowPlan;
                MonthlyReview = report.MonthlyReview;
            }
            else
            {
                Tasks = "";
                Results = "";
                TomorrowPlan = "";
                MonthlyReview = "";
            }
        }
    }
}
