using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLib
{
    public class Catalogue
    {
        public static string dt_pre = "DT_";
        public static string sp_pre = "proc_";

        public static string build(string description)
        {
            string[] token = description.Split('\n');
            if (token.Length < 2)
            {
                return "";
            }

            // name and column
            string name = dt_pre + "_" + token[0].Trim();
            List<DbColumn> columns = Catalogue.getColumns(token);

            // result
            string result = "";

            result += name;
            result += "\r\n" + Catalogue.sqlCreateDb(name, columns);
            result += "\r\n" + "\r\n" + "\r\n" + Catalogue.sqlProcSelect(name);

            return result;
        }

        private static string sqlCreateDb(string tableName, List<DbColumn> columns)
        {
            string sql = "";

            sql += "CREATE TABLE " + tableName + " (";

            foreach (DbColumn col in columns)
            {
                sql += "\r\n";
                sql += col.Name + " " + col.Type;

                if (col.Id)
                {
                    sql += col.Id ? " IDENTITY(1,1)" : "";
                }
                else
                {
                    sql += col.NotNull ? " not null" : " null";
                }

                sql += col.PrimaryKey ? " PRIMARY KEY" : "";
                sql += ",";
            }

            sql += "\r\n" + ");";

            return sql;
        }

        private static string sqlProcSelect(string tableName)
        {
            string sql = "";

            sql += "create procedure " + sp_pre + tableName + "_Select";
            sql += "\r\n" + "as";
            sql += "\r\n" + "begin";
            sql += "\r\n" + "set nocount on;";
            sql += "\r\n" + "select * from " + tableName;
            sql += "\r\n" + "set nocount off;";
            sql += "\r\n" + "end";

            return sql;
        }

        private static List<DbColumn> getColumns(string[] token)
        {
            List<DbColumn> list = new List<DbColumn>();

            for (int i = 1; i < token.Length; i++)
            {
                DbColumn col = new DbColumn(token[i].Trim());

                if (col.Valid)
                {
                    list.Add(col);
                }
            }

            return list;
        }
    }
}
