using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using DailyReportApp.Models;
using DailyReportApp.Services;
using DailyReportApp;

namespace DailyReportApp.ViewModels
{
    public class TemplateViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ReportTemplate> _reportTemplates = new ObservableCollection<ReportTemplate>();
        public ObservableCollection<ReportTemplate> ReportTemplates
        {
            get { return _reportTemplates; }
            set
            {
                _reportTemplates = value;
                OnPropertyChanged("ReportTemplates");
            }
        }

        private ReportTemplate _selectedReportTemplate;
        public ReportTemplate SelectedReportTemplate
        {
            get { return _selectedReportTemplate; }
            set
            {
                _selectedReportTemplate = value;
                OnPropertyChanged("SelectedReportTemplate");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddTemplateCommand { get; private set; }
        public ICommand EditTemplateCommand { get; private set; }
        public ICommand DeleteTemplateCommand { get; private set; }

        public TemplateViewModel()
        {
            AddTemplateCommand = new RelayCommand(AddTemplate);
            EditTemplateCommand = new RelayCommand(EditTemplate);
            DeleteTemplateCommand = new RelayCommand(DeleteTemplate);

            LoadReportTemplates();
        }

        private async void LoadReportTemplates()
        {
            var templateService = new TemplateService();
            var templates = await templateService.GetReportTemplatesAsync();

            ReportTemplates.Clear();
            foreach (var template in templates)
            {
                ReportTemplates.Add(template);
            }
        }

        private async void AddTemplate()
        {
            var template = new ReportTemplate { Name = "新しいテンプレート" }; // 仮のテンプレート
            var templateService = new TemplateService();
            bool isSuccess = await templateService.AddReportTemplateAsync(template);
            if (isSuccess)
            {
                LoadReportTemplates();
                MessageBox.Show("テンプレートを追加しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("テンプレートの追加に失敗しました。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditTemplate()
        {
            if (SelectedReportTemplate == null)
            {
                MessageBox.Show("テンプレートを選択してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var templateService = new TemplateService();
            bool isSuccess = await templateService.UpdateReportTemplateAsync(SelectedReportTemplate);
            if (isSuccess)
            {
                LoadReportTemplates();
                MessageBox.Show("テンプレートを編集しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("テンプレートの編集に失敗しました。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteTemplate()
        {
            if (SelectedReportTemplate == null)
            {
                MessageBox.Show("テンプレートを選択してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var templateService = new TemplateService();
            bool isSuccess = await templateService.DeleteReportTemplateAsync(SelectedReportTemplate.Id);
            if (isSuccess)
            {
                LoadReportTemplates();
                MessageBox.Show("テンプレートを削除しました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("テンプレートの削除に失敗しました。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
