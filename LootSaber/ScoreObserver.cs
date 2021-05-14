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


            Plugin.Log.Notice("FC: " + compres.fullCombo.ToString() + " Rank: " + tmp.text + " PB: " + personalBest.ToString());

            if (personalBest && !__instance.practice)
                Data.Player.currentData.Credits += 1;
            if (compres.fullCombo && !__instance.practice)
                Data.Player.currentData.Coins += 2;
            if (rank.Equals("SS") && !__instance.practice)
                Data.Player.currentData.Coins += 3;
            if (rank.Equals("S") && !__instance.practice)
                Data.Player.currentData.Coins += 1;
        }
    }
}
