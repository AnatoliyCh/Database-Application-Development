using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model;
using LR2.View;
using LR2.Model.Table;
using NHibernate;

namespace LR2.Controller
{
    class ControllGenre : IControll
    {
        public void Read()
        {
            int key;
            bool tmp = true;
            while (tmp)
            {                
                PrintCMD.Genre.PrintTable();
                PrintCMD.Genre.ReadMenu();
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
                PrintCMD.Genre.PrintTable();
                PrintCMD.Genre.CreateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        string newGenre = PrintCMD.ReadLine("Новый жанр", false);
                        Singleton.Instance.Genre.Create(new Genre { Title = newGenre });
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
            Genre genre;//то что изменяем
            string tmpTitle;//новое название
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Genre.PrintTable();
                PrintCMD.Genre.UpdateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1://изменение по Id
                        int id = PrintCMD.ReadKey("Id", false);
                        genre = Singleton.Instance.Genre.Read("Id", id.ToString())[0];
                        tmpTitle = PrintCMD.ReadLine(genre.Title, false);
                        genre.Title = tmpTitle;
                        Singleton.Instance.Genre.Update(genre);
                        genre = null; tmpTitle = null;
                        break;
                    case 2://изменение по Title
                        tmpTitle = PrintCMD.ReadLine("Название", false);
                        genre = Singleton.Instance.Genre.Read("Title", tmpTitle)[0];
                        tmpTitle = PrintCMD.ReadLine(genre.Title, false);
                        genre.Title = tmpTitle;
                        Singleton.Instance.Genre.Update(genre);
                        genre = null; tmpTitle = null;
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
                PrintCMD.Genre.PrintTable();
                PrintCMD.Genre.DeleteMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1://удаление по Id
                        int id = PrintCMD.ReadKey("Id", false);
                        Singleton.Instance.Genre.Delete("Id", id.ToString());                     
                        break;
                    case 2://удаление по Title
                        string tmpTitle = PrintCMD.ReadLine("Название", false);
                        Singleton.Instance.Genre.Delete("Title", tmpTitle);
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
    }
}