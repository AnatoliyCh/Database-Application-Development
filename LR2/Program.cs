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
    }
}
