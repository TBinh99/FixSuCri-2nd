using Autodesk.AutoCAD.ApplicationServices;
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
    public class SUCreationViewModel : ViewModelBase
    {
        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }

       
        public string RemovePartsFromSuText { get => _resManager.GetString("RemovePartsFromSuText", Culture); set { } }
        public string SearchSuButtonText { get => _resManager.GetString("SearchSuButtonText", Culture); set { } }
        public string SuInformationText { get => _resManager.GetString("SuInformationText", Culture); set { } }

        public string AddPartsToSuText { get => _resManager.GetString("AddPartsToSuText", Culture); set { } }


        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;

        public ICommand attachCompCommand { get; }
        public ICommand selectSUCommand { get; }

        public ICommand detachCompCommand { get; }

        public ICommand getXDataCommand { get; }

        public SUCreationViewModel()
        {
            ////Initialize Commands
            attachCompCommand = new ViewModelCommand(ExecuteAttachCompCommand);
            selectSUCommand = new ViewModelCommand(ExecuteSelectSUCommand);
            detachCompCommand = new ViewModelCommand(ExecuteDetachCompCommand);
            getXDataCommand = new ViewModelCommand(ExecuteGetXDataCommand);

            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);

            MainViewModel.CultureChanged = CulChanged;
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow); //ShowWindow needs an IntPtr

        private static void FocusProcess()
        {
            IntPtr hWnd; //change this to IntPtr
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "Acad")
                {
                    hWnd = pr.MainWindowHandle; //use it as IntPtr not int
                    ShowWindow(hWnd, 3);
                    SetForegroundWindow(hWnd); //set to topmost
                }
            }
        }

        private void ExecuteAttachCompCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AttachSupport\n", true, false, false);

        }

        private void ExecuteSelectSUCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.SelectSupport\n", true, false, false);
        }

        private void ExecuteDetachCompCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.DetachSupport\n", true, false, false);
        }

        private void ExecuteGetXDataCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.SupportXdata\n", true, false, false);

        }

    }
}
