using OpenTelemetry;
using OpenTelemetry.Metrics;
using Serilog;
using System.Diagnostics;

namespace lr_fourteen.Services
{
    public class SeqMetricExporter : BaseExporter<Metric>
    {
        public override ExportResult Export(in Batch<Metric> batch)
        {
            foreach (var metric in batch)
            {
                var data = new Dictionary<string, object>
                {
                    ["MetricName"] = metric.Name,
                    ["MetricType"] = metric.MetricType.ToString(),
                    ["Description"] = metric.Description,
                    ["Tags"] = metric.MeterTags
                };
                Log.Information("Metric: {data}", data);
            }
            return ExportResult.Success;
        }
    }
}
