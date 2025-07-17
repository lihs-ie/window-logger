using System.Globalization;
using Microsoft.Extensions.Logging;
using WindowLogger.Application.Ports;
using WindowLogger.Application.Services;
using WindowLogger.Domain.Aggregates;

namespace WindowLogger.Infrastructure.Exporters;

public sealed class FileHtmlExporter : IHtmlExporter
{
    private readonly string _exportDirectory;
    private readonly ILogger<FileHtmlExporter> _logger;
    private readonly HtmlExportService _htmlExportService;

    public FileHtmlExporter(string? exportDirectory = null, ILogger<FileHtmlExporter>? logger = null)
    {
        _exportDirectory = exportDirectory ?? GetDefaultExportDirectory();
        _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<FileHtmlExporter>.Instance;
        _htmlExportService = new HtmlExportService();
        
        EnsureExportDirectoryExists();
    }

    public string ExportToHtml(WindowActivityLog log)
    {
        return _htmlExportService.ExportToHtml(log);
    }

    public string ExportToHtmlFile(WindowActivityLog log)
    {
        try
        {
            var htmlContent = _htmlExportService.ExportToHtml(log);
            var fileName = GenerateHtmlFileName();
            var filePath = Path.Combine(_exportDirectory, fileName);
            
            File.WriteAllText(filePath, htmlContent);
            
            _logger.LogInformation("Exported window activity log to HTML file: {FilePath}", filePath);
            
            return filePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to export window activity log to HTML file");
            throw;
        }
    }

    private static string GetDefaultExportDirectory()
    {
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        return Path.Combine(documentsPath, "WindowLogger", "Exports");
    }

    private void EnsureExportDirectoryExists()
    {
        if (!Directory.Exists(_exportDirectory))
        {
            Directory.CreateDirectory(_exportDirectory);
            _logger.LogDebug("Created export directory at {ExportDirectory}", _exportDirectory);
        }
    }

    private static string GenerateHtmlFileName()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
        var shortGuid = Guid.NewGuid().ToString("N")[..8];
        return $"window-activity-log_{timestamp}_{shortGuid}.html";
    }
}