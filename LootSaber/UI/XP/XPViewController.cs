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
using System.Threading.Tasks;
using LootSaber.UI.Asset_Viewing;

namespace LootSaber.UI.XP
{
    [ViewDefinition("LootSaber.UI.XP.XPView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\XP\XPView.bsml")]
    internal class XPScreen : BSMLResourceViewController
    {
        internal static bool uh = false;
        public override string ResourceName => "LootSaber.UI.XP.XPView.bsml";
        internal static XPScreen Instance { get; private set; }
        [UIComponent("xp-amount")] private TextMeshProUGUI xpAmount;
        [UIComponent("level-number")] private TextMeshProUGUI levelNumber;

        [UIComponent("bonuses-list")] public CustomListTableData bonusList = new CustomListTableData();

        [UIComponent("bLootButton")] internal Button blueUIButton;
        [UIComponent("gLootButton")] internal Button greyUIButton;


        [UIAction("loot-button")]
        internal void OpenMainUI()
        {
            if (uh)
                blueUIButton.enabled = false;
            if (!uh)
                greyUIButton.enabled = false;
            AssetPreviews.LeftPreviewViewController.StaticPP();
            AssetPreviews.MiddlePreviewViewController.StaticPP();
            AssetPreviews.RightPreviewViewController.StaticPP();

            if(BaseGameUiHandler.Instance == null)
                BaseGameUiHandler.Instance = new BaseGameUiHandler(GameObject.Find("ScreenSystem").GetComponent<HierarchyManager>());

            BaseGameUiHandler.Instance.DismissGameUI();
            FloatingUI.middle();
            FloatingUI.Previews();
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
                int sc = currentScore + score + bonusScore;
                int levelsplus = (int)Mathf.Floor(sc / 10000);
                currentLevel += levelsplus;
                currentScore = sc - levelsplus;
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