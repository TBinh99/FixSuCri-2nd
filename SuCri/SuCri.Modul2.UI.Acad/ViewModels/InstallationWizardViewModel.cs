using Autodesk.AutoCAD.ApplicationServices;
using SuCri.Modul2.UI.Acad.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuCri.Modul2.UI.Acad.ViewModels
{
    public class InstallationWizardViewModel:ViewModelBase
    {

        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }

        public string CaptionText { get => _resManager.GetString("SettingsViewTab1Text1", Culture); set { } }
        public string WizardText1 { get => _resManager.GetString("WizardText1", Culture); set { } }
        public string WizardText2 { get => _resManager.GetString("WizardText2", Culture); set { } }
        public string WizardText3 { get => _resManager.GetString("WizardText3", Culture); set { } }
        public string WizardText4 { get => _resManager.GetString("WizardText4", Culture); set { } }
        public string WizardText5 { get => _resManager.GetString("WizardText5", Culture); set { } }



        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;

        public ICommand RunRegisterCommand { get; }

        public InstallationWizardViewModel()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);

            MainViewModel.CultureChanged = CulChanged;

            RunRegisterCommand = new ViewModelCommand(ExecuteRunRegisterCommand);
        }

        private void ExecuteRunRegisterCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.PLANTREGISTERCUSTOMSCRIPTS\n", true, false, false);
            //doc.CommandEnded += new CommandEventHandler(Doc_CommandEnded); ;
        }

        //private void Doc_CommandEnded(object sender, CommandEventArgs e)
        //{
        //    Properties.Settings.Default.IsRegisterCmdDone = true;
        //}
    }
}
