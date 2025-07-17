using System.Globalization;
using System.Text;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Aggregates;

namespace WindowLogger.Application.Services;

public sealed class HtmlExportService : IHtmlExporter
{
    private const string HTML_TEMPLATE = """
        <!DOCTYPE html>
        <html lang="ja">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Window Activity Log</title>
            <style>
                body {
                    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                    margin: 20px;
                    background-color: #f5f5f5;
                }
                .container {
                    max-width: 1200px;
                    margin: 0 auto;
                    background-color: white;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                }
                h1 {
                    color: #333;
                    text-align: center;
                    margin-bottom: 30px;
                }
                .summary {
                    background-color: #e8f4fd;
                    padding: 15px;
                    border-radius: 5px;
                    margin-bottom: 20px;
                    border-left: 4px solid #2196F3;
                }
                table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 20px;
                }
                th, td {
                    padding: 12px;
                    text-align: left;
                    border-bottom: 1px solid #ddd;
                }
                th {
                    background-color: #f8f9fa;
                    font-weight: 600;
                    color: #555;
                }
                tr:hover {
                    background-color: #f5f5f5;
                }
                .time-column {
                    width: 180px;
                    font-family: monospace;
                }
                .app-column {
                    width: 200px;
                }
                .empty-message {
                    text-align: center;
                    color: #666;
                    font-style: italic;
                    padding: 40px;
                }
            </style>
        </head>
        <body>
            <div class="container">
                <h1>Window Activity Log</h1>
                <div class="summary">
                    <strong>総記録数:</strong> {RECORD_COUNT} 件<br>
                    <strong>生成日時:</strong> {GENERATION_TIME}
                </div>
                {CONTENT}
            </div>
        </body>
        </html>
        """;

    public string ExportToHtml(WindowActivityLog log)
    {
        var recordCount = log.RecordCount;
        var generationTime = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss", CultureInfo.CreateSpecificCulture("ja-JP"));
        var content = GenerateContent(log);

        return HTML_TEMPLATE
            .Replace("{RECORD_COUNT}", recordCount.ToString())
            .Replace("{GENERATION_TIME}", generationTime)
            .Replace("{CONTENT}", content);
    }

    private static string GenerateContent(WindowActivityLog log)
    {
        if (log.RecordCount == 0)
        {
            return "<div class=\"empty-message\">記録されたアクティビティはありません。</div>";
        }

        var content = new StringBuilder();
        content.AppendLine("<table>");
        content.AppendLine("    <thead>");
        content.AppendLine("        <tr>");
        content.AppendLine("            <th class=\"time-column\">記録日時</th>");
        content.AppendLine("            <th class=\"app-column\">アプリケーション</th>");
        content.AppendLine("            <th>ウィンドウタイトル</th>");
        content.AppendLine("        </tr>");
        content.AppendLine("    </thead>");
        content.AppendLine("    <tbody>");

        foreach (var record in log.Records)
        {
            var timeString = record.RecordedAt.Value.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            var applicationName = EscapeHtml(record.ApplicationName);
            var windowTitle = EscapeHtml(record.WindowTitle.Value);

            content.AppendLine("        <tr>");
            content.AppendLine($"            <td class=\"time-column\">{timeString}</td>");
            content.AppendLine($"            <td class=\"app-column\">{applicationName}</td>");
            content.AppendLine($"            <td>{windowTitle}</td>");
            content.AppendLine("        </tr>");
        }

        content.AppendLine("    </tbody>");
        content.AppendLine("</table>");

        return content.ToString();
    }

    private static string EscapeHtml(string input)
    {
        return input
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;");
    }
}