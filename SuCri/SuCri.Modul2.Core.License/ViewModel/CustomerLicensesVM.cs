﻿using Autodesk.AutoCAD.Internal;
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
            WTLicenseKey.Instance.PropertyChanged += (sender, e) =>
                {
                    if(e.PropertyName == "LicenseKey" || e.PropertyName == "CustomerInfo")
                    {
                        LoadCustomerLicenses();
                        //OnPropertyChanged("ValidLicenseKeys");
                        //OnPropertyChanged("CustomerLicense");
                    }
                };
            foreach (WTKeyHelpers item in WTLicenseKey.Instance.AllProductLicenseKey.Values)
            {
                if(item.LicenseKey != null)
                {
                    item.NewLicenseKeyInput = item.LicenseKey.Key;
                }
            }
        }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CustomerSecertInput { get; set; }
        public string CustomerLicenseMessage { get; set; }
        public ObservableCollection<CustomerLicenseInfo> CustomerLicense { get; set; }

        public ICommand LoadCustomerLicensesCmd => new RelayCommand(LoadCustomerLicenses);
        public ICommand CloseCustomerLicensesCmd => new RelayCommand(CloseCustomerLicenses);
        public ICommand GetCustomerLicensesCmd => new RelayCommand(GetCustomerLicenses);
        public ICommand DeleteCustomerCmd => new RelayCommand(DeleteCustomer);
        public ICommand ActiveKeyCmd => new RelayCommand<CustomerLicenseInfo>(ActiveKey);
        public ICommand DeactiveKeyCmd => new RelayCommand<string>(DeactiveKey);
        void LoadCustomerLicenses()
        {
            if (WTLicenseKey.Instance.CustomerLicensesStatus == ResultType.Success)
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
                    Name = "";
                    CompanyName = "";
                    Email = "";
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
                CustomerLicenseMessage = WTLicenseKey.Instance.CustomerLicensesMessage;
            }
            else
            {
                if (WTLicenseKey.Instance.CustomerInfo != null && string.IsNullOrEmpty(CustomerSecertInput))
                {
                    CustomerLicenseMessage = "";
                    CustomerSecertInput = WTLicenseKey.Instance.CustomerSecret;
                }
                else
                {
                    CustomerLicenseMessage = WTLicenseKey.Instance.CustomerLicensesMessage;
                    //CustomerLicenseMessage = "Your customer secret is not valid";
                }
            }
        }
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
            }
        }
        void DeleteCustomer() 
        {
            Name = "";
            CompanyName = "";
            Email = "";
            CustomerLicense = new ObservableCollection<CustomerLicenseInfo>();
            CustomerSecertInput = "";
            WTLicenseKey.Instance.DeleteCustomerLicenses();
        }
        void ActiveKey(CustomerLicenseInfo license) 
        {
            int productId = WTLicenseKey.Instance.ProductsInfo.Products.First(x => x.Name == license.ProductName).Id;
            //AllLicenseKey.Instance.AllProductLicenseKey[(Product)productId].NewLicenseKeyInput = license.ValidLicenseKeys;
            WTLicenseKey.Instance.AllProductLicenseKey[(Product)productId].ActiveLicenseKey(license.ValidLicenseKeys, productId);
        }
        void DeactiveKey(string productName)
        {
            //LicenseKeyMessage = "";
            //AllLicenseKey.Instance.AllProductLicenseKey[(Product)productId].NewLicenseKeyInput = license.ValidLicenseKeys;
            int productId = WTLicenseKey.Instance.ProductsInfo.Products.First(x => x.Name == productName).Id;
            WTLicenseKey.Instance.AllProductLicenseKey[(Product)productId].DeactiveLicenseKey();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class TypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (WTKeyHelpers a in WTLicenseKey.Instance.AllProductLicenseKey.Values)
            {
                if (a.LicenseKey !=null && a.LicenseKey.Key == value.ToString())
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
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
