using WindowLogger.Application.Ports;

namespace WindowLogger.Application.Commands;

public sealed class RecordWindowActivityCommand
{
    private readonly IWindowActivityRecorder _recorder;
    private readonly IWindowActivityRepository _repository;

    public RecordWindowActivityCommand(IWindowActivityRecorder recorder, IWindowActivityRepository repository)
    {
        _recorder = recorder;
        _repository = repository;
    }

    public void Execute()
    {
        var record = _recorder.RecordCurrentWindowActivity();
        var log = _repository.LoadWindowActivityLog();
        log.AddRecord(record);
        _repository.SaveWindowActivityLog(log);
    }
}