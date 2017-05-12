using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFCommon.Common.Command;
using WPFCommon.Common.Controls;
using WPFCommon.Common.ViewModel.Basic;

namespace WPFCommon.Practice.Controls.Dialogs.ViewModelAccess
{
    /// <summary>
    /// TextInputUserControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TextInputUserControl : UserControlWithViewModel
    {
        public TextInputUserControl()
        {
            InitializeComponent();
            ViewModel = new TextInputUserControlVM();
       

        }

     
    }

    public class TextInputUserControlVM : ViewModelBase
    {

        private String _Text;
        public String Text
        {
            set
            {
                if (_Text == value) { return; }
                _Text = value;
                OnPropertyChanged(() => Text);

            }
            get
            {
                return _Text;
            }

        }

        DelegateCommand _SuccessCommand;
        public DelegateCommand SuccessCommand
        {
            get
            {
                return _SuccessCommand;
            }

            set
            {
                if (_SuccessCommand == value)
                {
                    return;
                }
                _SuccessCommand = value;
                OnPropertyChanged(() => SuccessCommand);

            }
        }
        DelegateCommand _FailCommand;
        public DelegateCommand FailCommand
        {
            get
            {
                return _FailCommand;
            }

            set
            {
                if (_FailCommand == value)
                {
                    return;
                }
                _FailCommand = value;
                OnPropertyChanged(() => FailCommand);

            }
        }


        DelegateCommand _CancelCommand;
        public DelegateCommand CancelCommand
        {
            get
            {
                return _CancelCommand;
            }

            set
            {
                if (_CancelCommand == value)
                {
                    return;
                }
                _CancelCommand = value;
                OnPropertyChanged(() => CancelCommand);

            }
        }

        public TextInputUserControlVM()
        {
            Text = "test";
            SuccessCommand = new DelegateCommand();
            FailCommand = new DelegateCommand(); ;
            CancelCommand = new DelegateCommand();
        }

    }


}
