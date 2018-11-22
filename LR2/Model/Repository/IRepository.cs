using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace LR2.Model.Repository
{
    interface IRepository<T> where T : class
    {
        //CRUD
        void Create(T obj);
        IQuery Read();
        void Update(T obj);
        void Delete(T obj);        
    }
}