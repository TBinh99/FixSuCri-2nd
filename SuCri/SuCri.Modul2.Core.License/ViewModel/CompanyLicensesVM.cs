using Autodesk.AutoCAD.Internal;
using CommunityToolkit.Mvvm.Input;
using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;
using SuCri.Modul2.Core.License.Model;
using System;
using System.Collections.Generic;
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
    public class CompanyLicensesVM : INotifyPropertyChanged
    {
        public CompanyLicensesVM() 
        {
            WTLicenseKey.Instance.PropertyChanged += (sender, e) => { CurrentCustomer = new GetCustomerLicensesResult(); };
        }
        public GetCustomerLicensesResult CurrentCustomer
        {
            get { return WTLicenseKey.Instance.CustomerInfo; }
            set { }
        }
        void OnCurrentCustomerChanged()
        {
            LoadCompanyLicenses();
        }
        private string tokenWithAllPermission = "WyI1OTMwMzI2NiIsInVpSXM4SG1rU3RYd09IQmh2MmRDRWRDTlNpYWVOcUdFRXJxak5uenEiXQ==";
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CustomerSecertInput { get; set; }
        public string CompanyLicenseMessage { get; set; }
        public List<CustomerLicenseInfo> CustomerLicense { get; set; }
        public ICommand LoadCompanyLicensesCmd => new RelayCommand(LoadCompanyLicenses);
        public ICommand GetCompanyLicenseCmd => new RelayCommand(GetCompanyLicense);
        public ICommand DeleteCustomerCmd => new RelayCommand(DeleteCustomer);
        public ICommand ActiveKeyCmd => new RelayCommand<CustomerLicenseInfo>(ActiveKey);
        public ICommand DeactiveKeyCmd => new RelayCommand(DeactiveKey);

        void GetCompanyLicense() 
        {
            if(!string.IsNullOrEmpty(CustomerSecertInput))
            {
                WTLicenseKey.Instance.GetCompanyLicenses(CustomerSecertInput);
                CompanyLicenseMessage = WTLicenseKey.Instance.CompanyLicensesMessage;
            }
        }
        void LoadCompanyLicenses()
        {
            if (WTLicenseKey.Instance.CustomerInfo != null)
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
                    // If dont have key, we cant get infomation of customer
                }
                CustomerLicense = new List<CustomerLicenseInfo>();
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
            }
            else
            {
                CompanyName = "None";
                Email = "None";
                Name = "None";
                CustomerLicense = new List<CustomerLicenseInfo>();
            }
        }
        void DeleteCustomer() 
        {
            WTLicenseKey.Instance.DeleteCompanyLicenses();
        }

        void ActiveKey(CustomerLicenseInfo license) 
        {
            int productId = WTLicenseKey.Instance.ProductsInfo.Products.First(x => x.Name == license.ProductName).Id;
            WTLicenseKey.Instance.ActiveLicenseKey(license.ValidLicenseKeys, productId);
        }
        void DeactiveKey()
        {
            WTLicenseKey.Instance.DeactiveKey();
        }
        public event PropertyChangedEventHandler PropertyChanged;
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
