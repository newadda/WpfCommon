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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPFCommon.Practice.Adorners;
using WPFCommon.Practice.Converters;
using WPFCommon.Practice.Databinding;
using WPFCommon.Practice.Style;

namespace Common
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void converterBt_Click(object sender, RoutedEventArgs e)
        {

            Window dialogWindow = new Window();
            dialogWindow.VerticalContentAlignment = VerticalAlignment.Stretch;
            dialogWindow.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ConverterTestUC dialog = new ConverterTestUC();
            dialogWindow.Content = dialog;
            dialogWindow.ShowDialog();
        }

        private void testBt_Click(object sender, RoutedEventArgs e)
        {
            Window dialogWindow = new Window();
            dialogWindow.VerticalContentAlignment = VerticalAlignment.Stretch;
            dialogWindow.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AdornersTestUC dialog = new AdornersTestUC();
            dialogWindow.Content = dialog;
            dialogWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ColltionViewPractice();
            window.ShowDialog();

        }

        private void button_Swipe(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("button_Swipe");
               
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window dialogWindow = new StyleTest01();
            dialogWindow.ShowDialog();
        }
    }
}
