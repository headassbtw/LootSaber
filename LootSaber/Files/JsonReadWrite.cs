using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static LootSaber.CustomTypes;

namespace LootSaber.Files
{
    class JsonReadWrite
    {
        public static void DownloadAssetDatabase()
        {
            var sw = new Stopwatch();
            sw.Start();
            string json = new WebClient().DownloadString("https://raw.githubusercontent.com/headassbtw/LootSaber/master/AssetDatabase.json");
            sw.Stop();
            Plugin.Log.Info("Database downloaded in " + sw.ElapsedMilliseconds + "ms");
            DownloadsDatabase items = JsonConvert.DeserializeObject<DownloadsDatabase>(json);
            FileManager.assetDB = items;
        }
        public static CustomTypes.PlayerData LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                CustomTypes.PlayerData items = JsonConvert.DeserializeObject<CustomTypes.PlayerData>(json);
                r.Close();
                return items;
            }
        }
        public static void SaveJson2(string path)
        {
            var bruh = new List<string> { "", "" };
            var bruh2 = new Rarity(bruh, bruh, bruh, bruh);
            var bruh3 = new DownloadsDatabase(bruh2, bruh2, bruh2, bruh2, bruh2);
            using (StreamWriter w = new StreamWriter(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                string contents = JsonConvert.SerializeObject(bruh3);
                var jtw = new JsonTextWriter(w);
                jtw.Formatting = Formatting.Indented;
                serializer.Serialize(jtw, bruh3);
                w.Close();
            }
        }
        public static void SaveJson(CustomTypes.PlayerData data, string path)
        {
            using (StreamWriter w = new StreamWriter(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                string contents = JsonConvert.SerializeObject(data);
                var jtw = new JsonTextWriter(w);
                jtw.Formatting = Formatting.Indented;
                serializer.Serialize(jtw, data);
                w.Close();
            }
        }

    }
}
