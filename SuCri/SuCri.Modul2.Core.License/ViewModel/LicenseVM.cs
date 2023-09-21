using SKM.V3.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SuCri.Modul2.Core.License.Model;
using System.Windows.Controls;

namespace SuCri.Modul2.Core.License.ViewModel
{
    public partial class LicenseVM : INotifyPropertyChanged
    {
        public ObservableDictionary<Product, WTKeyHelpers> LicenseKeySource { get; set; }

        public ICommand ActiveLicenseCmd => new RelayCommand<WTKeyHelpers>(ActiveLicense);
        public ICommand DeleteLicenseCmd => new RelayCommand<WTKeyHelpers>(DeleteLicense);
        public ICommand RefreshItemLicenseKeyCmd => new RelayCommand<SelectionChangedEventArgs>(RefreshItemLicenseKey);
        void RefreshItemLicenseKey(SelectionChangedEventArgs e) 
        {
            var oldItem = (KeyValuePair<Product, WTKeyHelpers>)e.RemovedItems[0];
            if(oldItem.Value.LicenseKey != null)
            {
                oldItem.Value.NewLicenseKeyInput = oldItem.Value.LicenseKey.Key;
                oldItem.Value.NewLicenseKeyMessage = oldItem.Value.LicenseKeyMessage;
                oldItem.Value.LicenseKeyStatus = ResultType.Success;
            }
            else 
            {
                oldItem.Value.NewLicenseKeyInput = "";
                oldItem.Value.NewLicenseKeyMessage = "";
            }
        }
        void DeleteLicense(WTKeyHelpers item)
        {
            item.DeactiveLicenseKey();
            item.NewLicenseKeyInput = "";
            item.NewLicenseKeyMessage = "";
        }
        void ActiveLicense(WTKeyHelpers item)
        {
            if (!string.IsNullOrEmpty(item.NewLicenseKeyInput))
            {
                Product foundKey = WTLicenseKey.Instance.AllProductLicenseKey.FirstOrDefault(x => x.Value.Equals(item)).Key;
                item.ActiveLicenseKey(item.NewLicenseKeyInput,((int)foundKey));
            }
        }
    }
}
