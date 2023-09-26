﻿using System.Linq.Expressions;

namespace IDataAccess
{
    public interface IService<T>
    {
        public void Add(T entity);
        public void Delete(T entity);
        public void Update(T entity);
        public IEnumerable<T> GetAll();
        public T Get(T entity);
        public bool Exists(T entity);

        public void Save();
        //ICollection<T> FindByCondition(Expression<Func<T, bool>> condition);
    }
}
