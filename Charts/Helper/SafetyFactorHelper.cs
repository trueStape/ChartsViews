using Charts.ForceModel;
using LiveCharts;
using System.Collections.Generic;
using System.Linq;

namespace Charts.Helper
{
    public static class SafetyFactorHelper
    {
        private static ChartValues<double> ValueForChartLeft { get; set; }
        private static ChartValues<double> ValueForChartRight{ get; set; }
        private static Dictionary<string, ChartValues<double>> SafetyFactorСomparisonleft { get; set; }
        private static Dictionary<string, ChartValues<double>> SafetyFactorСomparisonRight { get; set; }
        private static int IndexSpeed { get; set; }
       

        public static SafetyFactor AddSafetyFactor(SafetyFactor safetyFactor, string lastPlace, string lastSpeed)
        {
            if (lastPlace != "Стрелки")//исправить
            {
                ChoseIndexSpeed(lastSpeed);
            }
            else
            {
                ChoseIndexSpeedArrow(lastSpeed);
            }
            ChosenPlace(safetyFactor, lastPlace);
            ForeachDictionary(SafetyFactorСomparisonleft, lastSpeed, ValueForChartLeft);
            ForeachDictionary(SafetyFactorСomparisonRight, lastSpeed, ValueForChartRight);
            return safetyFactor;
        }
        //загрузка переменой в зависимости от участка пути 
        private static void ChosenPlace(SafetyFactor safetyFactor, string place)
        {
            switch (place)
            {
                case "Прямая":
                    SafetyFactorСomparisonleft = safetyFactor.StraightSafetyFactorLeft;
                    SafetyFactorСomparisonRight = safetyFactor.StraightSafetyFactorRight;
                    ValueForChartLeft = safetyFactor.ValueForChartStraightLeft;
                    ValueForChartRight = safetyFactor.ValueForChartStraightRight;
                    break;
                case "Кривая650":
                    SafetyFactorСomparisonleft = safetyFactor.R650SafetyFactorLeft;
                    SafetyFactorСomparisonRight = safetyFactor.R650SafetyFactorRight;
                    ValueForChartLeft = safetyFactor.ValueForChartR650Left;
                    ValueForChartRight = safetyFactor.ValueForChartR650Right;
                    break;
                case "Кривая350":
                    SafetyFactorСomparisonleft = safetyFactor.R350SafetyFactorLeft;
                    SafetyFactorСomparisonRight = safetyFactor.R350SafetyFactorRight;
                    ValueForChartLeft = safetyFactor.ValueForChartR350Left;
                    ValueForChartRight = safetyFactor.ValueForChartR350Right;
                    break;
                case "Стрелка":
                    SafetyFactorСomparisonleft = safetyFactor.ArrowSafetyFactorLeft;
                    SafetyFactorСomparisonRight = safetyFactor.ArrowSafetyFactorRight;
                    ValueForChartLeft = safetyFactor.ValueForChartArrowLeft;
                    ValueForChartRight = safetyFactor.ValueForChartArrowRight;
                    break;
            }
        }
        //выбор индекса для записи в список
        private static void ChoseIndexSpeed(string speed)
        {
            switch (speed)
            {
                case "0км/ч":
                    IndexSpeed = 0;
                    break;
                case "5км/ч":
                    IndexSpeed = 1;
                    break;
                case "20км/ч":
                    IndexSpeed = 2;
                    break;
                case "40км/ч":
                    IndexSpeed = 3;
                    break;
                case "60км/ч":
                    IndexSpeed = 4;
                    break;
                case "80км/ч":
                    IndexSpeed = 5;
                    break;
                case "100км/ч":
                    IndexSpeed = 6;
                    break;
                case "120км/ч":
                    IndexSpeed = 7;
                    break;
                case "132км/ч":
                    IndexSpeed = 8;
                    break;
            }
        }

        private static void ChoseIndexSpeedArrow(string speed)
        {
            switch (speed)
            {
                case "0км/ч":
                    IndexSpeed = 0;
                    break;
                case "5км/ч":
                    IndexSpeed = 1;
                    break;
                case "10км/ч":
                    IndexSpeed = 2;
                    break;
                case "20км/ч":
                    IndexSpeed = 3;
                    break;
                case "30км/ч":
                    IndexSpeed = 4;
                    break;
                case "40км/ч":
                    IndexSpeed = 5;
                    break;
            }
        }
        //нахождение среднего числа из добавленных значений для зависимости коэфф запаса от скорости
        private static void ForeachDictionary(Dictionary<string, ChartValues<double>> choosedSafetyFactorСomparison, 
                                              string speed, ChartValues<double> valueForChart)
        {

            //double value = 0;
            //var amountOfNumbers = 0;

            //if (choosedSafetyFactorСomparison == null) return;

            //foreach (var j in choosedSafetyFactorСomparison[speed])
            //{
            //    value += j;
            //    amountOfNumbers++;
            //}
            //var safetyFactorAdd = value / amountOfNumbers;

            var safetyFactorAdd = choosedSafetyFactorСomparison[speed].Average();
            valueForChart[IndexSpeed] = safetyFactorAdd;
        }
    }
}