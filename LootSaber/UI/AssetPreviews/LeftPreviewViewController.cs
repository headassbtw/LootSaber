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
using System.Threading;
using IPA.Utilities;
using LootSaber.ModelSaber;
using LootSaber.UI.Asset_Viewing;
using static LootSaber.Files.Downloading;

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

        private List<ModelSaberEntry> ets;
        internal async void GetPage(ModelSaberSearch search)
        {
            var pp = ModelSaberUtils.GetPage(search);
            while (!pp.IsCompleted)
            {
                Thread.Sleep(1);
            }

            ets = pp.Result;
            Plugin.Log.Notice($"ay yuh {pp.Result.Count} count");
            for (int i = 0; i < pp.Result.Count; i++)
            {
                

            Console.WriteLine($"Item {i} is a {pp.Result[i].Type} with the name of {pp.Result[i].Name}");
                
            }
            UnityEngine.Random.InitState(DateTime.UtcNow.Second);
            
            var model = pp.Result[UnityEngine.Random.Range(0, pp.Result.Count)];
            //if (!CheckIfModelInstalled(model))
            {
                AssetInstantiatePreviewing.ShowPreviewAsset(model, 1);
            }
        }
        
        
        internal void DL()
        {
            acceptButton.interactable = false;
            UnityEngine.Random.InitState(DateTime.UtcNow.Second);
            int rng = UnityEngine.Random.Range(0, 40);

            var thread = new Thread(async => GetPage(new ModelSaberSearch(page:rng)));
            thread.Start();
            //this is where you do shit, i thinK???>?
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
