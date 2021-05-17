using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LootSaber
{
    internal class CustomTypes
    {
        public struct PlayerData
        {
            public bool Sabers;
            public bool Notes;
            public bool Plats;
            public bool MenuFonts;
            public int XP;
            public int Level;
        }
        public struct Rarity
        {
            public List<string> Sabers;
            public List<string> Notes;
            public List<string> Platforms;
            public List<string> MenuFonts;

            public Rarity(List<string> sabers, List<string> notes, List<string> platforms, List<string> menufonts)
            {
                this.Sabers = sabers;
                this.Notes = notes;
                this.Platforms = platforms;
                this.MenuFonts = menufonts;
            }
        }

        public enum bonus
        {
            FC,
            SS,
            S,
            PB,
            YT
        }
        public struct AfterLevelBonus
        {
            public string name;
            public string imagePath;
            public int scoreBonus;

            public AfterLevelBonus(bonus b)
            {
                this.name = "Placeholder";
                this.scoreBonus = 0;
                this.imagePath = "Nah";

                switch (b)
                {
                    case bonus.FC:
                        this.name = "Full Combo";
                        this.imagePath = "LootSaber.UI.BonusIcons.FC.png";
                        this.scoreBonus = 1000;
                        break;
                    case bonus.SS:
                        this.name = "SS Rank";
                        this.imagePath = "LootSaber.UI.BonusIcons.SS.png";
                        this.scoreBonus = 500;
                        break;
                    case bonus.S:
                        this.name = "S Rank";
                        this.imagePath = "LootSaber.UI.BonusIcons.S.png";
                        this.scoreBonus = 250;
                        break;
                    case bonus.PB:
                        this.name = "Personal Best";
                        this.imagePath = "LootSaber.UI.BonusIcons.PB.png";
                        this.scoreBonus = 2000;
                        break;
                    case bonus.YT:
                        this.name = "You Tried";
                        this.imagePath = "LootSaber.UI.BonusIcons.YT.png";
                        this.scoreBonus = 69;
                        break;
                }
            }
        }


        public struct DownloadRequestResponse
        {
            public int tier;
            public string assetType;
            public string filePath;
            public WebClient client;
        }

        public struct DownloadsDatabase
        {
            public Rarity Tier1;
            public Rarity Tier2;
            public Rarity Tier3;
            public Rarity Tier4;
            public Rarity Tier5;

            public DownloadsDatabase(Rarity t1, Rarity t2, Rarity t3, Rarity t4, Rarity t5)
            {
                this.Tier1 = t1;
                this.Tier2 = t2;
                this.Tier3 = t3;
                this.Tier4 = t4;
                this.Tier5 = t5;
            }
        }

    }
}
