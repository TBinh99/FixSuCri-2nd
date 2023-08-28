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
    public class PropertiesViewModel : ViewModelBase
    {
        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }

        public string UpdatePropsDWGText { get => _resManager.GetString("UpdatePropsDWGText", Culture); set { } }
        public string UpdatePropsPRJText { get => _resManager.GetString("UpdatePropsPRJText", Culture); set { } }
        public string RestorePropsDWGText { get => _resManager.GetString("RestorePropsDWGText", Culture); set { } }
        public string RestorePropsPRJText { get => _resManager.GetString("RestorePropsPRJText", Culture); set { } }



        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;

        public ICommand UpdatePropsDWGCommand { get; }
        public ICommand UpdatePropsPRJCommand { get; }

        public ICommand RestorePropsDWGCommand { get; }

        public ICommand RestorePropsPRJCommand { get; }


        public PropertiesViewModel()
        {
            ////Initialize Commands
            UpdatePropsDWGCommand = new ViewModelCommand(ExecuteUpdatePropsDWGCommand);
            UpdatePropsPRJCommand = new ViewModelCommand(ExecuteUpdatePropsPRJCommand);
            RestorePropsDWGCommand = new ViewModelCommand(ExecuteRestorePropsDWGCommand);
            RestorePropsPRJCommand = new ViewModelCommand(ExecuteRestorePropsPRJCommand);

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

        private void ExecuteUpdatePropsDWGCommand(object obj)
        {


            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GetClampName\n", true, false, false);
        }

        private void ExecuteUpdatePropsPRJCommand(object obj)
        {


            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GetClampNameProject\n", true, false, false);
        }

        private void ExecuteRestorePropsDWGCommand(object obj)
        {


            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.UpdateDWGPropertiesFromSpec\n", true, false, false);
        }

        private void ExecuteRestorePropsPRJCommand(object obj)
        {


            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.UpdatePropertiesFromSpec\n", true, false, false);
        }
    }
}
