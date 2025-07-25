# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## **絶対に守ること**

- ユーザーには日本語で応答してください。
- 変数名、型、インターフェース名は多数の人たちの共通認識以外の言葉は省略せずに記載すること
- git 系のコマンドはルートディレクトリで実行してください
- domain 層では外部サービスの知識を持たないこと（firebase や google などの情報は持ってはいけない）

## 目的

■ 機能概要
本ソフトウェアは、画面上で作業対象となっている最前面のウィンドウ（フォアグラウンド ウィンドウ）のタイトルを取得して、そのタイトルを記録します。
最前面のウィンドウが変更される度にタイトルの取得と記録を行い、パソコンの作業履歴の作成ツールとして ご利用頂けます。

記録された履歴は HTML 形式のファイルとして保存され、ウェブ ブラウザ等で観覧できます。

■ 動作環境
・ Windows 11
・（ ウェブ ブラウザ等の HTML ファイルを観覧するアプリケーションが利用できる環境。）

■ 画面イメージ
UI

■ 使い方
・ 起動させると記録を開始します。 （※ 起動時は、タスク トレイに格納される状態となります。）
・ 記録を止めるには、アプリケーションを終了させて下さい。

タイトル バー ボタンの機能
＿ 最小化 タスク トレイに移動します。
□ 最大化 （ 機能しません。）
× 閉じる アプリケーションを終了します。

## 技術スタック

**推奨技術スタック: C# (.NET 8)**

### 主要パッケージ

- `Microsoft.Windows.CsWin32` - 型安全な Windows API 呼び出し
- `Microsoft.Extensions.Hosting` - DI + ライフサイクル管理
- `Microsoft.Extensions.Logging` - 構造化ログ
- `System.Text.Json` - JSON/HTML 出力
- `xUnit` + `FluentAssertions` - テストフレームワーク

### 選択理由

- Windows API 統合が最も優秀（P/Invoke、CsWin32 ライブラリ）
- タスクトレイ実装が容易（NotifyIcon）
- 関数型プログラミング対応（records、pattern matching、immutable collections）
- テスト駆動開発に最適（xUnit、MSTest、NUnit）
- Windows 11 ネイティブ対応

### 出力形式

- HTML 形式での履歴保存
- System.Text.Json による構造化データ出力

### 対象 OS

- Windows 11 (primary)
- Windows 10 (compatibility)

## 開発ワークフロー

### 要件定義を REQUIREMENTS_DEFINITION.md にまとめる

- 実装・修正を開始する前に**必ず**要件定義を ルートディレクトリの REQUIREMENTS_DEFINITION.md にまとめること
- 全ての要件が達成された後にこの md ファイルをクリアにすること

## 1. 基本哲学: テスト駆動

- **テストが開発を駆動する:** すべてのプロダクションコードは、失敗するテストをパスさせるためだけに書かれます。テストは後付けの作業ではありません。それ自身が仕様書であり、設計の駆動役です。
- **リファクタリングへの自信:** 包括的なテストスイートは我々のセーフティネットです。これにより、私たちは恐れることなく継続的にコードベースのリファクタリングと改善を行えます。
- **テスト容易性は良い設計に等しい:** コードがテストしにくい場合、それは悪い設計の兆候です。エージェントは、テスト容易性の高いコード作成を最優先しなければなりません。それは自然と、疎結合で凝集度の高いアーキテクチャにつながります。

## 2. 開発サイクル: レッド・グリーン・リファクタリング・コミット

エージェントは、いかに小さな変更であっても、必ずこの反復的なサイクルに従わなければなりません。コードを生成する際は、現在どのフェーズにいるのかを明示してください。

### フェーズ 1: レッド - 失敗するテストを書く

- **目的:** これから何を達成するのかを明確に定義する。
- **行動:** 実装コードを書く前に、これから実装したい単一の機能に対する、具体的で失敗するテストを一つ作成する。
- **条件:** 対応する実装がまだ存在しないため、このテストは必ず失敗（**レッド**）しなければならない。

### フェーズ 2: グリーン - テストをパスさせる

- **目的:** テストで示された要件を満たす。
- **行動:** 失敗したテストをパスさせるために必要な、**最小限のコード**を記述する。
- **条件:** この段階で余分な機能を追加しないこと。コードの美しさは追求せず、ただテストをパス（**グリーン**）させることだけを考える。

