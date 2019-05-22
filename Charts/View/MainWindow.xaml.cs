using System;
using Charts.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Charts.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindiwViewModel();
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutProject();
            about.Show();
        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            var about = new Settings();
            about.Show();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindiwViewModel;
            vm?.OpenFile();
            vm?.AddSafetyFactorHelper();
        }

        private void ButtonOpenMenu_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            string path = null;
            var _fileNameVertical = "График Вертикальных силы ";
            var _fileNameLateral = "График Боковых силы ";
            var _fileNameSafetyFactor = "График Коэффициента запаса от схода ";
            CreateDirectory(ref path);
            SaveFile(_fileNameVertical, path);
            SaveFile(_fileNameLateral, path);
            SaveFile(_fileNameSafetyFactor, path);
            MessageBoxViewSaveChart();
        }
        //сохранение графика как картинку
        private void SaveFile(string _fileName, string pathForSave)
        {
            var save = DataContext as MainWindiwViewModel;
            ChartValues<double> valueLeft = null;
            ChartValues<double> valueRight = null;
            string titleAxisY = null;
            //выбор данных
            switch (_fileName)
            {
                case "График Вертикальных силы ":
                    valueLeft = save.selectedForce.LeftVerticalForMovingAverage;
                    valueRight = save.selectedForce.RightVerticalForMovingAverage;
                    titleAxisY = "Вертикальные силы, кН";
                    break;
                case "График Боковых силы ":
                    valueLeft = save.selectedForce.LeftLateralForMovingAverage;
                    valueRight = save.selectedForce.RightLateralForMovingAverage;
                    titleAxisY = "Боковых силы, кН";
                    break;
                case "График Коэффициента запаса от схода ":
                    valueLeft = save.selectedForce.LeftSafetyFactorForMovingAverage;
                    valueRight = save.selectedForce.RightSafetyFactorForMovingAverage;
                    titleAxisY = "Коэффициент запаса от схода";
                    break;
            }
            //создавание графика
            if (save.selectedForce != null)
            {
                var myChart = new CartesianChart
                {
                    DataTooltip = null,
                    DisableAnimations = true,
                    Hoverable = true,
                    LegendLocation = LegendLocation.Bottom,
                    Width = 1920,
                    Height = 1080,
                    Series = new SeriesCollection
                    {
                        new LineSeries
                        {
                            StrokeThickness = 1,
                            LineSmoothness = 1,
                            Title = "Левое колесо",
                            Stroke = Brushes.Blue,
                            Fill = Brushes.Transparent,
                            PointGeometrySize = 6,
                            PointForeground = Brushes.White,
                            PointGeometry = null,
                            Values = valueLeft
                        },
                        new LineSeries
                        {
                            StrokeThickness = 1,
                            LineSmoothness = 1,
                            Title = "Правое колесо",
                            Stroke = Brushes.Red,
                            Fill = Brushes.Transparent,
                            PointGeometrySize = 6,
                            PointForeground = Brushes.Gold,
                            PointGeometry = null,
                            Values = valueRight
                        }
                    }
                };

                var _AxisAbscissa = new Axis();
                _AxisAbscissa.Title = "Время,сек";
                _AxisAbscissa.Labels = save.selectedForce.TimeForMovingAverage;
                myChart.AxisX.Add(_AxisAbscissa);

                var _AxisOrdinate = new Axis();
                _AxisOrdinate.Title = titleAxisY;
                myChart.AxisY.Add(_AxisOrdinate);

                var viewbox = new Viewbox();
                viewbox.Child = myChart;
                viewbox.Measure(myChart.RenderSize);
                viewbox.Arrange(new Rect(new Point(0, 0), myChart.RenderSize));
                myChart.Update(true, true);
                myChart.Background = Brushes.White;
                viewbox.UpdateLayout();
                var allName = _fileName + save.selectedForce._NameSignal + ".png";
                
                SaveToPng(myChart, allName, pathForSave);
            }
        }

        private void SaveToPng(FrameworkElement visual, string fileName, string pathForSave)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder, pathForSave);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder, string pathForSave)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            var path = Path.Combine(pathForSave, fileName);
            using (var stream = File.Create(path))
            {
                encoder.Save(stream);
            }
        }
        //создание папки для сохраненных графиков 
        private void CreateDirectory(ref string path)
        {
            try
            {
                if (DataContext is MainWindiwViewModel direct)
                {
                    var pathDirect = direct.pathToFile;
                    var subPathSingalName = @"Графики Сигналов\" + direct.selectedForce._NameSignal;
                    
                    var dirInfo = new DirectoryInfo(pathDirect);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                    dirInfo.CreateSubdirectory(subPathSingalName);
                    path = pathDirect + subPathSingalName;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        //MessageBox об завершении операции 
        private static void MessageBoxViewSaveChart()
        {
            var message = "Графики сохранены";
            var caption = "Результат сохранения";
            var buttons = MessageBoxButton.OK;
            var icon = MessageBoxImage.Information;
            MessageBox.Show(message, caption, buttons, icon);
        }
    }
}
