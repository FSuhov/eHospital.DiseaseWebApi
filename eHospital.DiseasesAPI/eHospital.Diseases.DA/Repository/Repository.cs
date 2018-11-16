using eHospital.Diseases.DA.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace eHospital.Diseases.DA.Repository
{
    public class Repository<T> : IRepository<T> where T : class
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
            return _entities.AsNoTracking();
        }

        public IQueryable<T> GetAll(Expression <Func<T,bool>> predicate)
        {
            return _entities.Where(predicate).AsNoTracking();
        }

        public T Get(int id)
        {
            return _entities.Find(id);
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
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
