using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFCommon.Common.RoutedEvents
{
    /**
    *  작성중
    */
    class NonAttachedSwipeEvent : Button
    {

        static NonAttachedSwipeEvent()
        {
            EventManager.RegisterClassHandler(typeof(NonAttachedSwipeEvent), SwipeEvent, new RoutedEventHandler(LocalOn));
        }


        internal static void LocalOn(object sender, RoutedEventArgs e)
        {
            e.Handled = false;

            MessageBox.Show(""+e.ToString());
            //e.Handled = true; //uncommenting this would cause ONLY the subclass' class handler to respond
        }


        public static readonly RoutedEvent SwipeEvent = EventManager.RegisterRoutedEvent(
    "Swipe", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NonAttachedSwipeEvent));

        public event RoutedEventHandler Swipe
        {
            add { AddHandler(SwipeEvent, value); }
            remove { RemoveHandler(SwipeEvent, value); }
        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(NonAttachedSwipeEvent.SwipeEvent);
            RaiseEvent(newEventArgs);
        }

        protected override void OnClick()
        {
            RaiseTapEvent();
        }

    }
}
