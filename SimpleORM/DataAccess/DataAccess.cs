using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleORM.Annotation;
using SimpleORM.Util;

namespace SimpleORM.DataAccess {
    public abstract class DataAccess<T> {

        protected string _connectionString;
        protected DataAccess(string connStr) {
            _connectionString = connStr;
        }

        public IList<T> ReadAll() {
            var table = SQLExecutor.GetDataTable(ORMUtil.ConstructSelectStatement(typeof(T)), _connectionString);
            return ORMUtil.ConvertDataTableToModel<T>(table);
        }

        public void AddOrUpdate(T model) {
            // Get Key
            var dictionary = new Dictionary<string, object>();
            foreach (var propertyInfo in typeof(T).GetProperties()) {
                foreach (var customAttribute in propertyInfo.GetCustomAttributes(typeof(Key), true)) {
                    dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(model, null));
                }
            }

            if (ReadByKey(dictionary).Count == 0) {
                // Insert
                Insert(model);
            } else {
                // Update
                Update(model);
            }
        }
        public void Update(T model) {
            string sql = ORMUtil.ConstructUpdateStatement(model.GetType());

            var objs = GetAllParamArrays(model);
            SQLExecutor.ExecuteNonQuery(sql, _connectionString, objs.ToArray());
        }

        public void Insert(T model) {
            string sql = ORMUtil.ConstructInsertStatement(model.GetType());
            var objs = GetStatementParamArray(model);
            SQLExecutor.ExecuteNonQuery(sql, _connectionString, objs.ToArray());

        }

        protected void DeleteByKey(IDictionary<string, object> conditions) {
            StringBuilder sb = new StringBuilder();
            sb.Append(ORMUtil.ConstructDeleteStatement(typeof(T)));
            sb.Append(ConstructWhereByDictionary(conditions));
            SQLExecutor.ExecuteNonQuery(sb.ToString(), _connectionString, conditions.Values.ToArray());

        }


        protected List<object> GetStatementParamArray(T model) {
            var objs = new List<object>();
            foreach (var prop in model.GetType().GetProperties()) {
                if (prop.GetCustomAttributes(typeof(DataBaseGenerated), true).Length > 0) {
                    continue;
                }
                objs.Add(prop.GetValue(model, null));
            }

            return objs;
        }

        protected List<object> GetKeyParamArray(T model) {
            var objsKey = new List<object>();
            foreach (var prop in model.GetType().GetProperties()) {
                foreach (var attr in prop.GetCustomAttributes(typeof(Key), true)) {
                    if (attr is Key) {
                        objsKey.Add(prop.GetValue(model, null));
                    }
                }
            }

            return objsKey;
        }


        protected List<object> GetAllParamArrays(T model) {
            var objs = GetStatementParamArray(model);
            var objsKey = GetKeyParamArray(model);
            objs.AddRange(objsKey);
            return objs;
        }


        protected IList<T> ReadByKey(IDictionary<string, object> conditions) {
            StringBuilder sb = new StringBuilder();
            sb.Append(ORMUtil.ConstructSelectStatement(typeof(T)));
            sb.Append(ConstructWhereByDictionary(conditions));
            return ORMUtil.ConvertDataTableToModel<T>(SQLExecutor.GetDataTable(sb.ToString(), _connectionString, conditions.Values.ToArray()));
        }

        private string ConstructWhereByDictionary(IDictionary<string, object> conditions) {
            StringBuilder sb = new StringBuilder();
            sb.Append(" WHERE ");
            for (int index = 0; index < conditions.Count; index++) {
                if (index != 0) {
                    sb.Append(" AND ");
                }
                var item = conditions.ElementAt(index);
                var itemKey = item.Key;
                sb.Append(itemKey);
                sb.Append("=@");
                sb.Append(index + 1);
            }
            return sb.ToString();
        }
    }
}
