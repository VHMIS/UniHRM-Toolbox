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

        private string name;

        private string type;

        private bool valid;

        public DbColumn(string description)
        {
            this.description = description;

            this.check();
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
        }

        public bool Valid
        {
            get
            {
                return valid;
            }
        }

        private void check()
        {
            string[] token = this.description.Split(',');

            if(token.Length < 2)
            {
                this.valid = false;
                return;
            }

            this.valid = true;
            this.name = token[0];
            this.type = token[1];
        }
    }
}
