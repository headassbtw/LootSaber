using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static LootSaber.CustomTypes;
namespace LootSaber.Files
{
    class FileManager
    {
        internal static string AssetCache = Path.Combine(UnityGame.UserDataPath, "LootSaber", "Asset Cache");
        internal static DownloadsDatabase assetDB;
        //rolls a few dice to give you an asset
        internal static string DiceRoll(DownloadsDatabase db)
        {
            var rnd = new Random();
            Rarity rolledRarity = new Rarity();
            List<String> rolledType = new List<string>();
            int amountOfRolledType = 0;
            switch (rnd.Next(0, 4))
            {
                case 0:
                    rolledRarity = db.Tier1;
                    Plugin.Log.Info("Rolled Tier 1");
                    break;
                case 1:
                    rolledRarity = db.Tier2;
                    Plugin.Log.Info("Rolled Tier 2");
                    break;
                case 2:
                    rolledRarity = db.Tier3;
                    Plugin.Log.Info("Rolled Tier 3");
                    break;
                case 3:
                    rolledRarity = db.Tier4;
                    Plugin.Log.Info("Rolled Tier 4");
                    break;
                case 4:
                    rolledRarity = db.Tier5;
                    Plugin.Log.Info("Rolled Tier 5");
                    break;
            }
            switch (rnd.Next(0, 3))
            {
                case 0:
                    rolledType = rolledRarity.Sabers;
                    amountOfRolledType = rolledType.Count;
                    Plugin.Log.Info("Rolled Saber");
                    break;
                case 1:
                    rolledType = rolledRarity.Notes;
                    amountOfRolledType = rolledType.Count;
                    Plugin.Log.Info("Rolled Note");
                    break;
                case 2:
                    rolledType = rolledRarity.Platforms;
                    amountOfRolledType = rolledType.Count;
                    Plugin.Log.Info("Rolled Platform");
                    break;
                case 3:
                    rolledType = rolledRarity.MenuFonts;
                    amountOfRolledType = rolledType.Count;
                    Plugin.Log.Info("Rolled Menu Font");
                    break;
            }
            return rolledType.ElementAt(rnd.Next(0, amountOfRolledType));
        }

        internal static void DownloadAsset(string URL)
        {
            var client = new WebClient();
            string _realname = URL.Substring(URL.LastIndexOf("/")).Replace("%20", " ");

            client.DownloadFile(URL, AssetCache + "\\" + _realname);
        }

    }
}