### フェーズ 3: リファクタリング - 設計を改善する

- **目的:** テストが通っている状態を維持しながら、コードの品質を向上させる。
- **行動:** テストが成功しているという安全な状態で、コードの内部構造を改善する。これには以下の作業が含まれるが、これに限定されない。
  - 重複の排除（DRY 原則）
  - 命名の明確化
  - 複雑なロジックの単純化
  - 後述のコーディング規約がすべて満たされていることの確認
  - 修正したファイルに対してコンパイルを実行し成功を確認
- **条件:** リファクタリングの全プロセスを通じて、すべてのテストは**グリーン**の状態を維持しなければならない。
  - テストが成功しない場合テストをスキップすることは禁止する
  - テストを成功させるために単純な別のテストファイルを作成することを禁止する

### フェーズ 4: コミット - 進捗を保存する

- **目的:** 正常に動作する小さな機能単位を、安全なバージョンとして記録する。
- **行動:** リファクタリングが完了し、全テストがグリーンであることを最終確認したら、`git add .` を実行して変更をステージングする。これは、次の機能開発へ進む前の安定したチェックポイントとなる。
- **条件:** このサイクルで実装された変更が、一つの意味のあるまとまりとして完結していること。コミットメッセージもその内容を簡潔に表現すること。

## 3. 厳格なコーディング規約と禁止事項

### 【最重要】ハードコードの絶対禁止

いかなる形式のハードコードも固く禁じます。

- **マジックナンバー:** 数値リテラルをロジック内に直接記述してはならない。意味のある名前の定数を定義すること。
  - _悪い例:_ `if (age > 20)`
  - _良い例:_ `const ADULT_AGE = 20; if (age > ADULT_AGE)`
- **設定値:** API キー、URL、ファイルパス、その他の環境設定は、必ず設定ファイル（`.env`など）や環境変数から読み込むこと。ソースコード内に直接記述してはならない。
- **ユーザー向け文字列:** UI に表示するテキスト、ログ、エラーメッセージなどは、メンテナンスと国際化を容易にするため、定数や言語ファイルで管理すること。

### その他の主要な規約

