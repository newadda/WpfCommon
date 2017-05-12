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
using WPFCommon.Common.Controls;
using WPFCommon.Common.ViewModel.Basic;
using WPFCommon.Practice.Controls.Dialogs.ViewModelAccess;

namespace WPFCommon.Practice.Controls.Dialogs
{
    /// <summary>
    /// DialogTest.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DialogTest : UserControlWithViewModel
    {
        public DialogTest()
        {
            InitializeComponent();
            ViewModel = new DialogTestVM();
        }


        /// <summary>
        /// ViewModel을 통한 접근 방식
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            TextInputUserControlVM viewModel = new TextInputUserControlVM();

            Window dialogWindow = new Window();
            dialogWindow.VerticalContentAlignment = VerticalAlignment.Stretch;
            dialogWindow.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;

            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;


            TextInputUserControl dialog = new TextInputUserControl();
            dialog.ViewModel = viewModel;

            viewModel.SuccessCommand.ExecuteTargets += () => {
                dialogWindow.DialogResult = true;
                tbk.Text = viewModel.Text;
                
            };
            viewModel.FailCommand.ExecuteTargets += () => { dialogWindow.DialogResult = false; };
            viewModel.CancelCommand.ExecuteTargets += () => { dialogWindow.DialogResult = false; };

            dialogWindow.Content = dialog;
            dialogWindow.ShowDialog();

        }
    }

    public class DialogTestVM : ViewModelBase
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
    }
}