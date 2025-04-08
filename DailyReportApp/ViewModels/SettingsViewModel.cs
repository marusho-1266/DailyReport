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
using DailyReportApp.Properties;
using DailyReportApp;

namespace DailyReportApp.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string _chatworkApiKey;
        public string ChatworkApiKey
        {
            get { return _chatworkApiKey; }
            set
            {
                _chatworkApiKey = value;
                OnPropertyChanged("ChatworkApiKey");
            }
        }

        private string _chatworkRoomId;
        public string ChatworkRoomId
        {
            get { return _chatworkRoomId; }
            set
            {
                _chatworkRoomId = value;
                OnPropertyChanged("ChatworkRoomId");
            }
        }

        private string _reportTemplate;
        public string ReportTemplate
        {
            get { return _reportTemplate; }
            set
            {
                _reportTemplate = value;
                OnPropertyChanged("ReportTemplate");
            }
        }

        private TimeSpan _dailyReminderTime;
        public TimeSpan DailyReminderTime
        {
            get { return _dailyReminderTime; }
            set
            {
                _dailyReminderTime = value;
                OnPropertyChanged("DailyReminderTime");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveSettingsCommand { get; private set; }

        public SettingsViewModel()
        {
            SaveSettingsCommand = new RelayCommand(SaveSettings);

            // 設定を読み込む
            ChatworkApiKey = Settings.Default.ChatworkApiKey;
            ChatworkRoomId = Settings.Default.ChatworkRoomId;
        }

        private void SaveSettings()
        {
            // 設定を保存
            Settings.Default.ChatworkApiKey = ChatworkApiKey;
            Settings.Default.ChatworkRoomId = ChatworkRoomId;
            Settings.Default.Save();

            MessageBox.Show("設定を保存しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