- **単一責任の原則 (SRP):** 一つのモジュール、クラス、関数は、機能の一部分に対してのみ責任を持つべきである。
- **DRY (Don't Repeat Yourself):** コードの重複は絶対に避けること。共通ロジックは抽象化し、再利用する。
- **明確で意図の伝わる命名:** 変数名や関数名は、その目的と意図が明確に伝わるように命名する。
- **ガード節 / 早期リターン:** 深くネストした`if-else`構造を避け、早期リターンを積極的に利用する。
- **セキュリティ第一:** ユーザーからの入力は常に信頼しないこと。一般的な脆弱性（XSS、SQL インジェクション等）を防ぐため、入力のサニタイズと出力のエンコードを徹底する。

### DDD

- 関数型ドメイン駆動設計で開発する
  - 純粋関数を優先
  - 不変データ構造を使用
  - 副作用を分離
  - 型安全性を確保

#### 実装手順

1. **型設計**

   - まず型を定義
   - ドメインの言語を型で表現

2. **純粋関数から実装**

   - 外部依存のない関数を先に
   - テストを先に書く

3. **副作用を分離**

   - IO 操作は関数の境界に押し出す
   - 副作用を持つ処理を Promise でラップ

4. **アダプター実装**
   - 外部サービスや DB へのアクセスを抽象化
   - テスト用モックを用意

## 関数型 DDD ディレクトリ構成

### 基本構成

```
WindowLogger/
├── src/
│   ├── WindowLogger.Domain/          # ドメイン層（純粋関数のみ）
│   │   ├── Window/                   # ウィンドウ関連ドメイン
│   │   ├── Activity/                 # アクティビティ記録ドメイン
│   │   └── ValueObjects/             # 値オブジェクト
│   ├── WindowLogger.Application/     # アプリケーション層
│   │   ├── Commands/                 # コマンド（書き込み操作）
│   │   ├── Queries/                  # クエリ（読み取り操作）
│   │   └── Ports/                    # ポート（外部依存の抽象化）
│   ├── WindowLogger.Infrastructure/  # インフラ層
│   │   ├── Adapters/                 # アダプター実装
│   │   ├── WindowsApi/               # Windows API統合
│   │   └── Repositories/             # データ永続化
│   └── WindowLogger.Presentation/    # プレゼンテーション層
│       ├── Services/                 # バックグラウンドサービス
│       ├── TrayIcon/                 # タスクトレイUI
│       └── Exporters/                # HTMLエクスポート
└── tests/
    ├── WindowLogger.Domain.Tests/
    ├── WindowLogger.Application.Tests/
    └── WindowLogger.Infrastructure.Tests/
```

### 各層の責務

1. **Domains 層**: 純粋関数のみ、外部依存なし、不変データ構造
2. **Application 層**: ワークフローとコマンド/クエリ分離、ポートによる外部依存の抽象化
3. **Infrastructure 層**: アダプター実装、ACL による外部サービスとの統合、副作用の集約
4. **Presentation 層**: UI 実装、状態管理、ワークフローの呼び出し

### ACL（Anti Corruption Layer）の重要性

- **型安全性の保護**: 外部サービスの型変更から内部ドメインを保護
- **純粋関数の保護**: 外部依存との直接結合を防ぐ
- **テスト容易性**: 変換ロジックを純粋関数として単体テスト可能
- **保守性**: 外部サービス変更の影響範囲を限定

## 開発コマンド

### 基本コマンド

```bash
dotnet restore                  # 依存関係復元
dotnet build                    # ビルド
dotnet test                     # テスト実行
dotnet run                      # アプリケーション実行
```

### 開発・品質管理

```bash
dotnet format                   # コードフォーマット
dotnet test --collect:"XPlat Code Coverage"  # カバレッジ付きテスト
dotnet publish -c Release       # リリースビルド
```

### テスト関連

```bash
dotnet test --filter "Category=Unit"        # 単体テスト実行
dotnet test --filter "Category=Integration" # 統合テスト実行
dotnet test --logger trx                    # テスト結果をTRX形式で出力
```

### Push前必須フロー

**重要: gitへのpush前に必ず以下のフローを実行してください**

#### Windows環境の場合
```bash
# 1. 依存関係復元
dotnet restore

# 2. ビルド確認（全プロジェクト）
dotnet build --configuration Release

# 3. テスト実行（全テスト）
dotnet test --configuration Release --verbosity normal

# 4. フォーマット確認
dotnet format --verify-no-changes

# 5. 上記すべて成功後にpush
git push origin main
```

#### macOS/Linux環境の場合
```bash
# 1. 依存関係復元
dotnet restore

# 2. ビルド確認（全プロジェクト）
dotnet build --configuration Release

# 3. テスト実行（Windows Forms以外）
dotnet test tests/WindowLogger.Domain.Tests/ --configuration Release --verbosity normal
dotnet test tests/WindowLogger.Application.Tests/ --configuration Release --verbosity normal
dotnet test tests/WindowLogger.Infrastructure.Tests/ --configuration Release --verbosity normal

# 4. フォーマット確認
dotnet format --verify-no-changes

# 5. 上記すべて成功後にpush
git push origin main
```

**注意事項:**
- ビルドエラーやテスト失敗がある場合は修正してから再実行
- フォーマットエラーがある場合は `dotnet format` で修正
- Windows専用機能（WindowLogger.Presentation）のテストはWindows環境でのみ実行
- macOS/Linux環境ではWindows Formsテストは除外してください

## プロジェクト固有の注意事項

### Windows API 操作

- `Microsoft.Windows.CsWin32`による型安全な Windows API 呼び出し
- `GetForegroundWindow()` + `GetWindowText()`でウィンドウタイトル取得
- `NotifyIcon`クラスによるタスクトレイ常駐機能
- `IDisposable`パターンによるシステムリソース管理

### HTML エクスポート機能

- `System.Text.Json`による構造化データ出力
- HTML テンプレートエンジン（RazorEngine 等）の活用
- 日時情報の`DateTime`→ISO8601 形式変換
- ウェブブラウザでの閲覧対応（CSS 組み込み）

### Windows 11 対応

- .NET 8 の Windows 11 最適化機能活用
- 最新の Windows API サポート
- Windows 10 との下位互換性確保
- Windows 11 固有のセキュリティ機能対応
