using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using Button = UnityEngine.UI.Button;
using static LootSaber.CustomTypes;
using static LootSaber.Files.FileManager;
using static LootSaber.UI.Asset_Viewing.AssetInstantiatePreviewing;
using System.IO;
using IPA.Utilities;

namespace LootSaber.UI.AssetPreviews
{
    class RightPreviewViewController : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.AssetPreviews.AssetPanel.bsml";
        public static RightPreviewViewController Instance;
        [UIComponent("paneltop")] internal TextMeshProUGUI topPanel = new TextMeshProUGUI();
        [UIComponent("panelmid")] internal TextMeshProUGUI middlePanel = new TextMeshProUGUI();
        [UIComponent("panelbot")] internal TextMeshProUGUI bottomPanel = new TextMeshProUGUI();
        [UIComponent("accept-button")] internal Button acceptButton;
        internal static DownloadRequestResponse downloadRequest = new DownloadRequestResponse();

        internal static void Download()
        {
            Instance.DL();
        }

        internal void DL()
        {
            acceptButton.interactable = false;
            var rnd = new Random();
            downloadRequest = DownloadAsset(assetDB);
            downloadRequest.client.DownloadProgressChanged += wc_progChange;
            downloadRequest.client.DownloadFileCompleted += wc_complete;
        }
        void wc_progChange(object sender, DownloadProgressChangedEventArgs e)
        {
            Instance.middlePanel.text = e.ProgressPercentage.ToString() + "%";
        }
        void wc_complete(object sender, AsyncCompletedEventArgs e)
        {
            Instance.topPanel.text = "Tier " + downloadRequest.tier.ToString();
            Instance.middlePanel.text = downloadRequest.assetType;
            Instance.bottomPanel.text = downloadRequest.filePath.Substring(downloadRequest.filePath.LastIndexOf("\\") + 1);
            ShowPreviewAsset(downloadRequest, 3);
            acceptButton.interactable = true;
        }

        internal static void StaticPP()
        {
            RightPreviewViewController lpv = new RightPreviewViewController();
            lpv.PostParse();
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            Instance = this;
        }

        [UIAction("accept-asset")]
        internal void AcceptAsset()
        {
            File.Copy(downloadRequest.filePath, UnityGame.InstallPath + "\\" + downloadRequest.filePath.Substring(downloadRequest.filePath.IndexOf("Asset Cache\\") + 12));
        }
    }
}
