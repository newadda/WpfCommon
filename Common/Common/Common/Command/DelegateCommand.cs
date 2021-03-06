﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFCommon.Common.Command
{
    public class DelegateCommand<T> : ICommand
    {
        Action<T> executeTargets = delegate { };
        Func<bool> canExecuteTargets = delegate { return true; };
        bool m_Enabled = false;

        public bool CanExecute(object parameter)
        {
            Delegate[] targets = canExecuteTargets.GetInvocationList();
            foreach (Func<bool> target in targets)
            {
                m_Enabled = false;
                bool localenable = target.Invoke();
                if (localenable)
                {
                    m_Enabled = true;
                    break;
                }
            }
            return m_Enabled;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter) == true)
                executeTargets(parameter != null ? (T)parameter : default(T));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public event Action<T> ExecuteTargets
        {
            add
            {
                executeTargets += value;
            }
            remove
            {
                executeTargets -= value;
            }
        }

        public event Func<bool> CanExecuteTargets
        {
            add
            {
                canExecuteTargets += value;
            }
            remove
            {
                canExecuteTargets -= value;
            }
        }
    }

    public class DelegateCommand : ICommand
    {
        Action executeTargets = delegate { };
        Func<bool> canExecuteTargets = delegate { return true; };
        bool m_Enabled = false;

        public bool CanExecute(object parameter)
        {
            Delegate[] targets = canExecuteTargets.GetInvocationList();
            foreach (Func<bool> target in targets)
            {
                m_Enabled = false;
                bool localenable = target.Invoke();
                if (localenable)
                {
                    m_Enabled = true;
                    break;
                }
            }
            return m_Enabled;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter) == true)
                executeTargets();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public event Action ExecuteTargets
        {
            add
            {
                executeTargets += value;
            }
            remove
            {
                executeTargets -= value;
            }
        }

        public event Func<bool> CanExecuteTargets
        {
            add
            {
                canExecuteTargets += value;
            }
            remove
            {
                canExecuteTargets -= value;
            }
        }
    }
}
