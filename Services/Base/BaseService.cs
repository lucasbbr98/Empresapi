using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Dapper;

namespace Services
{
    using Models;
    using Models.Attributes;

    public abstract class BaseService<T> where T : Model
    {
        public IConfiguration _configuration;
        private readonly ILogger<T> logger;
        public IConfiguration config;

        public BaseService(IConfiguration configuration, ILogger<T> logger) { _configuration = configuration; this.logger = logger; }


        public virtual string DbName { get; set; }

        public string GetConnection() => _configuration.GetSection("Connections").GetSection("ConnectionString").Value;

        public async Task<HttpStatusCode> Add(T obj)
        {
            try
            {
                string sql = InsertQuery<T>();
                using (var con = new MySqlConnection(GetConnection()))
                    await con.ExecuteAsync(sql, obj);
                return HttpStatusCode.OK;
            }
            catch (MySqlException e)
            {
                logger.LogError(e, e.Message);
                return MySqlExpcetionMapper.ExceptionHttpCode(e);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<T> Get(int id, string fields = null, bool showDeactivated = false)
        {
            try
            {

                string selectQuery = $"*";
                if (fields != null)
                    selectQuery = fields;

                string query = $"SELECT {selectQuery} FROM `{DbName}` WHERE Id = @Id AND DeactivatedOn IS NULL";
                if (showDeactivated)
                    query = $@"SELECT {selectQuery} FROM `{DbName}` WHERE Id = @Id";


                using (var con = new MySqlConnection(GetConnection()))
                    return await con.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return null;
            }
        }

        public async Task<T> GetWhere(object list, string fields = null, bool showDeactivated = false)
        {
            try
            {
                if (list == null)
                    return null;

                var parameterSqlString = "";
                Dictionary<string, object> values = PropertyValuesDict(list);

                foreach (KeyValuePair<string, object> entry in values)
                    parameterSqlString = parameterSqlString + entry.Key + " = @" + entry.Key + " AND ";

                string selectQuery = $"*";
                if (fields != null)
                    selectQuery = fields;

                string query = $"SELECT {selectQuery} FROM `{DbName}` WHERE {parameterSqlString} DeactivatedOn IS NULL";
                if (showDeactivated)
                    query = $@"SELECT {selectQuery} FROM `{DbName}` WHERE {parameterSqlString}";

                using (var con = new MySqlConnection(GetConnection()))
                    return await con.QueryFirstOrDefaultAsync<T>(query, list);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAll(string fields = null, bool showDeactivated = false)
        {
            try
            {
                string selectQuery = $"*";
                if (fields != null)
                    selectQuery = fields;

                string query = $@"SELECT {selectQuery} FROM `{DbName}` WHERE DeactivatedOn IS NULL";
                if (showDeactivated)
                    query = $@"SELECT {selectQuery} FROM `{DbName}`";

                using (var con = new MySqlConnection(GetConnection()))
                    return await con.QueryAsync<T>(query);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAllWhere(object list, string fields = null, bool showDeactivated = false)
        {
            try
            {
                if (list == null)
                    return null;

                var parameterSqlString = "";
                Dictionary<string, object> values = PropertyValuesDict(list);

                foreach (KeyValuePair<string, object> entry in values)
                    parameterSqlString = parameterSqlString + entry.Key + "= @" + entry.Key + " AND ";

                string selectQuery = $"*";
                if (fields != null)
                    selectQuery = fields;

                string query = $@"SELECT {selectQuery} FROM `{DbName}` WHERE {parameterSqlString} DeactivatedOn IS NULL";
                if(showDeactivated)
                    query = $@"SELECT {selectQuery} FROM `{DbName}` WHERE {parameterSqlString}".Remove($@"SELECT {selectQuery} FROM `{DbName}` WHERE {parameterSqlString}".LastIndexOf("AND"), "AND".Length);

                using (var con = new MySqlConnection(GetConnection()))
                    return await con.QueryAsync<T>(query, list);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return null;
            }
        }

        public async Task<bool?> Exists(int id)
        {
            try
            {
                string query = $@"SELECT count(1) from `{DbName}` WHERE Id = @Id AND DeactivatedOn IS NULL";
                using (var con = new MySqlConnection(GetConnection()))
                    return await con.ExecuteScalarAsync<bool>(query, new { Id = id });
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return null;
            }
        }

        public async Task<bool?> ExistsWhere(object list)
        {
            try
            {
                if (list == null)
                    return null;

                var parameterSqlString = "";
                Dictionary<string, object> values = PropertyValuesDict(list);

                foreach (KeyValuePair<string, object> entry in values)
                    parameterSqlString = parameterSqlString + entry.Key + "= @" + entry.Key + " AND ";

                string query = $@"SELECT count(1) from `{DbName}` WHERE {parameterSqlString} DeactivatedOn IS NULL";
                using (var con = new MySqlConnection(GetConnection()))
                    return await con.ExecuteScalarAsync<bool>(query, list);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return null;
            }
        }

        public async Task<HttpStatusCode> Deactivate(T obj)
        {
            try
            {
                if (obj.DeactivatedOn == null)
                    obj.DeactivatedOn = DateTime.Now;

                string sql = $@"
                    UPDATE `{DbName}` 
					SET
					    `DeactivatedOn` = @DeactivatedOn
                    WHERE
					    `Id` = @Id";

                using (var con = new MySqlConnection(GetConnection()))
                    await con.ExecuteAsync(sql, obj);
                return HttpStatusCode.OK;
            }
            catch (MySqlException e)
            {
                return MySqlExpcetionMapper.ExceptionHttpCode(e);
            }
        }

        public async Task<HttpStatusCode> Delete(T obj)
        {
            try
            {
                string sql = $@"DELETE FROM `{DbName}` WHERE `Id` = @Id";

                using (var con = new MySqlConnection(GetConnection()))
                    await con.ExecuteAsync(sql, obj);
                return HttpStatusCode.OK;
            }
            catch (MySqlException e)
            {
                logger.LogError(e, e.Message);
                return MySqlExpcetionMapper.ExceptionHttpCode(e);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> DeleteWhere(object list)
        {
            try
            {
                if (list == null)
                    return HttpStatusCode.UnprocessableEntity;

                var parameterSqlString = "";
                Dictionary<string, object> values = PropertyValuesDict(list);

                foreach (KeyValuePair<string, object> entry in values)
                    parameterSqlString = parameterSqlString + entry.Key + "= @" + entry.Key + " AND ";

                string sql = $@"DELETE FROM `{DbName}` WHERE {parameterSqlString} DeactivatedOn IS NULL";

                using (var con = new MySqlConnection(GetConnection()))
                    await con.ExecuteAsync(sql, list);
                return HttpStatusCode.OK;
            }
            catch (MySqlException e)
            {
                logger.LogError(e, e.Message);
                return MySqlExpcetionMapper.ExceptionHttpCode(e);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> Update(T obj, string updateFields = null)
        {
            try
            {
                string sql = UpdateQuery<T>(updateFields);

                using (var con = new MySqlConnection(GetConnection()))
                    await con.ExecuteAsync(sql, obj);
                return HttpStatusCode.OK;
            }
            catch (MySqlException e)
            {
                logger.LogError(e, e.Message);
                return MySqlExpcetionMapper.ExceptionHttpCode(e);
            }
        }

        #region Query Builders
        private string UpdateQuery<T>(string updateFields = null)
        {

            var properties = new List<string>();
            foreach (var p in typeof(T).GetProperties())
            {
                var pAttr = (QueryIgnoreAttribute[])p.GetCustomAttributes(typeof(QueryIgnoreAttribute), false);
                if (pAttr.Length > 0)
                {
                    IgnoreType ignoreType = pAttr[0].Ignore;
                    if (ignoreType == IgnoreType.All || ignoreType == IgnoreType.Update || ignoreType == IgnoreType.InsertAndUpdate)
                        continue;
                }
                if (updateFields == null)
                    properties.Add(p.Name);
                else if (updateFields.Contains(p.Name))
                    properties.Add(p.Name);
            }
            var propQueries = string.Join(", ", properties.Select(p => $"{p} = @{p}"));
            return $@"
                    UPDATE `{DbName}` 
					SET
                        {propQueries}
                    WHERE
					    `Id` = @Id".Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty); ;
        }

        private string InsertQuery<T>()
        {
            var properties = new List<string>();
            foreach (var p in typeof(T).GetProperties())
            {
                var pAttr = (QueryIgnoreAttribute[])p.GetCustomAttributes(typeof(QueryIgnoreAttribute), false);
                if (pAttr.Length > 0)
                {
                    IgnoreType ignoreType = pAttr[0].Ignore;
                    if (ignoreType == IgnoreType.All || ignoreType == IgnoreType.Insert || ignoreType == IgnoreType.InsertAndUpdate)
                        continue;
                }
                properties.Add(p.Name);
            }

            var propRight = "@" + properties.Aggregate((i, j) => $"{i}, @{j}");
            var propLeft = string.Join(", ", properties);
            return $"INSERT INTO `{DbName}` ({propLeft}) Values({propRight})".Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty); ;
        }

        #endregion

        private static Dictionary<string, object> PropertyValuesDict(object list)
        {
            return ((object)list).GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(list));
        }
    }
}