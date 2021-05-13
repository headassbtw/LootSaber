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
        public static void SaveJson(PlayerData data, string path)
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
