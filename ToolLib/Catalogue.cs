using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLib
{
    public class Catalogue
    {
        public static string build(string description)
        {
            string[] token = description.Split('\n');
            if(token.Length < 2)
            {
                return "";
            }

            string name = "TD_" + token[0].Trim();

            return name;
        }
    }
}
