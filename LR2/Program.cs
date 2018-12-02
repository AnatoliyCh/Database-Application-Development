using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model;
using LR2.View;
using LR2.Controller;
using LR2.Model.Table;
using NHibernate;

namespace LR2
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton.Instance.OpenSession();//открываем сессию
            CreateBeginItem();
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.MenuUP();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        Controll.Genre();
                        break;
                    case 2:
                        Controll.Actor();
                        break;
                    case 3:
                        Controll.Film();
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
        static void CreateBeginItem()
        {
            Singleton.Instance.Genre.Create(new Genre { Title = "Боевик" });
            Singleton.Instance.Genre.Create(new Genre { Title = "Вестерн" });
            Singleton.Instance.Genre.Create(new Genre { Title = "Детектив" });
            Singleton.Instance.Actor.Create(new Actor { Name = "А. Невский" });
            Singleton.Instance.Actor.Create(new Actor { Name = "кто-то" });
            Singleton.Instance.Actor.Create(new Actor { Name = "чувак" });

            Film t = new Film()
            {
                Title = "tt",
                Viewed = false,
                Rating = 0
            };
            t.GenresList.Add(Singleton.Instance.Genre.Read("Id", "1")[0]);
            t.GenresList.Add(Singleton.Instance.Genre.Read("Id", "2")[0]);
            Singleton.Instance.Film.Create(t);

            Film t1 = new Film()
            {
                Title = "t1",
                Viewed = true,
                Rating = 4
            };
            t1.GenresList.Add(Singleton.Instance.Genre.Read("Id", "2")[0]);
            t1.GenresList.Add(Singleton.Instance.Genre.Read("Id", "3")[0]);
            Singleton.Instance.Film.Create(t1);

            Film t2 = new Film()
            {
                Title = "t2",
                Viewed = false,
                Rating = 8
            };
            t2.GenresList.Add(Singleton.Instance.Genre.Read("Id", "3")[0]);
            t2.GenresList.Add(Singleton.Instance.Genre.Read("Id", "1")[0]);
            Singleton.Instance.Film.Create(t2);

            Film t3 = new Film()
            {
                Title = "t3",
                Viewed = true,
                Rating = 12
            };
            t3.GenresList.Add(Singleton.Instance.Genre.Read("Id", "1")[0]);
            t3.GenresList.Add(Singleton.Instance.Genre.Read("Id", "2")[0]);
            Singleton.Instance.Film.Create(t3);
        }
    }
}
