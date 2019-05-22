using Charts.ForceModel;
using LiveCharts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace Charts.ViewModel
{
    public class MainWindiwViewModel : BaseVM
    {
        public MovingAverage selectedForce;
        public ObservableCollection<MovingAverage> AllForceModelMovingAverage { get; set; }
        public SafetyFactor safetyFactor = new SafetyFactor();
        //переменные для диаграмм 
        public ChartValues<double> StraightSafetyFactorXAMLleft { get; set; }
        public ChartValues<double> StraightSafetyFactorXamRight { get; set; }

        public ChartValues<double> R650SafetyFactorXAMLleft { get; set; }
        public ChartValues<double> R650SafetyFactorXamRight { get; set; }

        public ChartValues<double> R350SafetyFactorXAMLleft { get; set; }
        public ChartValues<double> R350SafetyFactorXamRight { get; set; }

        public ChartValues<double> ArrowSafetyFactorXAMLLleft { get; set; }
        public ChartValues<double> ArrowSafetyFactorXAMLRight { get; set; }

        public List<string> SpeedForR650SafetyFactorXAML { get; set; }
        public List<string> SpeedForR350SafetyFactorXAML { get; set; }
        public List<string> SpeedForStraighSafetyFactorXAML { get; set; }
        public List<string> SpeedForArrowSafetyFactorXAML { get; set; }

        public string pathToFile = "c:\\";
        private string lastPlace { get; set; }
        private string lastSpeed { get; set; }

        public MovingAverage SelectedForce
        {
            get { return selectedForce; }
            set
            {
                selectedForce = value;
                OnPropertyChanged("SelectedForce");
            }
        }


        //Main
        public MainWindiwViewModel()
        {
            AllForceModelMovingAverage = new ObservableCollection<MovingAverage>();
            AddForXAML();
        }

        private void AddForXAML()
        {
            SpeedForStraighSafetyFactorXAML = safetyFactor.SpeedForStraightSafetyFactor;
            SpeedForR650SafetyFactorXAML = safetyFactor.SpeedForR650SafetyFactor;
            SpeedForR350SafetyFactorXAML = safetyFactor.SpeedForR350SafetyFactor;
            SpeedForArrowSafetyFactorXAML = safetyFactor.SpeedForArrowSafetyFactor;

            StraightSafetyFactorXAMLleft = safetyFactor.ValueForChartStraightLeft;
            StraightSafetyFactorXamRight = safetyFactor.ValueForChartStraightRight;
            R650SafetyFactorXAMLleft = safetyFactor.ValueForChartR650Left;
            R650SafetyFactorXamRight = safetyFactor.ValueForChartR650Right;
            R350SafetyFactorXAMLleft = safetyFactor.ValueForChartR350Left;
            R350SafetyFactorXamRight = safetyFactor.ValueForChartR350Right;
            ArrowSafetyFactorXAMLLleft = safetyFactor.ValueForChartArrowLeft;
            ArrowSafetyFactorXAMLRight = safetyFactor.ValueForChartArrowRight;
        }

        //Дополнительные методы
        public  void OpenFile()
        {
            bool errorOpenFile = false;
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = pathToFile,
                Filter = "All files (*.*)|*.*|CSV files(*.CSV)|*.CSV",
                FilterIndex = 2,
                RestoreDirectory = true
            };
           
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.FileName.Contains("ROGIBsignal"))
                {
                    try
                    {
                        var data = Helper.Helper.ReadCSVfile(openFileDialog.FileName, safetyFactor);
                        if (!errorOpenFile)
                        {
                            //запоминание пути до файла
                            var indexSignal = openFileDialog.FileName.IndexOf(("ROGIBsignal"), StringComparison.Ordinal);//-11
                            pathToFile = openFileDialog.FileName.Substring(0, indexSignal);
                            //тип участка и скорость последнего файла
                            lastPlace = data.Item2._Place;
                            lastSpeed = data.Item2._Speed;
                            //добавление сигнала в общий список
                            AllForceModelMovingAverage.Add(data.Item2);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    var message = "Ошибка";
                    var caption = "Выберите файл с названием ROGIBsignal";
                    var buttons = MessageBoxButton.OK;
                    var icon = MessageBoxImage.Error;
                    MessageBox.Show(message, caption, buttons, icon);
                }

            }
                
        }

        public void AddSafetyFactorHelper()
        {
            Helper.SafetyFactorHelper.AddSafetyFactor(safetyFactor, lastPlace, lastSpeed);
        }
    }
}
