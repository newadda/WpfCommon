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
using System.Windows.Threading;
using WPFCommon.Common.ViewModel.Basic;

namespace WPFCommon.Practice.Converters
{
    /// <summary>
    /// ConverterTestUC.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConverterTestUC : UserControl
    {
        public ConverterTestUC()
        {
            InitializeComponent();
            
        }

     
    }


    public class ConverterTestUCViewModel : ViewModelBase
    {
        private String _Test1 ;
        public String Test1
        {
            set
            {
                if (_Test1 == value) { return; }
                _Test1 = value;
                OnPropertyChanged(() => Test1);

            }
            get
            {
                return _Test1;
            }

        }

        private String _Test2;
        public String Test2
        {
            set
            {
                if (_Test2 == value) { return; }
                _Test2 = value;
                OnPropertyChanged(() => Test2);

            }
            get
            {
                return _Test2;
            }

        }

        private String _Test3;
        public String Test3
        {
            set
            {
                if (_Test3 == value) { return; }
                _Test3 = value;
                OnPropertyChanged(() => Test3);

            }
            get
            {
                return _Test3;
            }

        }


        public ConverterTestUCViewModel()
        {
            Test1 = "첫번째";
            Test2 = "두번째";
            Test3 = "세번째";
        }


    }





}
