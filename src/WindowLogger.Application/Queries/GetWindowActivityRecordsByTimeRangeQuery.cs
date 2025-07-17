using WindowLogger.Application.Ports;
using WindowLogger.Domain.Entities;

namespace WindowLogger.Application.Queries;

public sealed class GetWindowActivityRecordsByTimeRangeQuery
{
    private readonly IWindowActivityRepository _repository;

    public GetWindowActivityRecordsByTimeRangeQuery(IWindowActivityRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyCollection<WindowActivityRecord> Execute(DateTime startTime, DateTime endTime)
    {
        var log = _repository.LoadWindowActivityLog();
        return log.GetRecordsByTimeRange(startTime, endTime);
    }
}