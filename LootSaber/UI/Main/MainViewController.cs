using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static LootSaber.UI.Asset_Viewing.AssetInstantiatePreviewing;

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

        internal static DownloadRequestResponse Item1Request = new DownloadRequestResponse();
        internal static DownloadRequestResponse Item2Request = new DownloadRequestResponse();
        internal static DownloadRequestResponse Item3Request = new DownloadRequestResponse();

        [UIAction("go-back")]
        internal void ReMenu()
        {
            BaseGameUiHandler.Instance.PresentGameUI();
            yeetem();
            UI.Asset_Viewing.FloatingUI.unmiddle();
        }

        [UIAction("download-button")]
        internal void DLButton()
        {
            yeetem();
            var rnd = new Random();
            Item1Request = DownloadAsset(assetDB, new Random(rnd.Next()));
            Item1Request.client.DownloadProgressChanged += wc_progChange1;
            Item1Request.client.DownloadFileCompleted += wc_complete1;
            Task.Delay(51);
            Item2Request = DownloadAsset(assetDB, new Random(rnd.Next()));
            Item2Request.client.DownloadProgressChanged += wc_progChange2;
            Item2Request.client.DownloadFileCompleted += wc_complete2;
            Task.Delay(43);
            Item3Request = DownloadAsset(assetDB, new Random(rnd.Next()));
            Item3Request.client.DownloadProgressChanged += wc_progChange3;
            Item3Request.client.DownloadFileCompleted += wc_complete3;
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
            setpanel(2, 1, e.ProgressPercentage.ToString() + "%");
            Plugin.Log.Debug("DL progress: " + e.ProgressPercentage.ToString() + "%");
        }
        void wc_progChange3(object sender, DownloadProgressChangedEventArgs e)
        {
            setpanel(2, 3, e.ProgressPercentage.ToString() + "%");
            Plugin.Log.Debug("DL progress: " + e.ProgressPercentage.ToString() + "%");
        }
        void wc_progChange2(object sender, DownloadProgressChangedEventArgs e)
        {
            setpanel(2, 2, e.ProgressPercentage.ToString() + "%");
        }
        void wc_complete1(object sender, AsyncCompletedEventArgs e)
        {
            setpanel(1, 1, "Tier " + Item1Request.tier.ToString());
            setpanel(2, 1, Item1Request.assetType);
            ShowPreviewAsset(Item1Request, 1);
            if (e.Cancelled)
                setpanel(3, 1, "Cancelled");
        }
        void wc_complete2(object sender, AsyncCompletedEventArgs e)
        {
            setpanel(1, 2, "Tier " + Item2Request.tier.ToString());
            setpanel(2, 2, Item2Request.assetType);
            ShowPreviewAsset(Item2Request, 2);
            if (e.Cancelled)
                setpanel(3, 2, "Cancelled");
        }
        void wc_complete3(object sender, AsyncCompletedEventArgs e)
        {
            setpanel(1, 3, "Tier " + Item3Request.tier.ToString());
            setpanel(2, 3, Item3Request.assetType);
            ShowPreviewAsset(Item3Request, 3);
            if (e.Cancelled)
                setpanel(3, 3, "Cancelled");
        }
    }
}
