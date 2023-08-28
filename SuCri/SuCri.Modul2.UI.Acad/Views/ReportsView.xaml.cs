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
using SuCri.Modul2.UI.Acad.ViewModels;

namespace SuCri.Modul2.UI.Acad.Views
{
    /// <summary>
    /// Interaktionslogik für ReportsView.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        public ReportsView()
        {
            InitializeComponent();
            //PrimarySupportModel primSup = new PrimarySupportModel();
            //this.DataContext = primSup.getAllPrimarySupports();
        }
    }
}
