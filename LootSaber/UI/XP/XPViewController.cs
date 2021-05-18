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
using static LootSaber.Extensions.MovementExtensions;
using static LootSaber.CustomTypes;
using System.Threading.Tasks;
using LootSaber.UI.Asset_Viewing;

namespace LootSaber.UI.XP
{
    [ViewDefinition("LootSaber.UI.XP.XPView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\XP\XPView.bsml")]
    internal class XPScreen : BSMLResourceViewController
    {
        public static readonly int LevelMaxScore = 1000000;

        internal static bool uh = false;
        public override string ResourceName => "LootSaber.UI.XP.XPView.bsml";
        internal static XPScreen Instance { get; private set; }
        [UIValue("xp-amount")] private string xpAmount
        {
            get => Data.Player.currentData.XP.ToString().AddCommas();
            set
            {
                Data.Player.currentData.XP = Int32.Parse(value);
            }
        }
        [UIValue("level-number")] private string levelNumber
        {
            get => Data.Player.currentData.Level.ToString().AddCommas();
            set
            {
                Data.Player.currentData.Level = Int32.Parse(value);
            }
        }

        private int _Level
        {
            get => Int32.Parse(levelNumber);
            set
            {
                levelNumber = value.ToString();
            }
        }
        private int _XP
        {
            get => Int32.Parse(xpAmount);
            set
            {
                xpAmount = value.ToString();
            }
        }

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
            UI.ViewControllers.MainViewController.CheckPending();
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
                tempCell.subtext = "+" + bonus.scoreBonus.ToString().AddCommas() + " XP";
                tempCell.icon = TextureExtensions.FromEmbedded(bonus.imagePath);
                bonusList.data.Add(tempCell);
            }
            bonusList.tableView.ReloadData();
        }
        internal static void FuckWithScoreAndLevel(int score, int bonusScore)
        {
            Task.Delay(100);
            if(Instance._XP + score + bonusScore > LevelMaxScore)
            {
                int sc = Instance._XP + score + bonusScore;
                int levelsplus = (int)Mathf.Floor(sc / LevelMaxScore);

                for (int i = 0; i < levelsplus; i++)
                {
                    Instance._XP.GoUp(LevelMaxScore, 1 - (Instance._XP / LevelMaxScore));
                    //do some fancy level up anim here, but i'm too lazy to do it atm so i'll just delay
                    Task.Delay(100);
                    Instance._XP = 0;
                }
                Instance._XP.GoUp(sc - (levelsplus * LevelMaxScore), 1 - (sc - (levelsplus * LevelMaxScore)/LevelMaxScore));
                Instance._Level += levelsplus;
                Instance._XP = sc - (levelsplus * LevelMaxScore);
                Data.Player.currentData.PendingBoxes += levelsplus;
            }
            else
            {
                Instance._XP.GoUp((Instance._XP + score + bonusScore), 1 - (Instance._XP / LevelMaxScore));
            }

            Data.Player.currentData.XP = Instance._XP;
            Data.Player.currentData.Level = Instance._Level;
        }

        [UIAction("#post-parse")]
        internal void Init()
        {
            Instance = this;
        }

    }
}