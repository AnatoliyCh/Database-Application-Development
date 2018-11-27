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
    class ControllActor : IControll
    {
        public void Read()
        {
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Actor.PrintTable();
                PrintCMD.Actor.ReadMenu();
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
                PrintCMD.Actor.PrintTable();
                PrintCMD.Actor.CreateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        string newActor = PrintCMD.ReadLine("Новый актёр", false);
                        Singleton.Instance.Actor.Create(new Actor { Name = newActor });
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
            Actor actor;//то что изменяем
            string tmpName;//новое название
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.Actor.PrintTable();
                PrintCMD.Actor.UpdateMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1://изменение по Id
                        int id = PrintCMD.ReadKey("Id", false);
                        actor = Singleton.Instance.Actor.Read("Id", id.ToString());
                        tmpName = PrintCMD.ReadLine(actor.Name, false);
                        actor.Name = tmpName;
                        Singleton.Instance.Actor.Update(actor);
                        actor = null; tmpName = null;
                        break;
                    case 2://изменение по Name
                        tmpName = PrintCMD.ReadLine("Имя", false);
                        actor = Singleton.Instance.Actor.Read("Name", tmpName);
                        tmpName = PrintCMD.ReadLine(actor.Name, false);
                        actor.Name = tmpName;
                        Singleton.Instance.Actor.Update(actor);
                        actor = null; tmpName = null;
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
                PrintCMD.Actor.PrintTable();
                PrintCMD.Actor.DeleteMenu();
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1://удаление по Id
                        int id = PrintCMD.ReadKey("Id", false);
                        Singleton.Instance.Actor.Delete("Id", id.ToString());
                        break;
                    case 2://удаление по Name
                        string tmpName = PrintCMD.ReadLine("Имя", false);
                        Singleton.Instance.Actor.Delete("Name", tmpName);
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
