using WindowLogger.Domain.Aggregates;

namespace WindowLogger.Application.Ports;

public interface IHtmlExporter
{
    string ExportToHtml(WindowActivityLog log);
}