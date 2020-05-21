using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCommon.Common.ViewModel.Basic;

namespace WPFCommon.Practice.Databinding.CollectionView
{
    class CollectionViewViewModel : ViewModelBase
    {
        private ObservableCollection<KeyValueModel> _Datas;
        public ObservableCollection<KeyValueModel> Datas
        {
            set
            {
                if(_Datas!=value)
                {
                    _Datas = value;
                }
                OnPropertyChanged(() => Datas);
            }
            get
            {
                return this._Datas;
            }
        }

    }
}
