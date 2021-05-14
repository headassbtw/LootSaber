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

        DownloadRequestResponse Item1Request = new DownloadRequestResponse();
        DownloadRequestResponse Item2Request = new DownloadRequestResponse();
        DownloadRequestResponse Item3Request = new DownloadRequestResponse();

        [UIAction("download-button")]
        void DLButton()
        {
            Item1Request = DownloadAsset(assetDB, new Random(4352));
            Item1Request.downloadProgress += wc_progChange1;
            Item1Request.downloadComplete += wc_complete1;
            Task.Delay(51);
            Item2Request = DownloadAsset(assetDB, new Random(6745));
            Item2Request.downloadProgress += wc_progChange2;
            Item2Request.downloadComplete += wc_complete2;
            Task.Delay(43);
            Item3Request = DownloadAsset(assetDB, new Random(23456));
            Item3Request.downloadProgress += wc_progChange3;
            Item3Request.downloadComplete += wc_complete3;
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
        void wc_complete1(object sender, DownloadDataCompletedEventArgs e)
        {
            setpanel(3, 1, "Tier " + Item1Request.tier.ToString() + " " + Item1Request.assetType);
        }
        void wc_complete2(object sender, DownloadDataCompletedEventArgs e)
        {
            setpanel(3, 2, "Tier " + Item2Request.tier.ToString() + " " + Item2Request.assetType);
        }
        void wc_complete3(object sender, DownloadDataCompletedEventArgs e)
        {
            setpanel(3, 3, "Tier " + Item3Request.tier.ToString() + " " + Item3Request.assetType);
        }
    }
}
