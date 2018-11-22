using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Dialect;
using LR2.Model;

namespace LR2
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadNhCFG();
            var repos = new Repository();
            var tt = new Genres
            {
                Title = "cnh"
            };
            var t2 = new Actors
            {
                Name = "77"
            };
            IList<Genres> Gen= new List<Genres>();
            Gen.Add(tt);
            
            //create
            repos.Add(tt);
            repos.Add(t2);
            new Repository().Add(new Actors { Name = "s" });
            new Repository().Add(new Films { Title = "f", GenresList = Gen, Rating = 1.55f, Viewed = true });
            Gen.Add(new Genres { Title = "tth" });
            new Repository().Add(new Films { Title = "f1", GenresList = Gen, Rating = 1.55f, Viewed = true });
            //read
            //update
            //delete

            //Console.ReadKey();
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Write("\n [ESC] Exit \n [1] Просмотр \n [2] Добавление / Редактирование \n [3] Удаление \n [4] Поиск \n");
                //Console.ReadKey();
            }
        }
        public static void LoadNhCFG()
        {
            var cfg = new Configuration();//как конфигурируемся
            cfg.Configure();//ищем файл конфига
            cfg.AddAssembly(typeof(Films).Assembly);
            new SchemaExport(cfg).Execute(true, true, false);
        }
    }
}
