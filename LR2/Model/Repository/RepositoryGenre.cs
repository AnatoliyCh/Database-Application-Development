﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model.Table;
using System;
using NHibernate;

namespace LR2.Model.Repository
{
    class RepositoryGenre : IRepository<Genre>
    {
        void IRepository<Genre>.Create(Genre obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(obj);
                    try { transaction.Commit(); }
                    catch (Exception) { }
                }
            }
        }
        IList<Genre> IRepository<Genre>.Read()
        {
            return Singleton.Instance.OpenSession().CreateQuery("from Genre").List<Genre>();
        }
        IList<Genre> IRepository<Genre>.Read(string type, string param)
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT g FROM Genre g WHERE g." + type + " = :param");
            if (type == "Id") query.SetParameter("param", int.Parse(param));
            else query.SetParameter("param", param);
            return query.List<Genre>();
        }
        void IRepository<Genre>.Update(Genre obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(obj);
                    transaction.Commit();
                }
            }
        }
        void IRepository<Genre>.Delete(string type, string param)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Genre g WHERE g." + type + " = :param");
                    if (type == "Id")
                    {
                        DeleteCascadeTable(int.Parse(param));
                        query.SetParameter("param", int.Parse(param)).ExecuteUpdate();
                    }
                    else
                    {
                        DeleteCascadeTable(Singleton.Instance.Genre.Read(type, param)[0].Id);
                        query.SetParameter("param", param).ExecuteUpdate();
                    }
                }
            }
        }
        void IRepository<Genre>.Delete(Genre obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    DeleteCascadeTable(obj.Id);
                    session.Delete(obj);
                    transaction.Commit();
                }
            }
        }
        long IRepository<Genre>.GetAmountElements()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT count(*) FROM Genre");
            return (long)query.UniqueResult();
        }        

        //удаляем в связанных таблицаx
        private void DeleteCascadeTable(int id)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Films_Genres fg WHERE fg.Id_Genres = :param");
                    query.SetParameter("param", id).ExecuteUpdate();
                }
            }
        }
    }
}