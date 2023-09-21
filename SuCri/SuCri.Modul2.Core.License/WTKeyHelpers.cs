using Newtonsoft.Json;
using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;
using SuCri.Modul2.Core.License.Model;
using SuCri.Modul2.Core.License.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static SuCri.Modul2.Core.License.WTKeyHelpers;

namespace SuCri.Modul2.Core.License
{
    public class WTKeyHelpers :INotifyPropertyChanged
    {
        private void DeclareFeature() 
        {
            foreach (Feature feature in Enum.GetValues(typeof(Feature)))
            {
                FeatureActive[feature] = false;
            }
        }
        public WTKeyHelpers()
        {
            DeclareFeature();
        }
        public WTKeyHelpers(string licenseKey, int productId)
        {
            DeclareFeature();
            ActiveLicenseKey(licenseKey, productId);
        }

        public LicenseKey LicenseKey { get; set; }

        public void OnLicenseKeyChanged()
        {
            OnPropertyChanged("LicenseKey");
        }
        public string LicenseKeyMessage { get; set; }


        public string NewLicenseKeyInput { get; set; }
        public string NewLicenseKeyMessage { get; set; }
        public ResultType LicenseKeyStatus { get; set; } =  ResultType.Error;

        private string tokenWithAllPermission = "WyI1OTMwMzI2NiIsInVpSXM4SG1rU3RYd09IQmh2MmRDRWRDTlNpYWVOcUdFRXJxak5uenEiXQ==";

        public ObservableDictionary<Feature, bool> FeatureActive { get; set; } = new ObservableDictionary<Feature, bool>();

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
            if (result == null || result.Result == ResultType.Error || !result.LicenseKey.HasValidSignature(RSAPubKey).IsValid())
            {
                OnPropertyChanged("LicenseKey");
                if(string.IsNullOrEmpty(result.Message))
                {
                    NewLicenseKeyMessage = "Your license key is not valid";
                }
                else 
                {
                    NewLicenseKeyMessage = result.Message;
                }
            }
            else
            {
                if (result.LicenseKey.Period > 0)
                {
                    FeatureActive[Feature.F1] = result.LicenseKey.F1;
                    FeatureActive[Feature.F2] = result.LicenseKey.F2;
                    FeatureActive[Feature.F3] = result.LicenseKey.F3;
                    FeatureActive[Feature.F4] = result.LicenseKey.F4;
                    FeatureActive[Feature.F5] = result.LicenseKey.F5;
                    FeatureActive[Feature.F6] = result.LicenseKey.F6;
                    FeatureActive[Feature.F7] = result.LicenseKey.F7;
                    FeatureActive[Feature.F8] = result.LicenseKey.F8;

                    LicenseKeyMessage = "Your imported license key has been successfully activated";
                    LicenseKey = result.LicenseKey;

                    NewLicenseKeyMessage = LicenseKeyMessage;
                    NewLicenseKeyInput = result.LicenseKey.Key;
                }
                else
                {
                    NewLicenseKeyMessage = "Your license key has expired";
                    LockFeature();
                    if(LicenseKey == null)
                    {
                        LicenseKey = result.LicenseKey;
                        LicenseKeyMessage = NewLicenseKeyMessage;

                        NewLicenseKeyInput = result.LicenseKey.Key;
                    }
                }
            }
        }
        public void DeactiveLicenseKey()
        {
            if (LicenseKey != null)
            {
                // Deactive not work, dont know why yet
                Key.Deactivate(token: tokenWithAllPermission, parameters: new DeactivateModel()
                {
                    Key = LicenseKey.Key,
                    ProductId = LicenseKey.ProductId,
                    MachineCode = Helpers.GetMachineCodePI(v: 2),
                });
            }
            NewLicenseKeyMessage = "";
            NewLicenseKeyInput = "";
            LicenseKeyStatus = ResultType.Error;
            LockFeature();
            LicenseKey = null;
        }

        private void LockFeature()
        {
            FeatureActive[Feature.F1] = false;
            FeatureActive[Feature.F2] = false;
            FeatureActive[Feature.F3] = false;
            FeatureActive[Feature.F4] = false;
            FeatureActive[Feature.F5] = false;
            FeatureActive[Feature.F6] = false;
            FeatureActive[Feature.F7] = false;
            FeatureActive[Feature.F8] = false;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
