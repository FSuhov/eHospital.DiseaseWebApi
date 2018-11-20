using EHospital.Diseases.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EHospital.Diseases.Data
{
    public class Repository<T> : IRepository<T> where T: class, ISoftDeletable
    {
        private readonly DiseaseDBContext _context;

        private DbSet<T> _entities;

        public Repository(DiseaseDBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities.Where(e => e.IsDeleted != true).AsNoTracking();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(e => e.IsDeleted != true).Where(predicate).AsNoTracking();
        }

        public T Get(int id)
        {
            T item = _entities.Find(id);
            if (item.IsDeleted != true)
            {
                return item;
            }

            return null;
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Tried to insert null entity!");
            }

            _entities.Add(entity);

            return entity;
        }

        public T Update(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public T Delete(T entity)
        {
            entity.IsDeleted = true;
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }
    }
}
