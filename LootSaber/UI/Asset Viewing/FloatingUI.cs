using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LootSaber.UI.ViewControllers;
using HMUI;
using static HMUI.ViewController;
using LootSaber.UI.AssetPreviews;

namespace LootSaber.UI.Asset_Viewing
{
    class FloatingUI
    {
        internal static FloatingScreen middleScreen = FloatingScreen.CreateFloatingScreen(new Vector2(100, 20), false, new Vector3(0, 0.4f, 2.75f), Quaternion.Euler(65f, 0, 0), 0f, true);

        internal static FloatingScreen PLS = FloatingScreen.CreateFloatingScreen(new Vector2(30, 40), false, new Vector3(-0.67f, 0.87f, 3.5f), Quaternion.Euler(30f, -25, 0), 0f, true);
        internal static FloatingScreen PMS = FloatingScreen.CreateFloatingScreen(new Vector2(30, 40), false, new Vector3(0, 0.4f, 3.5f), Quaternion.Euler(30f, 0, 0), 0f, true);
        internal static FloatingScreen PRS = FloatingScreen.CreateFloatingScreen(new Vector2(30, 40), false, new Vector3(0.49f, 0.87f, 3.5f), Quaternion.Euler(30f, 25, 0), 0f, true);

        


        internal static void middle()
        {
            var _xpv = BeatSaberUI.CreateViewController<MainViewController>();
            middleScreen.SetRootViewController(_xpv, animationType: AnimationType.In);
            middleScreen.enabled = true;

        }
        internal static void unmiddle()
        {
            middleScreen.SetRootViewController(null, animationType: AnimationType.Out);
            middleScreen.enabled = false;
        }

        internal static void Previews()
        {
            var _lv = BeatSaberUI.CreateViewController<LeftPreviewViewController>();
            var _mv = BeatSaberUI.CreateViewController<MiddlePreviewViewController>();
            var _rv = BeatSaberUI.CreateViewController<RightPreviewViewController>();
            PLS.SetRootViewController(_lv, animationType: AnimationType.In);
            PLS.enabled = true;
            PMS.SetRootViewController(_mv, animationType: AnimationType.In);
            PMS.enabled = true;
            PRS.SetRootViewController(_rv, animationType: AnimationType.In);
            PRS.enabled = true;
        }
        internal static void unPreviews()
        {
            PLS.SetRootViewController(null, animationType: AnimationType.Out);
            PLS.enabled = false;
            PMS.SetRootViewController(null, animationType: AnimationType.Out);
            PMS.enabled = false;
            PRS.SetRootViewController(null, animationType: AnimationType.Out);
            PRS.enabled = false;
        }
    }
}
