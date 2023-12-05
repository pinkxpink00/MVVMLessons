using mvvmLessons.Infrastructure.Commands.Base;
using mvvmLessons.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace mvvmLessons.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "Анализ статистики CV19";

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

        }
    }
}
