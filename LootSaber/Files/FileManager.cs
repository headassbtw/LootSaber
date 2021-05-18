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
        internal static int rollcat1;

        internal static void CreateFolders()
        {
            if (!Directory.Exists(AssetCache))
                Directory.CreateDirectory(AssetCache);
            if (!Directory.Exists(Path.Combine(AssetCache, "CustomSabers")))
                Directory.CreateDirectory(Path.Combine(AssetCache, "CustomSabers"));
            if (!Directory.Exists(Path.Combine(AssetCache, "CustomNotes")))
                Directory.CreateDirectory(Path.Combine(AssetCache, "CustomNotes"));
            if (!Directory.Exists(Path.Combine(AssetCache, "CustomPlatforms")))
                Directory.CreateDirectory(Path.Combine(AssetCache, "CustomPlatforms"));
            if (!Directory.Exists(Path.Combine(AssetCache, "Fonts")))
                Directory.CreateDirectory(Path.Combine(AssetCache, "Fonts"));
        }

        internal static DownloadRequestResponse DownloadAsset(DownloadsDatabase db, Random rng)
        {
            DownloadRequestResponse resp = new DownloadRequestResponse();

            Rarity rolledRarity = new Rarity();
            List<String> rolledType = new List<string>();
            int amountOfRolledType = 0;
            switch (rng.Next(0, 5))
            {
                case 0:
                    rolledRarity = db.Tier1;
                    Plugin.Log.Info("Rolled Tier 1");
                    resp.tier = 1;
                    break;
                case 1:
                    rolledRarity = db.Tier2;
                    Plugin.Log.Info("Rolled Tier 2");
                    resp.tier = 2;
                    break;
                case 2:
                    rolledRarity = db.Tier3;
                    Plugin.Log.Info("Rolled Tier 3");
                    resp.tier = 3;
                    break;
                case 3:
                    rolledRarity = db.Tier4;
                    Plugin.Log.Info("Rolled Tier 4");
                    resp.tier = 4;
                    break;
                case 4:
                    rolledRarity = db.Tier5;
                    Plugin.Log.Info("Rolled Tier 5");
                    resp.tier = 5;
                    break;
            }
            string folder = "";
            int maxtype = 4;
            if (!AssetModDetection.MenuFonts)
                maxtype = 3;
            var a = rng.Next(0, maxtype);
            if (a.Equals(rollcat1))
            {
                rng = new Random(3294704);
                a = rng.Next(0, maxtype);
            }
            rollcat1 = a;
            switch (a)
            {
                case 0:
                    rolledType = rolledRarity.Sabers;
                    amountOfRolledType = rolledType.Count;
                    folder = "UserData\\LootSaber\\Asset Cache\\CustomSabers";
                    Plugin.Log.Info("Rolled Saber");
                    resp.assetType = "Saber";
                    break;
                case 1:
                    rolledType = rolledRarity.Notes;
                    amountOfRolledType = rolledType.Count;
                    folder = "UserData\\LootSaber\\Asset Cache\\CustomNotes";
                    Plugin.Log.Info("Rolled Note");
                    resp.assetType = "Note";
                    break;
                case 2:
                    rolledType = rolledRarity.Platforms;
                    amountOfRolledType = rolledType.Count;
                    folder = "UserData\\LootSaber\\Asset Cache\\CustomPlatforms";
                    Plugin.Log.Info("Rolled Platform");
                    resp.assetType = "Platform";
                    break;
                case 3:
                    rolledType = rolledRarity.MenuFonts;
                    amountOfRolledType = rolledType.Count;
                    folder = "UserData\\LootSaber\\Asset Cache\\Fonts";
                    Plugin.Log.Info("Rolled Menu Font");
                    resp.assetType = "Menu Text Font";
                    break;
            }
            var URL = rolledType.ElementAt(rng.Next(0, amountOfRolledType));

            var client = new WebClient();
            string _realname = URL.Substring(URL.LastIndexOf("/") + 1).Replace("%20", " ");
            if (_realname.Contains("?"))
                _realname = _realname.Substring(0, _realname.IndexOf("?"));
            

            string fileSavePath = UnityGame.InstallPath + "\\" + folder + "\\" + _realname;
            try
            {
                Plugin.Log.Notice("Downloading " + _realname + " From " + URL);
                client.DownloadFileAsync(
                 new Uri(URL),
                 fileSavePath
                );
                resp.client = client;
            }
            catch (Exception)
            {
                //if the folder doesn't exist, create it and try again
                Directory.CreateDirectory(UnityGame.InstallPath + "\\" + folder);
                client.DownloadFileAsync(
                 new Uri(URL),
                 fileSavePath
                );
                resp.client = client;
            }
            resp.filePath = fileSavePath;
            return resp;
        }



        //rolls a few dice to give you an asset
        internal static string DiceRoll(DownloadsDatabase db, Random rng)
        {
            Rarity rolledRarity = new Rarity();
            List<String> rolledType = new List<string>();
            int amountOfRolledType = 0;
            switch (rng.Next(0, 5))
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
            var a = rng.Next(0, 4);
            if (a.Equals(rollcat1))
            {
                rng = new Random(3294704);
                a = rng.Next(0, 4);
            }
            rollcat1 = a;
            switch (a)
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
            return rolledType.ElementAt(rng.Next(0, amountOfRolledType));
        }

        

    }
}
