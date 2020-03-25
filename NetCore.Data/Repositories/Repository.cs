using System;
using System.Collections.Generic;
using System.Linq;
using NetCore.Data.Domain;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Reflection;

namespace NetCore.Data.Repositories
{
    public abstract class Repository<TEntity> where TEntity : Entity, new()
    {
        //默认60S的数据库执行超时时间
        private readonly int commandTimeout = 60;

        /// <summary>
        /// 
        /// </summary>
        private DbConnectionFactory dbConnectionFactory;

        /// <summary>
        /// 
        /// </summary>
        public System.Data.IDbConnection Connection { get { return dbConnectionFactory.Connection; } }

        /// <summary>
        /// 表名称
        /// </summary>
        protected string TableName => GetTableName(EntityType);

        /// <summary>
        /// 主键名称
        /// </summary>
        protected string PrimaryKeyName => GetPrimaryKeyName(EntityType);

        private Type EntityType => typeof(TEntity);


        public Repository(DbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="entity">对象</param>
        /// <returns></returns>
        public virtual long Insert(TEntity entity)
        {
            return Connection.Insert(entity, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Update(TEntity entity)
        {
            return Connection.Update(entity, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        public virtual bool Delete(object id)
        {
            string sql = $"delete from [{TableName}] where [{PrimaryKeyName}] = @id";
            return Connection.Execute(sql, new { id }) > 0;
        }

        /// <summary>
        /// 根据ID集合删除所有符合条件的对象
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual bool Delete(IList<object> primaryKeyValues)
        {
            if (primaryKeyValues == null || !primaryKeyValues.Any())
            {
                return true;
            }
            string sql = $"delete from [{TableName}] where [{PrimaryKeyName}] in @primaryKeyValues";
            return Connection.Execute(sql, new { primaryKeyValues }) > 0;
        }

        /// <summary>
        /// 根据Id获取对象
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>对象</returns>
        public virtual TEntity FirstOrDefault(object primaryKeyValue)
        {
            string sql = $"select * from [{TableName}] where [{PrimaryKeyName}] = @primaryKeyValue";
            return Connection.QueryFirstOrDefault<TEntity>(sql, new { primaryKeyValue });
        }

        /// <summary>
        /// 有条件的查询
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(string whereCondition, object param = null)
        {
            string sql = $"select * from [{TableName}] where {whereCondition}";
            return Connection.QueryFirstOrDefault<TEntity>(sql, param);
        }

        /*protected IDbTransaction _IDbTransaction;
        protected string _guid;
        public string BeginTransaction()
        {
            if (this._IDbTransaction == null)
            {
                this._IDbTransaction = this.Connection.BeginTransaction();
                _guid = Guid.NewGuid().ToString();
                return _guid;
            }

            return string.Empty;
        }

        //提交，根据开始事务产生的GUID
        public void Commit(string guid)
        {
            if (this._IDbTransaction != null && this._guid == guid)
                this._IDbTransaction.Commit();
        }

        //注意多重调用，其中嵌套调用报错后，也需要释放
        public void Rollback(string guid)
        {
            if (this._IDbTransaction != null && this._guid == guid)
                this._IDbTransaction.Rollback();
        }*/

        /// <summary>  
        /// 获取表名  
        /// </summary>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        private static string GetTableName(Type type)
        {
            object[] attributes = type.GetCustomAttributes(false);
            foreach (var attr in attributes)
            {
                if (attr is TableAttribute)
                {
                    var tableAttribute = attr as TableAttribute;
                    return tableAttribute.Name;
                }
            }
            return type.Name;
        }

        private static string GetPrimaryKeyName(Type type)
        {
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            {
                var attributes = field.FieldType.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (attribute is ExplicitKeyAttribute)
                    {
                        return field.Name;
                    }
                }

            }
            throw new ArgumentException("Entity has no primary key.");
        }
    }
}
