using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootSaber.Extensions
{
    static class TextFormattingExtensions
    {
        public static string AddCommas(this string number)
        {
            int length = number.Length;
            //takes the number and rounds down by 3
            int commasToAdd = (int)Math.Floor((decimal)length / 3);
            //checks to see if the number is perfectly divisible by 3, to avoid an extra comma in front
            if (commasToAdd.Equals(length/3))
                commasToAdd -= 1;
            string retn = number;
            for(int i = 0; i < commasToAdd; i++)
            {
                //inserts a comma at the startint index of length - 3, but then adds 4 for each iteration of the for loop,
                //to account for previous commas
                retn = number.Insert(length - (3 + i*4), ",");
            }
            return retn;
        }
    }
}
