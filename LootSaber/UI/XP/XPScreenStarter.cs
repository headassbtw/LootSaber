using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static HMUI.ViewController;

namespace LootSaber.UI.XP
{
    class XPScreenStarter
    {
        internal static void yeet()
        {
            var _xpv = BeatSaberUI.CreateViewController<UI.XP.XPScreen>();
            var myFloatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(100,40), false, new Vector3(1,3.1f,3.75f), Quaternion.Euler(0,16.10f,0), 175f, false);
            myFloatingScreen.SetRootViewController(_xpv, animationType: AnimationType.None);
        }
    }
}
