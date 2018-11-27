using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.View;

namespace LR2.Controller
{
    class Controll
    {
        private static IControll _controllGenre = new ControllGenre();
        private static IControll _controllActor = new ControllActor();
        public static void Genre()
        {
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.MenuItem(1);
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        _controllGenre.Read();
                        break;
                    case 2:
                        _controllGenre.Create();
                        break;
                    case 3:
                        _controllGenre.Update();
                        break;
                    case 4:
                        _controllGenre.Delete();
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }            
        }
        public static void Actor()
        {
            int key;
            bool tmp = true;
            while (tmp)
            {
                PrintCMD.MenuItem(2);
                key = PrintCMD.ReadKey();
                switch (key)
                {
                    case 1:
                        _controllActor.Read();
                        break;
                    case 2:
                        _controllActor.Create();
                        break;
                    case 3:
                        _controllActor.Update();
                        break;
                    case 4:
                        _controllActor.Delete();
                        break;
                    case 0:
                        tmp = false;
                        break;
                }
            }
        }
    }
}
