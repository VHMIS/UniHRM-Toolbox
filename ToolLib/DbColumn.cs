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

        private bool notNull;

        private bool id;

        private bool primaryKey;

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

        public bool NotNull
        {
            get
            {
                return notNull;
            }
        }

        public bool PrimaryKey
        {
            get
            {
                return primaryKey;
            }
        }

        public bool Id
        {
            get
            {
                return id;
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

            this.notNull = false;
            this.id = false;
            if(token.Length >= 3)
            {
                if(token[2].Contains("not"))
                {
                    this.notNull = true;
                }

                if(token[2].Contains("id"))
                {
                    this.id = true;
                }
            }

            this.primaryKey = false;
            if(token.Length >= 4)
            {
                if(token[3].Contains("primary"))
                {
                    this.primaryKey = true;
                }
            }
        }
    }
}
