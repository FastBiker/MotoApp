﻿using MotoApp.Entities;

namespace MotoApp.Repositories
{
    public interface IWriteRepository<in T> where T : class, IEntity
    {
        void Add(T entity);
        void Remove(T entity);
        void Save();
    }
}
