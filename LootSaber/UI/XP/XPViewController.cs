using HMUI;
using System;
using Zenject;
using Tweening;
using System.Linq;
using UnityEngine;
using LootSaber.UI;
using System.ComponentModel;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.FloatingScreen;
using VRUIControls;
using IPA.Utilities;
using TMPro;
using LootSaber.Extensions;
using System.Reflection;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components;
using UnityEngine.UI;
using System.IO;
using static LootSaber.CustomTypes;

namespace LootSaber.UI.XP
{
    [ViewDefinition("LootSaber.UI.XP.XPView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\XP\XPView.bsml")]
    internal class XPScreen : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.XP.XPView.bsml";

        [UIComponent("xp-amount")] private TextMeshProUGUI xpAmount;
        [UIComponent("level-number")] private TextMeshProUGUI levelNumber;

        [UIComponent("bonuses-list")] public static CustomCellListTableData bonusList = new CustomCellListTableData();

        [UIAction("loot-button")]
        internal void OpenMainUI()
        {

        }
        internal static void ClearBonuses()
        {
            bonusList.data.Clear();
        }
        internal void AddBonuses(List<CustomTypes.AfterLevelBonus> bonuses)
        {
            foreach(var bonus in bonuses)
            {
                CustomListTableData.CustomCellInfo tempCell = new CustomListTableData.CustomCellInfo(bonus.name);
                tempCell.subtext = bonus.scoreBonus.ToString();
                tempCell.icon = TextureExtensions.FromEmbedded(bonus.imagePath);
            }
        }




        [UIAction("#post-parse")]
        internal void Init()
        {
            bonusList.data.Clear();

            xpAmount.text = Data.Player.currentData.XP.ToString();
            levelNumber.text = Data.Player.currentData.Level.ToString();
        }

    }
}