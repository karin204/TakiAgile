using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_Cards_StackPanel
{
    class Pocket: MainWindow
    {
        public Card[] pocket = new Card[117];

        public Pocket()
        {
            #region ConstructorOfPocket

            this.pocket[1] = new Card("Blue", 1, "Pocket");
            this.pocket[2] = new Card("Blue", 2, "Pocket");
            this.pocket[3] = new Card("Blue", 3, "Pocket");
            this.pocket[4] = new Card("Blue", 4, "Pocket");
            this.pocket[5] = new Card("Blue", 5, "Pocket");
            this.pocket[6] = new Card("Blue", 6, "Pocket");
            this.pocket[7] = new Card("Blue", 7, "Pocket");
            this.pocket[8] = new Card("Blue", 8, "Pocket");
            this.pocket[9] = new Card("Blue", 9, "Pocket");
            this.pocket[10] = new Card("Blue", 10, "Pocket");
            this.pocket[11] = new Card("Blue", 11, "Pocket");
            this.pocket[12] = new Card("Blue", 12, "Pocket");
            this.pocket[13] = new Card("Blue", 13, "Pocket");
            this.pocket[14] = new Card("Blue", 14, "Pocket");
            this.pocket[15] = new Card("Red", 15, "Pocket");
            this.pocket[16] = new Card("Red", 16, "Pocket");
            this.pocket[17] = new Card("Red", 17, "Pocket");
            this.pocket[18] = new Card("Red", 18, "Pocket");
            this.pocket[19] = new Card("Red", 19, "Pocket");
            this.pocket[20] = new Card("Red", 20, "Pocket");
            this.pocket[21] = new Card("Red", 21, "Pocket");
            this.pocket[22] = new Card("Red", 22, "Pocket");
            this.pocket[23] = new Card("Red", 23, "Pocket");
            this.pocket[24] = new Card("Red", 24, "Pocket");
            this.pocket[25] = new Card("Red", 25, "Pocket");
            this.pocket[26] = new Card("Red", 26, "Pocket");
            this.pocket[27] = new Card("Red", 27, "Pocket");
            this.pocket[28] = new Card("Red", 28, "Pocket");
            this.pocket[29] = new Card("Yellow", 29, "Pocket");
            this.pocket[30] = new Card("Yellow", 30, "Pocket");
            this.pocket[31] = new Card("Yellow", 31, "Pocket");
            this.pocket[32] = new Card("Yellow", 32, "Pocket");
            this.pocket[33] = new Card("Yellow", 33, "Pocket");
            this.pocket[34] = new Card("Yellow", 34, "Pocket");
            this.pocket[35] = new Card("Yellow", 35, "Pocket");
            this.pocket[36] = new Card("Yellow", 36, "Pocket");
            this.pocket[37] = new Card("Yellow", 37, "Pocket");
            this.pocket[38] = new Card("Yellow", 38, "Pocket");
            this.pocket[39] = new Card("Yellow", 39, "Pocket");
            this.pocket[40] = new Card("Yellow", 40, "Pocket");
            this.pocket[41] = new Card("Yellow", 41, "Pocket");
            this.pocket[42] = new Card("Yellow", 42, "Pocket");
            this.pocket[43] = new Card("Green", 43, "Pocket");
            this.pocket[44] = new Card("Green", 44, "Pocket");
            this.pocket[45] = new Card("Green", 45, "Pocket");
            this.pocket[46] = new Card("Green", 46, "Pocket");
            this.pocket[47] = new Card("Green", 47, "Pocket");
            this.pocket[48] = new Card("Green", 48, "Pocket");
            this.pocket[49] = new Card("Green", 49, "Pocket");
            this.pocket[50] = new Card("Green", 50, "Pocket");
            this.pocket[51] = new Card("Green", 51, "Pocket");
            this.pocket[52] = new Card("Green", 52, "Pocket");
            this.pocket[53] = new Card("Green", 53, "Pocket");
            this.pocket[54] = new Card("Green", 54, "Pocket");
            this.pocket[55] = new Card("Green", 55, "Pocket");
            this.pocket[56] = new Card("Green", 56, "Pocket");
            this.pocket[57] = new Card("Blue", 57, "Pocket");
            this.pocket[58] = new Card("Blue", 58, "Pocket");
            this.pocket[59] = new Card("Blue", 59, "Pocket");
            this.pocket[60] = new Card("Blue", 60, "Pocket");
            this.pocket[61] = new Card("Blue", 61, "Pocket");
            this.pocket[62] = new Card("Blue", 62, "Pocket");
            this.pocket[63] = new Card("Blue", 63, "Pocket");
            this.pocket[64] = new Card("Blue", 64, "Pocket");
            this.pocket[65] = new Card("Blue", 65, "Pocket");
            this.pocket[66] = new Card("Blue", 66, "Pocket");
            this.pocket[67] = new Card("Blue", 67, "Pocket");
            this.pocket[68] = new Card("Blue", 68, "Pocket");
            this.pocket[69] = new Card("Blue", 69, "Pocket");
            this.pocket[70] = new Card("Blue", 70, "Pocket");
            this.pocket[71] = new Card("Red", 71, "Pocket");
            this.pocket[72] = new Card("Red", 72, "Pocket");
            this.pocket[73] = new Card("Red", 73, "Pocket");
            this.pocket[74] = new Card("Red", 74, "Pocket");
            this.pocket[75] = new Card("Red", 75, "Pocket");
            this.pocket[76] = new Card("Red", 76, "Pocket");
            this.pocket[77] = new Card("Red", 77, "Pocket");
            this.pocket[78] = new Card("Red", 78, "Pocket");
            this.pocket[79] = new Card("Red", 79, "Pocket");
            this.pocket[80] = new Card("Red", 80, "Pocket");
            this.pocket[81] = new Card("Red", 81, "Pocket");
            this.pocket[82] = new Card("Red", 82, "Pocket");
            this.pocket[83] = new Card("Red", 83, "Pocket");
            this.pocket[84] = new Card("Red", 84, "Pocket");
            this.pocket[85] = new Card("Yellow", 85, "Pocket");
            this.pocket[86] = new Card("Yellow", 86, "Pocket");
            this.pocket[87] = new Card("Yellow", 87, "Pocket");
            this.pocket[88] = new Card("Yellow", 88, "Pocket");
            this.pocket[89] = new Card("Yellow", 89, "Pocket");
            this.pocket[90] = new Card("Yellow", 90, "Pocket");
            this.pocket[91] = new Card("Yellow", 91, "Pocket");
            this.pocket[92] = new Card("Yellow", 92, "Pocket");
            this.pocket[93] = new Card("Yellow", 93, "Pocket");
            this.pocket[94] = new Card("Yellow", 94, "Pocket");
            this.pocket[95] = new Card("Yellow", 95, "Pocket");
            this.pocket[96] = new Card("Yellow", 96, "Pocket");
            this.pocket[97] = new Card("Yellow", 97, "Pocket");
            this.pocket[98] = new Card("Yellow", 98, "Pocket");
            this.pocket[99] = new Card("Green", 99, "Pocket");
            this.pocket[100] = new Card("Green", 100, "Pocket");
            this.pocket[101] = new Card("Green", 101, "Pocket");
            this.pocket[102] = new Card("Green", 102, "Pocket");
            this.pocket[103] = new Card("Green", 103, "Pocket");
            this.pocket[104] = new Card("Green", 104, "Pocket");
            this.pocket[105] = new Card("Green", 105, "Pocket");
            this.pocket[106] = new Card("Green", 106, "Pocket");
            this.pocket[107] = new Card("Green", 107, "Pocket");
            this.pocket[108] = new Card("Green", 108, "Pocket");
            this.pocket[109] = new Card("Green", 109, "Pocket");
            this.pocket[110] = new Card("Green", 110, "Pocket");
            this.pocket[111] = new Card("Green", 111, "Pocket");
            this.pocket[112] = new Card("Green", 112, "Pocket");
            this.pocket[113] = new Card("ColorTaki", 113, "Pocket");
            this.pocket[114] = new Card("ColorTaki", 114, "Pocket");
            this.pocket[115] = new Card("ColorTaki", 115, "Pocket");
            this.pocket[116] = new Card("ColorTaki", 116, "Pocket");


            #endregion
        }
        public int GetRandomCard(int max, int[] tempArr)
        {
            int n;
            Random num = new Random();
            n = num.Next(0, 58);
            while (tempArr[n] == 0)
            {
                n = num.Next(0, 58);
            }
            tempArr[n]--;

            return n;

        }




        
    }
}
