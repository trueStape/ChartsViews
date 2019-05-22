using LiveCharts;
using System.Collections.Generic;

namespace Charts.ForceModel
{
    public class ForceValues 
    {
        public List<string> Time { get; set; } = new List<string>();
        public List<decimal> TimeDecimal { get; set; } = new List<decimal>();
        public ChartValues<double>  LeftVertical { get; set; } = new ChartValues<double>();
        public ChartValues<double>  RightVertical { get; set; } = new ChartValues<double> ();
        public ChartValues<double>  LeftLateral { get; set; } = new ChartValues<double> ();
        public ChartValues<double>  RightLateral { get; set; } = new ChartValues<double> ();
        public ChartValues<double>  LeftSafetyFactor { get; set; } = new ChartValues<double> ();
        public ChartValues<double>  RightSafetyFactor { get; set; } = new ChartValues<double> ();
    }
}
