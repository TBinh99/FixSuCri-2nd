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

namespace SuCri.Modul2.Core.License.View
{
    /// <summary>
    /// Interaction logic for CompanyLicensesUI.xaml
    /// </summary>
    public partial class CompanyLicensesUI : Window
    {
        public CompanyLicensesUI()
        {
            InitializeComponent();
        }

        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Status")
            {
                e.Column.Visibility = Visibility.Hidden;
            }
        }
    }
}
