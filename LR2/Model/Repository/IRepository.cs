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
        IList<T> Read();//вся таблица
        T Read(string type, string param);//читает type с param
        void Update(T obj);
        void Delete(string type, string param);//удаление по type
        //All
    }
}