using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;
using SuCri.Modul2.Core.License.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SuCri.Modul2.Core.License
{
    public class WTLicenseKey :INotifyPropertyChanged
    {
        public WTLicenseKey()
        {
            GetProductsInfomation();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.CustomerSecret))
            {
                GetCompanyLicenses(Properties.Settings.Default.CustomerSecret);
            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.LicenseKey))
            {
                ActiveLicenseKey(Properties.Settings.Default.LicenseKey, Properties.Settings.Default.ProductId);
            }
        }
        public static WTLicenseKey _instance { get; set; }
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

        public LicenseKey LicenseKey { get; set; }
        public GetProductsResult ProductsInfo { get; set; }
        public GetCustomerLicensesResult CustomerInfo { get; set; }

        public string CustomerSecret = "";

        public string LicenseKeyMessage = "";
        public string CompanyLicensesMessage = "";

        public ResultType LicenseKeyStatus = ResultType.Error;
        public ResultType CompanyLicensesStatus = ResultType.Error;

        private string tokenWithAllPermission = "WyI1OTMwMzI2NiIsInVpSXM4SG1rU3RYd09IQmh2MmRDRWRDTlNpYWVOcUdFRXJxak5uenEiXQ==";

        private void GetProductsInfomation()
        {
            ProductsInfo = ProductMethods.GetProducts(tokenWithAllPermission, new RequestModel());
        }

        public void ActiveLicenseKey(string keyInput, int productId)
        {
            var RSAPubKey = "<RSAKeyValue><Modulus>kWSSWcUTKvwvZRtCRrSY0ImORR1C9T2Oduhxq5P2BzT74zOPFef1V4Wx3Z93zZhgdYpLl1bG9wF+IIc1ppfbLs+dH6H37bWaiejny2MVVHYwZ/D5YA3P1tKmlnSoH7BbIozybCXT4ww+6WEduWguolBHbdAeb8GHz2YdFx2JjZZFghFzpd/xEu+GUDrxyuiFAH+rQ9/SZ2qMaB5LhuCZCbeuz71tKHY+rODO+0FXnhs+kaSZSEDsgaIuTAd1a/vfuMNWZ2Cnun3guVaDMvXJa4AUy7RaG4YQrCJSsqbnnCO0n/kLnLoPx2dm0A11xgzNwJN0OQBetv/O6HNEoME93Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var auth = "WyI1OTMwNjAxNyIsImZsMnhWeWVvN0U2bExxUCt4cHBvZHlPbXA1RTRpNVByMURPeTRkbWoiXQ==";

            var result = Key.Activate(token: auth, parameters: new ActivateModel()
            {
                Key = keyInput,
                ProductId = productId,  // <--  remember to change this to your Product Id
                Sign = true,
                MachineCode = Helpers.GetMachineCodePI(v: 2)
            });
            LicenseKeyStatus = result.Result;
            LicenseKeyMessage = result.Message;
            if (result == null || result.Result == ResultType.Error || !result.LicenseKey.HasValidSignature(RSAPubKey).IsValid())
            {
                OnPropertyChanged("LicenseKey");
            }
            else
            {
                Properties.Settings.Default.LicenseKey = result.LicenseKey.Key;
                Properties.Settings.Default.ProductId = productId;
                Properties.Settings.Default.Save();
                if (result.LicenseKey.Period > 0)
                {
                    Properties.Settings.Default.F1FeatureActive = result.LicenseKey.F1;
                    Properties.Settings.Default.F2FeatureActive = result.LicenseKey.F2;
                    Properties.Settings.Default.F3FeatureActive = result.LicenseKey.F3;
                    Properties.Settings.Default.F4FeatureActive = result.LicenseKey.F4;
                    Properties.Settings.Default.F5FeatureActive = result.LicenseKey.F5;
                    Properties.Settings.Default.F6FeatureActive = result.LicenseKey.F6;
                    Properties.Settings.Default.F7FeatureActive = result.LicenseKey.F7;
                    Properties.Settings.Default.F8FeatureActive = result.LicenseKey.F8;
                }
                else
                {
                    LockFeature();
                }
                LicenseKey = result.LicenseKey;
            }
        }
        public void DeactiveKey()
        {
            if(LicenseKey != null)
            {
                // Deactive not work, dont know why yet
                var deactive = Key.Deactivate(token: tokenWithAllPermission, parameters: new DeactivateModel()
                {
                    Key = LicenseKey.Key,
                    ProductId = LicenseKey.ProductId,
                    MachineCode = Helpers.GetMachineCodePI(v: 2),
                });
            }
            LicenseKeyStatus = ResultType.Error;
            Properties.Settings.Default.LicenseKey = null;
            Properties.Settings.Default.ProductId = 0;
            Properties.Settings.Default.Save();
            LockFeature();
            LicenseKey = null;
        }


        public void GetCompanyLicenses(string customerSecret)
        {
            GetCustomerLicensesBySecretModel customerInfo = new GetCustomerLicensesBySecretModel();
            customerInfo.Secret = customerSecret;
            customerInfo.Detailed = true;
            var customerLicenses = CustomerMethods.GetCustomerLicensesBySecret(tokenWithAllPermission, customerInfo);
            CompanyLicensesStatus = customerLicenses.Result;
            CompanyLicensesMessage = customerLicenses.Message;
            if (customerLicenses.Result == ResultType.Success)
            {
                CustomerSecret = customerSecret;
                CustomerInfo = customerLicenses;
                Properties.Settings.Default.CustomerSecret = customerSecret;
                Properties.Settings.Default.Save();
            }
            else 
            {
                OnPropertyChanged("CustomerInfo");
            }
        }
        public void DeleteCompanyLicenses()
        {
            CompanyLicensesStatus = ResultType.Error;
            Properties.Settings.Default.CustomerSecret = null;
            Properties.Settings.Default.Save();
            CustomerInfo = null;
            CustomerSecret = null;
        }

        private void LockFeature() 
        {
            Properties.Settings.Default.F1FeatureActive = false;
            Properties.Settings.Default.F2FeatureActive = false;
            Properties.Settings.Default.F3FeatureActive = false;
            Properties.Settings.Default.F4FeatureActive = false;
            Properties.Settings.Default.F5FeatureActive = false;
            Properties.Settings.Default.F6FeatureActive = false;
            Properties.Settings.Default.F7FeatureActive = false;
            Properties.Settings.Default.F8FeatureActive = false;
            Properties.Settings.Default.Save();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
