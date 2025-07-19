using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;

namespace WindowLogger.Application.Workflows;

public interface IWindowActivityWorkflow
{
    void RecordCurrentActivity();
    WindowActivityLog GetAllActivities();
    IReadOnlyCollection<WindowActivityRecord> GetActivitiesByTimeRange(DateTime startTime, DateTime endTime);
    string ExportToHtml();
    void ClearAllActivities();
}