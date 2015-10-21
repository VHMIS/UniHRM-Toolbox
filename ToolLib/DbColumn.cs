using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLib
{
    class DbColumn
    {
        private string name;

        private string type;

        private bool valid;

        private bool notNull;

        private bool id;

        private bool primaryKey;

        private bool key;

        public DbColumn()
        {
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
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

            set
            {
                notNull = value;
            }
        }

        public bool PrimaryKey
        {
            get
            {
                return primaryKey;
            }

            set
            {
                primaryKey = value;
            }
        }

        public bool Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public bool Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public void fromString(string description)
        {
            string[] token = description.Split(',');

            if(token.Length < 2)
            {
                this.valid = false;
                this.id = false;
                this.primaryKey = false;
                this.name = "";
                this.type = "";
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
            this.key = false;
            if(token.Length >= 4)
            {
                if(token[3].Contains("primary"))
                {
                    this.primaryKey = true;
                }

                if(token[3].Contains("key"))
                {
                    this.key = true;
                }
            }
        }
    }
}
