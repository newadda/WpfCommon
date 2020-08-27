using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPFCommon.Common.RoutedEvents
{
    /**
     *  작성중
     */
    static class AttachedSwipeEvent
    {
        public static readonly DependencyProperty Swipe2Property =
            DependencyProperty.RegisterAttached("Swipe2", typeof(RoutedEventHandler), typeof(AttachedSwipeEvent),
            new FrameworkPropertyMetadata(TemplateChanged));

        public static RoutedEventHandler GetSwipe2(UIElement target)
        {
            return (RoutedEventHandler)target.GetValue(Swipe2Property);
        }
        public static void SetSwipe2(UIElement target, RoutedEventHandler value)
        {
            
            target.SetValue(Swipe2Property, value);
        }
        private static void TemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ui = d as UIElement;
            Boolean flag = false;
       
            ui.PreviewMouseDown += (s, ee) => {
                RoutedEventArgs newEventArgs = new RoutedEventArgs(AttachedSwipeEvent.SwipeEvent);
                ui.RaiseEvent(newEventArgs);
            };
       
            AddSwipeHandler(d, (RoutedEventHandler)e.NewValue);

        }


        public static readonly RoutedEvent SwipeEvent = EventManager.RegisterRoutedEvent(
       "Swipe", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AttachedSwipeEvent));

        public static void AddSwipeHandler(DependencyObject d, RoutedEventHandler handler)
        {
           
            UIElement uie = d as UIElement;
            if (uie != null)
            {
                uie.AddHandler(AttachedSwipeEvent.SwipeEvent, handler);
            }
        }


        public static void RemoveSwipeHandler(DependencyObject d, RoutedEventHandler handler)
        {
            UIElement uie = d as UIElement;
            if (uie != null)
            {
                uie.RemoveHandler(AttachedSwipeEvent.SwipeEvent, handler);
            }

        }


    }
}
