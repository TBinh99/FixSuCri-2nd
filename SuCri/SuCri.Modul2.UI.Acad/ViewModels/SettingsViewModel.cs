using SuCri.Modul2.UI.Acad.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuCri.Modul2.UI.Acad.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }

        public string SettingsViewTabControlText1 { get => _resManager.GetString("SettingsViewTabControlText1", Culture); set { } }
        public string SettingsViewTabControlText2 { get => _resManager.GetString("SettingsViewTabControlText2", Culture); set { } }
        public string SettingsViewTabControlText3 { get => _resManager.GetString("SettingsViewTabControlText3", Culture); set { } }

        public string SettingsViewTabControlText4 { get => _resManager.GetString("SettingsViewTabControlText4", Culture); set { } }

        public string SettingsViewTab1Text1 { get => _resManager.GetString("SettingsViewTab1Text1", Culture); set { } }

        public string SettingsViewTab1ButtonText1 { get => _resManager.GetString("SettingsViewTab1ButtonText1", Culture); set { } }
        public string SettingsViewTab1Text3 { get => _resManager.GetString("SettingsViewTab1Text3", Culture); set { } }
        public string SettingsViewTab1Text4 { get => _resManager.GetString("SettingsViewTab1Text4", Culture); set { } }

        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;


        public SettingsViewModel()
        {

            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);

            MainViewModel.CultureChanged = CulChanged;
        }

    }
}
