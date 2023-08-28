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
    public class AutoFeaturesViewModel : ViewModelBase
    {
        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }

        public string MultiPlaceSupportClampText { get => _resManager.GetString("MultiPlaceSupportClampText", Culture); set { } }
        public string SinglePlaceSupportClampText { get => _resManager.GetString("SinglePlaceSupportClampText", Culture); set { } }

        public string MultiPlaceSupportText { get => _resManager.GetString("MultiPlaceSupportText", Culture); set { } }
        public string SinglePlaceSupportText { get => _resManager.GetString("SinglePlaceSupportText", Culture); set { } }

        public string MultiPlaceClampText { get => _resManager.GetString("MultiPlaceClampText", Culture); set { } }
        public string SinglePlaceClampText { get => _resManager.GetString("SinglePlaceClampText", Culture); set { } }



        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;



        public ICommand SinglePlaceClampCommand { get; }
        public ICommand SinglePlaceSupportClampCommand { get; }

        public ICommand SinglePlaceSupportCommand { get; }


        public ICommand MultiPlaceClampCommand { get; }
        public ICommand MultiPlaceSupportClampCommand { get; }

        public ICommand MultiPlaceSupportCommand { get; }

        public AutoFeaturesViewModel()
        {
            ////Initialize Commands
            SinglePlaceClampCommand = new ViewModelCommand(ExecuteSinglePlaceClampCommand);
            SinglePlaceSupportClampCommand = new ViewModelCommand(ExecuteSinglePlaceSupportClampCommand);
            SinglePlaceSupportCommand = new ViewModelCommand(ExecuteSinglePlaceSupportCommand);
            MultiPlaceClampCommand = new ViewModelCommand(ExecuteMultiPlaceClampCommand);
            MultiPlaceSupportClampCommand = new ViewModelCommand(ExecuteMultiPlaceSupportClampCommand);
            MultiPlaceSupportCommand = new ViewModelCommand(ExecuteMultiPlaceSupportCommand);

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

        private void ExecuteSinglePlaceClampCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_CLAMP\n", true, false, false);
        }

        private void ExecuteSinglePlaceSupportClampCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_SUPPORT\n", true, false, false);
        }

        private void ExecuteSinglePlaceSupportCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_SUPPORT_CLAMP\n", true, false, false);
        }

        private void ExecuteMultiPlaceClampCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_MULTIPLE_CLAMP\n", true, false, false);
        }

        private void ExecuteMultiPlaceSupportClampCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_MULTIPLE_SUPPORT_CLAMP\n", true, false, false);
        }

        private void ExecuteMultiPlaceSupportCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_MULTIPLE_SUPPORT\n", true, false, false);
        }
    }
}
