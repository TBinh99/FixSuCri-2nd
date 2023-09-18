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
                ActiveLicenseKey(Properties.Settings.Default.LicenseKey);
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


        private string tokenWithAllPermission = "WyI1OTMwMzI2NiIsInVpSXM4SG1rU3RYd09IQmh2MmRDRWRDTlNpYWVOcUdFRXJxak5uenEiXQ==";

        public string CustomerSecret { get; set; }

        public void ActiveLicenseKey(string keyInput)
        {
            var RSAPubKey = "<RSAKeyValue><Modulus>kWSSWcUTKvwvZRtCRrSY0ImORR1C9T2Oduhxq5P2BzT74zOPFef1V4Wx3Z93zZhgdYpLl1bG9wF+IIc1ppfbLs+dH6H37bWaiejny2MVVHYwZ/D5YA3P1tKmlnSoH7BbIozybCXT4ww+6WEduWguolBHbdAeb8GHz2YdFx2JjZZFghFzpd/xEu+GUDrxyuiFAH+rQ9/SZ2qMaB5LhuCZCbeuz71tKHY+rODO+0FXnhs+kaSZSEDsgaIuTAd1a/vfuMNWZ2Cnun3guVaDMvXJa4AUy7RaG4YQrCJSsqbnnCO0n/kLnLoPx2dm0A11xgzNwJN0OQBetv/O6HNEoME93Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var auth = "WyI1OTMwNjAxNyIsImZsMnhWeWVvN0U2bExxUCt4cHBvZHlPbXA1RTRpNVByMURPeTRkbWoiXQ==";
            int productId = 21673;

            var result = Key.Activate(token: auth, parameters: new ActivateModel()
            {
                Key = keyInput,
                ProductId = productId,  // <--  remember to change this to your Product Id
                Sign = true,
                MachineCode = Helpers.GetMachineCodePI(v: 2)
            });
            if (result == null || result.Result == ResultType.Error || !result.LicenseKey.HasValidSignature(RSAPubKey).IsValid())
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
            }
            else
            {
                LicenseKey = result.LicenseKey;
                if (LicenseKey.Period > 0)
                {
                    Properties.Settings.Default.F1FeatureActive = LicenseKey.F1;
                    Properties.Settings.Default.F2FeatureActive = LicenseKey.F2;
                    Properties.Settings.Default.F3FeatureActive = LicenseKey.F3;
                    Properties.Settings.Default.F4FeatureActive = LicenseKey.F4;
                    Properties.Settings.Default.F5FeatureActive = LicenseKey.F5;
                    Properties.Settings.Default.F6FeatureActive = LicenseKey.F6;
                    Properties.Settings.Default.F7FeatureActive = LicenseKey.F7;
                    Properties.Settings.Default.F8FeatureActive = LicenseKey.F8;
                }
                else
                {
                    Properties.Settings.Default.Reset();
                }
                Properties.Settings.Default.LicenseKey = LicenseKey.Key;
                Properties.Settings.Default.Save();
                ReloadCompanyLicense();
            }
        }
        public void DeactiveKey()
        {
            // Deactive not work, dont know why yet
            var deactive = Key.Deactivate(token: tokenWithAllPermission, parameters: new DeactivateModel()
            {
                Key = LicenseKey.Key,
                ProductId = LicenseKey.ProductId,
                MachineCode = Helpers.GetMachineCodePI(v: 2),
            });
            ReloadCompanyLicense();
            LicenseKey = null;
            Properties.Settings.Default.LicenseKey = null;
            Properties.Settings.Default.Save();
        }

        private void GetProductsInfomation() 
        {
            ProductsInfo = ProductMethods.GetProducts(tokenWithAllPermission, new RequestModel());
        }
        public void GetCompanyLicenses(string customerSecret)
        {
            GetCustomerLicensesBySecretModel customerInfo = new GetCustomerLicensesBySecretModel();
            customerInfo.Secret = customerSecret;
            customerInfo.Detailed = true;
            var customerLicenses = CustomerMethods.GetCustomerLicensesBySecret(tokenWithAllPermission, customerInfo);

            if(customerLicenses.Result == ResultType.Success)
            {
                CustomerInfo = customerLicenses;
                Properties.Settings.Default.CustomerSecret = customerSecret;
                Properties.Settings.Default.Save();
            }
        }
        private void ReloadCompanyLicense()
        {
            if (!string.IsNullOrEmpty(CustomerSecret))
            {
                GetCompanyLicenses(CustomerSecret);
            }
        }
        public void DeleteCompanyLicenses() 
        {
            CustomerInfo = null;
            CustomerSecret = null;
            Properties.Settings.Default.CustomerSecret = null;
            Properties.Settings.Default.Save();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
