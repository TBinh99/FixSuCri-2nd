using CommunityToolkit.Mvvm.Input;
using SKM.V3;
using SuCri.Modul2.Core.License.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuCri.Modul2.Core.License.ViewModel
{
    public class CompanyLicensesVM : INotifyPropertyChanged
    {
        public CompanyLicensesVM() { }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<CustomerLicenseInfo> CustomerLicense { get; set; }
        public ICommand LoadCompanyLicensesCmd => new RelayCommand(LoadCompanyLicenses);
        public int StatusIndex { get; set; } = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        void LoadCompanyLicenses()
        {
            if (WTLicenseKey.Instance.IsValidKey)
            {
                LicenseKey licenseKey = WTLicenseKey.Instance.LicenseKey;
                if (WTLicenseKey.Instance.HaveCustomerInfomation)
                {
                    CompanyName = licenseKey.Customer.CompanyName;
                    Email = licenseKey.Customer.Email;
                    Name = licenseKey.Customer.Name;
                    CustomerLicense = WTLicenseKey.Instance.CustomerLicense;
                }
                else
                {
                    CompanyName = "None";
                    Email = "None";
                    Name = "None";
                    CustomerLicense = new List<CustomerLicenseInfo>();
                }
            }
        }
    }
}
