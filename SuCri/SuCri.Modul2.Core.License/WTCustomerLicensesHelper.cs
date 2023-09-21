using SKM.V3.Methods;
using SKM.V3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SuCri.Modul2.Core.License.Model;

namespace SuCri.Modul2.Core.License
{
    public class WTCustomerLicensesHelper : INotifyPropertyChanged
    {
        private string tokenWithAllPermission = "WyI2MTU1MzA4NiIsIkxucmJRS1JnV1EwUk94MFdNbVkvN0NzeWpabjN1T2t6UFp0YTJoY1MiXQ==";
        public WTCustomerLicensesHelper(LicenseKey licenseKey,List<SKM.V3.Models.Product> productList, ObservableDictionary<Product, WTKeyHelpers> allProductLicenseKey)
        {
            LicenseKey = licenseKey;
            ValidLicenseKeys = licenseKey.Key;
            ProductName = productList.First(x => x.Id == licenseKey.ProductId).Name;
            ProductId = licenseKey.ProductId;
            ExpirationDate = licenseKey.Expires;
            Machines = $"{licenseKey.ActivatedMachines.Count()}/{licenseKey.MaxNoOfMachines}";
            Status = $"{(licenseKey.Period == 0 ? "Red" : (licenseKey.Period < 14) ? "Orange" : "Green")}";
            if (licenseKey.MaxNoOfMachines == 0)
            {
                if (allProductLicenseKey.Values.FirstOrDefault(x => x.LicenseKey?.Key == licenseKey.Key) == null)
                {
                    EnableDeactive = Visibility.Hidden;
                }
                else
                {
                    EnableDeactive = Visibility.Visible;
                }
            }
            else if (Helpers.IsOnRightMachinePI(LicenseKey, v: 2))
            {
                EnableDeactive = Visibility.Visible;
            }
            else
            {
                EnableDeactive = Visibility.Hidden;
            }
        }
        public WTCustomerLicensesHelper(LicenseKey licenseKey)
        {
            LicenseKey = licenseKey;
            ValidLicenseKeys = licenseKey.Key;
            ProductName = WTLicenseKey.Instance.ProductsInfo.Products.First(x => x.Id == licenseKey.ProductId).Name;
            ProductId = licenseKey.ProductId;
            ExpirationDate = licenseKey.Expires;
            Machines = $"{licenseKey.ActivatedMachines.Count()}/{licenseKey.MaxNoOfMachines}";
            Status = $"{(licenseKey.Period == 0 ? "Red" : (licenseKey.Period < 14) ? "Orange" : "Green")}";
            if (licenseKey.MaxNoOfMachines == 0)
            {
                if (WTLicenseKey.Instance.AllProductLicenseKey.Values.FirstOrDefault(x => x.LicenseKey?.Key == licenseKey.Key) == null)
                {
                    EnableDeactive = Visibility.Hidden;
                }
                else
                {
                    EnableDeactive = Visibility.Visible;
                }
            }
            else if (Helpers.IsOnRightMachinePI(LicenseKey, v: 2))
            {
                EnableDeactive = Visibility.Visible;
            }
            else
            {
                EnableDeactive = Visibility.Hidden;
            }
        }
        public LicenseKey LicenseKey { get; set; }
        public string ValidLicenseKeys { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Machines { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public void IsKeyOnMachine()
        {
            LicenseKey.Refresh(tokenWithAllPermission);
            Machines = $"{LicenseKey.ActivatedMachines.Count()}/{LicenseKey.MaxNoOfMachines}";
            if (LicenseKey.MaxNoOfMachines == 0)
            {
                if (WTLicenseKey.Instance.AllProductLicenseKey.Values.FirstOrDefault(x => x.LicenseKey?.Key == LicenseKey.Key) == null)
                {
                    EnableDeactive = Visibility.Hidden;
                }
                else
                {
                    EnableDeactive = Visibility.Visible;
                }
            }
            else if (Helpers.IsOnRightMachinePI(LicenseKey, v: 2))
            {
                EnableDeactive = Visibility.Visible;
            }
            else
            {
                EnableDeactive = Visibility.Hidden;
            }

        }
        public Visibility EnableDeactive { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
