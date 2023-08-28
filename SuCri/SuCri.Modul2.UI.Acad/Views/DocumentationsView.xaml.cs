using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SuCri.Modul2.UI.Acad.Views
{
    /// <summary>
    /// Interaktionslogik für DocumentationsView.xaml
    /// </summary>
    public partial class DocumentationsView : UserControl
    {

        private string curlanguage;

        public string CurLanguage
        {
            get { return curlanguage; }
            set { curlanguage = value; }
        }

        private string curTheme;

        public string CurTheme
        {
            get { return curTheme; }
            set { curTheme = value; }
        }


        public DocumentationsView()
        {
            InitializeComponent();
            curlanguage = Properties.Settings.Default.Language;
            curTheme = Properties.Settings.Default.Theme;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (curTheme == "Sikla")
            { 
                if (CurLanguage == "English")
                {
                    Process.Start("https://www.sikla.de/fast/600/54275_Sikla_Anwenderrichtlinien_hohe%20Aufl%C3%B6sung_EN.pdf");
                }
                else if (CurLanguage == "German")
                {
                    Process.Start("https://www.sikla.de/fast/600/Sikla_Anwenderrichtlinie_de_2017_09_12_web.pdf");
                }
                
            }

            else if (curTheme == "MPSS")
            {
                Process.Start("https://mpss-gmbh.de/wp-content/uploads/2022/08/MP-FLEX-060-Doppel-U-System.pdf");
            }
        }
    }
}
