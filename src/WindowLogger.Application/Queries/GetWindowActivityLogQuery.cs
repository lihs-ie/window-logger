using WindowLogger.Application.Ports;
using WindowLogger.Domain.Aggregates;

namespace WindowLogger.Application.Queries;

public sealed class GetWindowActivityLogQuery
{
    private readonly IWindowActivityRepository _repository;

    public GetWindowActivityLogQuery(IWindowActivityRepository repository)
    {
        _repository = repository;
    }

    public WindowActivityLog Execute()
    {
        return _repository.LoadWindowActivityLog();
    }
}