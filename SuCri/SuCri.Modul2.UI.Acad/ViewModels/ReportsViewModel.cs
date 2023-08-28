
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autodesk.AutoCAD.ApplicationServices;
using MaterialDesignThemes.Wpf;
using SuCri.Modul2.UI.Acad.Utils;

namespace SuCri.Modul2.UI.Acad.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }


        public string TotalHeightDWGText { get => _resManager.GetString("TotalHeightDWGText", Culture); set { } }
        public string TotalHeightPRJText { get => _resManager.GetString("TotalHeightPRJText", Culture); set { } }

        public string BomDWGText { get => _resManager.GetString("BomDWGText", Culture); set { } }
        public string BomPRJText { get => _resManager.GetString("BomPRJText", Culture); set { } }

        public string LoadMTOFilterText { get => _resManager.GetString("LoadMTOFilterText", Culture); set { } }

        public string SetTotalHeightOptionText { get => _resManager.GetString("TotalHeightOptionText", Culture); set { } }




        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;

        public ICommand exportTotalHeightDWGCommand { get; }
        public ICommand exportTotalHeightPRJCommand { get; }

        public ICommand ExportBOMDWGCommand { get; }
        public ICommand ExportBOMPRJCommand { get; }

        public ICommand LoadMTOFilterCommand { get; }

        public ICommand SetTotalHeightOptionCommand { get; }


        public ReportsViewModel()
        {

            exportTotalHeightDWGCommand = new ViewModelCommand(ExecuteExportTotalHeightCommand);
            exportTotalHeightPRJCommand = new ViewModelCommand(ExecuteexportTotalHeightPRJCommand);
            ExportBOMDWGCommand = new ViewModelCommand(ExecuteExportBOMDWGCommand);
            ExportBOMPRJCommand = new ViewModelCommand(ExecuteExportBOMPRJCommand);
            LoadMTOFilterCommand = new ViewModelCommand(ExecuteLoadMTOFilterCommand);
            SetTotalHeightOptionCommand = new ViewModelCommand(ExecuteSetTotalHeightOptionCommand);



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

        private void ExecuteExportTotalHeightCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GetTotalHeight\n", true, false, false);
        }

        private void ExecuteexportTotalHeightPRJCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GetTotalHeightAllDrawing\n", true, false, false);
        }
        private void ExecuteExportBOMDWGCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GET_REPORT_ACTIVE_DRAWING\n", true, false, false);
        }
        private void ExecuteExportBOMPRJCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GET_REPORT_ALL_DRAWING\n", true, false, false);
        }
        private void ExecuteLoadMTOFilterCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.LOAD_MTO_FILTER_FILE\n", true, false, false);
        }

        private void ExecuteSetTotalHeightOptionCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.TotalHeightOption\n", true, false, false);
        }

    }
}
