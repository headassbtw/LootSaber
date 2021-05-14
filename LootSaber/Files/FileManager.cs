using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootSaber.Files
{
    class FileManager
    {
        internal static string AssetCache = Path.Combine(UnityGame.UserDataPath, "LootSaber", "Asset Cache");
    }
}
