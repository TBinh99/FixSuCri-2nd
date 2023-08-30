//using WPFinACAD.SiklaForm.View;
//using WPFinACAD.SiklaForm.ViewModel;
using Autodesk.Internal.Windows;
using SuCri.Modul2.UI.Acad;

namespace SuCri.Modul2.Addin
{
    internal class UISingleton
    {
        public static string Theme { get; set; }
        public UISingleton()
        {
            ViewModel = new UI.Acad.ViewModels.MainViewModel();
            View = new WindowCover(Theme) { };
            View.DataContext = ViewModel;
            View.Closed += (s, e) =>
            {
                _instance = null;
            };
        }

        private static UISingleton _instance;

        public static UISingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UISingleton();
                }
                return _instance;
            }
        }

        public WindowCover View { get; set; }

        public SuCri.Modul2.UI.Acad.ViewModels.MainViewModel ViewModel { get; set; }

        public static void Show(string theme)
        {
            Theme = theme;
            if (Instance.View.IsLoaded)
            {
                Instance.View.Activate();
            }
            else
            {
                Instance.View.Show();
            }
        }
        public static void Close()
        {
            if(_instance != null) 
            {
                if (Instance.View.IsLoaded)
                {
                    Instance.View.Close();
                }
            }
        }
    }
}
