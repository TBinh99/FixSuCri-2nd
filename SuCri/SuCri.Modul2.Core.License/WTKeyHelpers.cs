using Autodesk.Internal.Windows;
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
using System.Windows.Forms;
using System.Windows.Input;
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
        public WTKeyHelpers(int productId)
        {
            DeclareFeature();
            ProductId = productId;
        }

        public LicenseKey LicenseKey { get; set; }
        public int ProductId;

        public string LicenseKeyMessage { get; set; }
        public string NewLicenseKeyInput { get; set; }
        public string NewLicenseKeyMessage { get; set; }
        public ResultType LicenseKeyStatus { get; set; } =  ResultType.Error;

        private string tokenWithAllPermission = "WyI2MTU1MzA4NiIsIkxucmJRS1JnV1EwUk94MFdNbVkvN0NzeWpabjN1T2t6UFp0YTJoY1MiXQ==";

        public ObservableDictionary<Feature, bool> FeatureActive { get; set; } = new ObservableDictionary<Feature, bool>();

        public void ActiveLicenseKey(string keyInput)
        {
            string RSAPubKey = "<RSAKeyValue><Modulus>kWSSWcUTKvwvZRtCRrSY0ImORR1C9T2Oduhxq5P2BzT74zOPFef1V4Wx3Z93zZhgdYpLl1bG9wF+IIc1ppfbLs+dH6H37bWaiejny2MVVHYwZ/D5YA3P1tKmlnSoH7BbIozybCXT4ww+6WEduWguolBHbdAeb8GHz2YdFx2JjZZFghFzpd/xEu+GUDrxyuiFAH+rQ9/SZ2qMaB5LhuCZCbeuz71tKHY+rODO+0FXnhs+kaSZSEDsgaIuTAd1a/vfuMNWZ2Cnun3guVaDMvXJa4AUy7RaG4YQrCJSsqbnnCO0n/kLnLoPx2dm0A11xgzNwJN0OQBetv/O6HNEoME93Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            string auth = "WyI1OTMwNjAxNyIsImZsMnhWeWVvN0U2bExxUCt4cHBvZHlPbXA1RTRpNVByMURPeTRkbWoiXQ==";

            KeyInfoResult result = SKM.V3.Methods.Key.Activate(token: auth, parameters: new ActivateModel()
            {
                Key = keyInput,
                ProductId = ProductId,
                Sign = true,
                MachineCode = Helpers.GetMachineCodePI(v: 2)
            });

            //Check if the returned result is valid
            if (result == null || result.Result == ResultType.Error || !result.LicenseKey.HasValidSignature(RSAPubKey).IsValid())
            {
                //The returned message could be null, so i customized a message for that case
                if (string.IsNullOrEmpty(result.Message))
                {
                    NewLicenseKeyMessage = "Your license key is not valid";
                }
                else 
                {
                    NewLicenseKeyMessage = result.Message;
                }
            }
            // License Key that valid
            else
            {
                if (result.LicenseKey.Period > 0)
                {
                    //If the user use new key then deactive old key
                    if (LicenseKey != null)
                    {
                        DeactiveLicenseKey();
                    }

                    FeatureActive[Feature.F1] = result.LicenseKey.F1;
                    FeatureActive[Feature.F2] = result.LicenseKey.F2;
                    FeatureActive[Feature.F3] = result.LicenseKey.F3;
                    FeatureActive[Feature.F4] = result.LicenseKey.F4;
                    FeatureActive[Feature.F5] = result.LicenseKey.F5;
                    FeatureActive[Feature.F6] = result.LicenseKey.F6;
                    FeatureActive[Feature.F7] = result.LicenseKey.F7;
                    FeatureActive[Feature.F8] = result.LicenseKey.F8;

                    //Return message for the UI (LicenseKeyMessage is used to display a message for the key in use.
                    //If the new key is invalid, the message will not be changed.Then only the NewLicenseKeyMessage changes.
                    //When you return the UI will display the licensekey in use and the message for it (LicenseKeyMessage).
                    LicenseKeyMessage = "Your imported license key has been successfully activated";
                    LicenseKey = result.LicenseKey;

                    //Return message for the UI
                    NewLicenseKeyMessage = LicenseKeyMessage;
                }
                // License Key that just expired
                else
                {
                    //Return message for the UI
                    NewLicenseKeyMessage = "Your license key has expired";

                    // Check to see if there is a license key in use, if not, save the input key
                    //if yes, throw a message for them and keep the old key
                    if (LicenseKey == null)
                    {
                        LicenseKey = result.LicenseKey;
                        LicenseKeyMessage = NewLicenseKeyMessage;
                    }
                    // Check if the license key being used is expired, if yes, lock all the feature
                    else
                    {
                        if(LicenseKey.Key == keyInput)
                        {
                            LockFeature();
                        }
                    }
                }
                NewLicenseKeyInput = keyInput;
                LicenseKeyStatus = result.Result;
            }
        }
        public void DeactiveLicenseKey()
        {
            if (LicenseKey != null)
            {
                SKM.V3.Methods.Key.Deactivate(token: tokenWithAllPermission, parameters: new DeactivateModel()
                {
                    Key = LicenseKey.Key,
                    ProductId = ProductId,
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
