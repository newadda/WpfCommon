using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common.Paging
{
    public class Pager : INotifyPropertyChanged
    {
        private IEnumerable _Items;
        public IEnumerable Items
        {
            get { return _Items; }
            set
            {
                if (_Items == value) { return; }
                _Items = value;
                OnPropertyChanged(() => Items);
            }
        }





        private int _ItemCount = -1;
        public int ItemCount
        {
            get { return _ItemCount; }
            set
            {
                if (_ItemCount == value) { return; }
                _ItemCount = value;
                OnPropertyChanged(() => ItemCount);
            }
        }


        private int _PageSize = 1;
        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                if (_PageSize == value || value <= 0) { return; }
                _PageSize = value;
                OnPropertyChanged(() => PageSize);
            }
        }

        private int _PageCount;
        public int PageCount
        {
            get
            {
                return ItemCount / PageSize;
            }
            private set
            {
                if (_PageCount == value) { return; }
                _PageCount = value;
                OnPropertyChanged(() => PageCount);
            }
        }

        private int _PageIndex;
        public int PageIndex
        {
            get { return _PageIndex; }
            set
            {
                if (_PageIndex == value) { return; }
                _PageIndex = value;
                OnPropertyChanged(() => PageIndex);
            }
        }


        public delegate IEnumerable MoveToPageDelegate(int pageSize, int pageIndex, out int TotalCount);
        private MoveToPageDelegate _ActionMoveToPage;
        public MoveToPageDelegate ActionMoveToPage
        {
            get { return _ActionMoveToPage; }
            set
            {
                if (_ActionMoveToPage == value) { return; }
                _ActionMoveToPage = value;
                OnPropertyChanged(() => ActionMoveToPage);
            }
        }

        public bool MoveToFirstPage()
        {
            return MoveToPage(0);
        }

        public bool MoveToLastPage()
        {
            return MoveToPage(PageCount - 1);
        }
        public bool MoveToNextPage()
        {
            return MoveToPage(PageIndex + 1);
        }
        public bool MoveToPreviousPage()
        {
            return MoveToPage(PageIndex - 1);
        }
        public bool MoveToPage(int movePageIndex)
        {
            if (_ActionMoveToPage == null) { return false; }
            int itemCount = -1;
            Items = _ActionMoveToPage(PageSize, movePageIndex, out itemCount);
            PageIndex = movePageIndex;
            if (ItemCount >= 0)
            {
                ItemCount = itemCount;
            }
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
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


        private void OnPropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression)
        {

            var propertyName = GetPropertyName(propertyExpression);
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
    }
}
