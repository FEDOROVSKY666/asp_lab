using OpenTelemetry;
using System.Diagnostics;
using Serilog;

namespace lr_fourteen.Services
{
    public class SeqTraceExporter : BaseExporter<Activity>
    {
        public override ExportResult Export(in Batch<Activity> batch)
        {
            foreach (var activity in batch)
            {
                var data = new Dictionary<string, object>
                {
                    ["TraceId"] = activity.TraceId.ToString(),
                    ["SpanId"] = activity.SpanId.ToString(),
                    ["DisplayName"] = activity.DisplayName,
                    ["Tags"] = activity.Tags
                };
                Log.Information("Tracer: {data}", data);
            }
            return ExportResult.Success;
        }
    }
}
