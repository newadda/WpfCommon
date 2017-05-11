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

namespace WPFCommon.Practice.Controls.CustomControls
{
    /// <summary>
    /// XAML 파일에서 이 사용자 지정 컨트롤을 사용하려면 1a 또는 1b단계를 수행한 다음 2단계를 수행하십시오.
    ///
    /// 1a단계) 현재 프로젝트에 있는 XAML 파일에서 이 사용자 지정 컨트롤 사용.
    /// 이 XmlNamespace 특성을 사용할 마크업 파일의 루트 요소에 이 특성을 
    /// 추가합니다.
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFCommon.Practice.Controls.CustomControls"
    ///
    ///
    /// 1b단계) 다른 프로젝트에 있는 XAML 파일에서 이 사용자 지정 컨트롤 사용.
    /// 이 XmlNamespace 특성을 사용할 마크업 파일의 루트 요소에 이 특성을 
    /// 추가합니다.
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFCommon.Practice.Controls.CustomControls;assembly=WPFCommon.Practice.Controls.CustomControls"
    ///
    /// 또한 XAML 파일이 있는 프로젝트의 프로젝트 참조를 이 프로젝트에 추가하고
    /// 다시 빌드하여 컴파일 오류를 방지해야 합니다.
    ///
    ///     솔루션 탐색기에서 대상 프로젝트를 마우스 오른쪽 단추로 클릭하고
    ///     [참조 추가]->[프로젝트]를 차례로 클릭한 다음 이 프로젝트를 찾아서 선택합니다.
    ///
    ///
    /// 2단계)
    /// 계속 진행하여 XAML 파일에서 컨트롤을 사용합니다.
    ///
    ///     <MyNamespace:NumberTextBox/>
    ///
    /// </summary>
    public class NumberTextBox : TextBox
    {
        static NumberTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberTextBox), new FrameworkPropertyMetadata(typeof(NumberTextBox)));
            PropertyMetadata baseMetadata = TextBox.TextProperty.GetMetadata(typeof(TextBox));
            TextBox.TextProperty.OverrideMetadata(typeof(NumberTextBox), new FrameworkPropertyMetadata("0", OnTextPropertyChangCallback, CoerceText));
            // TextBox.TextProperty.OverrideMetadata(typeof(NumberTextBox), new FrameworkPropertyMetadata("0", baseMetadata.PropertyChangedCallback, CoerceText));


        }
        public static void OnTextPropertyChangCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


        }

        /// <summary>
        /// 유효성 검사 ( 숫자 아닐시 입력 안되게 막았다. )
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object CoerceText(DependencyObject d, object value)
        {

            String newValue = value as String;
            if (newValue == null)
            {
                return "0";
            }
            try
            {
                int.Parse(newValue);
                return value;
            }
            catch
            {

            }
            return d.GetValue(NumberTextBox.TextProperty);


        }

        public NumberTextBox() : base()
        {
            Text = "0";

        }





        public void Up()
        {

        }

        public void Down()
        {

        }



    }
}