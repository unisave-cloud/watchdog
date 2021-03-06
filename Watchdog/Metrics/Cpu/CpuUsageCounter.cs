using System.IO;

namespace Watchdog.Metrics.Cpu
{
    /// <summary>
    /// Cumulative system CPU time consumed in seconds
    /// </summary>
    public class CpuUsageCounter : PrometheusMetric
    {
        public CpuUsageCounter(string name, string help = null)
            : base(name, help, "counter") { }

        protected override double GetMetricValue()
        {
            return PerformMeasurement();
        }

        public static double PerformMeasurement()
        {
            string nsText = File.ReadAllText(
                "/sys/fs/cgroup/cpu/cpuacct.usage"
            );
            ulong ns = ulong.Parse(nsText);
            return ns * 1e-9;
        }
    }
}