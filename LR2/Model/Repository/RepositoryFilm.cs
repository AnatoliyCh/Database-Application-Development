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
        void IRepository<Film>.Create(Film obj)
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
        IList<Film> IRepository<Film>.Read()
        {
            return Singleton.Instance.OpenSession().CreateQuery("from Film").List<Film>();
        }
        Film IRepository<Film>.Read(string type, string param)
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param");
            if (type == "Id") query.SetParameter("param", int.Parse(param));
            else query.SetParameter("param", param);
            return query.List<Film>()[0];
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
                    IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Genre g WHERE g." + type + " = :param");
                    if (type == "Id") query.SetParameter("param", int.Parse(param)).ExecuteUpdate();
                    else query.SetParameter("param", param).ExecuteUpdate();
                }
            }
        }

        long IRepository<Film>.GetAmountElements()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT count(*) FROM Film");
            return (long)query.UniqueResult();
        }
    }
}
