using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KP_Inventory_Library
{
    public static class UtilityObject
    {
        public static string DefineProggressBarColor(int percentage)
        {
            if (percentage >= 80)
                return "green";
            else if (percentage < 80 && percentage > 50)
                return "yellow";
            else
                return "red";
        }
        public static string DefinePeriodeValue(DateTime time)
        {
            string returnsValue;
            if (time.Month < 8 && time.Month > 1)
                returnsValue = "Genap " + (time.Year - 1).ToString() + " / " + time.Year.ToString();
            else
                returnsValue = "Ganjil " + time.Year.ToString() + " / " + (time.Year + 1).ToString();
            return returnsValue;
        }
        public static DateTime DefinePeriodeDate(string time)
        {
            DateTime returnsValue;
            string[] tmp = time.Split(' ');
            if (tmp[0].Contains("Genap"))
            {
                string[] year = tmp[1].Split('/');
                returnsValue = DateTime.Parse("02/02/" + year[1]);
            }
            else
            {
                string[] year = tmp[1].Split('/');
                returnsValue = DateTime.Parse("08/08/" + year[0]);
            }
            return returnsValue;
        }
    }
}
