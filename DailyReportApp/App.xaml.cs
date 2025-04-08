using System.Configuration;
using System.Configuration;
using System.Data;
using System.Windows;
using DailyReportApp.Properties;
using NLog;
using NLog.Config;
using System.IO;

namespace DailyReportApp
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ログ設定ファイルの読み込み
            try
            {
                LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
                logger.Info("アプリケーション起動");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ログ設定ファイルの読み込みに失敗しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // 設定ファイルの読み込み
            Settings.Default.Reload();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            LogManager.Shutdown();
        }
    }
}
