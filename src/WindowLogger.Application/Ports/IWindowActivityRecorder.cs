using WindowLogger.Domain.Entities;

namespace WindowLogger.Application.Ports;

public interface IWindowActivityRecorder
{
    WindowActivityRecord RecordCurrentWindowActivity();
}