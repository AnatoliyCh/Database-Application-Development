﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model.Repository;
using LR2.Model.Table;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace LR2.Model
{
    class Singleton
    {
        private static Singleton _instance;        
        private static ISessionFactory _sessionFactory;
        public IRepository<Genre> Genre { get; } = new RepositoryGenre();
        public IRepository<Actor> Actor { get; } = new RepositoryActor();
        public IRepository<Film> Film { get; } = new RepositoryFilm();
        private Singleton() { }
        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                    if (_sessionFactory == null)
                    {
                        var cfg = new Configuration();//как конфигурируемся
                        cfg.Configure();//ищем файл конфига
                        cfg.AddAssembly("LR2");
                        _sessionFactory = cfg.BuildSessionFactory();
                        new SchemaExport(cfg).Execute(true, true, false);
                    }
                }
                return _instance;
            }
        }
        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
