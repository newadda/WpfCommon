using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace WPFCommon.Common.Adorners
{
    
    public class AttachedAdorner
    {
        public static readonly DependencyProperty AdornerTypeProperty =
     DependencyProperty.RegisterAttached(
        "AdornerType", typeof(Type), typeof(AttachedAdorner),
        new FrameworkPropertyMetadata(default(Type), AdornerTypePropertyChanged));

        private static void AdornerTypePropertyChanged(
           DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue==null)
            {
                return;
            }

            SetAdornerInstance(d, (Adorner)Activator.CreateInstance((Type)e.NewValue , d as FrameworkElement));

            UpdateAdroner((UIElement)d, GetIsVisible(d), GetAdornerInstance(d));
        }


        public static void SetAdornerType(DependencyObject element, Type value)
        {
            element.SetValue(AdornerTypeProperty, value);
        }

        public static Type GetAdornerType(DependencyObject element)
        {
            return (Type)element.GetValue(AdornerTypeProperty);
        }

        

        public static readonly DependencyProperty AdornerInstanceProperty =
     DependencyProperty.RegisterAttached(
        "AdornerInstance", typeof(Adorner), typeof(AttachedAdorner),
        new FrameworkPropertyMetadata(default(Adorner), AdornerInstancePropertyChanged));

        private static void AdornerInstancePropertyChanged(
           DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
           
        }


        public static void SetAdornerInstance(DependencyObject element, Adorner value)
        {
            element.SetValue(AdornerInstanceProperty, value);
        }

        public static Adorner GetAdornerInstance(DependencyObject element)
        {
            return (Adorner)element.GetValue(AdornerInstanceProperty);
        }









        public static readonly DependencyProperty IsVisibleProperty =
          DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(AttachedAdorner),
          new PropertyMetadata(IsVisibleChanged));
        public static bool GetIsVisible(DependencyObject target)
        {
            return (bool)target.GetValue(IsVisibleProperty);
        }
        public static void SetIsVisible(DependencyObject target, bool value)
        {
            target.SetValue(IsVisibleProperty, value);
        }
        private static void IsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateAdroner((UIElement)d, (bool)e.NewValue, GetAdornerInstance(d));
        }


        

        private static void UpdateAdroner(UIElement adorned, bool isVisible, Adorner adorner)
        {
            
            if (adorner == null)
            {
                return;
            }


            var layer = AdornerLayer.GetAdornerLayer(adorned);

            if (layer == null)
            {
                // if we don't have an adorner layer it's probably
                // because it's too early in the window's construction
                // Let's re-run at a slightly later time
                Dispatcher.CurrentDispatcher.BeginInvoke(
                    DispatcherPriority.Loaded,
                    new Action(() => UpdateAdroner(adorned, isVisible, adorner)));
                return;
            }

            if (isVisible)
            {
                layer.Add(adorner);
            }
            else
            {
                layer.Remove(adorner);
            }

    
        }

    
    }
}
