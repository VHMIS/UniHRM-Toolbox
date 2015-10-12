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
            if (token.Length < 2)
            {
                return "";
            }

            // name and column
            string name = "TD_" + token[0].Trim();
            List<DbColumn> columns = Catalogue.getColumns(token);

            // sql string for create db
            string sqlCreateDb = Catalogue.sqlCreateDb(name, columns);

            // result
            string result = "";
            result += name;
            result += "\r\n" + sqlCreateDb;

            return result;
        }

        private static string sqlCreateDb(string tableName, List<DbColumn> columns)
        {
            string sql = "";

            sql += "CREATE TABLE " + tableName + " (";

            foreach (DbColumn col in columns)
            {
                sql += "\r\n";
                sql += col.Name + " " + col.Type + ",";
            }

            sql += "\r\n" + ") ON PRIMARY;";

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
