using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootSaber.Data
{
    class Player
    {
        internal static CustomTypes.PlayerData currentData;

        internal static void FirstTime()
        {
            CustomTypes.PlayerData data = new CustomTypes.PlayerData();
            data.Coins = 2;
            currentData = data;
        }
        internal static void Load()
        {
            Files.JsonReadWrite.LoadJson(Plugin.dataFilePath);
        }
        internal static void Save()
        {
            Files.JsonReadWrite.SaveJson(currentData, Plugin.dataFilePath);
        }
    }
}
