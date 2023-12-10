using mvvmLessons.Infrastructure.Commands.Base;
using mvvmLessons.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using mvvmLessons.Models;


namespace mvvmLessons.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Тестовый набор данных для визуализации графиков
        private IEnumerable<DataPoint> _TestDataPoints;

        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;

            set => Set(ref _TestDataPoints, value);
        } 
        #endregion

        #region Заголовок окна
        private string _Title = "Крипторынок";

        /// <summary>
        /// Заголовок окна
        /// </summary>
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
        #endregion

        #region Статус программы
        /// <summary>
        /// Статус программы
        /// </summary>

        private string _Status = "Ready!";
        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion

        #region Команды

        #region CloseCommand

        public ICommand CloseAppCommand { get; }

        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseAppCommandExecuted(object p) => true;

        #endregion

        #endregion
        public MainWindowViewModel()
        {
            #region CommandsObject

            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecuted);

            #endregion

            var data_points = new List<DataPoint>((int)(360/0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;

        }
    }
}
