using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TMPro;

namespace LootSaber
{
    //this is for observing what scores you get, to give you credits for certain things
    [HarmonyPatch(typeof(ResultsViewController), "DidActivate")]
    static class ScoreResultPatch
    {
        static void Postfix(ResultsViewController __instance)
        {
            Type type = __instance.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

            FieldInfo rank = type.GetField("_rankText", bindingFlags);
            FieldInfo pb = type.GetField("_newHighScore", bindingFlags);
            FieldInfo comp = type.GetField("_levelCompletionResults", bindingFlags);
            TextMeshProUGUI tmp = (TextMeshProUGUI)rank.GetValue(__instance);
            LevelCompletionResults compres = (LevelCompletionResults)comp.GetValue(__instance);
            bool personalBest = (bool)pb.GetValue(__instance);


            int _xpIncrease = (int)Math.Round((decimal)compres.modifiedScore / 10);
            if(__instance.practice)
                _xpIncrease = (int)Math.Round((decimal)compres.modifiedScore / 40);
            if (personalBest && !__instance.practice)
                _xpIncrease += 100;
            if (rank.Equals("S") && !__instance.practice)
                _xpIncrease += 200;
            if (compres.fullCombo && !__instance.practice)
                _xpIncrease += 300;
            if (rank.Equals("SS") && !__instance.practice)
                _xpIncrease += 400;

            UI.XP.XPViewController.IncreaseXP(_xpIncrease);
            
        }
    }
}
