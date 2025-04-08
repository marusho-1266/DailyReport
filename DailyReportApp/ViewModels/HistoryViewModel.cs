using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DailyReportApp.Models;
using DailyReportApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace DailyReportApp.ViewModels
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DailyReport> _dailyReports = new ObservableCollection<DailyReport>();
        public ObservableCollection<DailyReport> DailyReports
        {
            get { return _dailyReports; }
            set
            {
                _dailyReports = value;
                OnPropertyChanged("DailyReports");
            }
        }

        private List<DateTime> _reportDates = new List<DateTime>();
        public List<DateTime> ReportDates
        {
            get { return _reportDates; }
            set
            {
                _reportDates = value;
                OnPropertyChanged("ReportDates");
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
                LoadDailyReports();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HistoryViewModel()
        {
            LoadReportDates();
            LoadDailyReports();
        }

        private async void LoadDailyReports()
        {
            var databaseService = new DatabaseService();
            var reports = await databaseService.GetDailyReportsAsync(SelectedDate);

            DailyReports.Clear();
            foreach (var report in reports)
            {
                DailyReports.Add(report);
            }
        }

        private async void LoadReportDates()
        {
            var databaseService = new DatabaseService();
            var reports = await databaseService.GetAllDailyReportsAsync();

            ReportDates = reports.Select(r => r.Date).Distinct().ToList();
            OnPropertyChanged("ReportDates");
        }
    }
}
