using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;
using System.Reflection;
using SuCri.Modul2.UI.Acad.ViewModels;
using SuCri.Modul2.UI.Acad.Languages;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;
using System.Diagnostics;

namespace SuCri.Modul2.UI.Acad
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public string Theme { get; set; }
        public MainWindow(string theme)
        {
            Theme = theme;
            Properties.Settings.Default.Theme = Theme;
            //var theme = "MPSS";
            InitializeComponent();
            ResourceDictionary newRes = new ResourceDictionary();
            newRes.Source = new Uri("pack://application:,,,/SuCri.Modul2.UI.Acad;component/Themes/" + Theme + ".xaml");
            ResourceDictionary oldRes = this.Resources.MergedDictionaries[1];
            this.Resources.MergedDictionaries.Remove(oldRes);
            this.Resources.MergedDictionaries.Add(newRes);
            //cbxTheme.Text = theme;
        }

        //public MainWindow(string theme)
        //{
        //    var theme = "Sikla";
        //    InitializeComponent();
        //    ResourceDictionary newRes = new ResourceDictionary();
        //    newRes.Source = new Uri("pack://application:,,,/SuCri.Modul2.UI.Acad;component/Themes/" + theme + ".xaml");
        //    ResourceDictionary oldRes = this.Resources.MergedDictionaries[1];
        //    this.Resources.MergedDictionaries.Remove(oldRes);
        //    this.Resources.MergedDictionaries.Add(newRes);
        //    //cbxTheme.Text = theme;

        //}

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
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
            else if(Theme == "MPSS")
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
    }
}
