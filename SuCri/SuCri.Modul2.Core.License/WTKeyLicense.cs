using Newtonsoft.Json;
using SKM.V3.Methods;
using SKM.V3.Models;
using SKM.V3;
using SuCri.Modul2.Core.License.Model;
using SuCri.Modul2.Core.License.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuCri.Modul2.Core.License.WTKeyHelpers;

namespace SuCri.Modul2.Core.License
{
    public class WTLicenseKey : INotifyPropertyChanged
    {
        public WTLicenseKey()
        {
            GetProductsInfomation();
            AllProductLicenseKey = new ObservableDictionary<Product, WTKeyHelpers>();
            foreach (Product product in Enum.GetValues(typeof(Product)))
            {
                WTKeyHelpers keyHelpers = new WTKeyHelpers();
                keyHelpers.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "LicenseKey")
                    {
                        OnPropertyChanged("LicenseKey");
                    }
                };
                AllProductLicenseKey[product] = keyHelpers;
            }
            if (!string.IsNullOrEmpty(Settings.Default.AllProductLicenseKey))
            {
                ObservableDictionary<int, WTKeyHelpers> savedLicensesKey = JsonConvert.DeserializeObject<ObservableDictionary<int, WTKeyHelpers>>(Settings.Default.AllProductLicenseKey);
                foreach (KeyValuePair<int, WTKeyHelpers> item in savedLicensesKey)
                {
                    if(Enum.IsDefined(typeof(Product),item.Key))
                    {
                        LicenseKey checkKey = item.Value.LicenseKey;
                        if (checkKey is null) { continue; }
                        WTKeyHelpers keyHelpers = new WTKeyHelpers(checkKey.Key, checkKey.ProductId);
                        keyHelpers.PropertyChanged += (s, e) =>
                        {
                            if (e.PropertyName == "LicenseKey")
                            {
                                OnPropertyChanged("LicenseKey");
                            }
                        };
                        AllProductLicenseKey[(Product)item.Key] = keyHelpers;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Settings.Default.CustomerSecret))
            {
                GetCustomerLicenses(Settings.Default.CustomerSecret);
            }
        }

        private static WTLicenseKey _instance;
        public static WTLicenseKey Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WTLicenseKey();
                }
                return _instance;
            }
        }

        public GetProductsResult ProductsInfo { get; set; }
        public GetCustomerLicensesResult CustomerInfo { get; set; }
        public ObservableDictionary<Product, WTKeyHelpers> AllProductLicenseKey { get; set; }

        private string tokenWithAllPermission = "WyI1OTMwMzI2NiIsInVpSXM4SG1rU3RYd09IQmh2MmRDRWRDTlNpYWVOcUdFRXJxak5uenEiXQ==";
        public string CustomerLicensesMessage = "";
        public string CustomerSecret = "";

        public ResultType CustomerLicensesStatus = ResultType.Error;

        private void GetProductsInfomation()
        {
            ProductsInfo = ProductMethods.GetProducts(tokenWithAllPermission, new RequestModel());
        }
        public void GetCustomerLicenses(string customerSecret)
        {
            GetCustomerLicensesBySecretModel customerInfo = new GetCustomerLicensesBySecretModel();
            customerInfo.Secret = customerSecret;
            customerInfo.Detailed = true;
            var customerLicenses = CustomerMethods.GetCustomerLicensesBySecret(tokenWithAllPermission, customerInfo);
            CustomerLicensesStatus = customerLicenses.Result;
            CustomerLicensesMessage = customerLicenses.Message;
            if (customerLicenses.Result == ResultType.Success)
            {
                CustomerSecret = customerSecret;
                CustomerInfo = customerLicenses;
                Settings.Default.CustomerSecret = customerSecret;
                Settings.Default.Save();
            }
            else
            {
                OnPropertyChanged("CustomerInfo");
            }
        }
        public void DeleteCustomerLicenses()
        {
            CustomerLicensesStatus = ResultType.Error;
            Properties.Settings.Default.CustomerSecret = null;
            Properties.Settings.Default.Save();
            CustomerInfo = null;
            CustomerSecret = null;
        }

        public bool CheckLicense(Product product, Feature feature)
        {
            return AllProductLicenseKey[product].FeatureActive[feature];
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public enum Product
    {
        Sikla = 21948,
        TestProduct = 21673,
        SecondProduct = 21881,
        WT = 21883,
    }
    public enum Feature
    {
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8
    }
}
