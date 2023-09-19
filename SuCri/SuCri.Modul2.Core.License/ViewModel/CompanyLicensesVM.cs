using Autodesk.AutoCAD.Internal;
using CommunityToolkit.Mvvm.Input;
using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;
using SuCri.Modul2.Core.License.Model;
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
        //public LicenseVM()
        //{
        //    WTLicenseKey.Instance.PropertyChanged += (sender, e) =>
        //        {
        //            if (e.PropertyName == "CustomerInfo")
        //            {
        //                LoadCompanyLicenses();
        //            }
        //        };
        //}
        public GetCustomerLicensesResult CurrentCustomer
        {
            get { return WTLicenseKey.Instance.CustomerInfo; }
            set { }
        }
        void OnCurrentCustomerChanged()
        {
            LoadCompanyLicenses();
        }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CustomerSecertInput { get; set; }
        public string CompanyLicenseMessage { get; set; }
        public ObservableCollection<CustomerLicenseInfo> CustomerLicense { get; set; }
        public ICommand LoadCompanyLicensesCmd => new RelayCommand(LoadCompanyLicenses);
        public ICommand CloseCompanyLicensesCmd => new RelayCommand(CloseCompanyLicenses);
        public ICommand GetCompanyLicenseCmd => new RelayCommand(GetCompanyLicense);
        public ICommand DeleteCustomerCmd => new RelayCommand(DeleteCustomer);
        public ICommand ActiveKeyCmd => new RelayCommand<CustomerLicenseInfo>(ActiveKey);
        public ICommand DeactiveKeyCmd => new RelayCommand(DeactiveKey);
        void LoadCompanyLicenses()
        {
            if (WTLicenseKey.Instance.CompanyLicensesStatus == ResultType.Success)
            {
                var firstKey = WTLicenseKey.Instance.CustomerInfo.LicenseKeys.FirstOrDefault();
                if (firstKey != null)
                {
                    Name = firstKey.Customer.Name;
                    CompanyName = firstKey.Customer.CompanyName;
                    Email = firstKey.Customer.Email;
                }
                else
                {
                    Name = "";
                    CompanyName = "";
                    Email = "";
                    // If dont have key, we cant get infomation of customer
                }
                CustomerLicense = new ObservableCollection<CustomerLicenseInfo>();
                foreach (var licenseInfo in WTLicenseKey.Instance.CustomerInfo.LicenseKeys)
                {
                    CustomerLicense.Add(new CustomerLicenseInfo()
                    {
                        ValidLicenseKeys = licenseInfo.Key,
                        ProductName = WTLicenseKey.Instance.ProductsInfo.Products.First(x => x.Id == licenseInfo.ProductId).Name,
                        ExpirationDate = licenseInfo.Expires,
                        Machines = $"{licenseInfo.ActivatedMachines.Count()}/{licenseInfo.MaxNoOfMachines}",
                        Status = $"{(licenseInfo.Period == 0 ? "Red" : (licenseInfo.Period < 14) ? "Orange" : "Green")}"
                    });
                }
                CustomerSecertInput = WTLicenseKey.Instance.CustomerSecret;
                CompanyLicenseMessage = WTLicenseKey.Instance.CompanyLicensesMessage;
            }
            else
            {
                if (WTLicenseKey.Instance.CustomerInfo != null && string.IsNullOrEmpty(CustomerSecertInput))
                {
                    CompanyLicenseMessage = "";
                    CustomerSecertInput = WTLicenseKey.Instance.CustomerSecret;
                }
                else
                {
                    CompanyLicenseMessage = WTLicenseKey.Instance.CompanyLicensesMessage;
                    //CompanyLicenseMessage = "Your customer secret is not valid";
                }
            }
        }
        void CloseCompanyLicenses() 
        {
            CustomerSecertInput = "";
        }
        void GetCompanyLicense() 
        {
            if(!string.IsNullOrEmpty(CustomerSecertInput))
            {
                WTLicenseKey.Instance.GetCompanyLicenses(CustomerSecertInput);
            }
        }
        void DeleteCustomer() 
        {
            Name = "";
            CompanyName = "";
            Email = "";
            CustomerLicense = new ObservableCollection<CustomerLicenseInfo>();
            CustomerSecertInput = "";
            WTLicenseKey.Instance.DeleteCompanyLicenses();
        }

        void ActiveKey(CustomerLicenseInfo license) 
        {
            LicenseKeyInput = license.ValidLicenseKeys;
            ProductName = license.ProductName;
            int productId = WTLicenseKey.Instance.ProductsInfo.Products.First(x => x.Name == license.ProductName).Id;
            WTLicenseKey.Instance.ActiveLicenseKey(license.ValidLicenseKeys, productId);
        }
        void DeactiveKey()
        {
            LicenseKeyInput = "";
            LicenseKeyMessage = "";
            ProductName = null;
            WTLicenseKey.Instance.DeactiveKey();
        }
        //public event PropertyChangedEventHandler PropertyChanged;
    }
    public class TypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (WTLicenseKey.Instance.LicenseKey!=null)
            {
                return (value.ToString() == WTLicenseKey.Instance.LicenseKey.Key) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
            else 
            {
                return System.Windows.Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
