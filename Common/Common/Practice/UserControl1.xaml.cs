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

namespace WPFCommon.Practice
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl1 : UserControlWithViewModel
    {
        public UserControl1() 
        {
            InitializeComponent();
            ViewModel = new UserControl1VM();
        }

    
    }

    public class UserControl1VM  : ViewModelBase{

        private String _Test="zzzzzzzzz";
        public String Test {
            set
            {
                if(_Test == value) { return; }
                _Test = value;
                OnPropertyChanged(() => Test);

            }
            get
            {
                return _Test;
            }

        }
        

    }

}
