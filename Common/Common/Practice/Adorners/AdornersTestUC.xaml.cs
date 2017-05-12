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

namespace WPFCommon.Practice.Adorners
{
    /// <summary>
    /// AdornersTestUC.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AdornersTestUC : UserControl
    {
        public AdornersTestUC()
        {
            InitializeComponent();
            this.Loaded += AdornersTestUC_Loaded;
        
        }

        private void AdornersTestUC_Loaded(object sender, RoutedEventArgs e)
        {
            TestAdorner testAdorner = new TestAdorner(bt2);
            var layer = AdornerLayer.GetAdornerLayer(bt2);
            layer.Add(testAdorner);
        }
    }


    public class TestAdorner : Adorner
    {
        VisualCollection visualChildren;
        TextBox a;
        TextBox b;
        public TestAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);
            a = (TextBox)adornedElement;
            b = new TextBox();

            visualChildren.Add(b);

            a.GotFocus += A_GotFocus;
        }

        private void A_GotFocus(object sender, RoutedEventArgs e)
        {
            b.Visibility = Visibility.Hidden;
        }

        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }


        protected override Size ArrangeOverride(Size finalSize)
        {
            b.IsHitTestVisible = false;
            b.Text = "ccccccccccccccccccccc";
            b.Measure(finalSize);
            b.Arrange(new Rect(0, 0, a.ActualWidth, a.ActualHeight));
            return new Size(a.ActualWidth, a.ActualHeight);
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            /*
            var text = new FormattedText(
                    "Test Adorner",
                    System.Globalization.CultureInfo.CurrentCulture,
                    System.Windows.FlowDirection.LeftToRight,
                    new System.Windows.Media.Typeface(a.FontFamily.ToString()),
                    a.FontSize,
                    a.Foreground);
            

            drawingContext.DrawText(text, new Point(1, 1));*/
        }
    }


}
