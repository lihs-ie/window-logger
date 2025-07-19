namespace WindowLogger.Presentation.Constants;

public static class UserMessages
{
    public const string CLEAR_CONFIRMATION_TITLE = "ログクリア確認";
    public const string CLEAR_CONFIRMATION_MESSAGE = "すべてのアクティビティログを削除しますか？\nこの操作は取り消せません。";
    
    public const string CLEAR_SUCCESS_TITLE = "WindowLogger";
    public const string CLEAR_SUCCESS_MESSAGE = "すべてのアクティビティログを削除しました。";
    
    public const string CLEAR_ERROR_TITLE = "エラー";
    public const string CLEAR_ERROR_MESSAGE = "ログのクリア中にエラーが発生しました。";
    
    public const string EXPORT_NO_RECORDS_TITLE = "WindowLogger";
    public const string EXPORT_NO_RECORDS_MESSAGE = "エクスポートする記録がありません。";
    
    public const string EXPORT_SUCCESS_TITLE = "エクスポート完了";
    public const string EXPORT_SUCCESS_MESSAGE_FORMAT = "HTMLファイルを出力しました:\n{0}\n\nファイルを開きますか？";
    
    public const string EXPORT_ERROR_TITLE = "エラー";
    public const string EXPORT_ERROR_MESSAGE = "HTMLエクスポート中にエラーが発生しました。";
}