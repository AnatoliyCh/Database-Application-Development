using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model.Table;
using System;
using NHibernate;

namespace LR2.Model.Repository
{
    class RepositoryFilm : IRepository<Film>
    {
        public enum ViewedType
        {
            Full = 0,
            TRUE = 1,
            FALSE = 2
        };
        void IRepository<Film>.Create(Film obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(obj);
                    transaction.Commit();
                }
            }
        }
        IList<Film> IRepository<Film>.Read()
        {
            return Singleton.Instance.OpenSession().CreateQuery("from Film").List<Film>();
        }
        IList<Film> IRepository<Film>.Read(string type, string param)
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param");
            if (type == "Id") query.SetParameter("param", int.Parse(param));
            else query.SetParameter("param", param);
            return query.List<Film>();
        }
        void IRepository<Film>.Update(Film obj)
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
        void IRepository<Film>.Delete(string type, string param)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Film f WHERE f." + type + " = :param");
                    if (type == "Id") query.SetParameter("param", int.Parse(param)).ExecuteUpdate();
                    else query.SetParameter("param", param).ExecuteUpdate();
                }
            }
        }
        void IRepository<Film>.Delete(Film obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(obj);
                    transaction.Commit();
                }
            }
        }
        long IRepository<Film>.GetAmountElements()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT count(*) FROM Film");
            return (long)query.UniqueResult();
        }
        public IList<Film> Search(string type, string param, ViewedType viewedType)
        {
            IQuery query;
            switch (viewedType)
            {
                case ViewedType.Full:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param");
                    query.SetParameter("param", param);
                    return query.List<Film>();
                case ViewedType.TRUE:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param and f.Viewed = true");
                    query.SetParameter("param", param);
                    return query.List<Film>();
                case ViewedType.FALSE:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param and f.Viewed = false");
                    query.SetParameter("param", param);
                    return query.List<Film>();
            }            
            return null;
        }
        public void JoinSearch()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT f from Film INNER JOIN SubTuble_Genres_Film s WHERE SubTuble_Genres_Film.Id_Film = 8 AND SubTuble_Genres_Film.Id_Genres = Боевик");
            IList<Film> list = query.List<Film>();
            foreach (var arr in list)
            {
                Console.WriteLine(arr.Title);
            }
        }
    }
}
