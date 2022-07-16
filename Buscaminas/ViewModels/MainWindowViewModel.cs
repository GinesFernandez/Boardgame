using System.Windows.Input;
using Common.Base;
using Commono.Base;
using MineSweeper.Services;

namespace MineSweeper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MineGameOrchestrator _orchestrator;
        public MineGameOrchestrator Orchestrator
        {
            get { return _orchestrator; }
            set
            {
                if (_orchestrator == value)
                    return;

                _orchestrator = value;
                RaisePropertyChanged();
            }
        }

        public int _rows = Globals.Rows;
        public int Rows
        {
            get { return _rows; }
            set
            {
                if (_rows == value)
                {
                    return;
                }

                if (!ValidateSetup(value, _columns, _mines))
                {
                    return;
                }

                _rows = value;
                RaisePropertyChanged();

                Orchestrator.Reset(_rows, _columns, _mines);
            }
        }

        public int _columns = Globals.Columns;
        public int Columns
        {
            get { return _columns; }
            set
            {
                if (_columns == value)
                {
                    return;
                }

                if (!ValidateSetup(_rows, value, _mines))
                {
                    return;
                }

                _columns = value;
                RaisePropertyChanged();

                Orchestrator.Reset(_rows, _columns, _mines);
            }
        }

        public int _mines = Globals.Mines;
        public int Mines
        {
            get { return _mines; }
            set
            {
                if (_mines == value)
                {
                    return;
                }

                if (!ValidateSetup(_rows, _columns, value))
                {
                    return;
                }

                _mines = value;
                RaisePropertyChanged();

                Orchestrator.Reset(_rows, _columns, _mines);
            }
        }

        private ICommand _restartCommand;
        public ICommand RestartCommand
        {
            get
            {
                return _restartCommand ?? (_restartCommand = new CommandHandler(() => Restart(), true));
            }
        }

        public void Restart()
        {
            Orchestrator.Reset();
        }

        public MainWindowViewModel(MineGameOrchestrator orchestrator)
        {
            Orchestrator = orchestrator;
        }

        private bool ValidateSetup(int rows, int columns, int mines)
        {
            return (mines < (rows * columns)) && (rows * columns > 1);
        }
    }
}
