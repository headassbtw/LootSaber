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
            FieldInfo comp = type.GetField("_levelCompletionResults", bindingFlags);

            TextMeshProUGUI tmp = (TextMeshProUGUI)rank.GetValue(__instance);
            LevelCompletionResults compres = (LevelCompletionResults)comp.GetValue(__instance);

            

            if(compres.fullCombo && !__instance.practice)
                Plugin.Log.Notice("Full Combo " + tmp.text + " rank.");
            else
                Plugin.Log.Notice(tmp.text + " rank.");
        }
    }
}
