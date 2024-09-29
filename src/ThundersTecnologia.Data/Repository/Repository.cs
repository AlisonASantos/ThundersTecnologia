using ThundersTecnologia.Business.Intefaces;
using ThundersTecnologia.Business.Models;
using ThundersTecnologia.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ThundersTecnologia.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> GetId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            DbSet.Add(entity);
            await Db.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
