using System.Collections.Generic;

namespace SuCri.Modul2.UI.Acad.Utils
{
    internal readonly struct Language
    {
        public static readonly Dictionary<string, string> Languages = new Dictionary<string, string>
{
    { English, "en-US" },
    { German, "de-DE" }
};
        public const string English = "English";
        public const string German = "German";
    }
}
