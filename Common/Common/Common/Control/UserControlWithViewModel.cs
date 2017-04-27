using WPFCommon.Common.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFCommon.Common.Control;

namespace WPFCommon.Common.Control
{
    public class ViewModelChangedEventArgs : RoutedEventArgs
    {
        public Object OldVeiwModel { private set; get; }
        public Object NewVeiwModel { private set; get; }

        public ViewModelChangedEventArgs(Object oldViewModel, Object newViewModel)
        {
            this.OldVeiwModel = oldViewModel;
            this.NewVeiwModel = newViewModel;
        }

    }


    /// <summary>
    /// ViewModel을 명시적으로 넘기기 위해 만들었다.
    /// 외부에서 UserControl내의 ViewModel을 바꾸거나 접근하여 이것저것 할 수 있다.
    /// </summary>
    public  class UserControlWithViewModel : UserControl
    {
        

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(Object), typeof(UserControlWithViewModel),
             new FrameworkPropertyMetadata(new PropertyChangedCallback(OnViewModelPropertyChanged)));

        private static void OnViewModelPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UserControlWithViewModel control = sender as UserControlWithViewModel;
            control.RaiseViewModelChangedEvent(e.OldValue,e.NewValue);
        }

        public static readonly RoutedEvent ViewModelChangedEvent = EventManager.RegisterRoutedEvent(
         "ViewModelChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(UserControlWithViewModel));

        public virtual event RoutedEventHandler ViewModelChanged
        {
            add { AddHandler(ViewModelChangedEvent, value); }
            remove { RemoveHandler(ViewModelChangedEvent, value); }
        }

        public virtual  void  RaiseViewModelChangedEvent(Object oldViewModel,Object newViewModel)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(UserControlWithViewModel.ViewModelChangedEvent, newViewModel);
            RaiseEvent(newEventArgs);
        }


        public virtual Object ViewModel
        {
            set { SetValue(ViewModelProperty, value); }
            get { return (Object)GetValue(ViewModelProperty); }
        }

    }

       
}
