using SKM.V3.Methods;
using SKM.V3.Models;
using SKM.V3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using SuCri.Modul2.Core.License.Model;
using Autodesk.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace SuCri.Modul2.Core.License.ViewModel
{
    public class LicenseVM : INotifyPropertyChanged
    {
        public LicenseVM() { }
        public ICommand CheckLicenseCmd => new RelayCommand(CheckLicense);
        public ICommand LoadLicenseCmd => new RelayCommand(LoadLicense);
        public ICommand DeleteLicenseCmd => new RelayCommand(DeleteLicense);
        
        public Visibility LicenseIsValid { get; set; } = Visibility.Hidden;
        public string Test { get; set; } = "Test";
        public string LicenseKey { get; set; }
        public string LicenseKeyStatus { get; set; }
        public string F1Feature { get; set; }
        public string F2Feature { get; set; }
        public string F3Feature { get; set; }
        public string F4Feature { get; set; }
        public string F5Feature { get; set; }
        public string F6Feature { get; set; }
        public string F7Feature { get; set; }
        public string F8Feature { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ActivatedMachines { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiresDate { get; set; }
        public List<CustomerLicenseInfo> CustomerLicense { get; set; }
        void DeleteLicense() 
        {
            LicenseKey = "";
            LicenseKeyStatus = "";
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            LicenseIsValid = Visibility.Hidden;
        }
        void LoadLicense() 
        {
            if(!string.IsNullOrEmpty(Properties.Settings.Default.LicenseKey)) 
            {
                LicenseKey = Properties.Settings.Default.LicenseKey;
                CheckLicense();
            }
        }
        void CheckLicense()
        {
            var RSAPubKey = "<RSAKeyValue><Modulus>kWSSWcUTKvwvZRtCRrSY0ImORR1C9T2Oduhxq5P2BzT74zOPFef1V4Wx3Z93zZhgdYpLl1bG9wF+IIc1ppfbLs+dH6H37bWaiejny2MVVHYwZ/D5YA3P1tKmlnSoH7BbIozybCXT4ww+6WEduWguolBHbdAeb8GHz2YdFx2JjZZFghFzpd/xEu+GUDrxyuiFAH+rQ9/SZ2qMaB5LhuCZCbeuz71tKHY+rODO+0FXnhs+kaSZSEDsgaIuTAd1a/vfuMNWZ2Cnun3guVaDMvXJa4AUy7RaG4YQrCJSsqbnnCO0n/kLnLoPx2dm0A11xgzNwJN0OQBetv/O6HNEoME93Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var auth = "WyI1OTMwNjAxNyIsImZsMnhWeWVvN0U2bExxUCt4cHBvZHlPbXA1RTRpNVByMURPeTRkbWoiXQ==";
            int productId = 21673;

            var result = SKM.V3.Methods.Key.Activate(token: auth, parameters: new ActivateModel()
            {
                Key = LicenseKey,
                ProductId = productId,  // <--  remember to change this to your Product Id
                Sign = true,
                MachineCode = Helpers.GetMachineCodePI(v: 2)
            });
            if (result == null || result.Result == ResultType.Error ||
    !result.LicenseKey.HasValidSignature(RSAPubKey).IsValid())
            {
                LicenseIsValid = Visibility.Hidden;
                LicenseKeyStatus = "Your license key does not exist";
            }
            else
            {
                LicenseKey licenseKey = result.LicenseKey;
                if (licenseKey.Period > 0)
                {
                    Properties.Settings.Default.F1FeatureActive = licenseKey.F1;
                    Properties.Settings.Default.F2FeatureActive = licenseKey.F2;
                    Properties.Settings.Default.F3FeatureActive = licenseKey.F3;
                    Properties.Settings.Default.F4FeatureActive = licenseKey.F4;
                    Properties.Settings.Default.F5FeatureActive = licenseKey.F5;
                    Properties.Settings.Default.F6FeatureActive = licenseKey.F6;
                    Properties.Settings.Default.F7FeatureActive = licenseKey.F7;
                    Properties.Settings.Default.F8FeatureActive = licenseKey.F8;
                    Properties.Settings.Default.Save();
                    LicenseKeyStatus = "Your license key has been activated";
                }
                else
                {
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.Save();
                    LicenseKeyStatus = "Your license key has expired";
                }
                Properties.Settings.Default.LicenseKey = LicenseKey;

                LicenseIsValid = Visibility.Visible;
                F1Feature = licenseKey.F1 ? "Active" : "None";
                F2Feature = licenseKey.F2 ? "Active" : "None";
                F3Feature = licenseKey.F3 ? "Active" : "None";
                F4Feature = licenseKey.F4 ? "Active" : "None";
                F5Feature = licenseKey.F5 ? "Active" : "None";
                F6Feature = licenseKey.F6 ? "Active" : "None";
                F7Feature = licenseKey.F7 ? "Active" : "None";
                F8Feature = licenseKey.F8 ? "Active" : "None";

                CompanyName = licenseKey.Customer.CompanyName;
                Email = licenseKey.Customer.Email;
                Name = licenseKey.Customer.Name;
                ActivatedMachines = licenseKey.ActivatedMachines.Count().ToString();
                CreatedDate = licenseKey.Created;
                ExpiresDate = licenseKey.Expires;

                GetCustomerLicensesModel customerInfo = new GetCustomerLicensesModel();
                customerInfo.CustomerId = licenseKey.Customer.Id;
                customerInfo.Detailed = true;
                var customerLicenses = CustomerMethods.GetCustomerLicenses("WyI1OTMwMzI2NiIsInVpSXM4SG1rU3RYd09IQmh2MmRDRWRDTlNpYWVOcUdFRXJxak5uenEiXQ==", customerInfo);
                CustomerLicense = new List<CustomerLicenseInfo>();
                foreach (var licenseInfo in customerLicenses.LicenseKeys)
                {
                    CustomerLicense.Add(new CustomerLicenseInfo() { Valid_License_Keys = licenseInfo.Key, Expiration_Date = licenseInfo.Expires, Machines = $"{licenseInfo.ActivatedMachines.Count()}/{licenseInfo.MaxNoOfMachines}", Status = $"{(licenseInfo.Period < 0 ? "Red" : (licenseInfo.Period < 14) ? "Blue" : "Green")}" });
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
