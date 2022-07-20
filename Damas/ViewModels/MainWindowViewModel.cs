using System.Windows.Input;
using Common.Base;
using Commono.Base;
using Damas.Services;

namespace Damas.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public DamasGameOrchestrator _orchestrator;
        public DamasGameOrchestrator Orchestrator
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

                _rows = value;
                RaisePropertyChanged();

                Orchestrator.Reset(_rows, _columns);
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

                _columns = value;
                RaisePropertyChanged();

                Orchestrator.Reset(_rows, _columns);
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

        public MainWindowViewModel(DamasGameOrchestrator orchestrator)
        {
            Orchestrator = orchestrator;
        }
    }
}
