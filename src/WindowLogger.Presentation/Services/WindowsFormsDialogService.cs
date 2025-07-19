namespace WindowLogger.Presentation.Services;

public sealed class WindowsFormsDialogService : IDialogService
{
    public UserDialogResult ShowQuestion(string message, string title)
    {
        var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        return result switch
        {
            System.Windows.Forms.DialogResult.Yes => UserDialogResult.Yes,
            System.Windows.Forms.DialogResult.No => UserDialogResult.No,
            _ => UserDialogResult.None
        };
    }

    public void ShowInformation(string message, string title)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public void ShowError(string message, string title)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}