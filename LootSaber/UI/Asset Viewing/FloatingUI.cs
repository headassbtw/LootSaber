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

namespace LootSaber.UI.Asset_Viewing
{
    class FloatingUI
    {
        internal static FloatingScreen middleScreen = FloatingScreen.CreateFloatingScreen(new Vector2(100, 60), false, new Vector3(0, 0.4f, 2.75f), Quaternion.Euler(65f, 0, 0), 0f, true);
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
    }
}
