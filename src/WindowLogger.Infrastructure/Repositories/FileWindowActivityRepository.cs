using System.Text.Json;
using Microsoft.Extensions.Logging;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Infrastructure.Repositories;

public sealed class FileWindowActivityRepository : IWindowActivityRepository
{
    private readonly string _dataDirectory;
    private readonly ILogger<FileWindowActivityRepository> _logger;
    private readonly string _filePath;
    
    private const string DATA_FILE_NAME = "window-activity-log.json";

    public FileWindowActivityRepository(string? dataDirectory = null, ILogger<FileWindowActivityRepository>? logger = null)
    {
        _dataDirectory = dataDirectory ?? GetDefaultDataDirectory();
        _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<FileWindowActivityRepository>.Instance;
        _filePath = Path.Combine(_dataDirectory, DATA_FILE_NAME);
        
        EnsureDataDirectoryExists();
    }

    public void SaveWindowActivityLog(WindowActivityLog log)
    {
        try
        {
            var dto = ConvertToDto(log);
            var json = JsonSerializer.Serialize(dto, GetJsonSerializerOptions());
            
            File.WriteAllText(_filePath, json);
            
            _logger.LogDebug("Saved window activity log with {RecordCount} records to {FilePath}", 
                log.RecordCount, _filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save window activity log to {FilePath}", _filePath);
            throw;
        }
    }

    public WindowActivityLog LoadWindowActivityLog()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                _logger.LogDebug("No existing log file found at {FilePath}, returning empty log", _filePath);
                return WindowActivityLog.Create();
            }

            var json = File.ReadAllText(_filePath);
            var dto = JsonSerializer.Deserialize<WindowActivityLogDto>(json, GetJsonSerializerOptions());
            
            if (dto == null)
            {
                _logger.LogWarning("Failed to deserialize log file at {FilePath}, returning empty log", _filePath);
                return WindowActivityLog.Create();
            }

            var log = ConvertFromDto(dto);
            
            _logger.LogDebug("Loaded window activity log with {RecordCount} records from {FilePath}", 
                log.RecordCount, _filePath);
                
            return log;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load window activity log from {FilePath}, returning empty log", _filePath);
            return WindowActivityLog.Create();
        }
    }

    private static string GetDefaultDataDirectory()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(appDataPath, "WindowLogger");
    }

    private void EnsureDataDirectoryExists()
    {
        if (!Directory.Exists(_dataDirectory))
        {
            Directory.CreateDirectory(_dataDirectory);
            _logger.LogDebug("Created data directory at {DataDirectory}", _dataDirectory);
        }
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    private static WindowActivityLogDto ConvertToDto(WindowActivityLog log)
    {
        var recordDtos = log.Records.Select(record => new WindowActivityRecordDto
        {
            Id = record.Id.Value.ToString(),
            WindowTitle = record.WindowTitle.Value,
            RecordedAt = record.RecordedAt.Value,
            ApplicationName = record.ApplicationName
        }).ToArray();

        return new WindowActivityLogDto
        {
            Records = recordDtos,
            RecordCount = log.RecordCount
        };
    }

    private static WindowActivityLog ConvertFromDto(WindowActivityLogDto dto)
    {
        var log = WindowActivityLog.Create();

        foreach (var recordDto in dto.Records)
        {
            try
            {
                var id = WindowActivityRecordIdentifier.Create(Ulid.Parse(recordDto.Id));
                var title = WindowTitle.Create(recordDto.WindowTitle);
                var recordedAt = RecordedAt.Create(recordDto.RecordedAt);
                
                var record = WindowActivityRecord.Create(id, title, recordedAt);
                log.AddRecord(record);
            }
            catch (Exception)
            {
                // Skip invalid records
                continue;
            }
        }

        return log;
    }

    private sealed record WindowActivityLogDto
    {
        public WindowActivityRecordDto[] Records { get; init; } = Array.Empty<WindowActivityRecordDto>();
        public int RecordCount { get; init; }
    }

    private sealed record WindowActivityRecordDto
    {
        public string Id { get; init; } = string.Empty;
        public string WindowTitle { get; init; } = string.Empty;
        public DateTime RecordedAt { get; init; }
        public string ApplicationName { get; init; } = string.Empty;
    }
}