using Microsoft.EntityFrameworkCore;
using LeaveRequest.Context;
using LeaveRequest.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequest.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly Context conn;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(Context conn)
        {
            this.conn = conn;
            entities = conn.Set<Entity>();
        }
        public int Delete(Key key)
        {
            var entity = entities.Find(key);
            entities.Remove(entity);
            var result = conn.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = conn.SaveChanges();
            return result;
        }

        public int Update(Entity entity)
        {
            conn.Entry(entity).State = EntityState.Modified;
            var result = conn.SaveChanges();
            return result;
        }
    }
}
