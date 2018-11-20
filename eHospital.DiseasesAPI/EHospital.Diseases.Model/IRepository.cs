using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EHospital.Diseases.Model
{
    /// <summary>
    /// Generic Interface to be implemented for separating data source
    /// </summary>
    /// <typeparam name="T">Data Type of Database Entry </typeparam>
    public interface IRepository<T> where T : ISoftDeletable
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        T Get(int id);

        T Insert(T entity);

        T Update(T entity);

        T Delete(T entity);
    }
}
