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
            currentData = data;
        }
        internal static void Load()
        {
            currentData = Files.JsonReadWrite.LoadJson(Plugin.dataFilePath);
        }
        internal static void Save()
        {
            Files.JsonReadWrite.SaveJson(currentData, Plugin.dataFilePath);
        }
    }
}
