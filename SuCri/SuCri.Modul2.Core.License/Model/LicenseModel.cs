using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuCri.Modul2.Core.License.Model
{
    public class CustomerLicenseInfo : INotifyPropertyChanged
    {
        public string Valid_License_Keys { get; set; }
        public DateTime Expiration_Date { get; set; }
        public string Machines { get; set; }
        public string Status { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public enum FeatureOption
    {
        F1FeatureActive,
        F2FeatureActive,
        F3FeatureActive,
        F4FeatureActive,
        F5FeatureActive,
        F6FeatureActive,
        F7FeatureActive,
        F8FeatureActive
    }
}
