using MyLayercake.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyLayercake.Core.DataAccess {
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : IEntity<Guid>, new() {
        public IDatabaseContext Context { get; set; }

        public RepositoryBase(IDatabaseContext context) {
            Context = context ?? throw new ArgumentException("Database Context has not been initialized");
        }

        public void Delete(TEntity entity) {
            using var command = Context.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = entity.BuildDeleteText();
            command.AddDeleteParamaters(entity);

            command.ExecuteNonQuery();
        }

        public Task DeleteAsync(TEntity entity) {
            return Task.Run(() => Delete(entity));
        }

        public void DeleteMany(IEnumerable<TEntity> entities) {
            foreach (var entity in entities) {
                Delete(entity);
            }
        }

        public Task DeleteManyAsync(IEnumerable<TEntity> entities) {
            return Task.Run(() => DeleteMany(entities));
        }

        public IEnumerable<TEntity> GetAll() {
            using var command = Context.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = typeof(TEntity).BuildSelectText();

            return command.ExecuteReader().DomainObjects<TEntity>();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync() {
            return Task.Run(() => GetAll());
        }

        public TEntity GetById(object oid) {
            using var command = Context.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = typeof(TEntity).BuildSelectText();
            command.AddSelectByIDParamaters<TEntity>(oid);

            return command.ExecuteReader().DomainObject<TEntity>();
        }

        public Task<TEntity> GetByIdAsync(object oid) {
            return Task.Run(() => GetById(oid));
        }

        public void Insert(TEntity entity) {
            using var command = Context.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = entity.BuildInsertText();
            command.AddInsertParamaters(entity);

            command.ExecuteNonQuery();
        }

        public Task InsertAsync(TEntity entity) {
            return Task.Run(() => Insert(entity));
        }

        public void InsertMany(IEnumerable<TEntity> entities) {
            foreach (var entity in entities) {
                Insert(entity);
            }
        }

        public Task InsertManyAsync(IEnumerable<TEntity> entities) {
            return Task.Run(() => InsertMany(entities));
        }

        public void Update(TEntity entity) {
            using var command = Context.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = entity.BuildUpdateText();
            command.AddUpdateParamaters(entity);

            command.ExecuteNonQuery();
        }

        public Task UpdateAsync(TEntity entity) {
            return Task.Run(() => Update(entity));
        }

        public void UpdateMany(IEnumerable<TEntity> entities) {
            foreach (var entity in entities) {
                Update(entity);
            }
        }

        public Task UpdateManyAsync(IEnumerable<TEntity> entities) {
            return Task.Run(() => UpdateMany(entities));
        }

        public void Dispose() {
            if (Context != null)
                Context.Dispose();
        }
    }
}
