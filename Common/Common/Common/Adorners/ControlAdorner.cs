using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFCommon.Common.Adorners
{
    public class ControlAdorner : Adorner
    {
        private Control _child;

        public ControlAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException();
            return _child;
        }

        public Control Child
        {
            get { return _child; }
            set
            {
                if (_child != null)
                {
                    RemoveVisualChild(_child);
                }
                _child = value;
                if (_child != null)
                {
                    AddVisualChild(_child);
                }
                
            }
        }


        protected override Size MeasureOverride(Size constraint)
        {
            Child.Measure(constraint);
            return Child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Child.Arrange(new Rect(new Point(0, 0), finalSize));
            UpdateLocation();
            return new Size(Child.ActualWidth, Child.ActualHeight);
        }

        private void UpdateLocation()
        {
            AdornedPlaceholder Placeholder = FindChildByType<AdornedPlaceholder>(this);

            if (Placeholder == null) return;
            Transform t = (Transform)TransformToDescendant(Placeholder);
            if (t == Transform.Identity) return;
            var oldTransfor = RenderTransform;
            if (oldTransfor == null || oldTransfor == Transform.Identity)
            {
                RenderTransform = t;
            }
            else
            {
                TransformGroup g = new TransformGroup();
                g.Children.Add(oldTransfor);
                g.Children.Add(t);
                RenderTransform =
                    new MatrixTransform(g.Value);
            }
        }

        public  T FindChildByType<T>(DependencyObject parent)
  where T : DependencyObject
        {
            // Confirm parent and childName are valid.
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChildByType<T>(child);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else if (childType != null)
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child’s name is set for search
                    if (frameworkElement != null && frameworkElement.GetType() == childType.GetType())
                    {
                        // if the child’s name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }

}

