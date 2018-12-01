using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model.Table;
using NHibernate;

namespace LR2.Model.Repository
{
    class RepositoryActor : IRepository<Actor>
    {
        void IRepository<Actor>.Create(Actor obj)
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
        IList<Actor> IRepository<Actor>.Read()
        {
            return Singleton.Instance.OpenSession().CreateQuery("from Actor").List<Actor>();
        }
        IList<Actor> IRepository<Actor>.Read(string type, string param)
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT a FROM Actor a WHERE a." + type + " = :param");
            if (type == "Id") query.SetParameter("param", int.Parse(param));
            else query.SetParameter("param", param);
            return query.List<Actor>();
        }
        void IRepository<Actor>.Update(Actor obj)
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
        void IRepository<Actor>.Delete(string type, string param)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Actor a WHERE a." + type + " = :param");
                    if (type == "Id") query.SetParameter("param", int.Parse(param)).ExecuteUpdate();
                    else query.SetParameter("param", param).ExecuteUpdate();
                }
            }
        }
        void IRepository<Actor>.Delete(Actor obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    transaction.Commit();
                }
            }
        }

        long IRepository<Actor>.GetAmountElements()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT count(*) FROM Actor");
            return (long)query.UniqueResult();
        }
        
    }
}
