using SuCri.Modul2.UI.Acad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuCri.Modul2.UI.Acad.Utils
{
    public class ViewModelLocator
    {
        private static MainViewModel _mainViewModelInstance;

        public MainViewModel MainViewModelInstance
        {
            get
            {
                if (_mainViewModelInstance == null)
                {
                    _mainViewModelInstance = new MainViewModel();
                }
                return _mainViewModelInstance;
            }
        }
    }

}
