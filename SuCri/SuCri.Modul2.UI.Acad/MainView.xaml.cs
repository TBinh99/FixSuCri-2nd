using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuCri.Modul2.UI.Acad
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            Properties.Settings.Default.Theme = Theme;
            //var theme = "MPSS";
            ResourceDictionary newRes = new ResourceDictionary();
            newRes.Source = new Uri("pack://application:,,,/SuCri.Modul2.UI.Acad;component/Themes/" + Theme + ".xaml");
            ResourceDictionary oldRes = this.Resources.MergedDictionaries[1];
            this.Resources.MergedDictionaries.Remove(oldRes);
            this.Resources.MergedDictionaries.Add(newRes);
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(Window.GetWindow(this));
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow(this);
            if(currentWindow != null) 
            {
                Window.GetWindow(this).Close();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow(this);
            if (currentWindow != null)
            {
                currentWindow.WindowState = WindowState.Minimized;
            }
        }
        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            WindowExpander.IsExpanded = false;
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow(this);
            if (currentWindow != null) 
            {
                if (currentWindow.WindowState == WindowState.Normal)
                    currentWindow.WindowState = WindowState.Maximized;
                else currentWindow.WindowState = WindowState.Normal;
            }
        }

        //private void cbxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    var newtheme = cbxTheme.SelectedItem as ComboBoxItem;

        //    ResourceDictionary newRes = new ResourceDictionary();
        //    newRes.Source = new Uri("pack://application:,,,/SuCri.Modul2.UI.Acad;component/Themes/" + newtheme?.Content + ".xaml");
        //    ResourceDictionary oldRes = this.Resources.MergedDictionaries[1];
        //    this.Resources.MergedDictionaries.Remove(oldRes);
        //    this.Resources.MergedDictionaries.Add(newRes);
        //}

        private void cbxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            string copyright = fvi.LegalCopyright;
            lblCopyright.Text = String.Format("{0} | SuCri Build {1}", copyright, version);
        }

        private void MailButton_Click(object sender, RoutedEventArgs e)
        {
            if (Theme == "Sikla")
            {
                var emailAddress = "plant3d@sikla.de";
                var betreff = "Problem with SuCri - Sikla Extension";

                Process.Start("mailto:" + emailAddress + "?subject=" + betreff);
            }
            else if (Theme == "MPSS")
            {
                var emailAddress = "plant3d@mpss.de";
                var betreff = "Problem with SuCri - MPSS Extension";

                Process.Start("mailto:" + emailAddress + "?subject=" + betreff);
            }
        }

        private void Image_Manu_Homepage(object sender, MouseButtonEventArgs e)
        {
            if (Theme == "Sikla")
            {
                Process.Start("https://www.sikla.de");
            }
            else if (Theme == "MPSS")
            {
                Process.Start("https://www.mpss-gmbh.de");
            }
        }

        private void Image_SuCri_Homepage(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://www.sucri.de");
        }

        private static readonly Type ControlType = typeof(MainView);
        public string Theme
        {
            get => (string)GetValue(ThemeProperty);
            set {
                SetValue(ThemeProperty, value);
                ResourceDictionary newRes = new ResourceDictionary();
                newRes.Source = new Uri("pack://application:,,,/SuCri.Modul2.UI.Acad;component/Themes/" + value + ".xaml");
                ResourceDictionary oldRes = this.Resources.MergedDictionaries[1];
                this.Resources.MergedDictionaries.Remove(oldRes);
                this.Resources.MergedDictionaries.Add(newRes);
            }
        }
        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(
         nameof(Theme),
         typeof(string),
         ControlType,
         new FrameworkPropertyMetadata("Sikla"));
    }
}
