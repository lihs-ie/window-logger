using WindowLogger.Domain.Aggregates;

namespace WindowLogger.Application.Ports;

public interface IWindowActivityRepository
{
    void SaveWindowActivityLog(WindowActivityLog log);
    WindowActivityLog LoadWindowActivityLog();
}
