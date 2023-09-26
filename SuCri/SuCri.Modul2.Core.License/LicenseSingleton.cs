using SuCri.Modul2.Core.License.View;
using SuCri.Modul2.Core.License.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuCri.Modul2.Core.License
{
    public partial class LicenseSingleton
    {
        public LicenseSingleton()
        {
            ViewModel = new LicenseVM();
            View = new LicenseUI() { };
            View.DataContext = ViewModel;
            View.Closed += (s, e) =>
            {
                _instance = null;
            };
        }

        private static LicenseSingleton _instance;

        public static LicenseSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LicenseSingleton();
                }
                return _instance;
            }
        }

        public LicenseUI View { get; set; }

        public LicenseVM ViewModel { get; set; }

        public static void Show()
        {
            if (Instance.View.IsLoaded)
            {
                Instance.View.Activate();
            }
            else
            {
                Instance.View.Show();
            }
        }
    }
}
