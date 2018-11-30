using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model;
using LR2.View;
using LR2.Model.Table;

namespace LR2.Controller
{
    class ControllFilm : IControll
    {
        public void Read()
        {
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Film.PrintTable();
                PrintCMD.Film.ReadMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
        public void Create()
        {
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Film.PrintTable();
                PrintCMD.Film.CreateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        string newFilm = PrintCMD.ReadLine("Новый фильм", false);
                        SubCreate(new Film { Title = newFilm });
                        //Singleton.Instance.Film.Create(new Film { Title = newFilm });
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
        public void Update()
        {
            int key;
            Film film;//то что изменяем
            string tmpTitle;//новое название
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Film.PrintTable();
                PrintCMD.Film.UpdateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1://изменение по Id
                        int id = PrintCMD.ReadKey("Id", false);
                        film = Singleton.Instance.Film.Read("Id", id.ToString());
                        tmpTitle = PrintCMD.ReadLine(film.Title, false);
                        film.Title = tmpTitle;
                        Singleton.Instance.Film.Update(film);
                        film = null; tmpTitle = null;
                        break;
                    case 2://изменение по Title
                        tmpTitle = PrintCMD.ReadLine("Имя", false);
                        film = Singleton.Instance.Film.Read("Name", tmpTitle);
                        tmpTitle = PrintCMD.ReadLine(film.Title, false);
                        film.Title = tmpTitle;
                        Singleton.Instance.Film.Update(film);
                        film = null; tmpTitle = null;
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
        public void Delete()
        {
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Film.PrintTable();
                PrintCMD.Film.DeleteMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1://удаление по Id
                        int id = PrintCMD.ReadKey("Id", false);
                        Singleton.Instance.Film.Delete("Id", id.ToString());
                        break;
                    case 2://удаление по Name
                        string tmpTitle = PrintCMD.ReadLine("Имя", false);
                        Singleton.Instance.Film.Delete("Name", tmpTitle);
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
        /// <summary>
        /// VOID
        /// </summary>
        public void Search()
        {
            throw new NotImplementedException();
        }
        //дополнительные функции
        private void SubCreate(Film film)//меню создания фильма
        {
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Film.CreateFilm(film);
                PrintCMD.Film.SubCreateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        string newFilm = PrintCMD.ReadLine("Новый фильм", false);
                        SubCreate(new Film { Title = newFilm });
                        //Singleton.Instance.Film.Create(new Film { Title = newFilm });
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
    }
}
