using Autodesk.AutoCAD.Windows;
using SuCri.Modul2.UI.Acad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using System.Windows.Forms;

namespace SuCri.Modul2.Addin
{
    internal class PaletteSingleton
    {
        public static string Theme { get; set; }
        public PaletteSet paletteSet = null;
        public PaletteSingleton()
        {
            // Create the palette set
            paletteSet = new PaletteSet("Sikla Palette");
            paletteSet.Size = new System.Drawing.Size(400, 600);

            paletteSet.DockEnabled = (DockSides)((int)DockSides.Left + (int)DockSides.Right);

            MainView mainView = new MainView();
            mainView.Theme = "Sikla";

            ElementHost host = new ElementHost();

            host.AutoSize = true;
            host.Dock = DockStyle.Fill;
            host.Child = mainView;

            paletteSet.Add("AddVisual", host);
        }

        private static PaletteSingleton _instance;

        public static PaletteSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaletteSingleton();
                }
                return _instance;
            }
        }
        public static void Show(string theme)
        {
            Theme = theme;
            if (Instance.paletteSet.Visible)
            {
                Instance.paletteSet.Visible = false;
                Instance.paletteSet.KeepFocus = false;
            }
            else
            {
                Instance.paletteSet.Visible = true;
                Instance.paletteSet.KeepFocus = true;
            }
        }
        public static void Hide() 
        {
            if(_instance != null) 
            {
                if (Instance.paletteSet.Visible)
                {
                    Instance.paletteSet.Visible = false;
                    Instance.paletteSet.KeepFocus = false;
                }
            }
        }
    }
}
