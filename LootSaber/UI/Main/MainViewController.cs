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
using LootSaber.UI.AssetPreviews;

namespace LootSaber.UI.ViewControllers
{
    class MainViewController : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.Main.MainView.bsml";

        internal static DownloadRequestResponse Item1Request = new DownloadRequestResponse();
        internal static DownloadRequestResponse Item2Request = new DownloadRequestResponse();
        internal static DownloadRequestResponse Item3Request = new DownloadRequestResponse();

        [UIAction("go-back")]
        internal void ReMenu()
        {
            BaseGameUiHandler.Instance.PresentGameUI();
            yeetem();
            Asset_Viewing.FloatingUI.unmiddle();
            Asset_Viewing.FloatingUI.unPreviews();
        }

        [UIAction("download-button")]
        internal void DLButton()
        {
            yeetem();
            var rnd = new Random();
            LeftPreviewViewController.Download();
            Task.Delay(51);
            MiddlePreviewViewController.Download();
            Task.Delay(43);
            RightPreviewViewController.Download();
            Task.Delay(27);
        }
    }
}
