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
                Title = "tt"
            };
            t.GenresList.Add(Singleton.Instance.Genre.Read("Id", "1")[0]);
            t.GenresList.Add(Singleton.Instance.Genre.Read("Id", "2")[0]);
            //Singleton.Instance.Film.Create(t);
        }
    }
}
