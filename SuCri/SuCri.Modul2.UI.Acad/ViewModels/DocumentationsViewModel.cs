using SuCri.Modul2.UI.Acad.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuCri.Modul2.UI.Acad.ViewModels
{
    public class DocumentationsViewModel : ViewModelBase
    {
        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }

        public string DocumentationViewTabControlText1 { get => _resManager.GetString("DocumentationViewTabControlText1", Culture); set { } }
        public string DocumentationViewTabControlText2 { get => _resManager.GetString("DocumentationViewTabControlText2", Culture); set { } }
        public string DocumentationSuCriFooterText1 { get => _resManager.GetString("DocumentationSuCriFooterText1", Culture); set { } }
        public string DocumentationSuCriFooterText2 { get => _resManager.GetString("DocumentationSuCriFooterText2", Culture); set { } }
        public string DocumentationSuCriButtonText1 { get => _resManager.GetString("DocumentationSuCriButtonText1", Culture); set { } }
        public string DocumentationSuCriButtonText2 { get => _resManager.GetString("DocumentationSuCriButtonText2", Culture); set { } }

        public string DocumentationManuFooterText1 { get => _resManager.GetString("DocumentationManuFooterText1", Culture); set { } }
        public string DocumentationManuFooterText2 { get => _resManager.GetString("DocumentationManuFooterText2", Culture); set { } }
        public string DocumentationManuFooterText3 { get => _resManager.GetString("DocumentationManuFooterText3", Culture); set { } }
        public string DocumentationManuButtonText1 { get => _resManager.GetString("DocumentationManuButtonText1", Culture); set { } }
        public string DocumentationManuButtonText2 { get => _resManager.GetString("DocumentationManuButtonText2", Culture); set { } }
        public string DocumentationManuButtonText3 { get => _resManager.GetString("DocumentationManuButtonText3", Culture); set { } }

        public string SiklaInstallationGuidelinesLink { get => _resManager.GetString("SiklaInstallationGuidelinesLink", Culture); set { } }
        public string MPSSInstallationGuidelinesLink { get => _resManager.GetString("MPSSInstallationGuidelinesLink", Culture); set { } }

        public string SiklaTypicalsLink { get => _resManager.GetString("SiklaTypicalsLink", Culture); set { } }
        public string MPSSTypicalsLink { get => _resManager.GetString("MPSSTypicalsLink", Culture); set { } }

        public string SuCriUserManual { get => _resManager.GetString("SuCriUserManual", Culture); set { } }
        public string SuCriIndexSheet { get => _resManager.GetString("SuCriIndexSheet", Culture); set { } }

        public string SiklaDatasheetsLink { get => _resManager.GetString("SiklaDatasheetsLink", Culture); set { } }
        public string MPSSDatasheetsLink { get => _resManager.GetString("MPSSDatasheetsLink", Culture); set { } }

        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;


        public ICommand ShowInstallationGuidelines { get; }

        public ICommand ShowTypicals { get; }

        public ICommand ShowSuCriUsermanual { get; }
        public ICommand ShowSuCriIndexSheet { get; }

        public ICommand ShowManuDataSheets { get; }
        public DocumentationsViewModel()
        {

            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);

            MainViewModel.CultureChanged = CulChanged;

            ShowInstallationGuidelines = new ViewModelCommand(ExecuteShowInstallationGuidelines);
            ShowTypicals = new ViewModelCommand(ExecuteShowTypicals);
            ShowSuCriUsermanual = new ViewModelCommand(ExecuteShowSuCriUsermanual);
            ShowSuCriIndexSheet = new ViewModelCommand(ExecuteShowSuCriIndexSheet);
            ShowManuDataSheets = new ViewModelCommand(ExecuteShowManuDataSheets);
        }

        private void ExecuteShowInstallationGuidelines(object obj)
        {
            var theme = Properties.Settings.Default.Theme;
            if (theme == "Sikla")
            {
                Process.Start(SiklaInstallationGuidelinesLink);
            }

            else if (theme == "MPSS")
            {
                Process.Start(MPSSInstallationGuidelinesLink);
            }
        }

        private void ExecuteShowTypicals(object obj)
        {
            var theme = Properties.Settings.Default.Theme;
            if (theme == "Sikla")
            {
                Process.Start(SiklaTypicalsLink);
            }

            else if (theme == "MPSS")
            {
                Process.Start(MPSSTypicalsLink);
            }
        }

        private void ExecuteShowManuDataSheets(object obj)
        {
            var theme = Properties.Settings.Default.Theme;
            if (theme == "Sikla")
            {
                Process.Start(SiklaDatasheetsLink);
            }

            else if (theme == "MPSS")
            {
                Process.Start(MPSSDatasheetsLink);
            }
        }

        private void ExecuteShowSuCriUsermanual(object obj)
        {
            Process.Start(SuCriUserManual);
        }
        private void ExecuteShowSuCriIndexSheet(object obj)
        {
            Process.Start(SuCriIndexSheet);
        }
    }
}
