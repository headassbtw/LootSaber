using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LootSaber.CustomTypes;

namespace LootSaber.Extensions
{
    internal static class InternalFunctionExtensions
    {
        public static int BonusScore(this List<CustomTypes.AfterLevelBonus> b)
        {
            int retn = 0;
            foreach(var bonus in b)
            {
                retn += bonus.scoreBonus;
            }
            return retn;
        }
    }
}
