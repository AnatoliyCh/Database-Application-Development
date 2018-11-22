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
    class ControllGenre
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
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Genre.PrintTable();
                PrintCMD.Genre.UpdateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        int id = PrintCMD.ReadKey("Id", false);
                        IQuery query1 = Singleton.Instance.Query("select g from Genre g where g.Id =:id");
                        query1.SetParameter("id", id);
                        string tmpTitle1 = PrintCMD.ReadLine(query1.List<Genre>()[0].Title, false);
                        query1.List<Genre>()[0].Title = tmpTitle1;
                        Singleton.Instance.Genre.Update(query1.List<Genre>()[0]);
                        break;
                    case 2:
                        string tmpTitle2 = PrintCMD.ReadLine("Название", false);
                        IQuery query2 = Singleton.Instance.Query("select g from Genre g where g.Title =:title");
                        query2.SetParameter("title", tmpTitle2);
                        tmpTitle2 = PrintCMD.ReadLine(query2.List<Genre>()[0].Title, false);
                        query2.List<Genre>()[0].Title = tmpTitle2;
                        Singleton.Instance.Genre.Update(query2.List<Genre>()[0]);
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
                    case 1:
                        int id = PrintCMD.ReadKey("Id", false);
                        IQuery query1 = Singleton.Instance.Query("DELETE FROM Genre g WHERE g.Id = :id");
                        query1.SetParameter("id", id).ExecuteUpdate();                      
                        break;
                    case 2:
                        string tmpTitle = PrintCMD.ReadLine("Название", false);
                        IQuery query2 = Singleton.Instance.Query("DELETE FROM Genre g WHERE g.Title = :title");
                        query2.SetParameter("title", tmpTitle).ExecuteUpdate();
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
    }
}
