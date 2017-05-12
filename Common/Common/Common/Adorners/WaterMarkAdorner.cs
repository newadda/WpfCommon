using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFCommon.Common.Adorners
{
    public class WaterMarkAdorner : Adorner
    {
        VisualCollection visualChildren;
        TextBox waterMarkTextBox;

        private String _WaterMarkText;
        public String WaterMarkText {
            set
            {
                _WaterMarkText = value;
                UpdateWaterMarkText(value);
            }
            get
            {
                return _WaterMarkText;
            }

        }


        public WaterMarkAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);
            waterMarkTextBox = new TextBox();
            visualChildren.Add(waterMarkTextBox);


        }

        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }


        private void UpdateWaterMarkText(string waterMarkText)
        {
            waterMarkTextBox.Text= waterMarkText;
        }



        protected override Size ArrangeOverride(Size finalSize)
        {
           

            TextBox origin=AdornedElement as TextBox;

            waterMarkTextBox.IsHitTestVisible = false;
            waterMarkTextBox.Focusable = false;
            waterMarkTextBox.Foreground = new SolidColorBrush(Color.FromArgb((byte)0xB3, 0, 0, 0));
            waterMarkTextBox.Background = new SolidColorBrush(Color.FromArgb((byte)0x00,0,0,0));
            waterMarkTextBox.FontFamily = origin.FontFamily;
            waterMarkTextBox.FontSize = origin.FontSize;
            waterMarkTextBox.FontStretch = origin.FontStretch;
            waterMarkTextBox.FontWeight = origin.FontWeight;

            waterMarkTextBox.Measure(finalSize);
            waterMarkTextBox.Arrange(new Rect(0, 0, ((FrameworkElement)AdornedElement).ActualWidth, ((FrameworkElement)AdornedElement).ActualHeight));
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

    }
}
