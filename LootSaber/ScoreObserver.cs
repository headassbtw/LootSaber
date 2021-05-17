using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TMPro;
using static LootSaber.CustomTypes;
using static LootSaber.Extensions.InternalFunctionExtensions;
using static LootSaber.Extensions.MovementExtensions;

namespace LootSaber
{
    //this is for observing what scores you get, to give you credits for certain things
    [HarmonyPatch(typeof(ResultsViewController), "DidActivate")]
    static class ScoreResultPatch
    {
        static void Postfix(ResultsViewController __instance)
        {
            UnityEngine.Transform fs = UI.XP.XPScreenStarter.xpFloatingScreen.transform;
            fs.Move(fs.position.x, 2.55f, fs.position.z, 0.1f);
            Type type = __instance.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

            FieldInfo rank = type.GetField("_rankText", bindingFlags);
            FieldInfo pb = type.GetField("_newHighScore", bindingFlags);
            FieldInfo comp = type.GetField("_levelCompletionResults", bindingFlags);
            TextMeshProUGUI tmp = (TextMeshProUGUI)rank.GetValue(__instance);
            LevelCompletionResults compres = (LevelCompletionResults)comp.GetValue(__instance);
            bool personalBest = (bool)pb.GetValue(__instance);
            int _xpIncrease = (int)Math.Round((decimal)compres.modifiedScore / 10);

            var bonuses = new List<AfterLevelBonus>();



            if (compres.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed)
                bonuses.Add(new AfterLevelBonus(bonus.YT));
            if(__instance.practice)
                _xpIncrease = (int)Math.Round((decimal)compres.modifiedScore / 40);
            if (personalBest && !__instance.practice)
                bonuses.Add(new AfterLevelBonus(bonus.PB));
            if (rank.Equals("S") && !__instance.practice)
                bonuses.Add(new AfterLevelBonus(bonus.S));
            if (compres.fullCombo && !__instance.practice)
                bonuses.Add(new AfterLevelBonus(bonus.FC));
            if (rank.Equals("SS") && !__instance.practice)
                bonuses.Add(new AfterLevelBonus(bonus.SS));
            UI.XP.XPScreen.FuckWithScoreAndLevel(_xpIncrease,bonuses.BonusScore());
            UI.XP.XPScreen.Instance.AddBonuses(bonuses);
        }
    }
    [HarmonyPatch(typeof(ResultsViewController), "ContinueButtonPressed")]
    static class ScoreContinuePatch
    {
        static void Prefix(ResultsViewController __instance)
        {
            UnityEngine.Transform fs = UI.XP.XPScreenStarter.xpFloatingScreen.transform;
            fs.Move(fs.position.x, 3.25f, fs.position.z, 0.75f, EasingFunction.Ease.EaseInOutQuad);
            UI.XP.XPScreen.Instance.ClearBonuses();
            
        }
    }

}
