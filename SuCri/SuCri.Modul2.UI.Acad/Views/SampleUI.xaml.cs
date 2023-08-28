using Autodesk.AutoCAD.Windows;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SuCri.Modul2.UI.Acad.Views
{
    /// <summary>
    /// Interaction logic for SampleUI.xaml
    /// </summary>
    public partial class SampleUI : System.Windows.Window
    {
        public SampleUI()
        {
            InitializeComponent();
            ColorAdjustToggle.RaiseEvent(new RoutedEventArgs(ToggleButton.CheckedEvent));
        }

        private void ToggleButton_ChangeChecked(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;
            Properties.Settings.Default.IsDarkMode = toggle.IsChecked==true;
           
            //var bundleTheme = Resources.MergedDictionaries[0].MergedDictionaries[0] as BundledTheme;
            //bundleTheme.BaseTheme = Properties.Settings.Default.IsDarkMode ? BaseTheme.Dark : BaseTheme.Light;


            var myResourceDictionary = Resources.MergedDictionaries[0];
            ITheme theme = ResourceDictionaryExtensions.GetTheme(myResourceDictionary);

            theme.SetBaseTheme(Properties.Settings.Default.IsDarkMode ? Theme.Dark : Theme.Light);

            ResourceDictionaryExtensions.SetTheme(myResourceDictionary, theme);
        }
  
        private void ChangeColor(object sender, RoutedEventArgs e)
        {
        
            var button = sender as Button;
            var background = button.Background.ToString();
            var myResourceDictionary = Resources.MergedDictionaries[0];
            ITheme theme = ResourceDictionaryExtensions.GetTheme(myResourceDictionary);
            /*theme.SetPrimaryColor((Color)ColorConverter.ConvertFromString(background));*/
            theme.SetPrimaryColor((Color)ColorConverter.ConvertFromString(background));
            ResourceDictionaryExtensions.SetTheme(myResourceDictionary, theme);
        }

        private void ColorAdjust(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;
            var check = toggle.IsChecked == true;
            var myResourceDictionary = Resources.MergedDictionaries[0];
            ITheme theme = ResourceDictionaryExtensions.GetTheme(myResourceDictionary);
            Theme internalTheme = theme as Theme;
            if (check)
            {
                internalTheme.ColorAdjustment = new ColorAdjustment
                {
                    DesiredContrastRatio = 4.5f,
                    Contrast = Contrast.Medium,
                    Colors = MaterialDesignThemes.Wpf.ColorSelection.All
                };
            }
            else { 
                internalTheme.ColorAdjustment = null; 
            };
           
            ResourceDictionaryExtensions.SetTheme(myResourceDictionary, theme);
            
        }
    }
}
