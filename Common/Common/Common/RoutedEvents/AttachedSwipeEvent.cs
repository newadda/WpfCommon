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
    class AttachedSwipeEvent
    {
       
     
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

        private static void Uie_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("this is invoked before the On* class handler on UIElement");
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
