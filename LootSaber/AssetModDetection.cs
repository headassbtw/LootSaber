using IPA.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootSaber
{
    class AssetModDetection
    {
        public static bool Notes;
        public static bool Sabers;
        public static bool Platforms;
        public static bool MenuFonts;

        internal static void DetectAssetMods()
        {
            try
            {
                try
                {
                    PluginManager.GetPlugin("CustomSaber");
                    Sabers = true;
                }
                catch (Exception)
                {
                    PluginManager.GetPlugin("SaberFactory");
                    Sabers = true;
                }
            }
            catch (Exception) { Sabers = false; }

            try
            {
                PluginManager.GetPlugin("CustomNotes");
                Notes = true;
            }
            catch (Exception)
            {
                Notes = false;
            }

            try
            {
                PluginManager.GetPlugin("CustomPlatforms");
                Platforms = true;
            }
            catch (Exception)
            {
                Platforms = false;
            }

            try
            {
                PluginManager.GetPlugin("CustomMenuText");
                MenuFonts = true;
            }
            catch (Exception)
            {
                MenuFonts = false;
            }
        }

    }
}
