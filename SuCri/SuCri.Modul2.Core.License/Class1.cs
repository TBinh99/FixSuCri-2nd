using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SKM.V3;
using SKM.V3.Methods;
using SKM.V3.Models;
using Autodesk.AutoCAD.DatabaseServices;
using Microsoft.Win32;

namespace SuCri.Modul2.Core.License
{
    public class Class1
    {
        public bool ValidateFloatingLicense()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string auth = "WyI1ODQ2NDE4MCIsInpRZUhDTm9HK3JKVjg5eU9scVVCV0pXMlVieE9TY3d6V2M0QXQ2TW0iXQ==";
            const string registryKeyPath = @".DEFAULT\Environment";
            const string valueNameKey = "SuCriModule4Extension";
            string licenseKey = "";
            using (var registryKey = Registry.Users.OpenSubKey(registryKeyPath))
            {
                if (registryKey != null)
                {
                    licenseKey = registryKey.GetValue(valueNameKey) as string;
                }
            }
            if (licenseKey != "")
            {

            }
            else
            {
                string input = "";
                //ShowInputDialog(ref input);
                licenseKey = input;
            }
            var result = Key.Activate(token: auth, parameters: new ActivateModel() { Key = licenseKey, ProductId = 21579, Sign = true, MachineCode = Helpers.GetMachineCode(), FloatingTimeInterval = 600, MaxOverdraft = 1 });
            if (Helpers.IsOnRightMachine(result.LicenseKey, isFloatingLicense: true, allowOverdraft: true))
            {
                ed.WriteMessage("License valid"); return true;
            }
            else
            {
                ed.WriteMessage("License invalid"); return false;
            }
        }
        public static bool KeyVerification(string licenseKey, string machineCode)
        {

            var RSAPubKey = "<RSA public key>";

            var auth = "WyI1ODQ2NDE4MCIsInpRZUhDTm9HK3JKVjg5eU9scVVCV0pXMlVieE9TY3d6V2M0QXQ2TW0iXQ==";
            var result = Key.Activate(token: auth, parameters: new ActivateModel()
            {
                Key = licenseKey,
                ProductId = 3349,
                Sign = true,
                MachineCode = machineCode
            });

            if (result == null || result.Result == ResultType.Error ||
                !result.LicenseKey.HasValidSignature(RSAPubKey).IsValid())
            {
                // an error occurred or the key is invalid or it cannot be activated
                // (eg. the limit of activated devices was achieved)
                Console.WriteLine("The license does not work.");
                return false;
            }
            else
            {
                // everything went fine if we are here!
                Console.WriteLine("The license is valid!");
                return true;
            }
        }
    }
}
