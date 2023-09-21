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
using System.Collections.ObjectModel;
using System.Xml.Linq;

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
                WTKeyHelpers keyHelpers = new WTKeyHelpers((int)product);
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
                        AllProductLicenseKey[(Product)item.Key].ActiveLicenseKey(checkKey.Key);
                    }
                }
            }
            if (!string.IsNullOrEmpty(Settings.Default.CustomerSecret))
            {
                GetCustomerLicenses(Settings.Default.CustomerSecret);
            }

            //List<string> namePropertyCustomerLicenses = new List<string>() { "CustomerName", "CompanyName", "Email", "CustomerLicensesMessage", "CustomerSecret", "CustomerLicense" };
            //CustomerLicenses = new ObservableDictionary<string, object>();
            //foreach (string nameProperty in namePropertyCustomerLicenses)
            //{
            //    CustomerLicenses.Add(nameProperty, "");
            //    if (nameProperty == "CustomerLicense")
            //    { }
            //}

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

        //public ObservableDictionary<string, object> CustomerLicenses { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string CustomerLicensesMessage { get; set; }
        public string CustomerSecret { get; set; }

        public ResultType CustomerLicensesStatus = ResultType.Error;
        public ObservableCollection<WTCustomerLicensesHelper> CustomerLicense { get; set; }
        public ObservableDictionary<Product, WTKeyHelpers> AllProductLicenseKey { get; set; }


        private string tokenWithAllPermission = "WyI2MTU1MzA4NiIsIkxucmJRS1JnV1EwUk94MFdNbVkvN0NzeWpabjN1T2t6UFp0YTJoY1MiXQ==";

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

                var firstKey = customerLicenses.LicenseKeys.FirstOrDefault();
                CustomerName = firstKey?.Customer.Name;
                CompanyName = firstKey?.Customer.CompanyName;
                Email = firstKey?.Customer.Email;
                ObservableCollection<WTCustomerLicensesHelper> licenses = new ObservableCollection<WTCustomerLicensesHelper>();
                foreach (var licenseInfo in customerLicenses.LicenseKeys)
                {
                    licenses.Add(new WTCustomerLicensesHelper(licenseInfo, ProductsInfo.Products, AllProductLicenseKey));
                }
                CustomerLicense = licenses;
            }
            else
            {
            }
        }
        public void DeleteCustomerLicenses()
        {
            CustomerLicensesStatus = ResultType.Error;
            Settings.Default.CustomerSecret = null;
            Settings.Default.Save();
            CustomerInfo = null;

            CustomerName = "";
            CompanyName = "";
            Email = "";
            CustomerLicensesMessage = "";
            CustomerSecret = null;
            CustomerLicense.Clear();
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
