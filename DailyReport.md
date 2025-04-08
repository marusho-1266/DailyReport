# 日報管理アプリケーション仕様書

## 1. 概要

Windowsデスクトップ向けの日報管理アプリケーションを開発する。ユーザーが日報データを入力し、ローカルDBに保存、Chatworkに連携する機能を備えたアプリケーションとする。

## 2. 技術スタック

- **開発言語**: C#
- **フレームワーク**: 
  - WPF
- **データベース**: SQLite + Entity Framework Core
- **外部API連携**: RestSharp（Chatwork API）

## 3. 機能要件

### 3.1 ユーザーインターフェース

- メインウィンドウ
  - 日付選択部（カレンダーコントロール）
  - 入力フォーム部（日報項目）
  - アクションボタン部（保存、送信、履歴表示）
  - ステータス表示部（操作結果、エラーメッセージ等）

### 3.2 日報入力機能

必須入力項目（毎日）:
1. **今日行ったタスク・かかった時間**
   - マルチラインテキストエリア

1. **今日の結果**
   - マルチラインテキストエリア

1. **明日の予定と目標**
   - マルチラインテキストエリア

オプション入力項目（毎月末）:
4. **月次の振返り（課題点に対する対応策や改善策）**
   - マルチラインテキストエリア

### 3.3 データベース機能

- 日報データの永続化
  - 日付、作成時間、更新時間を自動記録
  - 各入力項目の保存
  - バージョン管理（編集履歴）

- クエリ機能
  - 日付による検索
  - テキスト内容による検索
  - 月別のデータ集計

### 3.4 Chatwork連携機能

- Chatwork APIを使用した投稿機能
  - API設定画面（APIキー、ルームID等の設定）
  - 投稿テンプレート設定
  - 送信履歴管理

### 3.5 レポート・分析機能（任意）

- 月次レポートの自動生成
  - 月間のタスク集計
  - 作業時間の分析（オプション）
- エクスポート機能
  - CSV/Excel形式でのエクスポート
  - PDF形式での出力（オプション）

### 3.6 過去データ参照・再利用機能 【新規追加】

- **過去の日報内容呼び出し機能**
  - 日付選択による特定の過去日報の呼び出し
  - 特定項目のみの選択的呼び出し（タスクのみ、結果のみなど）
  - テンプレートとしての過去データ活用

- **テンプレート管理機能**
  - よく使うタスク文言や結果文言のテンプレート保存
  - カテゴリ別のテンプレート管理
  - ワンクリックでの呼び出し・挿入

- **スマート入力支援**
  - 前日の「明日の予定と目標」を当日の「今日行ったタスク」に自動連携
  - 頻出キーワードのサジェスト機能
  - 類似日報の検索と参照機能

- **プリセット機能**
  - ユーザー定義の入力テンプレートの保存
  - 定型タスクのクイック入力
  - タスクの組み合わせをプリセットとして保存

## 4. 非機能要件

### 4.1 パフォーマンス

- アプリケーション起動時間: 3秒以内
- データベース操作応答時間: 1秒以内
- API連携処理: 5秒以内のタイムアウト設定

### 4.2 セキュリティ

- APIキーの安全な保管（暗号化）
- ローカルデータの適切な保護

### 4.3 拡張性

- プラグイン構造による機能拡張の容易さ
- 他社製チャットツールへの連携拡張可能性

### 4.4 ユーザビリティ

- シンプルで直感的なUI
- ショートカットキーのサポート
- ダークモード対応（オプション）

## 5. データモデル

### 5.1 日報エンティティ
```csharp
public class DailyReport
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Tasks { get; set; }
    public string Results { get; set; }
    public string TomorrowPlan { get; set; }
    public string MonthlyReview { get; set; }
    public bool IsSentToChatwork { get; set; }
    public DateTime? SentToChatworkAt { get; set; }
}
```

### 5.2 設定エンティティ
```csharp
public class AppSettings
{
    public int Id { get; set; }
    public string ChatworkApiKey { get; set; }
    public string ChatworkRoomId { get; set; }
    public string ReportTemplate { get; set; }
    public bool AutoSendToChatwork { get; set; }
    public TimeSpan DailyReminderTime { get; set; }
}
```

### 5.3 テンプレートエンティティ 【新規追加】
```csharp
public class ReportTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string TasksContent { get; set; }
    public string ResultsContent { get; set; }
    public string TomorrowPlanContent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UsageCount { get; set; }
}
```

## 6. 画面設計

### 6.1 メイン画面
- 上部: 日付選択、アクションボタン
- 中央: タブ付き入力フォーム
  - タブ1: 日報（3項目）
  - タブ2: 月次振り返り
- 下部: ステータスバー
- **サイドパネル: 過去データ・テンプレート選択パネル【新規追加】**

### 6.2 設定画面
- Chatwork API設定
- テンプレート設定
- 通知・リマインダー設定

### 6.3 履歴閲覧画面
- カレンダービュー
- リスト表示
- 検索機能

### 6.4 テンプレート管理画面【新規追加】
- カテゴリ別テンプレート一覧
- 編集機能
- 使用頻度表示
- ドラッグ&ドロップによる並べ替え

## 7. システム構成

### 7.1 アーキテクチャ
- MVVM（Model-View-ViewModel）パターン採用
- クラス構成:
  - Models: データモデル
  - ViewModels: 画面ロジック
  - Views: UI定義
  - Services: DB操作、API連携など

### 7.2 ディレクトリ構造
```
DailyReportApp/
├── Models/
│   ├── DailyReport.cs
│   ├── AppSettings.cs
│   └── ReportTemplate.cs
├── ViewModels/
│   ├── MainViewModel.cs
│   ├── SettingsViewModel.cs
│   ├── HistoryViewModel.cs
│   └── TemplateViewModel.cs
├── Views/
│   ├── MainWindow.xaml
│   ├── SettingsWindow.xaml
│   ├── HistoryWindow.xaml
│   └── TemplateWindow.xaml
├── Services/
│   ├── DatabaseService.cs
│   ├── ChatworkService.cs
│   ├── ReportingService.cs
│   └── TemplateService.cs
└── App.xaml
```

