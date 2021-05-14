using System;
using System.Collections.Generic;
using System.Linq;
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
            public int Coins;
            public int Credits;
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
