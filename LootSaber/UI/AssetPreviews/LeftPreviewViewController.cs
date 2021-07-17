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
using HMUI;
using Button = UnityEngine.UI.Button;
using static LootSaber.CustomTypes;
using static LootSaber.Files.FileManager;
using static LootSaber.UI.Asset_Viewing.AssetInstantiatePreviewing;
using System.IO;
using IPA.Utilities;
using LootSaber.ModelSaber;

namespace LootSaber.UI.AssetPreviews
{
    class LeftPreviewViewController : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.AssetPreviews.AssetPanel.bsml";
        public static LeftPreviewViewController Instance;
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
        }

        internal static void StaticPP()
        {
            LeftPreviewViewController lpv = new LeftPreviewViewController();
            lpv.PostParse();
        }

        internal static void Clear()
        {
            Instance.topPanel.text = "";
            Instance.middlePanel.text = "";
            Instance.bottomPanel.text = "";
        }


        [UIAction("#post-parse")]
        internal void PostParse()
        {
            Instance = this;
        }

        [UIAction("accept-asset")]
        internal void AcceptAsset()
        {
            
        }
    }
}
