using Autodesk.AutoCAD.Runtime;
using System;
using System.Reflection;
using System.Windows.Forms;
using SuCri.Modul2.UI.Acad.Views;
using SuCri.Modul2.UI.Acad;
using Autodesk.AutoCAD.Windows;
using System.Drawing;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Forms.Integration;
using Autodesk.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using SuCri.Modul2.Core.License.View;
using SuCri.Modul2.Core.License.ViewModel;
using SuCri.Modul2.Core.License;

[assembly: CommandClass(typeof(SuCri.Modul2.Addin.SiklaCmd))]

namespace SuCri.Modul2.Addin
{
    public class SiklaCmd
    {
        [CommandMethod("SiklaForm")]
        public void SiklaForm()
        {
            //SiklaSingleton.Instance.ViewModel.SUNumberSource = new System.Collections.ObjectModel.ObservableCollection<Model.SUNumber>() {
            //new Model.SUNumber(){
            //Name="SU001",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2" } },
            //   new Model.SUNumber(){
            //Name="SU002",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2", "Item3", "Item4" } },
            //};

            //SiklaSingleton.Instance.ViewModel.SelectedSUNumber= SiklaSingleton.Instance.ViewModel.SUNumberSource[0];

            //SiklaSingleton.Instance.ViewModel.NotiList = new System.Collections.ObjectModel.ObservableCollection<Model.NotiItem>() 
            //{
            //    new Model.NotiItem(){
            //    Content = "Note 1",
            //    Messages = "First One"},

            //     new Model.NotiItem(){
            //    Content = "Note 2",
            //    Messages = "Second One"},
            //};


            //try
            //{
            //    //var mainVM = new SuCri.Modul2.UI.Acad.ViewModels.MainViewModel();
            //    var _wmw = new MainWindow("Sikla") /*{ DataContext = mainVM }*/;
            //    _wmw.Show();
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(Environment.NewLine + ex.Message);
            //}
            PaletteSingleton.Hide();
            UISingleton.Show("Sikla");
        }

        [CommandMethod("HiltiForm")]
        public void HiltiForm()
        {
            //SiklaSingleton.Instance.ViewModel.SUNumberSource = new System.Collections.ObjectModel.ObservableCollection<Model.SUNumber>() {
            //new Model.SUNumber(){
            //Name="SU001",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2" } },
            //   new Model.SUNumber(){
            //Name="SU002",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2", "Item3", "Item4" } },
            //};

            //SiklaSingleton.Instance.ViewModel.SelectedSUNumber= SiklaSingleton.Instance.ViewModel.SUNumberSource[0];

            //SiklaSingleton.Instance.ViewModel.NotiList = new System.Collections.ObjectModel.ObservableCollection<Model.NotiItem>() 
            //{
            //    new Model.NotiItem(){
            //    Content = "Note 1",
            //    Messages = "First One"},

            //     new Model.NotiItem(){
            //    Content = "Note 2",
            //    Messages = "Second One"},
            //};


            //try
            //{
            //    //var sampleVM = new SampleFormVM();
            //    var _wmw = new MainWindow("Hilti") /*{ DataContext = sampleVM }*/;
            //    _wmw.Show();
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(Environment.NewLine + ex.Message);
            //}
            //PaletteSingleton.Hide();
            //SiklaSingleton.Show();
        }


        [CommandMethod("MPSSForm")]
        public void MPSSForm()
        {
            //SiklaSingleton.Instance.ViewModel.SUNumberSource = new System.Collections.ObjectModel.ObservableCollection<Model.SUNumber>() {
            //new Model.SUNumber(){
            //Name="SU001",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2" } },
            //   new Model.SUNumber(){
            //Name="SU002",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2", "Item3", "Item4" } },
            //};

            //SiklaSingleton.Instance.ViewModel.SelectedSUNumber= SiklaSingleton.Instance.ViewModel.SUNumberSource[0];

            //SiklaSingleton.Instance.ViewModel.NotiList = new System.Collections.ObjectModel.ObservableCollection<Model.NotiItem>() 
            //{
            //    new Model.NotiItem(){
            //    Content = "Note 1",
            //    Messages = "First One"},

            //     new Model.NotiItem(){
            //    Content = "Note 2",
            //    Messages = "Second One"},
            //};


            //try
            //{
            //    //var sampleVM = new SampleFormVM();
            //    var _wmw = new MainWindow("Hilti") /*{ DataContext = sampleVM }*/;
            //    _wmw.Show();
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(Environment.NewLine + ex.Message);
            //}
            PaletteSingleton.Hide();
            UISingleton.Show("MPSS");
        }

