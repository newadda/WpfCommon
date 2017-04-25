using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common.ViewModel.Basic
{
    /// <summary>
    /// 아주 간단한 ViewModelBase
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)        {            PropertyChangedEventHandler handler = this.PropertyChanged;            if (handler != null)            {                var e = new PropertyChangedEventArgs(propertyName);                handler(this, e);            }        }

        public void OnPropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression)
        {

            var propertyName = GetPropertyName(propertyExpression);
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }


        private  string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }


      
    }
}
