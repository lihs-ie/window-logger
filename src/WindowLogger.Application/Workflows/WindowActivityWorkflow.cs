using WindowLogger.Application.Ports;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;

namespace WindowLogger.Application.Workflows;

public sealed class WindowActivityWorkflow : IWindowActivityWorkflow
{
    private readonly IWindowActivityRecorder _recorder;
    private readonly IWindowActivityRepository _repository;
    private readonly IHtmlExporter? _htmlExporter;

    public WindowActivityWorkflow(
        IWindowActivityRecorder recorder, 
        IWindowActivityRepository repository,
        IHtmlExporter? htmlExporter = null)
    {
        _recorder = recorder;
        _repository = repository;
        _htmlExporter = htmlExporter;
    }

    public void RecordCurrentActivity()
    {
        var record = _recorder.RecordCurrentWindowActivity();
        var log = _repository.LoadWindowActivityLog();
        log.AddRecord(record);
        _repository.SaveWindowActivityLog(log);
    }

    public WindowActivityLog GetAllActivities()
    {
        return _repository.LoadWindowActivityLog();
    }

    public IReadOnlyCollection<WindowActivityRecord> GetActivitiesByTimeRange(DateTime startTime, DateTime endTime)
    {
        var log = _repository.LoadWindowActivityLog();
        return log.GetRecordsByTimeRange(startTime, endTime);
    }

    public string ExportToHtml()
    {
        if (_htmlExporter == null)
        {
            throw new InvalidOperationException("HTML exporter is not configured.");
        }

        var log = _repository.LoadWindowActivityLog();
        return _htmlExporter.ExportToHtml(log);
    }

    public void ClearAllActivities()
    {
        var log = _repository.LoadWindowActivityLog();
        log.ClearAllRecords();
        _repository.SaveWindowActivityLog(log);
    }
}