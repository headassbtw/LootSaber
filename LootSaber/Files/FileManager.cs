using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    break;
                case 1:
                    rolledRarity = db.Tier2;
                    break;
                case 2:
                    rolledRarity = db.Tier3;
                    break;
                case 3:
                    rolledRarity = db.Tier4;
                    break;
                case 4:
                    rolledRarity = db.Tier5;
                    break;
            }
            switch (rnd.Next(0, 3))
            {
                case 0:
                    rolledType = rolledRarity.Sabers;
                    amountOfRolledType = rolledType.Count;
                    break;
                case 1:
                    rolledType = rolledRarity.Notes;
                    amountOfRolledType = rolledType.Count;
                    break;
                case 2:
                    rolledType = rolledRarity.Platforms;
                    amountOfRolledType = rolledType.Count;
                    break;
                case 3:
                    rolledType = rolledRarity.MenuFonts;
                    amountOfRolledType = rolledType.Count;
                    break;
            }
            return rolledType.ElementAt(rnd.Next(1, amountOfRolledType));
        }
    }
}
