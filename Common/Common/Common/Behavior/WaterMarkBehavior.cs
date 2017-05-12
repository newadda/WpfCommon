using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFCommon.Common.Adorners;

namespace WPFCommon.Common.Behavior
{
    public class WaterMarkBehavior
    {
        public static readonly DependencyProperty WaterMarkTextProperty =
    DependencyProperty.RegisterAttached(
       "WaterMarkText", typeof(String), typeof(WaterMarkBehavior),
       new FrameworkPropertyMetadata(default(String), PropertyChangedCallback));

        private static void PropertyChangedCallback(
           DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            var target = d as TextBox;
            if(target == null)
            {
                return;
            }


            target.SetValue(AttachedAdorner.AdornerInstanceProperty, new WaterMarkAdorner((UIElement)target) { WaterMarkText = GetWaterMarkText((DependencyObject)target) });
            UpdateVisible(target);

            target.TextChanged += Target_TextChanged;
 

            
        }

        private static void UpdateVisible(TextBox target)
        {
            if (String.IsNullOrEmpty(target.Text))
            {
                target.SetValue(AttachedAdorner.IsVisibleProperty, true);
            }
            else
            {
                target.SetValue(AttachedAdorner.IsVisibleProperty, false);
            }
        }


        private static void Target_TextChanged(object sender, TextChangedEventArgs e)
        {
            var target = sender as TextBox;
            UpdateVisible(target);
        }




        public static void SetWaterMarkText(DependencyObject element, String value)
        {
            element.SetValue(WaterMarkTextProperty, value);
        }

        public static String GetWaterMarkText(DependencyObject element)
        {
            return (String)element.GetValue(WaterMarkTextProperty);
        }


    }
}
