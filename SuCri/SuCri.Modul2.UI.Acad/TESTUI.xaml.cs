

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
using System.Windows.Shapes;

namespace SuCri.Modul2.UI.Acad
{
    /// <summary>
    /// Interaktionslogik für TESTUI.xaml
    /// </summary>
    public partial class TESTUI : Window
    {
        public TESTUI()
        {
            InitializeComponent();
        }

        private void cbxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newtheme = cbxTheme.SelectedItem as ComboBoxItem;

            ResourceDictionary newRes = new ResourceDictionary();
            newRes.Source = new Uri("pack://application:,,,/WPFinACAD;component/Dictionaries/"+ newtheme?.Content+".xaml");
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newRes);
        }
    }
}
