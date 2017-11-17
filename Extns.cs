using System.Windows.Controls.Primitives;

namespace Azotus
{
    public static partial class Extns
    {
        public static String ExpandTime(this Int32 seconds, Boolean InWords = true, Boolean FullyJustify = false)
        {
            Int32[] bits = new Int32[2];
            for (int Lp1 = 0; Lp1 < 2; Lp1++)
            {
                bits[Lp1] = seconds % 60;
                seconds /= 60;
            }

            String rc = "";
            if (FullyJustify || seconds > 0) rc += seconds.ToString() + (InWords ? " hour" + (seconds == 1 ? "" : "s") : "");
            if (FullyJustify || bits[1] > 0)
            {
                if (FullyJustify || InWords && seconds > 0) rc += (InWords ? ", " : ":");
                rc += bits[1].ToString(InWords ? "" : "00") + (InWords ? " minute" + (bits[1] == 1 ? "" : "s") : "");
            }
            if (FullyJustify || (seconds + bits[1] > 0 && bits[0] > 0) || (!InWords && seconds + bits[1] > 0)) rc += (InWords ? " and " : ":");
            if (!FullyJustify && !InWords && bits[1] == 0) rc += (seconds > 0 ? "00:" : "0:");
            if (FullyJustify || bits[0] > 0 || (!InWords && seconds + bits[1] > 0))
                rc += bits[0].ToString(InWords ? "" : "00") + (InWords ? " second" + (bits[0] == 1 ? "" : "s") : "");
            return rc;
        }

        public static void Populate(this xList fldData, Selector frmField, int chosenIdx = -1, bool useTitle = true, bool inclParmData = false)
        {
            int selIdx = chosenIdx;
            if (useTitle)
                frmField.Items.Add(fldData.Title);

            for (int Lp1 = 1; Lp1 <= fldData.MaxIdx; ++Lp1)
            {
                string thing = fldData[Lp1, xList.Fld.Descr].ToString();
                if (fldData[Lp1, xList.Fld.Idx].Equals(chosenIdx)) selIdx = Lp1;
                if (inclParmData) thing += " - " + fldData[Lp1, xList.Fld.Param] + "[" + fldData[Lp1, xList.Fld.Idx] + "]";
                frmField.Items.Add(thing);
            }

            if (selIdx >= 0)
                frmField.SelectedIndex = selIdx;
        }
    }
}
