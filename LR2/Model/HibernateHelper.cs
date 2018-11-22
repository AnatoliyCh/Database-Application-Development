using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;


namespace LR2.Model
{
    class HibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var cfg = new Configuration();//как конфигурируемся
                    cfg.Configure();//ищем файл конфига
                    cfg.AddAssembly(typeof(Films).Assembly);
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
