using Charts.ForceModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Charts.ViewModel;

namespace Charts.Helper
{
    public static class Helper
    {
       public static Tuple<ForceValues, MovingAverage> ReadCSVfile(string filePath, SafetyFactor safetyFactor)
       {
               var forceValues = new ForceValues();
               var movingAverage = new MovingAverage();
               //запись имени сигнала
               AddNameSignal(filePath, movingAverage);
               //считывание файла
               ReadFile(filePath, forceValues, movingAverage);
               //подсчет скользящей средней
               MovingAverage(forceValues, movingAverage);
               //Запись значений для коэффициента запаса от схода
               SafetyFactorСomparison(forceValues, safetyFactor, movingAverage);
              return Tuple.Create(forceValues, movingAverage);
        }

        private static void ReadFile(string filePath, ForceValues forceValues, MovingAverage movingAverage)
        {
               var isFirstLine = true;
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line != null)
                        {
                            var values = line.Split(';');
                            //прочтение первой строки
                            if (isFirstLine)
                            {
                                //заполение направления движения 
                                //movingAverage._trainDirection = values[12];
                                
                                movingAverage._Speed = values[9];
                                //длина размерности км/ч
                                const int dimension = 4;
                                //измерение длины слова 
                                var wordLength = movingAverage._Speed.Length;
                                //выделение из слова числовую часть и перевод в Decimal
                                movingAverage._decimalSpeed = Convert.ToDecimal(movingAverage._Speed.Substring(0, wordLength - dimension));
                                movingAverage._Place = values[11];
                                isFirstLine = false;
                                continue;
                            }

                            //чтение следующих строк со значениями
                            forceValues.Time.Add(values[1]);
                            forceValues.TimeDecimal.Add(Convert.ToDecimal(values[1]));
                            if (double.TryParse(values[2], out var resultLeftVertical))
                            {
                                forceValues.LeftVertical.Add(resultLeftVertical);
                            }

                            if (double.TryParse(values[3], out var resultRightVertical))
                            {
                                forceValues.RightVertical.Add(resultRightVertical);
                            }

                            if (double.TryParse(values[4], out var resultLeftLateral))
                            {
                                forceValues.LeftLateral.Add(resultLeftLateral);
                            }

                            if (double.TryParse(values[5], out var resultRightLateral))
                            {
                                forceValues.RightLateral.Add(resultRightLateral);
                            }

                            if (double.TryParse(values[6], out var resultLeftSafetyFactor))
                            {
                                forceValues.LeftSafetyFactor.Add(resultLeftSafetyFactor);
                            }

                            if (double.TryParse(values[7], out var resultRightSafetyFactor))
                            {
                                forceValues.RightSafetyFactor.Add(resultRightSafetyFactor);
                            }
                        }
                    }
                }
            
         }

        private static void AddNameSignal(string filePathForMethod, MovingAverage movingAverage)
        {
            //длинна слова ROGIBsignal
            const int wordLength = 11;
            //число необходимых цифр после слова ROGIBsignal
            const int numberSignal = 4;
            const string nameConst = "Сигнал";
            //находим индекс слова ROGIBsignal 
            var indexSignal = filePathForMethod.IndexOf(("ROGIBsignal"), StringComparison.Ordinal);
            //удаление символов до цифр сигнала
            var removalBefore = filePathForMethod.Substring(indexSignal + wordLength);
            //удаление в названии .csv
            var removalAfter = removalBefore.Substring(0, numberSignal);
            movingAverage.NameSignal = nameConst + removalAfter;
        }

        private static void MovingAverage(ForceValues signal, MovingAverage movingAverage)
        {
            //нахождения среднего числа из 10 элементов
            var tenElemetns = 0;
            if (movingAverage._decimalSpeed <= 40)
            {
                ForSpeedLess40(signal, movingAverage);
                return;
            }
            //количество элементов
            var totalCount = signal.TimeDecimal.Count;
            //численное значение для цикла while
            var countWhile = 0;
            //количество значений при прохождении расстояния в одно окно
            var countSumValue = 10;
            //смещение границы значений т.е первые 10 значений,потом с 11 по 20 и тд
            var countValue = 10;
            //индекс записываемого времени 
            var timeForMmovingAverage = 9;

            while (countWhile <= totalCount)
            {
                //сумма чисел
                var results = new Dictionary<string, double>
                {
                    {"sumValueLeftVertical",0},
                    {"sumValueRightVertical",0},

                    {"sumValueLeftLateral",0},
                    {"sumValueRightLateral",0},

                    {"sumValueLeftSafetyFactor",0},
                    {"sumValueRightSafetyFactor",0}
                };
                //суммирование значений
                for (var i = tenElemetns; i < countValue; i++)
                {
                    results["sumValueLeftVertical"] += signal.LeftVertical[i];
                    results["sumValueRightVertical"] += signal.RightVertical[i];
                    results["sumValueLeftLateral"] += signal.LeftLateral[i];
                    results["sumValueRightLateral"] += signal.RightLateral[i];
                    results["sumValueLeftSafetyFactor"] += signal.LeftSafetyFactor[i];
                    results["sumValueRightSafetyFactor"] += signal.RightSafetyFactor[i];
                }
                //добавление значений в модель
                foreach (var i in results)
                {
                    
                    var value = Math.Round(i.Value / countSumValue, 2);
                    ////Изменить !!!
                    //if (value < 1.3)
                    //{
                    //    value = 1.3;
                    //}
                   
                    if ( !double.IsNaN(value))
                    {
                        switch (i.Key)
                        {
                            case "sumValueLeftVertical":
                                movingAverage.LeftVerticalForMovingAverage.Add(value);
                                break;
                            case "sumValueRightVertical":
                                movingAverage.RightVerticalForMovingAverage.Add(value);
                                break;
                            case "sumValueLeftLateral":
                                movingAverage.LeftLateralForMovingAverage.Add(value);
                                break;
                            case "sumValueRightLateral":
                                movingAverage.RightLateralForMovingAverage.Add(value);
                                break;
                            case "sumValueLeftSafetyFactor":
                                movingAverage.LeftSafetyFactorForMovingAverage.Add(value);
                                break;
                            case "sumValueRightSafetyFactor":
                                movingAverage.RightSafetyFactorForMovingAverage.Add(value);
                                break;
                        }
                    }
                }
                //добавление время элемента кратному 10 элементу 
                movingAverage.TimeForMovingAverage.Add(signal.Time[timeForMmovingAverage]);

                tenElemetns += 10;
                countValue += 10;
                timeForMmovingAverage += 10;
                if (countWhile == totalCount)
                {
                    break;
                }
                if (countValue < totalCount)
                {
                    countWhile += countSumValue;
                }
                if(countValue > totalCount)
                {
                    countSumValue = totalCount % 10;
                    countWhile = totalCount;
                    countValue = totalCount;
                    timeForMmovingAverage = totalCount - 1;
                }
            }
        }
        private static void SafetyFactorСomparison(ForceValues force, SafetyFactor safetyFactor, MovingAverage movingAverage)
        {
            //определение минимальных значений коэффициента запаса от схода для левого и правого колеса
            var  minLeftSafetyFactor = movingAverage.LeftSafetyFactorForMovingAverage.Min();
            var  minRightSafetyFactor = movingAverage.RightSafetyFactorForMovingAverage.Min();
            //запись в текестовой форме
            movingAverage._MinSafetyFactorLeft = Convert.ToString(minLeftSafetyFactor, CultureInfo.InvariantCulture);
            movingAverage._MinSafetyFactorRight = Convert.ToString(minRightSafetyFactor, CultureInfo.InvariantCulture);
            //запись значений в словарь со скоростями и типом движения
            switch (movingAverage.Place)
            {
                case "Прямая":
                    safetyFactor.StraightSafetyFactorLeft[movingAverage._Speed].Add(minLeftSafetyFactor);
                    safetyFactor.StraightSafetyFactorRight[movingAverage._Speed].Add(minRightSafetyFactor);
                    break;
                case "Кривая650":
                    safetyFactor.R650SafetyFactorLeft[movingAverage._Speed].Add(minLeftSafetyFactor);
                    safetyFactor.R650SafetyFactorRight[movingAverage._Speed].Add(minRightSafetyFactor);
                    break;
                case "Кривая350":
                    safetyFactor.R350SafetyFactorLeft[movingAverage._Speed].Add(minLeftSafetyFactor);
                    safetyFactor.R350SafetyFactorRight[movingAverage._Speed].Add(minRightSafetyFactor);
                    break;
                case "Стрелка":
                    safetyFactor.ArrowSafetyFactorLeft[movingAverage._Speed].Add(minLeftSafetyFactor);
                    safetyFactor.ArrowSafetyFactorRight[movingAverage._Speed].Add(minRightSafetyFactor);
                    break;
            }
        }

        private static void ForSpeedLess40(ForceValues signal, MovingAverage movingAverage)
        {
            movingAverage.LeftVerticalForMovingAverage = signal.LeftVertical;
            movingAverage.RightVerticalForMovingAverage = signal.RightVertical;

            movingAverage.LeftLateralForMovingAverage = signal.LeftLateral;
            movingAverage.RightLateralForMovingAverage = signal.RightLateral;

            movingAverage.LeftSafetyFactorForMovingAverage = signal.LeftSafetyFactor;
            movingAverage.RightSafetyFactorForMovingAverage = signal.RightSafetyFactor;

            movingAverage.TimeForMovingAverage = signal.Time;
        }
    }
}
