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
using static LootSaber.UI.Asset_Viewing.FloatingUI;
using LootSaber.UI.XP;
using LootSaber.UI.AssetPreviews;
using UnityEngine;
using LootSaber.UI.Asset_Viewing;
using UnityEngine.UI;

namespace LootSaber.UI.ViewControllers
{
    class MainViewController : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.Main.MainView.bsml";

        internal static DownloadRequestResponse Item1Request = new DownloadRequestResponse();
        internal static DownloadRequestResponse Item2Request = new DownloadRequestResponse();
        internal static DownloadRequestResponse Item3Request = new DownloadRequestResponse();

        [UIComponent("dl-button")] internal Button DlButton;
        [UIValue("pendingNumber")]
        internal string PendingNumber
        {
            get => Data.Player.currentData.PendingBoxes.ToString();
            set
            {
                Data.Player.currentData.PendingBoxes = Int32.Parse(value);
            }
        }

        [UIAction("go-back")]
        internal void ReMenu()
        {
            if (XPScreen.uh)
                XPScreen.Instance.blueUIButton.enabled = true;
            if (!XPScreen.uh)
                XPScreen.Instance.greyUIButton.enabled = true;

            BaseGameUiHandler.Instance.PresentGameUI();
            yeetem();
            FloatingUI.unmiddle();
            FloatingUI.unPreviews();
        }

        [UIAction("download-button")]
        internal void DLButton()
        {
            yeetem();
            var rnd = new System.Random();
            FloatingUI.PLS.transform.SetPositionAndRotation(LeftP, Quaternion.Euler(30f, 0, 0));
            LeftPreviewViewController.Download();
            Task.Delay(51);
            FloatingUI.PMS.transform.SetPositionAndRotation(MiddleP, Quaternion.Euler(30f, 0, 0));
            MiddlePreviewViewController.Download();
            Task.Delay(43);
            FloatingUI.PRS.transform.SetPositionAndRotation(RightP, Quaternion.Euler(30f, 00, 0));
            RightPreviewViewController.Download();
            Task.Delay(27);
        }
    }
}
