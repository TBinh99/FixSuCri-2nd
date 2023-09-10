using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows.AcadDM;
using Autodesk.AutoCAD.Windows.Data;
using CommunityToolkit.Mvvm.Input;
using SuCri.Modul2.UI.Acad.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
namespace SuCri.Modul2.UI.Acad.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public CultureInfo Culture { get; set; }

        void CulChanged()
        {
            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
        }

        public string SinglePlaceClampText { get => _resManager.GetString("SinglePlaceClampText", Culture); set { } }
        public string SinglePlaceSupportText { get => _resManager.GetString("SinglePlaceSupportText", Culture); set { } }
        public string RemovePartsFromSuText { get => _resManager.GetString("RemovePartsFromSuText", Culture); set { } }
        public string ReplaceSupportsDWGText { get => _resManager.GetString("ReplaceSupportsDWGText", Culture); set { } }
        public string ReplaceSupportsPRJText { get => _resManager.GetString("ReplaceSupportsPRJText", Culture); set { } }
        public string SearchSuButtonText { get => _resManager.GetString("SearchSuButtonText", Culture); set { } }
        public string SuInformationText { get => _resManager.GetString("SuInformationText", Culture); set { } }

        public string TotalHeightDWGText { get => _resManager.GetString("TotalHeightDWGText", Culture); set { } }

        public string UpdatePropsDWGText { get => _resManager.GetString("UpdatePropsDWGText", Culture); set { } }

        public string AddPartsToSuText { get => _resManager.GetString("AddPartsToSuText", Culture); set { } }

        public string BomDWGText { get => _resManager.GetString("BomDWGText", Culture); set { } }

        public string LoadMTOFilterText { get => _resManager.GetString("LoadMTOFilterText", Culture); set { } }




        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;

        public ICommand attachCompCommand { get; }
        public ICommand selectSUCommand { get; }

        public ICommand detachCompCommand { get; }

        public ICommand getXDataCommand { get; }
        public ICommand exportTotalHeightDWGCommand { get; }

        public ICommand ExportBOMDWGCommand { get; }

        public ICommand LoadMTOFilterCommand { get; }

        public ICommand UpdatePropsDWGCommand { get; }

        public ICommand ReplaceSupportDWGCommand { get; }

        public ICommand SinglePlaceSupportCommand { get; }

        public ICommand SingleiPlaceClampCommand { get; }

        public HomeViewModel()
        {
            ////Initialize Commands
            attachCompCommand = new ViewModelCommand(param => 
            {
                if (param is System.Windows.Controls.UserControl p)
                {
                    System.Windows.Window window = System.Windows.Window.GetWindow(p);
                    ExecuteAttachCompCommand(window);
                }
            });
            selectSUCommand = new ViewModelCommand(ExecuteSelectSUCommand);
            detachCompCommand = new ViewModelCommand(ExecuteDetachCompCommand);
            getXDataCommand = new ViewModelCommand(ExecuteGetXDataCommand);
            exportTotalHeightDWGCommand = new ViewModelCommand(ExecuteExportTotalHeightDWGCommand);
            ExportBOMDWGCommand = new ViewModelCommand(ExecuteExportBOMDWGCommand);
            LoadMTOFilterCommand = new ViewModelCommand(ExecuteLoadMTOFilterCommand);
            UpdatePropsDWGCommand = new ViewModelCommand(ExecuteUpdatePropsDWGCommand);
            ReplaceSupportDWGCommand = new ViewModelCommand(ExecuteReplaceSupportDWGCommand);
            SinglePlaceSupportCommand = new ViewModelCommand(ExecuteSinglePlaceSupportCommand);
            SingleiPlaceClampCommand = new ViewModelCommand(ExecuteSingleiPlaceClampCommand);

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

        private void ExecuteAttachCompCommand(System.Windows.Window p)
        {
            if (CheckLicense("F2FeatureActive"))
            {
                p.Hide();
                Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
                if (doc == null)
                    return;
                string commandToExcute = "Test";
                doc.CommandCancelled += (sender, e) => doc_CommandHandler(sender, e, p, commandToExcute);
                doc.CommandEnded += (sender, e) => doc_CommandHandler(sender, e, p, commandToExcute);

                doc.SendStringToExecute(commandToExcute, true, false, false);
                //Application.DocumentManager.MdiActiveDocument.CommandCancelled += (sender, e) => MdiActiveDocument_CommandCancelled(sender, e, p);
                //doc.SendStringToExecute("_.AttachSupport\n", true, false, false);
                //Application.DocumentManager.MdiActiveDocument.CommandCancelled += (sender, e) => MdiActiveDocument_CommandCancelled(sender, e, p);
            }
        }

        static void doc_CommandHandler(object sender, CommandEventArgs e, System.Windows.Window p, string commandName)
        {
            if (e.GlobalCommandName == commandName.ToUpper())
            {
                p.Show();
            }
        }
        private void MdiActiveDocument_CommandCancelled(object sender, CommandEventArgs e,System.Windows.Window p) 
        {
            Application.DocumentManager.MdiActiveDocument.CommandCancelled -= (sender1, e1) => MdiActiveDocument_CommandCancelled(sender1, e1, p);
        }
        private bool CheckLicense(string featureName)
        {
            if ((bool)Core.License.Properties.Settings.Default[featureName])
            {
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"Feature is not active");
                return false;
            }
        }
        private void ExecuteSelectSUCommand(object obj)
        {
            if (CheckLicense("F1FeatureActive")) 
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (doc == null)
                    return;
                doc.SendStringToExecute("_.SelectSupport\n", true, false, false);
            }
        }

        private void ExecuteDetachCompCommand(object obj)
        {
            if (CheckLicense("F3FeatureActive"))
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (doc == null)
                    return;
                doc.SendStringToExecute("_.DetachSupport\n", true, false, false);
            }
        }

        private void ExecuteGetXDataCommand(object obj)
        {
            if (CheckLicense("F4FeatureActive"))
            {
                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (doc == null)
                    return;
                doc.SendStringToExecute("_.SupportXdata\n", true, false, false);
            }
        }

        private void ExecuteExportBOMDWGCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GET_REPORT_ACTIVE_DRAWING\n", true, false, false);
        }
        private void ExecuteLoadMTOFilterCommand(object obj)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.LOAD_MTO_FILTER_FILE\n", true, false, false);

        }
        private void ExecuteExportTotalHeightDWGCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GetTotalHeight\n", true, false, false);
        }

        private void ExecuteUpdatePropsDWGCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.GetClampName\n", true, false, false);
        }

        private void ExecuteReplaceSupportDWGCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.REPLACE_EXISTING_SUPPORT_DWG\n", true, false, false);
        }

        private void ExecuteSinglePlaceSupportCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_SUPPORT\n", true, false, false);
        }

        private void ExecuteSingleiPlaceClampCommand(object obj)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
                return;
            doc.SendStringToExecute("_.AUTO_SELECT_CLAMP\n", true, false, false);
        }

    }
}
