using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using System.Reflection;
using LootSaber.UI;
using Conf = IPA.Config.Config;
using IPA.Loader;

namespace LootSaber
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony harmony;

        internal static string dataFilePath = Path.Combine(UnityGame.UserDataPath, "LootSaber") + "\\playerdata";




        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        /// 
        public Plugin(IPALogger log)
        {
            Instance = this;
            Log = log;
            harmony = new Harmony("com.headassbtw.lootsaber");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            

            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            if (!Directory.Exists(Path.Combine(UnityGame.UserDataPath, "LootSaber")))
                Directory.CreateDirectory(Path.Combine(UnityGame.UserDataPath, "LootSaber"));
            Files.FileManager.CreateFolders();
            if (!File.Exists(dataFilePath))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("LootSaber.Data.DefaultPlayerData.txt"))
                {
                    using (var file = new FileStream(dataFilePath, FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
            Data.Player.Load();
            Files.JsonReadWrite.DownloadAssetDatabase();
            //Files.JsonReadWrite.SaveJson2(Path.Combine(UnityGame.UserDataPath, "LootSaber") + "\\yes.json");
            new GameObject("LootSaberController").AddComponent<LootSaberController>();
            AssetModDetection.DetectAssetMods();

        }
        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (arg1.name.Contains("Menu")) // Only run in menu scene
            {
                if(!UI.XP.XPScreen.Instance)
                    UI.XP.XPScreenStarter.yeet();
            }
        }
        [OnExit]
        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            Data.Player.Save();
            Directory.Delete(Files.FileManager.AssetCache);
        }
    }
}
