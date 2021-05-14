using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LootSaber.CustomTypes;

namespace LootSaber.Files
{
    class JsonReadWrite
    {
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
