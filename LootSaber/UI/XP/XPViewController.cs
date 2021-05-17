﻿using HMUI;
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
using System.Threading.Tasks;

namespace LootSaber.UI.XP
{
    [ViewDefinition("LootSaber.UI.XP.XPView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\XP\XPView.bsml")]
    internal class XPScreen : BSMLResourceViewController
    {
        public override string ResourceName => "LootSaber.UI.XP.XPView.bsml";
        internal static XPScreen Instance { get; private set; }
        [UIComponent("xp-amount")] private TextMeshProUGUI xpAmount;
        [UIComponent("level-number")] private TextMeshProUGUI levelNumber;

        [UIComponent("bonuses-list")] public CustomListTableData bonusList = new CustomListTableData();

        [UIAction("loot-button")]
        internal void OpenMainUI()
        {

        }
        internal void ClearBonuses()
        {
            bonusList.data.Clear();
            bonusList.tableView.ReloadData();
        }
        internal void AddBonuses(List<CustomTypes.AfterLevelBonus> bonuses)
        {
            foreach(var bonus in bonuses)
            {
                CustomListTableData.CustomCellInfo tempCell = new CustomListTableData.CustomCellInfo(bonus.name);
                tempCell.subtext = "+" + bonus.scoreBonus.ToString() + " XP";
                tempCell.icon = TextureExtensions.FromEmbedded(bonus.imagePath);
                bonusList.data.Add(tempCell);
            }
            bonusList.tableView.ReloadData();
        }
        internal static void FuckWithScoreAndLevel(int score, int bonusScore)
        {
            Task.Delay(100);
            int currentScore = Data.Player.currentData.XP;
            int currentLevel = Data.Player.currentData.Level;

            if(currentScore + score + bonusScore > 10000)
            {
                currentLevel += 1;
                currentScore = (currentScore + score + bonusScore) - 10000;
            }
            else
            {
                currentScore = (currentScore + score + bonusScore);
            }
            //eventually do some fancy UI progression and effects woo, but for now just snap it

            Data.Player.currentData.XP = currentScore;
            Data.Player.currentData.Level = currentLevel;
            Instance.refreshPlayerData();
        }

        internal void refreshPlayerData()
        {
            xpAmount.text = Data.Player.currentData.XP.ToString();
            levelNumber.text = Data.Player.currentData.Level.ToString();
        }

        [UIAction("#post-parse")]
        internal void Init()
        {
            Instance = this;

            refreshPlayerData();
        }

    }
}