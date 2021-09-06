using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFCommon.Practice.Databinding.CollectionView;

namespace WPFCommon.Practice.Databinding
{
    /// <summary>
    /// ColltionViewPractice.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColltionViewPractice : Window
    {
        CollectionViewViewModel vm;

        public ColltionViewPractice()
        {
            InitializeComponent();
            vm = new CollectionViewViewModel();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ObservableCollection<KeyValueModel> datas = new ObservableCollection<KeyValueModel>();
            datas.Add(new KeyValueModel() { Key = "이순신", Value = "장군" });
            datas.Add(new KeyValueModel() { Key = "홍길동", Value = "장군" });
            datas.Add(new KeyValueModel() { Key = "임꺽정", Value = "장군" });
            datas.Add(new KeyValueModel() { Key = "김유신", Value = "장군" });
            datas.Add(new KeyValueModel() { Key = "이성계", Value = "장군" });
            datas.Add(new KeyValueModel() { Key = "게이츠", Value = "장군" });
            vm.Datas = datas;

        }

        private void tbx_search_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            
           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lb_datas.ItemsSource);
            if (view == null)
            {
                return;
            }
            

            view.Filter = item => {
                var temp = item as KeyValueModel;
                return temp.Key.Contains(tbx_search.Text);
            };
        }
    }
}
