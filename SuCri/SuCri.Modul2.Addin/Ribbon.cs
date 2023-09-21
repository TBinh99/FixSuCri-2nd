using System.Windows.Media.Imaging;
using System;
using System.IO;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows;
using Autodesk.AutoCAD.Windows;
using SuCri.Modul2.UI.Acad;
using System.Windows.Forms.Integration;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using SuCri.Modul2.Addin.CustomPalette;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Windows.ToolPalette;
using Autodesk.AutoCAD.DatabaseServices;
using System.Windows.Shapes;
using System.Xml.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal.Reactors;
using System.Linq;

namespace SuCri.Modul2.Addin
{
    public class Ribbon
    {
        private RibbonCombo _pan1Ribcombo1 = new RibbonCombo();
        private readonly RibbonCombo _pan3Ribcombo = new RibbonCombo();

        [CommandMethod("LoadSuCriRibbon")]
        public void MyRibbon()
        {
            try
            {
                var ribbonControl = ComponentManager.Ribbon;
                var tab = new RibbonTab
                {
                    Title = "SuCri",
                    Id = "SuCri_TAB_ID"
                };

                ribbonControl.Tabs.Add(tab);

                // create Ribbon panels
                var panel1Panel = new RibbonPanelSource
                {
                    Title = "Extensions"
                };

                var panel1 = new RibbonPanel
                {
                    Source = panel1Panel
                };

                tab.Panels.Add(panel1);

                var pan1Button1 = new RibbonButton
                {
                    Text = "Sikla",
                    ShowText = true,
                    ShowImage = true,
                    Image = Images.GetBitmap(Properties.Resources.SiklaImage),
                    LargeImage = Images.GetBitmap(Properties.Resources.SiklaImage),
                    Orientation = System.Windows.Controls.Orientation.Vertical,
                    Size = RibbonItemSize.Large,
                    CommandHandler = new RibbonCommandHandler(),
                    CommandParameter = "SiklaForm",
                };

                var pan1Button2 = new RibbonButton
                {
                    Text = "MPSS",
                    ShowText = true,
                    ShowImage = true,
                    Image = Images.GetBitmap(Properties.Resources.MPSS),
                    LargeImage = Images.GetBitmap(Properties.Resources.MPSS),
                    Orientation = System.Windows.Controls.Orientation.Vertical,
                    Size = RibbonItemSize.Large,
                    CommandHandler = new RibbonCommandHandler(),
                    CommandParameter = "MPSSForm",
                };

                //var pan1Button3 = new RibbonButton
                //{
                //    Text = "Halfen",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.Halfen),
                //    LargeImage = Images.GetBitmap(Properties.Resources.Halfen),
                //    Orientation = System.Windows.Controls.Orientation.Vertical,
                //    Size = RibbonItemSize.Large,
                //    CommandHandler = new RibbonCommandHandler(),
                //    CommandParameter = "HalfenForm",
                //};

                var pan1Button4 = new RibbonButton
                {
                    Text = "Palette",
                    ShowText = true,
                    ShowImage = true,
                    Image = Images.GetBitmap(Properties.Resources.Halfen),
                    LargeImage = Images.GetBitmap(Properties.Resources.Halfen),
                    Orientation = System.Windows.Controls.Orientation.Vertical,
                    Size = RibbonItemSize.Large,
                    CommandHandler = new RibbonCommandHandler(),
                    CommandParameter = "Palette",
                };

                var pan1Button5 = new RibbonButton
                {
                    Text = "LicenseInfo",
                    ShowText = true,
                    ShowImage = true,
                    Image = Images.GetBitmap(Properties.Resources.License),
                    LargeImage = Images.GetBitmap(Properties.Resources.License),
                    Orientation = System.Windows.Controls.Orientation.Vertical,
                    Size = RibbonItemSize.Large,
                    CommandHandler = new RibbonCommandHandler(),
                    CommandParameter = "LicenseForm",
                };

                //var panel2Panel = new RibbonPanelSource
                //{
                //    Title = "Modul4"
                //};

                //var panel3Panel = new RibbonPanelSource
                //{
                //    Title = "About"
                //};
                //var panel2 = new RibbonPanel
                //{
                //    Source = panel2Panel
                //};

                //var panel3 = new RibbonPanel
                //{
                //    Source = panel3Panel
                //};
                /* tab.Panels.Add(panel2)*/
                ;
                //tab.Panels.Add(panel3);

                //var pan3Button1 = new RibbonButton
                //{
                //    Text = "About",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.infomation),
                //    LargeImage = Images.GetBitmap(Properties.Resources.infomation),
                //    Orientation = System.Windows.Controls.Orientation.Vertical,
                //    Size = RibbonItemSize.Large,
                //    CommandHandler = new RibbonCommandHandler(),
                //    CommandParameter = "ExtensionInfo"
                //};

                //var pan1Button3 = new RibbonButton
                //{
                //    Text = "Button3",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                // Set te properties for the RibbonCombo
                // Te ribboncombo control does not listen to the command handler
                _pan1Ribcombo1.Text = " ";
                _pan1Ribcombo1.ShowText = true;
                _pan1Ribcombo1.MinWidth = 150;

                //var pan1Row1 = new RibbonRowPanel();
                //pan1Row1.Items.Add(pan1Button2);
                //pan1Row1.Items.Add(new RibbonRowBreak());
                //pan1Row1.Items.Add(pan1Button3);
                //pan1Row1.Items.Add(new RibbonRowBreak());
                //pan1Row1.Items.Add(_pan1Ribcombo1);

                panel1Panel.Items.Add(pan1Button1);
                panel1Panel.Items.Add(pan1Button2);
                //panel1Panel.Items.Add(pan1Button3);
                panel1Panel.Items.Add(pan1Button4);
                panel1Panel.Items.Add(pan1Button5);
                //panel3Panel.Items.Add(pan3Button1);
                //panel1Panel.Items.Add(new RibbonSeparator());
                //panel1Panel.Items.Add(pan1Row1);

                //var panel2Panel = new RibbonPanelSource
                //{
                //    Title = "Panel2"
                //};
                //var panel2 = new RibbonPanel
                //{
                //    Source = panel2Panel
                //};
                //tab.Panels.Add(panel2);

                //var pan2SplitButton = new RibbonSplitButton
                //{
                //    Text = "SplitButton", //Required not to crash AutoCAD when using cmd locators 
                //    CommandHandler = new RibbonCommandHandler(),
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    IsSplit = true,
                //    Size = RibbonItemSize.Large
                //};

                //var pan2Button1 = new RibbonButton
                //{
                //    Text = "Button1",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    Size = RibbonItemSize.Large,
                //    Orientation = System.Windows.Controls.Orientation.Vertical,
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan2Button2 = new RibbonButton
                //{
                //    Text = "Button2",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan2Button3 = new RibbonButton
                //{
                //    Text = "Button3",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //pan2SplitButton.Items.Add(pan2Button1);
                //pan2SplitButton.Items.Add(pan2Button2);

                //var pan2Row1 = new RibbonRowPanel();
                //pan2Row1.Items.Add(pan2Button2);
                //pan2Row1.Items.Add(new RibbonRowBreak());
                //pan2Row1.Items.Add(pan2Button3);
                //pan2Row1.Items.Add(new RibbonRowBreak());
                //pan2Row1.Items.Add(new RibbonCombo());

                //panel2Panel.Items.Add(pan2SplitButton);
                //panel2Panel.Items.Add(pan2Row1);

                //var panel3Panel = new RibbonPanelSource
                //{
                //    Title = "Panel3"
                //};
                //var panel3 = new RibbonPanel
                //{
                //    Source = panel3Panel
                //};
                //tab.Panels.Add(panel3);

                //var pan3Button1 = new RibbonButton
                //{
                //    Text = "Button1",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    Size = RibbonItemSize.Large,
                //    Orientation = System.Windows.Controls.Orientation.Vertical,
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan3Button2 = new RibbonButton
                //{
                //    Text = "Button2",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    Size = RibbonItemSize.Large,
                //    Orientation = System.Windows.Controls.Orientation.Vertical,
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan3Button3 = new RibbonButton
                //{
                //    Text = "Button3",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan3Button4 = new RibbonButton
                //{
                //    Text = "Button4",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan3Button5 = new RibbonButton
                //{
                //    Text = "Button5",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan3Row1 = new RibbonRowPanel();
                //pan3Row1.Items.Add(pan3Button3);
                //pan3Row1.Items.Add(new RibbonRowBreak());
                //pan3Row1.Items.Add(pan3Button4);
                //pan3Row1.Items.Add(new RibbonRowBreak());
                //pan3Row1.Items.Add(pan3Button5);

                //panel3Panel.Items.Add(pan3Button1);
                //panel3Panel.Items.Add(pan3Row1);

                //var pan4Panel = new RibbonPanelSource
                //{
                //    Title = "Panel4"
                //};
                //var panel4 = new RibbonPanel
                //{
                //    Source = pan4Panel
                //};
                //tab.Panels.Add(panel4);

                //var pan4Button1 = new RibbonButton
                //{
                //    Text = "Button1",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    Size = RibbonItemSize.Large,
                //    Orientation = System.Windows.Controls.Orientation.Vertical,
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan4Button2 = new RibbonButton
                //{
                //    Text = "Button2",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan4Button3 = new RibbonButton
                //{
                //    Text = "Button3",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan4Button4 = new RibbonButton
                //{
                //    Text = "Button4",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    Size = RibbonItemSize.Large,
                //    Orientation = System.Windows.Controls.Orientation.Vertical,
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan4Ribcombobutton1 = new RibbonButton
                //{
                //    Text = "Button1",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //var pan4Ribcombobutton2 = new RibbonButton
                //{
                //    Text = "Button2",
                //    ShowText = true,
                //    ShowImage = true,
                //    Image = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    LargeImage = Images.GetBitmap(Properties.Resources.EmailSignatur32),
                //    CommandHandler = new RibbonCommandHandler()
                //};

                //_pan3Ribcombo.Width = 150;
                //_pan3Ribcombo.Items.Add(pan4Ribcombobutton1);
                //_pan3Ribcombo.Items.Add(pan4Ribcombobutton2);
                //_pan3Ribcombo.Current = pan4Ribcombobutton1;

                //var vvorow1 = new RibbonRowPanel();
                //vvorow1.Items.Add(pan4Button2);
                //vvorow1.Items.Add(new RibbonRowBreak());
                //vvorow1.Items.Add(pan4Button3);
                //vvorow1.Items.Add(new RibbonRowBreak());
                //vvorow1.Items.Add(_pan3Ribcombo);

                //pan4Panel.Items.Add(pan4Button1);
                //pan4Panel.Items.Add(vvorow1);
                //pan4Panel.Items.Add(new RibbonSeparator());
                //pan4Panel.Items.Add(pan4Button4);

                tab.IsActive = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        #region 
        //    const string catName = "ScriptCatalog";

        //    const string palName = "Scripts";
        //    ToolPaletteManager tpm = ToolPaletteManager.Manager;
        //    Type t = typeof(SiklaTool);
        //    GuidAttribute ga =(GuidAttribute)t.GetCustomAttributes(typeof(GuidAttribute), false)[0];

        //    Guid g = new Guid(ga.Value);
        //    SiklaTool tool = new SiklaTool();

        //    Catalog cat;

        //    Autodesk.AutoCAD.Windows.ToolPalette.Palette pal = null;
        //    CatalogItem ci = tpm.StockToolCatalogs.Find(g);
        //    var test = tpm.StockToolCatalogs;

        //    if (ci != null)
        //    {
        //        // If it is, search each catalog for our palette
        //        foreach (CatalogItem ci2 in tpm.Catalogs)
        //        {
        //            for (int i = 0; i < ci2.ChildCount; i++)
        //            { 
        //                CatalogItem ci3 = ci2.GetChild(i);
        //                if (ci3 != null && ci3.Name == palName)
        //                {
        //                    pal = ci3 as Autodesk.AutoCAD.Windows.ToolPalette.Palette;
        //                    break;
        //                }
        //            }
        //            if (pal != null)
        //                break;
        //        }
        //    }
        //    if (pal == null)
        //    {
        //        cat = tool.CreateStockTool(catName);
        //        pal = tool.CreatePalette(cat, palName);
        //        //cat.DeleteAllChildren();
        //        //pal.DeleteAllChildren();
        //    }
        //    ImageInfo ii = new ImageInfo();
        //    ii.ResourceFile = "D:\\PTB\\JOB\\Fromgithub\\FixSuCri-2nd\\SuCri\\SuCri.Modul2.Addin\\Resources\\sikla.png";
        //    ii.Size = new System.Drawing.Size(32, 32);
        //    string name = "Sikla";

        //    tool.CreateCommandTool(
        //      pal,
        //      name,
        //      ii,
        //      "COMMAND SiklaForm"
        //    );

        //    //// Finally we reload the catalogs to display the change
        //    tpm.LoadCatalogs(CatalogTypeFlags.Catalog, LoadFlags.LoadImages );
        //}

        //[Guid("3D4EB45D-F638-439B-9555-8D489693D6C5")]
        //[Tool("SiklaTool", "IDB_TOOL")]
        //[ClassInterface(ClassInterfaceType.AutoDual)]
        //public class SiklaTool : CustomToolBase
        //{
        //}
        #endregion
        //public class MyUserControl : System.Windows.Controls.UserControl
        //{
        //    public MyUserControl()
        //    {
        //        // Add your MainWindow content here
        //        // For example, you can create a new instance of MainWindow
        //        UserControl1 mainWindow = new UserControl1();
        //        mainWindow.Theme = "Sikla";

        //        // Add the MainWindow content to the UserControl's layout panel
        //        // This can be a Grid, StackPanel, etc.
        //        Content = mainWindow;
        //    }
        //}
        private class RibbonCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var button = parameter as RibbonButton;
                if (button.CommandParameter == null) return;
                Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.
                    SendStringToExecute(button.CommandParameter + " ", true, false, false);
            }
        }

        private static class Images
        {
            public static BitmapImage GetBitmap(Bitmap image)
            {
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Png);
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = stream;
                bmp.EndInit();

                return bmp;
            }
        }
    }
}
