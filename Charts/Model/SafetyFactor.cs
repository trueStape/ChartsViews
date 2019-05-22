using System.Collections.Generic;
using LiveCharts;
using static System.Double;

namespace Charts.ForceModel
{
    public class SafetyFactor
    {
        public List<string> SpeedForStraightSafetyFactor { get; set; } = new List<string> { "0","5", "20", "40", "60", "80", "100", "120", "132"};
        public List<string> SpeedForArrowSafetyFactor { get; set; } = new List<string> { "0", "5", "10", "20", "30","40"};
        public List<string> SpeedForR650SafetyFactor { get; set; } = new List<string> { "0", "5", "20", "40", "60", "80", "100", "120"};
        public List<string> SpeedForR350SafetyFactor { get; set; } = new List<string> { "0", "5", "20", "40", "60", "80" };

        public Dictionary<string, ChartValues<double>> StraightSafetyFactorLeft = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()},
            {"60км/ч",new ChartValues<double>()},
            {"80км/ч",new ChartValues<double>()},
            {"100км/ч",new ChartValues<double>()},
            {"120км/ч",new ChartValues<double>()},
            {"132км/ч",new ChartValues<double>()}
        };
        public Dictionary<string, ChartValues<double>> StraightSafetyFactorRight = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()},
            {"60км/ч",new ChartValues<double>()},
            {"80км/ч",new ChartValues<double>()},
            {"100км/ч",new ChartValues<double>()},
            {"120км/ч",new ChartValues<double>()},
            {"132км/ч",new ChartValues<double>()}
        };

        public Dictionary<string, ChartValues<double>> R650SafetyFactorLeft = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()},
            {"60км/ч",new ChartValues<double>()},
            {"80км/ч",new ChartValues<double>()},
            {"100км/ч",new ChartValues<double>()},
            {"120км/ч",new ChartValues<double>()},
            {"132км/ч",new ChartValues<double>()}
        };
        public Dictionary<string, ChartValues<double>> R650SafetyFactorRight = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()},
            {"60км/ч",new ChartValues<double>()},
            {"80км/ч",new ChartValues<double>()},
            {"100км/ч",new ChartValues<double>()},
            {"120км/ч",new ChartValues<double>()},
            {"132км/ч",new ChartValues<double>()}
        };

        public Dictionary<string, ChartValues<double>> ArrowSafetyFactorLeft = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"10км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"30км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()}
        };
        public Dictionary<string, ChartValues<double>> ArrowSafetyFactorRight = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"10км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"30км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()}
        };

        public Dictionary<string, ChartValues<double>> R350SafetyFactorLeft = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()},
            {"60км/ч",new ChartValues<double>()},
            {"80км/ч",new ChartValues<double>()},
            {"100км/ч",new ChartValues<double>()},
            {"120км/ч",new ChartValues<double>()},
            {"132км/ч",new ChartValues<double>()}
        };
        public Dictionary<string, ChartValues<double>> R350SafetyFactorRight = new Dictionary<string, ChartValues<double>>
        {
            {"5км/ч",new ChartValues<double>()},
            {"20км/ч",new ChartValues<double>()},
            {"40км/ч",new ChartValues<double>()},
            {"60км/ч",new ChartValues<double>()},
            {"80км/ч",new ChartValues<double>()},
            {"100км/ч",new ChartValues<double>()},
            {"120км/ч",new ChartValues<double>()},
            {"132км/ч",new ChartValues<double>()}
        };

        public ChartValues<double> ValueForChartStraightLeft = new ChartValues<double>{ 10, NaN, NaN, NaN, NaN, NaN, NaN, NaN, NaN };
        public ChartValues<double> ValueForChartStraightRight = new ChartValues<double> { 10, NaN, NaN, NaN, NaN, NaN, NaN, NaN, NaN };

        public ChartValues<double> ValueForChartR650Left = new ChartValues<double> { 10, NaN, NaN, NaN, NaN, NaN, NaN, NaN };
        public ChartValues<double> ValueForChartR650Right = new ChartValues<double> { 10, NaN, NaN, NaN, NaN, NaN, NaN, NaN };

        public ChartValues<double> ValueForChartR350Left = new ChartValues<double> { 10, NaN, NaN, NaN, NaN, NaN };
        public ChartValues<double> ValueForChartR350Right = new ChartValues<double> { 10, NaN, NaN, NaN, NaN, NaN };

        public ChartValues<double> ValueForChartArrowLeft = new ChartValues<double> { 10, NaN, NaN, NaN, NaN, NaN};
        public ChartValues<double> ValueForChartArrowRight = new ChartValues<double> { 10, NaN, NaN, NaN, NaN, NaN};
    }
}