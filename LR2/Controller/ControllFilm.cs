﻿using System;
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
            Film film = new Film();//просметреваемый
            while (tmp)
            {
                PrintCMD.Film.PrintTable();
                PrintCMD.Film.ReadMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        int id = PrintCMD.ReadKey("Id", false);
                        film = Singleton.Instance.Film.Read("Id", id.ToString())[0];
                        PrintCMD.Film.CurrentFilm(film);
                        PrintCMD.ReadKey("enter -> 1", false);
                        film = null;
                        break;
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
                        film = Singleton.Instance.Film.Read("Id", id.ToString())[0];
                        tmpTitle = PrintCMD.ReadLine(film.Title, false);
                        film.Title = tmpTitle;
                        Singleton.Instance.Film.Update(film);
                        film = null; tmpTitle = null;
                        break;
                    case 2://изменение по Title
                        tmpTitle = PrintCMD.ReadLine("Имя", false);
                        film = Singleton.Instance.Film.Read("Name", tmpTitle)[0];
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
            string strTmp;//строковый параметр
            int tmpId;//числ параметр
            while (tmp)
            {
                PrintCMD.Film.CurrentFilm(film);
                PrintCMD.Film.SubCreateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1://назвение
                        strTmp = PrintCMD.ReadLine("название", false);
                        film.Title = strTmp;
                        break;
                    case 2://жанр
                        if (Singleton.Instance.Genre.GetAmountElements() > 0)
                        {
                            PrintCMD.Genre.PrintTable();
                            tmpId = PrintCMD.ReadKey("Id жанра", false);
                            Genre genre = Singleton.Instance.Genre.Read("Id", tmpId.ToString())[0];
                            if (!EqualsGenre(film.GenresList as List<Genre>, genre))
                                film.GenresList.Add(genre);                         
                        }                            
                        break;
                    case 3://актёры
                        if (Singleton.Instance.Actor.GetAmountElements() > 0)
                        {
                            PrintCMD.Actor.PrintTable();
                            tmpId = PrintCMD.ReadKey("Id актёра", false);
                            Actor actor = Singleton.Instance.Actor.Read("Id", tmpId.ToString())[0];
                            if (!EqualsActor(film.ActorsList as List<Actor>, actor))
                                film.ActorsList.Add(actor);
                        }
                        break;
                    case 4://рейтинг
                        strTmp = PrintCMD.ReadLine("рейтинг", false);
                        film.Rating = Convert.ToSingle(strTmp);
                        break;
                    case 5://просмотрено
                        film.Viewed = !film.Viewed;
                        break;
                    case 6://готово
                        Singleton.Instance.Film.Create(film);
                        tmp = false;
                        break;
                    case 0://назад
                        tmp = false;
                        break;
                }
            }
        }
        private bool EqualsGenre(List<Genre> collection, Genre i)
        {
            foreach (var item in collection)
            {
                if (item.Title == i.Title)
                    return true;
            }
            return false;
        }
        private bool EqualsActor(List<Actor> collection, Actor i)
        {
            foreach (var item in collection)
            {
                if (item.Name == i.Name)
                    return true;
            }
            return false;
        }
    }
}
