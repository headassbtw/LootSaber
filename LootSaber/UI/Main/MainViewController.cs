using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using IPA.Utilities;
using TMPro;
using static LootSaber.CustomTypes;
using static LootSaber.Files.FileManager;

namespace LootSaber.UI.ViewControllers
{
    class MainViewController : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.Main.MainView.bsml";
        [UIComponent("1rdpanelmid")] private TextMeshProUGUI tm1 = new TextMeshProUGUI();
        [UIComponent("2rdpanelmid")] private TextMeshProUGUI tm2 = new TextMeshProUGUI();
        [UIComponent("3rdpanelmid")] private TextMeshProUGUI tm3 = new TextMeshProUGUI();
        [UIComponent("1rdpanedtop")] private TextMeshProUGUI pt1 = new TextMeshProUGUI();
        [UIComponent("2rdpaneltop")] private TextMeshProUGUI pt2 = new TextMeshProUGUI();
        [UIComponent("3rdpaneltop")] private TextMeshProUGUI pt3 = new TextMeshProUGUI();
        [UIComponent("1rdpanelbot")] private TextMeshProUGUI pb1 = new TextMeshProUGUI();
        [UIComponent("2rdpanelbot")] private TextMeshProUGUI pb2 = new TextMeshProUGUI();
        [UIComponent("3rdpanelbot")] private TextMeshProUGUI pb3 = new TextMeshProUGUI();
        [UIAction("download-button")]
        void DLButton()
        {
            DownloadAsset(assetDB, new Random(4352), 1);
            Task.Delay(51);
            DownloadAsset(assetDB, new Random(6745), 2);
            Task.Delay(43);
            DownloadAsset(assetDB, new Random(23456), 3);
            Task.Delay(27);
        }
        internal void setpanel(int location, int panel, string text)
        {
            if (location.Equals(1) && panel.Equals(1))
                pt1.text = text;
            if (location.Equals(1) && panel.Equals(2))
                pt2.text = text;
            if (location.Equals(1) && panel.Equals(3))
                pt3.text = text;
            if (location.Equals(2) && panel.Equals(1))
                tm1.text = text;
            if (location.Equals(2) && panel.Equals(2))
                tm2.text = text;
            if (location.Equals(2) && panel.Equals(3))
                tm3.text = text;
            if (location.Equals(3) && panel.Equals(1))
                pb1.text = text;
            if (location.Equals(3) && panel.Equals(2))
                pb2.text = text;
            if (location.Equals(3) && panel.Equals(3))
                pb3.text = text;
        }

        internal async void DownloadAsset(DownloadsDatabase db, Random rng, int slot)
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
                    setpanel(3, slot, "Saber");
                    Plugin.Log.Info("Rolled Saber");
                    break;
                case 1:
                    rolledType = rolledRarity.Notes;
                    amountOfRolledType = rolledType.Count;
                    setpanel(3, slot, "Note");
                    Plugin.Log.Info("Rolled Note");
                    break;
                case 2:
                    rolledType = rolledRarity.Platforms;
                    amountOfRolledType = rolledType.Count;
                    setpanel(3, slot, "Platform");
                    Plugin.Log.Info("Rolled Platform");
                    break;
                case 3:
                    rolledType = rolledRarity.MenuFonts;
                    amountOfRolledType = rolledType.Count;
                    setpanel(3, slot, "Menu Font");
                    Plugin.Log.Info("Rolled Menu Font");
                    break;
            }
            var URL = rolledType.ElementAt(rng.Next(0, amountOfRolledType));

            var client = new WebClient();
            string _realname = URL.Substring(URL.LastIndexOf("/")).Replace("%20", " ");
            if (_realname.Contains("?"))
                _realname = _realname.Substring(0, _realname.IndexOf("?"));
            switch (slot)
            {
                case 1:
                    client.DownloadProgressChanged += wc_progChange1;
                    break;
                case 2:
                    client.DownloadProgressChanged += wc_progChange2;
                    break;
                case 3:
                    client.DownloadProgressChanged += wc_progChange3;
                    break;
            }
            string folder = "";
            switch (a)
            {
                case 0:
                    folder = "CustomSabers";
                    break;
                case 1:
                    folder = "CustomNotes";
                    break;
                case 2:
                    folder = "CustomPlatforms";
                    break;
                case 3:
                    folder = "UserData\\CustomMenuText\\Fonts";
                    break;
            }
            try {
                client.DownloadFileAsync(
                 new Uri(URL),
                 UnityGame.InstallPath + "\\" + folder + "\\" + _realname
                );
            }
            catch (Exception)
            {
                //if the folder doesn't exist, create it and try again
                Directory.CreateDirectory(UnityGame.InstallPath + "\\" + folder);
                client.DownloadFileAsync(
                 new Uri(URL),
                 UnityGame.InstallPath + "\\" + folder + "\\" + _realname
                );
            }
        }
        void wc_progChange1(object sender, DownloadProgressChangedEventArgs e)
        {
            tm1.text = e.ProgressPercentage.ToString() + "%";
            Plugin.Log.Debug("DL progress: " + e.ProgressPercentage.ToString() + "%");
        }
        void wc_progChange3(object sender, DownloadProgressChangedEventArgs e)
        {
            tm3.text = e.ProgressPercentage.ToString() + "%";
            Plugin.Log.Debug("DL progress: " + e.ProgressPercentage.ToString() + "%");
        }
        void wc_progChange2(object sender, DownloadProgressChangedEventArgs e)
        {
            tm2.text = e.ProgressPercentage.ToString() + "%";
            Plugin.Log.Debug("DL progress: " + e.ProgressPercentage.ToString() + "%");
        }
    }
}
