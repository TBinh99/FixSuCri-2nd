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
        public ICommand ActiveLicenseCmd => new RelayCommand(ActiveLicense);
        public ICommand LoadLicenseCmd => new RelayCommand(LoadLicense);
        public ICommand DeleteLicenseCmd => new RelayCommand(DeleteLicense);
        
        public Visibility LicenseIsValid { get; set; } = Visibility.Hidden;
        
        public string LicenseKeyInput { get; set; }
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
        void DeleteLicense() 
        {
            LicenseKeyInput = "";
            LicenseKeyStatus = "";
            WTLicenseKey.Instance.DeactiveKey();
            LoadLicense();
            LicenseIsValid = Visibility.Hidden;
        }
        void LoadLicense() 
        {
            if (WTLicenseKey.Instance.LicenseKey != null) 
            {
                LicenseKeyInput = WTLicenseKey.Instance.LicenseKey.Key;
                LicenseKey licenseKey = WTLicenseKey.Instance.LicenseKey;
                if (licenseKey.Period > 0)
                {
                    LicenseKeyStatus = "Your license key has been activated";
                    F1Feature = licenseKey.F1 ? "Active" : "None";
                    F2Feature = licenseKey.F2 ? "Active" : "None";
                    F3Feature = licenseKey.F3 ? "Active" : "None";
                    F4Feature = licenseKey.F4 ? "Active" : "None";
                    F5Feature = licenseKey.F5 ? "Active" : "None";
                    F6Feature = licenseKey.F6 ? "Active" : "None";
                    F7Feature = licenseKey.F7 ? "Active" : "None";
                    F8Feature = licenseKey.F8 ? "Active" : "None";
                }
                else
                {
                    LicenseKeyStatus = "Your license key has expired";
                    F1Feature = "None";
                    F2Feature = "None";
                    F3Feature = "None";
                    F4Feature = "None";
                    F5Feature = "None";
                    F6Feature = "None"; 
                    F7Feature = "None"; 
                    F8Feature = "None";
                }
                LicenseIsValid = Visibility.Visible;
                if (licenseKey.Customer != null)
                {
                    CompanyName = licenseKey.Customer.CompanyName;
                    Email = licenseKey.Customer.Email;
                    Name = licenseKey.Customer.Name;
                }
                else
                {
                    CompanyName = "None";
                    Email = "None";
                    Name = "None";
                }
                ActivatedMachines = licenseKey.ActivatedMachines.Count().ToString();
                CreatedDate = licenseKey.Created;
                ExpiresDate = licenseKey.Expires;
            }
            else 
            {
                LicenseIsValid = Visibility.Hidden;
                if (!string.IsNullOrEmpty(LicenseKeyInput))
                { 
                    LicenseKeyStatus = "Your license key does not exist"; 
                }
            }
        }
        void ActiveLicense()
        {
            if (LicenseKeyInput != null) 
            {
                WTLicenseKey.Instance.ActiveLicenseKey(LicenseKeyInput);
                LoadLicense();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