        [CommandMethod("HalfenForm")]
        public void HalfenForm()
        {
            //SiklaSingleton.Instance.ViewModel.SUNumberSource = new System.Collections.ObjectModel.ObservableCollection<Model.SUNumber>() {
            //new Model.SUNumber(){
            //Name="SU001",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2" } },
            //   new Model.SUNumber(){
            //Name="SU002",
            //Values=new System.Collections.ObjectModel.ObservableCollection<string>(){"Item1","Item2", "Item3", "Item4" } },
            //};

            //SiklaSingleton.Instance.ViewModel.SelectedSUNumber= SiklaSingleton.Instance.ViewModel.SUNumberSource[0];

            //SiklaSingleton.Instance.ViewModel.NotiList = new System.Collections.ObjectModel.ObservableCollection<Model.NotiItem>() 
            //{
            //    new Model.NotiItem(){
            //    Content = "Note 1",
            //    Messages = "First One"},

            //     new Model.NotiItem(){
            //    Content = "Note 2",
            //    Messages = "Second One"},
            //};


            //try
            //{
            //    //var sampleVM = new SampleFormVM();
            //    var _wmw = new MainWindow("Halfen") /*{ DataContext = sampleVM }*/;
            //    _wmw.Show();
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(Environment.NewLine + ex.Message);
            //}
            //PaletteSingleton.Hide();
            //SiklaSingleton.Show();
        }
        [CommandMethod("Palette")]
        public void Palette()
        {
            UISingleton.Close();
            PaletteSingleton.Show("Sikla");
        }
        [CommandMethod("ExtensionInfo")]
        public static void ExtensionInfo()
        {
            string copyRight, version;
            copyRight = ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyCopyrightAttribute))).Copyright;
            version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            System.Windows.Forms.MessageBox.Show($"Current version : {version}\n{copyRight}", "About",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        [CommandMethod("Test")]
        public void Test()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor editor = doc.Editor;

            // Prompt user for the start point of the line
            PromptPointResult startPointResult = editor.GetPoint("Enter start point: ");
            if (startPointResult.Status != PromptStatus.OK)
                return;

            // Prompt user for the end point of the line
            PromptPointResult endPointResult = editor.GetPoint("Enter end point: ");
            if (endPointResult.Status != PromptStatus.OK)
                return;

            // Create the line using the user's input
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord modelSpace = tr.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                Line line = new Line(startPointResult.Value, endPointResult.Value);
                modelSpace.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                tr.Commit();
            }

            editor.WriteMessage("Line created successfully.\n");
        }

        [CommandMethod("LicenseForm")]
        public void LicenseForm()
        {
            PaletteSingleton.Hide();
            LicenseUI ui = new LicenseUI();
            ui.Show();
        }
        [CommandMethod("CompanyLicensesForm")]
        public void CompanyLicensesForm()
        {
            PaletteSingleton.Hide();
            if (!WTLicenseKey.Instance.IsValidKey || WTLicenseKey.Instance.LicenseKey == null)
            {
                System.Windows.MessageBox.Show("Please open license first");
                return;
            }
            if (!WTLicenseKey.Instance.HaveCustomerInfomation)
            {
                System.Windows.MessageBox.Show("No customer information found");
                return;
            }
            CompanyLicensesUI ui = new CompanyLicensesUI();
            ui.Show();
        }
        //private void RegisterHaleyThemes()
        //{
        //    ts.SetupInternalTheme(HaleyThemeProvider.Prepare);
        //    var _internalgroup = new InternalThemeBuilder();
        //    _internalgroup
        //        .Add("Theme1", InternalThemeMode.Normal)
        //        .Add("Theme2", InternalThemeMode.Mild)
        //        .Add("Theme3", InternalThemeMode.Dark);
        //    ts.RegisterGroup(_internalgroup);
        //}
    }
}

