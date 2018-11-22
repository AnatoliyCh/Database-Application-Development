using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace LR2.Model
{
    class Repository
    {
        public void Add(Genres genres)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(genres);
                    transaction.Commit();
                }
            }
        }
        public void Add(Actors actors)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(actors);
                    transaction.Commit();
                }
            }
        }
        public void Add(Films films)
        {
            using (ISession session = HibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(films);
                    transaction.Commit();
                }
            }
        }
    }
}
