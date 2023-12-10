using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using mvvmLessons.Infrastructure.Commands.Base;
using mvvmLessons.ViewModels.Base;
using OxyPlot;
using OxyPlot.Series;

namespace mvvmLessons.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private PlotModel _plotModel;

        public PlotModel PlotModel
        {
            get => _plotModel;
            set => Set(ref _plotModel, value);
        }

        private IEnumerable<DataPoint> _testDataPoints;

        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }

        private string _Title = "Крипторынок";

        public string Title
        {
            get => _Title;
            set
            {
                if (Equals(value, _Title)) return;
                _Title = value;
                OnPropertyChanged();
            }
        }

        private string _Status = "Ready!";

        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        public ICommand CloseAppCommand { get; }

        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseAppCommandExecuted(object p) => true;

        public MainWindowViewModel()
        {
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecuted);

            var dataPoints = new List<DataPoint>();

            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double toRad = Math.PI / 180;
                var y = Math.Sin(x * toRad);

                dataPoints.Add(new DataPoint(x, y));
            }

            TestDataPoints = dataPoints;

            PlotModel = new PlotModel();

            var lineSeries = new LineSeries
            {
                Title = "Sin(x)"
            };

            foreach (var dataPoint in TestDataPoints)
            {
                lineSeries.Points.Add(dataPoint);
            }

            PlotModel.Series.Add(lineSeries);
        }
    }
}