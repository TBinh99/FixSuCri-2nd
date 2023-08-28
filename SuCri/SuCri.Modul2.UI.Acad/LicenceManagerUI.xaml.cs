using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

namespace SuCri.Modul2.UI.Acad
{
    /// <summary>
    /// Interaktionslogik für InstallationWizardUi.xaml
    /// </summary>
    public partial class LicenceManagerUI : Window
    {

        private string theme;

        public string Theme
        {
            get { return theme; }
            set { theme = value; }
        }

        public LicenceManagerUI()
        {
            theme = Properties.Settings.Default.Theme;
            //var theme = "MPSS";
            InitializeComponent();
            ResourceDictionary newRes = new ResourceDictionary();
            newRes.Source = new Uri("pack://application:,,,/SuCri.Modul2.UI.Acad;component/Themes/" + theme + ".xaml");
            ResourceDictionary oldRes = this.Resources.MergedDictionaries[1];
            this.Resources.MergedDictionaries.Remove(oldRes);
            this.Resources.MergedDictionaries.Add(newRes);
        }


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

        private void MailButton_Click(object sender, RoutedEventArgs e)
        {
            if (Theme == "Sikla")
            {
                var emailAddress = "plant3d@sikla.de";
                var betreff = "Problem with Licences - Sikla Extension";

                Process.Start("mailto:" + emailAddress + "?subject=" + betreff);
            }
            else if (Theme == "MPSS")
            {
                var emailAddress = "plant3d@mpss.de";
                var betreff = "Problem with Licences - MPSS Extension";

                Process.Start("mailto:" + emailAddress + "?subject=" + betreff);
            }
        }
    }


}
