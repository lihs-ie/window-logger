# WindowLogger リリース手順

## 新しいリリースを作成する方法

1. **バージョンタグを作成**
   ```bash
   git tag v1.0.0
   git push origin v1.0.0
   ```

2. **自動ビルドの確認**
   - GitHub Actionsでビルドとテストが自動実行されます
   - 成功すると自動でリリースが作成されます

3. **手動実行の場合**
   - GitHub の Actions タブから "Build and Release" を手動実行可能

## リリース内容

- **WindowLogger-win-x64.zip**: Windows用実行ファイル（単体実行可能）
- 全ての依存関係を含む自己完結型パッケージ
- .NET Runtimeのインストール不要

## バージョン管理

- `v{major}.{minor}.{patch}` 形式（例: v1.0.0）
- セマンティックバージョニングに準拠