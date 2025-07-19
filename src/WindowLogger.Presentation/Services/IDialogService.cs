namespace WindowLogger.Presentation.Services;

public interface IDialogService
{
    UserDialogResult ShowQuestion(string message, string title);
    void ShowInformation(string message, string title);
    void ShowError(string message, string title);
}

public enum UserDialogResult
{
    None,
    OK,
    Cancel,
    Yes,
    No
}