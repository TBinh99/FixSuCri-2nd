using Autodesk.AutoCAD.Internal;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;
using SuCri.Modul2.Core.License.Model;
using SuCri.Modul2.Core.License.Properties;
using SuCri.Modul2.Core.License.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SuCri.Modul2.Core.License.ViewModel
{
    public partial class LicenseVM : INotifyPropertyChanged
    {
        public LicenseVM()
        {
            LicenseKeySource = WTLicenseKey.Instance.AllProductLicenseKey;
            foreach (WTKeyHelpers item in WTLicenseKey.Instance.AllProductLicenseKey.Values)
            {
                if(item.LicenseKey != null)
                {
                    item.NewLicenseKeyInput = item.LicenseKey.Key;
                }
            }
            CustomerChanged();
        }
        public void CustomerChanged() 
        {
            if (WTLicenseKey.Instance.CustomerLicensesStatus == ResultType.Success)
            {
                CompanyName = WTLicenseKey.Instance.CompanyName;
                Email = WTLicenseKey.Instance.Email;
                Name = WTLicenseKey.Instance.CustomerName;
                CustomerSecertInput = WTLicenseKey.Instance.CustomerSecret;
                CustomerLicenseMessage = null; // or successful etc...
                CustomerLicense = WTLicenseKey.Instance.CustomerLicense;
            }
            else
            {
                //When you trying to put new Customer Secret but not valid and you already have one Customer Secret saved, you will reset info to current customer.
                if (WTLicenseKey.Instance.CustomerInfo != null)
                {
                    WTLicenseKey.Instance.CustomerLicensesStatus = ResultType.Success;
                    CustomerLicenseMessage = WTLicenseKey.Instance.CustomerLicensesMessage;
                }
                //In case you dont have any Customer Secret you will get message and return no infomation
                else if (!string.IsNullOrEmpty(CustomerSecertInput))
                {
                    CustomerLicenseMessage = WTLicenseKey.Instance.CustomerLicensesMessage;
                }
                else 
                {
                    CustomerLicenseMessage = null;
                }
                Name = null;
                CompanyName = null;
                Email = null;
                CustomerLicense = null;
            }
        }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CustomerSecertInput { get; set; }
        public string CustomerLicenseMessage { get; set; }
        public ObservableCollection<WTCustomerLicensesHelper> CustomerLicense { get; set; }

        public ICommand CloseCustomerLicensesCmd => new RelayCommand(CloseCustomerLicenses);
        public ICommand GetCustomerLicensesCmd => new RelayCommand(GetCustomerLicenses);
        public ICommand DeleteCustomerCmd => new RelayCommand(DeleteCustomer);
        public ICommand ActiveKeyCmd => new RelayCommand<WTCustomerLicensesHelper>(ActiveKey);
        public ICommand DeactiveKeyCmd => new RelayCommand<WTCustomerLicensesHelper>(DeactiveKey);
        public ICommand RefreshCustomerInfoCmd => new RelayCommand(CustomerChanged);
        void CloseCustomerLicenses() 
        {
            CustomerSecertInput = "";
            foreach (WTKeyHelpers item in WTLicenseKey.Instance.AllProductLicenseKey.Values)
            {
                item.NewLicenseKeyInput = "";
                item.LicenseKeyMessage = "";
            }
            var intDictionary = new ObservableDictionary<int, WTKeyHelpers>(WTLicenseKey.Instance.AllProductLicenseKey.ToDictionary(kv => (int)kv.Key, kv => kv.Value));
            string json = JsonConvert.SerializeObject(intDictionary);
            Settings.Default.AllProductLicenseKey = json;
            Settings.Default.Save();
        }
        void GetCustomerLicenses()
        {
            if(!string.IsNullOrEmpty(CustomerSecertInput))
            {
                WTLicenseKey.Instance.GetCustomerLicenses(CustomerSecertInput);
                CustomerChanged();
            }
        }
        void DeleteCustomer() 
        {
            WTLicenseKey.Instance.DeleteCustomerLicenses();
            CustomerSecertInput = null;
            CustomerChanged();
        }
        void ActiveKey(WTCustomerLicensesHelper license) 
        {
            WTLicenseKey.Instance.AllProductLicenseKey[(Product)license.ProductId].ActiveLicenseKey(license.ValidLicenseKeys);
            license.IsKeyOnMachine();
        }
        void DeactiveKey(WTCustomerLicensesHelper license)
        {
            WTLicenseKey.Instance.AllProductLicenseKey[(Product)license.ProductId].DeactiveLicenseKey();
            license.IsKeyOnMachine();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class StatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ResultType result = (ResultType)value;
            return result == ResultType.Success ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
