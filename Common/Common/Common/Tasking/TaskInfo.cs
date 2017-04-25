using Common.Common.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Common.Tasking
{

    public enum TaskSimpleStatus { NONE, RUNNING, FAILED, CANCELED, COMPLETED }

    public class TaskInfo : ViewModelBase
    {
        private Task _CurrentTask;
        private CancellationTokenSource _CancelSource;
        private TaskSimpleStatus _Status;
        private Exception _FailedReason;

        public Task CurrentTask
        {
            get
            {
                return _CurrentTask;
            }
            set
            {
                if (_CurrentTask == value) { return ; }
                _CurrentTask = value;
                OnPropertyChanged(() => CurrentTask);
            }
        }


        public CancellationTokenSource CancelSource
        {
            get
            {

                return _CancelSource;
            }
            set
            {
                if (_CancelSource == value) { return; }
                _CancelSource = value;
                OnPropertyChanged(() => CancelSource);
            }

        }

        public TaskSimpleStatus Status
        {
            get
            {

                return _Status;
            }
            set
            {
                if (_Status == value) { return; }
                _Status = value;
                OnPropertyChanged(() => Status);
            }

        }


        public Exception FailedReason
        {
            get
            {

                return _FailedReason;
            }
            set
            {
                if (_FailedReason == value) { return; }
                _FailedReason = value;
                OnPropertyChanged(() => FailedReason);
    
            }

        }

      

        public TaskInfo()
        {
            Reset();
        }

        public void Reset()
        {
            CurrentTask = null;
            CancelSource = null;
            FailedReason = null;
            Status = TaskSimpleStatus.NONE;
        }




        public void SetTaskCancelSource(Task currentTask, CancellationTokenSource cancelSource)
        {
            if(currentTask==null)
            {
                return;
            }

            CurrentTask = currentTask;
            CurrentTask.ContinueWith((task) =>
            {
                Update();
            });

            _CancelSource = cancelSource;
            Update();
        }

        public void start()
        {
            Task task = CurrentTask;
            if (task != null && task.Status == TaskStatus.Created)
            {
                task.Start();
            }
            Update();
        }

        public void Cancel()
        {

            CancellationTokenSource cancelSource = CancelSource;
            if (cancelSource != null)
            {
                cancelSource.Cancel();
                Update();
            }
        }

        public void Update()
        {
            Task task = CurrentTask;
            if (task != null)
            {
                switch (task.Status)
                {
                    case TaskStatus.Canceled:
                        this.Status = TaskSimpleStatus.CANCELED;
                        break;
                    case TaskStatus.Faulted:
                        this.Status = TaskSimpleStatus.FAILED;
                        this.FailedReason = task.Exception.GetBaseException();
                        break;
                    case TaskStatus.RanToCompletion:
                        this.Status = TaskSimpleStatus.COMPLETED;
                        break;
                    default:
                        this.Status = TaskSimpleStatus.RUNNING;
                        break;
                }
            }
            else
            {
                this.Status = TaskSimpleStatus.NONE;
            }


            
        }
    }
}
