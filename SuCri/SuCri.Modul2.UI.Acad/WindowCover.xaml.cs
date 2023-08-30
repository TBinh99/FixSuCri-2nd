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
    /// Interaction logic for WindowCover.xaml
    /// </summary>
    public partial class WindowCover : Window
    {
        public WindowCover(string theme)
        {
            InitializeComponent();
            MainView.Theme = theme;
            MainView.DataContext = new UI.Acad.ViewModels.MainViewModel();
        }
    }
}
