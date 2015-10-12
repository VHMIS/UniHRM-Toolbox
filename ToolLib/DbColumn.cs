using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLib
{
    class DbColumn
    {
        private string description;

        private string name { get; }

        public DbColumn(string description)
        {
            this.description = description;

            this.check();
        }

        private void check()
        {
            string[] token = this.description.Split(',');
            string name = token[0];
        }
    }
}
