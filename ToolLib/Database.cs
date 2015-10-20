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
            result += "\r\n" + "\r\n" + Database.sqlCreateDb(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcSelect(name);
            result += "\r\n" + "\r\n" + Database.sqlProcInsert(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcDel(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcUpdate(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcInUse(name, columns);
            result += "\r\n" + "\r\n" + Database.sqlProcExists(name, columns);

            result += "\r\n" + "\r\n" + Database.codeMetaData(name, columns);
            result += "\r\n" + "\r\n" + Database.codeCommandList(name);

            string codeInsert = Database.codeDataLayerInsert(name, columns);
            string codeInUse = Database.codeDataLayerInUse(name, columns);

            result += "\r\n" + Database.codeDataLayerOpen(name);
            result += "\r\n" + Database.codeDataLayerSelect(name);
            result += "\r\n" + codeInsert;
            result += "\r\n" + Database.codeDataLayerUpdate(codeInsert);
            result += "\r\n" + Database.codeDataLayerDelete(codeInUse);
            result += "\r\n" + codeInUse;
            result += "\r\n" + Database.codeDataLayerExists(codeInUse);
            result += "\r\n}";

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
                }
                else
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

        private static string codeMetaData(string tableName, List<DbColumn> columns)
        {
            string code = "";

            code += "#region";
            code += "\r\n//";
            code += "\r\n//" + tableName.ToUpper();
            code += "\r\n//";
            code += "\r\npublic const string " + tableName.Replace("DT_", "").ToUpper() + " = \"" + tableName + "\";";
            foreach (DbColumn col in columns)
            {
                code += "\r\npublic const string " + tableName.Replace("DT_", "").ToUpper() + "_" + col.Name.ToUpper() + " = \"" + col.Name + "\";";
            }

            code += "\r\n#endregion";

            return code;
        }

        private static string codeCommandList(string tableName)
        {
            string code = "";

            code += "#region";
            code += "\r\n//";
            code += "\r\n//" + tableName.ToUpper();
            code += "\r\n//";
            code += "\r\npublic const string " + tableName.Replace("DT_", "") + "_Select = \"" + Database.sp_pre + tableName + "_Select\";";
            code += "\r\npublic const string " + tableName.Replace("DT_", "") + "_Insert = \"" + Database.sp_pre + tableName + "_Insert\";";
            code += "\r\npublic const string " + tableName.Replace("DT_", "") + "_Update = \"" + Database.sp_pre + tableName + "_Update\";";
            code += "\r\npublic const string " + tableName.Replace("DT_", "") + "_Delete = \"" + Database.sp_pre + tableName + "_Delete\";";
            code += "\r\npublic const string " + tableName.Replace("DT_", "") + "_InUse = \"" + Database.sp_pre + tableName + "_InUse\";";
            code += "\r\npublic const string " + tableName.Replace("DT_", "") + "_Exists = \"" + Database.sp_pre + tableName + "_Exists\";";

            code += "\r\n#endregion";

            return code;
        }

        private static string codeDataLayerSelect(string tableName)
        {
            string code = @"
                public DataTable Select()
                {{
                    try
                    {{
                        DbCommand cmd = CreateCommand(CommandList.{0}, CommandType.StoredProcedure);
                        DataTable dt = FillDataTable(cmd, MetaData.{1});
                        return dt;
                    }}
                    catch (Exception ex)
                    {{
                        throw ex;
                    }}
                }}";

            string command = tableName + "_Select";
            string meta = getMetaDataName(tableName);

            return String.Format(code, command, meta);
        }

        private static string codeDataLayerInsert(string tableName, List<DbColumn> columns)
        {
            string code = @"
                public bool Insert({0})
                {{
                    try
                    {{
                        using (DbCommand cmd = CreateCommand(CommandList.{1}, CommandType.StoredProcedure))
                        {{
                            {2}
                            int rowAffected = ExecuteNonQuery(cmd);
                            return rowAffected > 0;
                        }}
                    }}
                    catch (Exception ex)
                    {{
                        return false;
                    }}
                }}";

            List<String> declaredCmdParams = new List<string>();
            List<String> declaredParams = new List<string>();
            string command = tableName + "_Insert";

            foreach (DbColumn col in columns)
            {
                declaredParams.Add("string " + col.Name);
                declaredCmdParams.Add("cmd.Parameters.Add(CreateParameter(\"@\" + MetaData." + getMetaDataName(tableName, col.Name) + ", DbType.String, " + col.Name + "));");
            }

            return String.Format(code, String.Join(", ", declaredParams), command, String.Join("\r\n", declaredCmdParams));
        }

        private static string codeDataLayerUpdate(string insertCode)
        {
            return insertCode.Replace("Insert", "Update");
        }

        private static string codeDataLayerUpdate(string tableName, List<DbColumn> columns)
        {
            return codeDataLayerUpdate(codeDataLayerInsert(tableName, columns));
        }

        private static string codeDataLayerInUse(string tableName, List<DbColumn> columns)
        {
            string code = @"
                public bool InUse({0})
                {{
                    try
                    {{
                        DbCommand cmd = CreateCommand(CommandList.{1}, CommandType.StoredProcedure);
                        {2}
                        return Helper.ConvertToBool(ExecuteScalar(cmd));
                    }}
                    catch (Exception ex)
                    {{
                        return true;
                    }}
                }}";

            List<String> declaredCmdParams = new List<string>();
            List<String> declaredParams = new List<string>();
            string command = tableName + "_InUse";

            foreach (DbColumn col in columns)
            {
                if (col.PrimaryKey)
                {
                    declaredParams.Add("string " + col.Name);
                    declaredCmdParams.Add("cmd.Parameters.Add(CreateParameter(\"@\" + MetaData." + getMetaDataName(tableName, col.Name) + ", DbType.String, " + col.Name + "));");
                }
            }

            return String.Format(code, String.Join(", ", declaredParams), command, String.Join("\r\n", declaredCmdParams));
        }

        private static string codeDataLayerExists(string inUseCode)
        {
            return inUseCode.Replace("InUse", "Exists");
        }

        private static string codeDataLayerExists(string tableName, List<DbColumn> columns)
        {
            return codeDataLayerExists(codeDataLayerInUse(tableName, columns));
        }

        private static string codeDataLayerDelete(string inUseCode)
        {
            return inUseCode.Replace("InUse", "Delete").Replace("return true;", "return false;").Replace("return Helper.ConvertToBool(ExecuteScalar(cmd));", "ExecuteNonQuery(cmd);\r\nreturn true;");
        }

        private static string codeDataLayerDelete(string tableName, List<DbColumn> columns)
        {
            return codeDataLayerDelete(codeDataLayerInUse(tableName, columns));
        }

        private static string codeDataLayerOpen(string tableName)
        {
            string code = @"
                public class {0} : BaseData
                {{
                    public {0}(string provider, string connectionString)
                        : base(provider, connectionString)
                    {{}}

                    public {0}(string connectionString)
                        : base(connectionString)
                    {{}}";

            return String.Format(code, tableName);
        }

        private static string getMetaDataName(string name)
        {
            return name.Replace("DT_", "").ToUpper();
        }

        private static string getMetaDataName(string name1, string name2)
        {
            return name1.Replace("DT_", "").ToUpper() + "_" + name2.ToUpper();
        }

        private static string getCommandListName(string name)
        {
            return name.Replace("DT_", "");
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
