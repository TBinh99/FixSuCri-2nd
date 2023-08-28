using Autodesk.AutoCAD.Customization;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using SuCri.Modul2.UI.Acad.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Application;
using SuCri.Modul2.UI.Acad;

namespace SuCri.Modul2.Addin
{
    public class SampleCmd 
    {
        [CommandMethod("Sample")]
        public static void SampleForm()
        {
        //    try
        //    {
        //        //var sampleVM = new SampleFormVM();
        //        var sampleUI = new MainWindow() /*{ DataContext = sampleVM }*/;
        //        sampleUI.ShowDialog();
        //    }
        //    catch (System.Exception ex)
        //    {
        //      MessageBox.Show(Environment.NewLine + ex.Message);
        //    }
        //}

        //public static void ModifyTheme(Action<ITheme> modificationAction)
        //{
        //    var myResourceDictionary = new ResourceDictionary();
        //    myResourceDictionary.Source =
        //        new Uri("SuCri.Modul2.UI.Acad;component/Dictionaries/MainResource.xaml",
        //                UriKind.RelativeOrAbsolute);

        //    ITheme theme = ResourceDictionaryExtensions.GetTheme(myResourceDictionary);

        //    modificationAction?.Invoke(theme);

        //    ResourceDictionaryExtensions.SetTheme(myResourceDictionary, theme);

        //    ITheme theme2 = ResourceDictionaryExtensions.GetTheme(myResourceDictionary);
        }
    }
}
