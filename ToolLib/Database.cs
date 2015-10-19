using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLib
{
    public class Database
    {
        public static string dt_pre = "DT_";
        public static string sp_pre = "proc_";

        public static string fromString(string description)
        {
            string[] token = description.Split('\n');
            if (token.Length < 2)
            {
                return "";
            }

            // name and column
            string name = dt_pre + token[0].Trim();
            List<DbColumn> columns = Database.getColumns(token);

            // result
            string result = "";

            result += name;
            result += "\r\n" + Database.sqlCreateDb(name, columns);
            result += "\r\n" + "\r\n" + "\r\n" + Database.sqlProcSelect(name);
            result += "\r\n" + "\r\n" + Database.sqlProcInsert(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcDel(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcUpdate(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcInUse(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcExists(name, columns);

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

        private static string sqlProcDel(string tableName, List<DbColumn> columns)
        {
            string sql = "";

            foreach (DbColumn col in columns)
            {
                if (col.PrimaryKey)
                {
                    sql += "create procedure " + sp_pre + tableName + "_Delete";
                    sql += "\r\n" + "@" + col.Name + " " + col.Type;
                    sql += "\r\n" + "as";
                    sql += "\r\n" + "begin";
                    sql += "\r\n" + "delete from " + tableName;
                    sql += "\r\n" + "where " + col.Name + " = @" + col.Name;
                    sql += "\r\n" + "end";
                    sql += "\r\n";
                }
            }

            return sql;
        }

        private static string sqlProcExists(string tableName, List<DbColumn> columns)
        {
            string sql = "";

            foreach (DbColumn col in columns)
            {
                if (col.PrimaryKey)
                {
                    sql += "create procedure " + sp_pre + tableName + "_Exists";
                    sql += "\r\n" + "@" + col.Name + " " + col.Type;
                    sql += "\r\n" + "as";
                    sql += "\r\n" + "begin";
                    sql += "\r\n" + "if exists(select * from " + tableName + " where " + col.Name + " = @" + col.Name + ")";
                    sql += "\r\n" + "select 1 as IsExists";
                    sql += "\r\n" + "else select 0 as IsExists";
                    sql += "\r\n" + "end";
                    sql += "\r\n";
                }
            }

            return sql;
        }

        private static string sqlProcInsert(string tableName, List<DbColumn> columns)
        {
            string sql = "";
            List<String> declares = new List<string>();
            List<String> cols = new List<string>();
            List<String> vals = new List<string>();

            sql += "create procedure " + sp_pre + tableName + "_Insert";

            foreach (DbColumn col in columns)
            {
                if (!col.Id)
                {
                    declares.Add("@" + col.Name + " " + col.Type);
                    cols.Add(col.Name);
                    vals.Add("@" + col.Name);
                }
            }

            sql += "\r\n" + String.Join(",\r\n", declares);

            sql += "\r\n" + "as";
            sql += "\r\n" + "begin";
            sql += "\r\n insert into " + tableName + "(" + String.Join(", ", cols) + ")";
            sql += "\r\n values (" + String.Join(", ", vals) + ")";
            sql += "\r\n" + "end";
            sql += "\r\n";

            return sql;
        }

        private static string sqlProcInUse(string tableName, List<DbColumn> columns)
        {
            string sql = "";

            foreach (DbColumn col in columns)
            {
                if (col.PrimaryKey)
                {
                    sql += "create procedure " + sp_pre + tableName + "_InUse";
                    sql += "\r\n" + "@" + col.Name + " " + col.Type;
                    sql += "\r\n" + "as";
                    sql += "\r\n" + "begin";
                    sql += "\r\n" + "if exists(select HoSoID from HoSo_xxxxx where " + col.Name + " = @" + col.Name + ")";
                    sql += "\r\n" + "select 1 as InUse";
                    sql += "\r\n" + "else select 0 as InUse";
                    sql += "\r\n" + "end";
                    sql += "\r\n";
                }
            }

            return sql;
        }

        private static string sqlProcUpdate(string tableName, List<DbColumn> columns)
        {
            string sql = "";
            List<String> declares = new List<string>();
            List<String> updates = new List<string>();
            string where = "";

            sql += "create procedure " + sp_pre + tableName + "_Update";

            foreach (DbColumn col in columns)
            {
                declares.Add("@" + col.Name + " " + col.Type);

                if (!col.PrimaryKey)
                {
                    updates.Add(col.Name + " = @" + col.Name);
                } else
                {
                    where = col.Name + " = @" + col.Name;
                }
            }

            sql += "\r\n" + String.Join(",\r\n", declares);

            sql += "\r\n" + "as";
            sql += "\r\n" + "begin";
            sql += "\r\n update " + tableName;
            sql += "\r\n set " + String.Join(", ", updates);
            sql += "\r\n where " + where;
            sql += "\r\n" + "end";
            sql += "\r\n";

            return sql;
        }

        private static List<DbColumn> getColumns(string[] token)
        {
            List<DbColumn> list = new List<DbColumn>();

            for (int i = 1; i < token.Length; i++)
            {
                DbColumn col = new DbColumn();
                col.fromString(token[i]);

                if (col.Valid)
                {
                    list.Add(col);
                }
            }

            return list;
        }
    }
}
