using System.Collections.Generic;
using System.ComponentModel;
using LiveCharts;
using LiveCharts.Helpers;

namespace Charts.ForceModel
{
    public class MovingAverage : BaseVM
    {
        public string _Speed;
        public decimal _decimalSpeed;
        public string _Place;
        public string _NameSignal;
        public string _MinSafetyFactorLeft;
        public string _MinSafetyFactorRight;

        public string _trainDirection;

        public List<string> TimeForMovingAverage { get; set; } = new List<string>();

        public ChartValues<double> LeftVerticalForMovingAverage { get; set; } = new ChartValues<double>();
        public ChartValues<double> RightVerticalForMovingAverage { get; set; } = new ChartValues<double>();

        public ChartValues<double> LeftLateralForMovingAverage { get; set; } = new ChartValues<double>();
        public ChartValues<double> RightLateralForMovingAverage { get; set; } = new ChartValues<double>();

        public ChartValues<double> LeftSafetyFactorForMovingAverage { get; set; } = new ChartValues<double>();
        public ChartValues<double> RightSafetyFactorForMovingAverage { get; set; } = new ChartValues<double>();

        public string Speed
        {
            get => _Speed;
            set
            {
                if (_Speed != value)
                {
                    _Speed = value;
                    OnPropertyChanged("Speed");
                }
            }
        }
        public string Place
        {
            get => _Place;
            set
            {
                if (_Place != value)
                {
                    _Place = value;
                    OnPropertyChanged("Place");
                }
            }
        }
        public string NameSignal
        {
            get => _NameSignal;
            set
            {
                if (_NameSignal != value)
                {
                    _NameSignal = value;
                    OnPropertyChanged("NameSignal");
                }
            }
        }
        public string MinSafetyFactorLeft
        {
            get => _MinSafetyFactorLeft;
            set
            {
                if (_MinSafetyFactorLeft != value)
                {
                    _MinSafetyFactorLeft = value;
                    OnPropertyChanged("MinSafetyFactor");
                }
            }
        }
        public string MinSafetyFactorRight
        {
            get => _MinSafetyFactorRight;
            set
            {
                if (_MinSafetyFactorRight != value)
                {
                    _MinSafetyFactorRight = value;
                    OnPropertyChanged("MinSafetyFactor");
                }
            }
        }
        public string TrainDirection
        {
            get => _trainDirection;
            set
            {
                if (_trainDirection != value)
                {
                    _trainDirection = value;
                    OnPropertyChanged("TrainDirection");
                }
            }
        }

    }
}