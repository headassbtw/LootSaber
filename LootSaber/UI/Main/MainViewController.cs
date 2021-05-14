using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using static LootSaber.Files.FileManager;

namespace LootSaber.UI.ViewControllers
{
    class MainViewController : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.Main.MainView.bsml";
        [UIComponent("1rdpanelmid")] private TextMeshProUGUI tm1 = new TextMeshProUGUI();
        [UIComponent("2rdpanelmid")] private TextMeshProUGUI tm2 = new TextMeshProUGUI();
        [UIComponent("3rdpanelmid")] private TextMeshProUGUI tm3 = new TextMeshProUGUI();
        [UIAction("download-button")]
        void DLButton()
        {
            DownloadAsset(DiceRoll(assetDB, new Random(4352)), 1);
            Task.Delay(51);
            DownloadAsset(DiceRoll(assetDB, new Random(6745)), 2);
            Task.Delay(43);
            DownloadAsset(DiceRoll(assetDB, new Random(23456)), 3);
            Task.Delay(27);
        }


        internal async void DownloadAsset(string URL, int slot)
        {
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
            client.DownloadFileAsync(
                   new Uri(URL),
                   AssetCache + "\\" + _realname
            );
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
