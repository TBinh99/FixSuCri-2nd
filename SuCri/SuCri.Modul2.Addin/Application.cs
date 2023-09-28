using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using SuCri.Modul2.Core.License;
using SuCri.Modul2.Core.License.View;
using SuCri.Modul2.Core.License.ViewModel;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

[assembly: ExtensionApplication(typeof(SuCri.Modul2.Addin.Application))]

namespace SuCri.Modul2.Addin
{
    public class Application : IExtensionApplication
    {
        public void Initialize()
        {
            LoadRefFiles();
           
            new Ribbon().MyRibbon();

            var alllicenseKey = WTLicenseKey.Instance;

            if(WTLicenseKey.Instance.CustomerInfo == null)
            {
                RegisterVM registerVM = new RegisterVM();

                RegisterUI registerUI = new RegisterUI() { DataContext = registerVM };
                registerUI.ShowDialog();
            }

            //OverruleStartUp overruleStartUp = new OverruleStartUp();
            //overruleStartUp.LockLayerCommand();
        }

        public void Terminate()
        {

        }

        private void LoadRefFiles()
        {
            string dllPath = Assembly.GetExecutingAssembly().Location;
            string parentPath = Directory.GetParent(dllPath).ToString();

            try
            {
                Assembly.LoadFrom(parentPath + @"\MaterialDesignThemes.Wpf.dll");
                Assembly.LoadFrom(parentPath + @"\Microsoft.Xaml.Behaviors.dll");

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

    }
}
