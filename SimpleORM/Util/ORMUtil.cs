using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleORM.Annotation;

namespace SimpleORM.Util {
    public class ORMUtil {
        //        public static void PrintReturnTypeByColumn(DataSet dsOut) {
        //            for (int i = 0; i <= dsOut.Tables[0].Columns.Count - 1; i++) {
        //                Console.WriteLine(dsOut.Tables[0].Columns[i].ColumnName + ":  " + dsOut.Tables[0].Columns[i].DataType);
        //            }
        //        }

        #region model to dataset
        private static DataTable constructTable<T>(IList<T> records) {
            DataTable table = new DataTable();
            // Add columns
            foreach (var prop in records[0].GetType().GetProperties()) {

                if (prop.PropertyType != typeof(string) && prop.PropertyType != typeof(int) && prop.PropertyType != typeof(double) && prop.PropertyType != typeof(decimal) && prop.PropertyType != typeof(Guid) && prop.PropertyType != typeof(DateTime)) {
                    table.Columns.Add(prop.Name, typeof(string));
                } else {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            // Add rows
            foreach (var record in records) {
                DataRow row = table.NewRow();
                foreach (var prop in record.GetType().GetProperties()) {
                    if (prop.PropertyType == typeof(int)) {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = 0;
                        else row[prop.Name] = int.Parse(temp.ToString());
                    } else if (prop.PropertyType == typeof(int?)) {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = null;
                        else row[prop.Name] = int.Parse(temp.ToString());
                    } else if (prop.PropertyType == typeof(decimal)) {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = 0d;
                        else row[prop.Name] = decimal.Parse(temp.ToString());
                    } else if (prop.PropertyType == typeof(decimal?)) {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = null;
                        else row[prop.Name] = decimal.Parse(temp.ToString());
                    } else if (prop.PropertyType == typeof(double)) {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = 0;
                        else row[prop.Name] = double.Parse(temp.ToString());
                    } else if (prop.PropertyType == typeof(double?)) {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = null;
                        else row[prop.Name] = double.Parse(temp.ToString());
                    } else if (prop.PropertyType == typeof(Int64)) {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = 0;
                        else row[prop.Name] = double.Parse(temp.ToString());
                    } else {
                        var temp = prop.GetValue(record, null);
                        if (temp == null) row[prop.Name] = "";
                        else row[prop.Name] = temp.ToString();
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable ConvertToDataTable<T>(IList<T> records) {
            DataTable table = constructTable<T>(records);
            table.TableName = records[0].GetType().Name;
            return table;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> records, string tableName) {
            DataTable table = constructTable<T>(records);
            table.TableName = tableName;
            return table;
        }
        public static DataTable ConvertToDataTable<T>(T record) {
            DataTable table = constructTable<T>(new List<T>() { record });
            table.TableName = record.GetType().Name;
            return table;
        }

        public static DataTable ConvertToDataTable<T>(T record, string tableName) {
            DataTable table = constructTable<T>(new List<T>() { record });
            table.TableName = tableName;
            return table;
        }

        #endregion

        public static string ConstructSelectStatement(Type type) {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            var propertyInfos = type.GetProperties();
            var temp = 0;
            foreach (var propertyInfo in propertyInfos) {
                var customAttributes = propertyInfo.GetCustomAttributes(typeof(Column), true);
                if (customAttributes.Length > 0) {
                    var attr = (Column)customAttributes[0];
                    sb.Append(attr.Name);
                    sb.Append(" AS ");
                }
                sb.Append(propertyInfo.Name);

                temp++;
                if (temp != propertyInfos.Length) {
                    sb.Append(" ,");
                }
            }
            sb.Append(" FROM ");
            sb.Append(GetTableName(type));

            return sb.ToString();
        }

        private static string GetTableName(Type type) {
            var customAttributes = type.GetCustomAttributes(typeof(Table), true);
            if (customAttributes.Length > 0) {
                var firstOrDefault = (Table)customAttributes.FirstOrDefault();
                if (firstOrDefault != null)
                    return firstOrDefault.Name.ToString();
            } else {
                return (type.Name.ToString());
            }
            return "";
        }

        private static string GetFieldName(PropertyInfo property) {
            if (property == null) return "";
            var customAttributes = property.GetCustomAttributes(typeof(Column), true);
            if (customAttributes.Length > 0) {
                return ((Column)customAttributes[0]).Name;
            } else {
                return property.Name;
            }
        }


        public static string ConstructUpdateStatement(Type type) {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append(GetTableName(type));
            sb.Append(" SET ");
            var i = 0;
            var skipCounter = 0;
            var count = type.GetProperties().Length;
            foreach (var p in type.GetProperties()) {
                if (p.GetCustomAttributes(typeof(DataBaseGenerated), true).Length >0)
                {
                    skipCounter ++;
                    continue;
                }
                sb.Append(GetFieldName(p));
                sb.Append("= @");
                sb.Append(++i);

                if ((i+ skipCounter) != count) {
                    sb.Append(",");
                }
            }
            // Where Statement
            sb.Append(ConstructWhereClause(type, i));

            return sb.ToString();
        }

        public static string ConstructInsertStatement(Type type) {
            // INSERT INTO tablename (Column1, Column2) VALUES (Value1, Value2)
            StringBuilder sb = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();
            sbValues.Append("VALUES(");
            sb.Append("INSERT INTO  ");
            sb.Append(GetTableName(type));
            sb.Append("(");
            var i = 0;
            var skipCount = 0;
            var properties = type.GetProperties();
            var count = properties.Length;
            foreach (var p in properties) {
                i++;

                if (p.GetCustomAttributes(typeof(DataBaseGenerated), true).Length > 0) {
                    skipCount++;
                    continue;
                }

                sb.Append(GetFieldName(p));

                if (i != count) {
                    sb.Append(",");
                }

                sbValues.Append("@");
                sbValues.Append(i - skipCount);
                if (i != count) {
                    sbValues.Append(",");
                }
            }
            sbValues.Append(")");
            sb.Append(") ");

            // Where Statement

            return sb.Append(sbValues).ToString();
        }



        public static IList<T> ConvertDataTableToModel<T>(DataTable table) {
            IList<T> ret = new List<T>();
            var type = typeof(T);
            foreach (DataRow row in table.Rows) {
                T obj = (T)Activator.CreateInstance(type);
                foreach (DataColumn column in table.Columns) {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance);
                    if (prop == null) { continue; } else if (prop.PropertyType == typeof(int?)) {
                        if (row[column] == DBNull.Value || row[column].ToString() == "") {
                            prop.SetValue(obj, null, null);
                        } else {
                            prop.SetValue(obj, int.Parse(row[column].ToString()), null);
                        }
                    } else if (prop != null) prop.SetValue(obj, row[column].ToString() == "" || row[column] == DBNull.Value ? null : row[column], null);

                }
                ret.Add(obj);
            }

            return ret;
        }
        public static string ReplaceFirst(string text, string search, string replace) {
            int pos = text.IndexOf(search);
            if (pos < 0) return text;
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }


        private static string ConstructWhereClause(Type type, int i = 0) {
            StringBuilder sb = new StringBuilder();
            sb.Append(" WHERE ");
            var properties = type.GetProperties();
            var count = properties.Length;
            var temp = 0;
            foreach (var p in properties) {
                foreach (var attr in p.GetCustomAttributes(typeof(Key), true)) {
                    if (attr is Key) {
                        if ((++temp) != 1) {
                            sb.Append(" AND ");
                        }
                        sb.Append(GetFieldName(p));
                        sb.Append("= @");
                        sb.Append(++i);
                    }
                }


            }
            return sb.ToString();
        }
        public static string ConstructDeleteStatement(Type type) {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM ");
            sb.Append(GetTableName(type));
            //            sb.Append(ConstructWhereClause(type, 0));

            return sb.ToString();
        }

        private static string GetModelDefinition(DataTable table) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("namespace SimpleORM.Model {");
            sb.AppendLine(string.Format("\t[SimpleORM.Annotation.Table(\"{0}\")]", table.TableName));
            sb.AppendLine(string.Format("\tpublic class {0}{{", table.TableName));
            for (int i = 0; i < table.Columns.Count; i++) {
                sb.AppendLine(string.Format("\t\t[SimpleORM.Annotation.Column(\"{0}\")]", table.Columns[i].ColumnName));
                sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get;set;}}", table.Columns[i].DataType.Name, table.Columns[i].ColumnName));
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static void GenerateClass(string connectionString, string sql)
        {
            DataTable table = SQLExecutor.GetDataTable(sql, connectionString);

            var dataAccessFolder = string.Format("{0}\\DataAccess", Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            var modelFolder = string.Format("{0}\\Model", Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            
            if (!Directory.Exists(dataAccessFolder))
            {
                Directory.CreateDirectory(dataAccessFolder);
            }
            if (!Directory.Exists(modelFolder))
            {
                Directory.CreateDirectory(modelFolder);
            } 

            string modelClass = GetModelDefinition(table);
            string daoClass = GetDataAccessDefinition(table);
            File.WriteAllText(string.Format("{0}\\{1}Service.cs", dataAccessFolder, table.TableName), daoClass);
            File.WriteAllText(string.Format("{0}\\{1}.cs", modelFolder, table.TableName), modelClass);
        }

        private static string GetDataAccessDefinition(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using SimpleORM.Model;");
            sb.AppendLine("namespace SimpleORM.DataAccess {");
            sb.AppendLine(string.Format("\tpublic class {0}Service : DataAccess<{1}>{{", table.TableName, table.TableName));
            sb.AppendLine(string.Format("\t\tpublic {0}Service(string connStr): base(connStr){{}}", table.TableName));
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
